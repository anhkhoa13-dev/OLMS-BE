using API.ErrorMessage;
using Application.Queries.Quizzes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("quiz")]
[ApiController]
public class QuizController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetQuiz([FromQuery] GetQuizDetailCommand command)
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
        return Ok(result.Value);
    }
}
