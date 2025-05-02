using API.ErrorMessage;
using Application.Commands.Instructors;
using Application.Queries.Instructors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("instructor")]
[ApiController]
public class InstructorController(ISender sender) : Controller
{
    private readonly ISender _sender = sender;

    [HttpPost("course")]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
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
        return Ok(new { Message = "Course created successfully" });
    }

    [HttpGet("courses")]
    public async Task<IActionResult> GetCourses([FromQuery] GetCoursesCommand command)
    {
        var courses = await _sender.Send(command);
        return Ok(courses);
    }
}
