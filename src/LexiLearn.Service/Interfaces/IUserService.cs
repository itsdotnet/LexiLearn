using LexiLearn.Domain.Entity.User;
using LexiLearn.Service.DTOs.Users;
using LexiLearn.Service.Helpers;

namespace LexiLearn.Domain.Services
{
    public interface IUserService
    {
        Task<Response<UserResultDto>> CreateAsync(UserCreationDto dto);

        Task<Response<UserResultDto>> UpdateAsync(UserUpdateDto dto);

        Task<Response<bool>> IsExsistUsernameAsync(string username);

        Task<Response<bool>> IsExsistEmailAsync(string email);

        Task<Response<bool>> CheckPassword(long id, string password);

        Task<Response<bool>> CheckPasswordByUsername(string username, string password);
        
        Task<Response<bool>> CheckPasswordByEmail(string email, string password);

        Task<Response<bool>> ChangePassword(long id, string  oldPass, string newPass);

        Task<Response<bool>> DeleteAsync(long id);

        Task<Response<User>> GetByEmailAsync(string email);

        Task<Response<User>> GetByUsernameAsync(string username);
        
        Task<Response<User>> GetByIdAsync(long id);

        Task<Response<IEnumerable<User>>> GetAllAsync();
    }
}