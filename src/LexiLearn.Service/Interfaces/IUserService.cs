using LexiLearn.Domain.Entity.User;
using LexiLearn.Service.DTOs.Users;
using LexiLearn.Service.Helpers;

namespace LexiLearn.Domain.Services
{
    public interface IUserService
    {
        Task<Response<UserResultDto>> CreateAsync(UserCreationDto dto);

        Task<Response<UserResultDto>> UpdateAsync(UserUpdateDto dto);

        Task<Response<bool>> ChangePassword(long id, string  oldPass, string newPass);

        Task<Response<bool>> DeleteAsync(long id);

        Task<Response<User>> GetByIdAsync(long id);

        Task<Response<IEnumerable<User>>> GetAllAsync();
    }
}