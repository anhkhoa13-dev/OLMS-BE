using API.ErrorMessage;
using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("authentication")]
public class AuthenticationController(
    ISender sender) : Controller
{
    private readonly ISender _sender = sender;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateAccountCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occured",
                ErrorCode = result.Error.Code
            });
        }
        return Ok(new { Message = "User registered successfully" });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginAccountCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = result.Error.ErrorMessage ?? "Error occured",
                ErrorCode = result.Error.Code
            });
        }
        return Ok(new { Token = result.Value });
    }
}
