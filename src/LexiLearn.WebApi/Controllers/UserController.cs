using LexiLearn.DAL.Constexts;
using LexiLearn.Domain.Services;
using LexiLearn.Service.DTOs.Users;
using LexiLearn.Service.Services;
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
        return Ok((await userService.GetAllAsync()).Data);
    }

    [HttpGet("{id}", Name = "getById")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok((await userService.GetByIdAsync(id)).Data);
    }

    [HttpDelete]
    public async Task<IActionResult> Post(long id)
    {
        return Ok((await userService.DeleteAsync(id)).Data);
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserCreationDto dto)
    {
        return Ok((await userService.CreateAsync(dto)).Data);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UserUpdateDto dto)
    {
        return Ok((await userService.UpdateAsync(dto)).Data);
    }
}