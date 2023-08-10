using LexiLearn.Domain.Entity.User;
using LexiLearn.Domain.Services;
using LexiLearn.Service.DTOs.Users;
using LexiLearn.Service.Services;
using LexiLearn.Views.StartView;

namespace LexiLearn.Views.UserView;

public class ProfileControl
{
    private IUserService userService;
    private User currentUser;
    private bool isRunning = true;
    public ProfileControl(User currentUser)
    {
        this.userService = new UserService();
        this.currentUser = currentUser;
    }

    public async Task StartAsync()
    {
        while (isRunning)
        {
            Console.WriteLine("Welcome to LexiLearn");
            Console.WriteLine("\n1. Update all Informations");
            Console.WriteLine("2. Update Password");
            Console.WriteLine("3. Delete Account");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await UpdateAllAsync();
                    break;
                case "2":
                    await UpdatePasswordAsync();
                    break;
                case "3":
                    await DeleteAccount();
                    break;
                case "4":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private async Task UpdatePasswordAsync()
    {
        Console.Clear();

        Console.Write("Enter old password: ");
        var oldPassword = Console.ReadLine();

        Console.Write("Enter new password: ");
        var newPassword = Console.ReadLine();

        var response = await userService.ChangePassword(currentUser.Id, oldPassword, newPassword);

        if (response.StatusCode == 200)
        {
            Console.WriteLine("Password updated successfully!");
        }
        else
        {
            Console.WriteLine($"Error updating Password: {response.Message}");
        }
    }


    private async Task UpdateAllAsync()
    {
        Console.Clear();

        var userId = currentUser.Id;

        var userResponse = await userService.GetByIdAsync(userId);

        Console.Write("Enter new firstname: ");
        var newFirstname = Console.ReadLine();

        Console.Write("Enter new lastname: ");
        var newLastname = Console.ReadLine();

        Console.Write("Enter new username: ");
        var newUsername = Console.ReadLine();

        Console.Write("Enter new password: ");
        var newPassword = Console.ReadLine();

        var updateUserDto = new UserUpdateDto
        {
            Id = userId,
            FirstName = newFirstname,
            LastName = newLastname,
            UserName = newUsername,
            Password = newPassword
        };

        var response = await userService.UpdateAsync(updateUserDto);

        if (response.StatusCode == 200)
        {
            Console.WriteLine("Account updated successfully!");
        }
        else
        {
            Console.WriteLine($"Error updating Account: {response.Message}");
        }
    }

    private async Task DeleteAccount()
    {
        Console.Clear();

        Console.Write("Are you sure to delete your account (y/n): ");
        if (Console.ReadLine() != "y")
            return;

        var userId = currentUser.Id;

        var response = await userService.DeleteAsync(userId);

        if (response.StatusCode == 200)
        {
            Console.WriteLine("Account deleted successfully!");
        }
        else
        {
            Console.WriteLine($"Error deleting Account: {response.Message}");
        }
        MainView mainView = new MainView();
        await mainView.StartAsync();
        isRunning = false;
    }
}