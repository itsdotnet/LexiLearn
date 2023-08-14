using LexiLearn.Domain.Entity.User;
using LexiLearn.Service.DTOs.Users;
using LexiLearn.Service.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LexiLearn.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        UserService userService = new UserService();

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            return Ok((await userService.GetByIdAsync(id)).Data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserCreationDto dto)
        {
            return Ok((await userService.CreateAsync(dto)).Data);
        }
    }
}