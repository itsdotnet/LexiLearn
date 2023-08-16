using LexiLearn.Service.DTOs.Questions;
using LexiLearn.Service.Helpers;
using LexiLearn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LexiLearn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(QuestionCreationDto dto)
    {
        var response = await _questionService.CreateAsync(dto);
        return Ok(response.Data);
    }

    [HttpPut]
    public async Task<IActionResult> Update(QuestionUpdateDto dto)
    {
        var response = await _questionService.UpdateAsync(dto);

        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await _questionService.DeleteAsync(id);
        return Ok(response.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await _questionService.GetByIdAsync(id);

        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _questionService.GetAllAsync();
        return Ok(response.Data);
    }

    [HttpGet("word/{wordId}")]
    public async Task<IActionResult> GetQuestionsByWord(long wordId)
    {
        var response = await _questionService.GetQuestionsByWordAsync(wordId);
        return Ok(response.Data);
    }

    [HttpGet("quiz/{quizId}")]
    public async Task<IActionResult> GetQuestionsByQuizId(long quizId)
    {
        var response = await _questionService.GetQuestionsByQuizIdAsync(quizId);
        return Ok(response.Data);
    }

    [HttpPost("check/{id}")]
    public async Task<IActionResult> Check(long id, [FromBody] string answer)
    {
        var response = await _questionService.CheckAsync(id, answer);
        return Ok(response.Data);
    }
}
