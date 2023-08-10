using AutoMapper;
using LexiLearn.DAL.IRepositories;
using LexiLearn.DAL.Repository;
using LexiLearn.Domain.Entity.User;
using LexiLearn.Domain.Services;
using LexiLearn.Service.DTOs.Users;
using LexiLearn.Service.Exstensions;
using LexiLearn.Service.Helpers;
using LexiLearn.Service.Mappers;

namespace LexiLearn.Service.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public UserService() 
    {
        unitOfWork = new UnitOfWork();
        mapper = new Mapper(new MapperConfiguration(cfg => 
        { 
            cfg.AddProfile<MappingProfile>();
        }));
        
    }
    public async Task<Response<bool>> ChangePassword(long id, string oldPass, string newPass)
    {
        var user = unitOfWork.UserRepository.Select(id);
        var passStatus = (await CheckPassword(id, oldPass)).Data;

        if (passStatus)
        {
            user.Password = newPass;
            unitOfWork.UserRepository.SaveChanges();

            return new Response<bool>
            {
                StatusCode = 200,
                Message = "Password is changed",
                Data = true
            };
        }

        return new Response<bool>
        {
            StatusCode = 401,
            Message = "Password is wrong",
            Data = false
        };
    }

    public async Task<Response<bool>> CheckPassword(long id, string password)
    {
        var user = unitOfWork.UserRepository.Select(id);

        if (user is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "User not found",
                Data = false
            };

        password.Hasher();

        if (user.Password == password)
            return new Response<bool>
            {
                StatusCode = 200,
                Message = "Password is true",
                Data = true
            };

        return new Response<bool>
        {
            StatusCode = 401,
            Message = "Password is wrong",
            Data = false
        };
    }

    public async Task<Response<bool>> CheckPasswordByEmail(string email, string password)
    {
        var user = unitOfWork.UserRepository.SelectAll()
            .Where(u => u.Email.ToLower() == email.ToLower())
                .FirstOrDefault();

        if (user is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "User not found",
                Data = false
            };

        password.Hasher();

        if (user.Password == password)
            return new Response<bool>
            {
                StatusCode = 200,
                Message = "Password is true",
                Data = true
            };

        return new Response<bool>
        {
            StatusCode = 401,
            Message = "Password is wrong",
            Data = false
        };
    }

    public async Task<Response<bool>> CheckPasswordByUsername(string username, string password)
    {
        var user = unitOfWork.UserRepository.SelectAll()
            .FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());

        if (user is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "User not found",
                Data = false
            };

        password.Hasher();

        if (user.Password == password)
            return new Response<bool>
            {
                StatusCode = 200,
                Message = "Password is true",
                Data = true
            };

        return new Response<bool>
        {
            StatusCode = 401,
            Message = "Password is wrong",
            Data = false
        };
    }

    public async Task<Response<UserResultDto>> CreateAsync(UserCreationDto dto)
    {
        if (dto.UserName.Contains("@"))
            return new Response<UserResultDto>
            {
                StatusCode = 400,
                Message = "Username cannot contains `@`",
                Data = mapper.Map<UserResultDto>(dto)
            };

        dto.Password.Hasher();

        unitOfWork.UserRepository.Add(mapper.Map<User>(dto));
        unitOfWork.UserRepository.SaveChanges();

        return new Response<UserResultDto>
        {
            StatusCode = 200,
            Message = "User created",
            Data = mapper.Map<UserResultDto>(dto)
        };
    }

    public async Task<Response<bool>> DeleteAsync(long id)
    {
        var user = unitOfWork.UserRepository.Select(id);

        if (user is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "User not found",
                Data = false
            };

        unitOfWork.UserRepository.Delete(user);
        unitOfWork.UserRepository.SaveChanges();

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "User account deleted",
            Data = true
        };
    }

    public async Task<Response<IEnumerable<User>>> GetAllAsync()
    {
        var allUsers = unitOfWork.UserRepository.SelectAll();
         
        if (allUsers is null)
            return new Response<IEnumerable<User>>
            {
                StatusCode = 404,
                Message = "Users not found",
                Data = null
            };

        return new Response<IEnumerable<User>>
        {
            StatusCode = 200,
            Message = "All users returned",
            Data = allUsers
        };
    }

    public async Task<Response<User>> GetByEmailAsync(string email)
    {
        var user = unitOfWork.UserRepository.SelectAll()
            .FirstOrDefault(u => u.Email == email);

        if (user is null)
            return new Response<User>
            {
                StatusCode = 404,
                Message = "User not found",
                Data = null
            };

        return new Response<User>
        {
            StatusCode = 200,
            Message = "User returned",
            Data = user
        };
    }

    public async Task<Response<User>> GetByIdAsync(long id)
    {
        var user = unitOfWork.UserRepository.Select(id);
        
        if (user is null)
            return new Response<User>
            {
                StatusCode = 404,
                Message = "User not found",
                Data = null
            };

        return new Response<User>
        {
            StatusCode = 200,
            Message = "User returned",
            Data = user
        };
    }

    public async Task<Response<User>> GetByUsernameAsync(string username)
    {
        var user = unitOfWork.UserRepository.SelectAll()
            .FirstOrDefault(u => u.UserName == username);

        if (user is null)
            return new Response<User>
            {
                StatusCode = 404,
                Message = "User not found",
                Data = null
            };

        return new Response<User>
        {
            StatusCode = 200,
            Message = "User returned",
            Data = user
        };
    }

    public async Task<Response<bool>> IsExsistEmailAsync(string email)
    {
        var allUsers = unitOfWork.UserRepository.SelectAll();
        var exsistUser = allUsers.FirstOrDefault(x => x.Email == email);

        if (exsistUser is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "User not found",
                Data = false
            };

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "User is exsist",
            Data = true
        };
    }

    public async Task<Response<bool>> IsExsistUsernameAsync(string username)
    {
        var allUsers = unitOfWork.UserRepository.SelectAll();
        var exsistUser = allUsers.FirstOrDefault(x => x.UserName == username);

        if (exsistUser is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "User not found",
                Data = false
            };

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "User is exsist",
            Data = true
        };
    }

    public async Task<Response<UserResultDto>> UpdateAsync(UserUpdateDto dto)
    {
        var exsistUser = unitOfWork.UserRepository.Select(dto.Id);

        if (exsistUser is null)
            return new Response<UserResultDto>
            {
                StatusCode = 404,
                Message = "User not found",
                Data = null
            };

        dto.Password.Hasher();

        mapper.Map(dto, exsistUser);
        unitOfWork.UserRepository.Update(exsistUser);
        unitOfWork.UserRepository.SaveChanges();

        return new Response<UserResultDto>
        {
            StatusCode = 200,
            Message = "User updated",
            Data = mapper.Map<UserResultDto>(exsistUser)
        };
    }

    public async Task UpdateScoreAsync(long id, int score)
    {
        var exsistUser = unitOfWork.UserRepository.Select(id);

        if (exsistUser is null)
            return;

        exsistUser.Score += score;
        unitOfWork.UserRepository.Update(exsistUser);
        unitOfWork.UserRepository.SaveChanges();
    }
}