using LexiLearn.Service.DTOs.Quizzes.QuizHistory;
using LexiLearn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LexiLearn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class QuizHistoryController : ControllerBase
{
    private readonly IQuizHistoryService _quizHistoryService;

    public QuizHistoryController(IQuizHistoryService quizHistoryService)
    {
        _quizHistoryService = quizHistoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(QuizHistoryCreationDto dto)
    {
        var response = await _quizHistoryService.CreateAsync(dto);
        return Ok(response.Data);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetQuizHistoryByUserId(long userId)
    {
        var response = await _quizHistoryService.GetQuizHistoryByUserIdAsync(userId);

        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpGet("quiz/{quizId}")]
    public async Task<IActionResult> GetQuizHistoryByQuizId(long quizId)
    {
        var response = await _quizHistoryService.GetQuizHistoryByQuizIdAsync(quizId);

        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await _quizHistoryService.DeleteAsync(id);
        return Ok(response.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await _quizHistoryService.GetByIdAsync(id);

        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpGet("valid")]
    public async Task<IActionResult> IsValidId(long userId, long quizId)
    {
        var response = await _quizHistoryService.IsValidId(userId, quizId);
        return Ok(response.Data);
    }
}