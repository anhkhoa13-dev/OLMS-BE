using Domain.Results;
using MediatR;

namespace Application.Queries.Instructors;

public record CoursesDTOForInstructor(
    Guid CourseId,
    string Title,
    string Description,
    string Code
    );

public record GetCoursesCommand(Guid InstructorId) : IRequest<IEnumerable<CoursesDTOForInstructor>>
{
}


public class GetCoursesCommandHandler(IInstructorQueries instructorQueries) : IRequestHandler<GetCoursesCommand, IEnumerable<CoursesDTOForInstructor>>
{
    private readonly IInstructorQueries _instructorQueries = instructorQueries;
    public async Task<IEnumerable<CoursesDTOForInstructor>> Handle(GetCoursesCommand request, CancellationToken cancellationToken)
    {
        return await _instructorQueries.GetCoursesByInstructorIdAsync(request.InstructorId);
    }
}