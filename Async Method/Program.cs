using Program.Data;
using Program.Repositories.Abstract;
using Program.Repositories.Concrete;
using Program.Services.Abstract;
using Program.Services.Concrete;
using Program.UI;

namespace Program;


public class Program
{
    static async Task Main()
    {

        using var context = new AppDbContext();




        IUserRepository userRepository = new UserRepository(context);
        IAuthService authService = new AuthService(userRepository);


        await Menu.RunAsync(authService);


    }
}
