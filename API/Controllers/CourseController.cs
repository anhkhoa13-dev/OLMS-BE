using API.ErrorMessage;
using Application.Commands.Courses;
using Application.Commands.Quizzes;
using Application.Queries.Courses;
using Application.Queries.Quizzes;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("course")]
[ApiController]
public class CourseController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetCourses([FromQuery] GetCourseDetailCommand command)
    {
        var courses = await _sender.Send(command);
        return Ok(courses);
    }

    #region Section
    [HttpPost("section")]
    public async Task<IActionResult> AddSection([FromBody] AddSectionCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occurred",
                ErrorCode = result.Error.Code
            });
        }
        return Ok(new { Message = "Section added successfully" });
    }

    [HttpDelete("section")]
    public async Task<IActionResult> RemoveSection([FromBody] RemoveSectionCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occurred",
                ErrorCode = result.Error.Code
            });
        }
        return Ok(new { Message = "Section removed successfully" });
    }
    #endregion

    #region Quiz
    [HttpPost("quiz")]
    public async Task<IActionResult> AddQuiz([FromBody] CreateQuizCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occurred",
                ErrorCode = result.Error.Code
            });
        }
        return Ok(new { Message = "Quiz added successfully" });
    }
    #endregion
}

