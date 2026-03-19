

using Program.Services.Abstract;

namespace Program.UI;

public class Menu
{

    public static async Task RunAsync(IAuthService authService)
    {

        Console.Title = "Authentication System";

        Console.Write("\n\n===== Authentication System =====\n\n");
        while (true)
        {
            Console.Write("1 - Register\n\n");
            Console.Write("2 - Login\n\n");
            Console.Write("0 - Exit\n\n");
            
            Console.Write("\nChoose an action => ");
            string choice = Console.ReadLine();

            if (choice == "0") break;

            switch (choice)
            {
                case "1":
                    await RegisterUserAsync(authService);
                    break;
                case "2":
                    await LoginUserAsync(authService);
                    break;
                default:
                    Console.Write("Invalid input.\n");
                    break;
            }


        }



    }






    private static async Task RegisterUserAsync(IAuthService authService)
    {
        Console.Write("Username => ");
        string userName = Console.ReadLine();

        Console.Write("Email => ");

        string email = Console.ReadLine();

        Console.Write("Password => ");

        string password = Console.ReadLine();

        var (success, errorMessage) = await authService.RegisterAsync(userName, email, password);

        if (success)
            Console.Write("Registration successful!\n");

        else
            Console.Write($"Registration failed: {errorMessage}\n");
    
    
    }



    private static async Task LoginUserAsync(IAuthService authService)
    {
        Console.Write("Email => ");
        string email = Console.ReadLine();
        Console.Write("Password => ");
        string password = Console.ReadLine();

        var (success, errorMessage) = await authService.LoginAsync(email, password);
        
        if (success)
            Console.Write("Login successful! Welcome.\n");
        
        else
            Console.Write($"Login failed: {errorMessage}\n");
    }








}
