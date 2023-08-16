using LexiLearn.Domain.Services;
using LexiLearn.Service.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace LexiLearn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")] 

public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet(Name = "all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await userService.GetAllAsync();
        return Ok(response.Data);
    }

    [HttpGet("{id}", Name = "getById")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await userService.GetByIdAsync(id);

        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpDelete]
    public async Task<IActionResult> Post(long id)
    {
        var response = await userService.DeleteAsync(id);
        return Ok(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserCreationDto dto)
    {
        var response = await userService.CreateAsync(dto);
        return Ok(response.Data);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UserUpdateDto dto)
    {
        var response = await userService.UpdateAsync(dto);
       
        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpGet("checkemail")]
    public async Task<IActionResult> IsExistEmail(string email)
    {
        var response = await userService.IsExsistEmailAsync(email);
        return Ok(response.Data);
    }

    [HttpGet("checkusername")]
    public async Task<IActionResult> IsExistUsername(string username)
    {
        var response = await userService.IsExsistUsernameAsync(username);
        return Ok(response.Data);
    }
}