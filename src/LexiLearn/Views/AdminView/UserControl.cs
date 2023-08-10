using LexiLearn.Domain.Services;
using LexiLearn.Service.DTOs.Users;
using LexiLearn.Service.Services;

namespace LexiLearn.Views.AdminView;

public class UserControl
{
    private readonly IUserService userService;

    public UserControl()
    {
        userService = new UserService();
    }

    public async Task StartAsync()
    {
        Console.WriteLine("Welcome to User Management Console");

        while (true)
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Create User");
            Console.WriteLine("2. Update User");
            Console.WriteLine("3. Delete User");
            Console.WriteLine("4. View All Users");
            Console.WriteLine("5. View User by Id");
            Console.WriteLine("6. Exit");

            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateUser();
                    break;
                case "2":
                    await UpdateUser();
                    break;
                case "3":
                    await DeleteUser();
                    break;
                case "4":
                    await ViewAllUsers();
                    break;
                case "5":
                    await ViewUserById();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }
        }
    }

    private async Task CreateUser()
    {
        Console.Write("Enter firstname: ");
        var firstname = Console.ReadLine();

        Console.Write("Enter lastname: ");
        var lastname = Console.ReadLine();

        Console.Write("Enter username: ");
        var username = Console.ReadLine();

        Console.Write("Enter email: ");
        var email = Console.ReadLine();

        Console.Write("Enter password: ");
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
            Console.WriteLine("User created successfully!");
        }
        else
        {
            Console.WriteLine($"Error creating user: {response.Message}");
        }
    }

    private async Task UpdateUser()
    {
        Console.Write("Enter user ID to update: ");
        var userId = Convert.ToInt64(Console.ReadLine());

        var userResponse = await userService.GetByIdAsync(userId);

        if (userResponse.StatusCode != 200)
        {
            Console.WriteLine($"Error retrieving user: {userResponse.Message}");
            return;
        }

        var user = userResponse.Data;

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
            Console.WriteLine("User updated successfully!");
        }
        else
        {
            Console.WriteLine($"Error updating user: {response.Message}");
        }
    }

    private async Task DeleteUser()
    {
        Console.Write("Enter user ID to delete: ");
        var userId = Convert.ToInt64(Console.ReadLine());

        var response = await userService.DeleteAsync(userId);

        if (response.StatusCode == 200)
        {
            Console.WriteLine("User deleted successfully!");
        }
        else
        {
            Console.WriteLine($"Error deleting user: {response.Message}");
        }
    }

    private async Task ViewUserById()
    {
        var userId = Convert.ToInt64(Console.ReadLine());

        var response = await userService.GetByIdAsync(userId);

        if (response.StatusCode == 200)
        {
            var user = response.Data;

            Console.WriteLine($"User ID: {user.Id}, Username: {user.UserName}, Email: {user.Email}");
        }
        else
        {
            Console.WriteLine($"Error retrieving users: {response.Message}");
        }
    }

    private async Task ViewAllUsers()
    {
        var response = await userService.GetAllAsync();

        if (response.StatusCode == 200)
        {
            foreach (var user in response.Data)
            {
                Console.WriteLine($"User ID: {user.Id}, Username: {user.UserName}, Email: {user.Email}");
            }
        }
        else
        {
            Console.WriteLine($"Error retrieving users: {response.Message}");
        }
    }
}
