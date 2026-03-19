


namespace Program.Services.Abstract;

public interface IAuthService
{   
    Task<(bool Success, string ErrorMessage)> LoginAsync(string email, string password); // Return tuple, что бы я мог понять что метод данные не валидные и так же мог вернуть сообщение об ошибке
    Task<(bool Success, string ErrorMessage)> RegisterAsync (string userName, string email, string password); 




}
