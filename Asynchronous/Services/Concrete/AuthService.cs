using Program.Models;
using Program.Repositories.Abstract;
using Program.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Program.Services.Concrete
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<(bool Success, string ErrorMessage)> RegisterAsync(string userName, string email, string password)
        {

            if (string.IsNullOrWhiteSpace(email))
                return (false, "Email cannot be empty.\n");



            if (!email.Contains("@gmail.com"))
                return (false, "Email must be a valid @gmail.com address.\n");

            if (email.Contains(" "))
                return (false, "Email must not contain spaces.\n");

            


            if (string.IsNullOrWhiteSpace(password))
                return (false, "Password cannot be empty.\n");

            if (password.Length > 8)
                return (false, "Password must be at least 8 characters long.\n");

            if (!password.Any(char.IsUpper))
                return (false, "Password must contain at least one uppercase letter.\n");

            if (!password.Any(char.IsDigit))
                return (false, "Password must contain at least one digit.\n");

            

            var existingUser = await _userRepository.GetByEmailAsync(email);
            
            if (existingUser != null)
                return (false, "User with this email already exists.\n");



            var salt = GenerateSalt();

            var passwordHash = HashPassword(password, salt);

            var user = new User
            {
                UserName = userName,
                Email = email,
                Salt = salt,
                PasswordHash = passwordHash,


            };

            await _userRepository.AddUserAsync(user);

            return (true, null);



        }

        public async Task<(bool Success, string ErrorMessage)> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return (false, "Email and password are required.");


            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                return (false, "Invalid email or password.");




            string hashToCheck = HashPassword(password, user.Salt);

            if (hashToCheck != user.PasswordHash)
                return (false, "Invalid email or password.");

            return (true, null);





        }






        //Я нашел этот способ на StackOverflow

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
            
                byte[] saltedPassword = Encoding.UTF8.GetBytes(password + salt);
                
                byte[] hashBytes = sha256.ComputeHash(saltedPassword);
                
                return Convert.ToBase64String(hashBytes);
            }

        }








        }

}
