

using Program.Models;

namespace Program.Repositories.Abstract;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);
    Task AddUserAsync(User user);
}
