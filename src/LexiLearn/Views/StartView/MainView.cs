using LexiLearn.AdminConsole;
using LexiLearn.Domain.Services;
using LexiLearn.Service.DTOs.Users;
using LexiLearn.Service.Helpers;
using LexiLearn.Service.Services;
using LexiLearn.Views.UserView;

namespace LexiLearn.Views.StartView;

public class MainView
{
    private string localLogin;
    private string localPassword;
    private IUserService userService;
    private Mail mail;

    public MainView()
    {
        this.userService = new UserService();
        this.localLogin = "admin";
        this.localPassword = "admin";
        mail = new Mail();
    }

    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("[1] - Login\n[2] - Register\n[3] - Exit\n");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await LoginAsync();
                    break;
                case "2":
                    await RegisterAsync();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option please try again.");
                    break;
            }
        }
    }

    private async Task LoginAsync()
    {
        Console.Write("Enter Username or Email: ");
        string login = Console.ReadLine();

        Console.Write("Enter Password: ");
        string password = Console.ReadLine();

        if (password == localPassword && localLogin == login)
        {
            MainAdminView mainAdminView = new MainAdminView();
            await mainAdminView.StartAsync();
        }
        else
        {
            if (login.Contains("@"))
            {
                var response = await userService.CheckPasswordByEmail(login, password);
                if (response.Data)
                {
                    var user = (await userService.GetByEmailAsync(login)).Data;
                    MainUserUI mainUserUI = new MainUserUI(user);
                    await mainUserUI.StartAsync();
                }
                else
                {
                    Console.WriteLine("Error on login :" + response.Message);
                }
            }
            else
            {
                var response = await userService.CheckPasswordByUsername(login, password);
                if (response.Data)
                {
                    var user = (await userService.GetByUsernameAsync(login)).Data;
                    MainUserUI mainUserUI = new MainUserUI(user);
                    await mainUserUI.StartAsync();
                }
                else
                {
                    Console.WriteLine("Error on login :" + response.Message);
                }
            }
        }
    }

    private async Task RegisterAsync()
    {
        Console.Clear();

        Console.Write("Enter firstname: ");
        var firstname = Console.ReadLine();

        Console.Write("Enter lastname: ");
        var lastname = Console.ReadLine();

        Console.Write("Enter email: ");
        var email = Console.ReadLine();

        while ((await userService.IsExsistEmailAsync(email)).Data)
        {
            Console.Write("\nThis email already taken.\nEnter new email: ");
            email = Console.ReadLine();
        }

        var code = mail.Verify(email);

        Console.Write("Verify your email(enter code on your email): ");
        var verifyCode = int.Parse(Console.ReadLine());

        for (int i = 0; i < 3; i++)
        {
            if (verifyCode == code)
            {
                code = 0;
                break;
            }
            else
            {
                Console.Write("Verify code is wrong try again: ");
                verifyCode = int.Parse(Console.ReadLine());
            }
        }

        if (code == 0)
        {
            Console.WriteLine("Email verified!");
        }
        else
        {
            Console.WriteLine("Email verifing failed!");
            return;
        }

        Console.Write("Enter new username: ");
        var username = Console.ReadLine();

        while ((await userService.IsExsistUsernameAsync(username)).Data)
        {
            Console.Write("\nThis username already taken.\nEnter new username: ");
            username = Console.ReadLine();
        }

        Console.Write("Enter new password: ");
        var password = Console.ReadLine();

        var newUserDto = new UserCreationDto
        {
            FirstName = firstname,
            LastName = lastname,
            UserName = username,
            Email = email,
            Password = password
        };

        var response = await userService.CreateAsync(newUserDto);

        if (response.StatusCode == 200)
        {
            Console.WriteLine("\nAccount created successfully!\n");
            var newUser = (await userService.GetByIdAsync(response.Data.Id)).Data;
            MainUserUI mainUserUI = new MainUserUI(newUser);
            await mainUserUI.StartAsync();
        }
        else
        {
            Console.WriteLine($"\nError creating account: {response.Message}\n");
        }
    }
}