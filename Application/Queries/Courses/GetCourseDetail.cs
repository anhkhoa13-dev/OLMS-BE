using MediatR;

namespace Application.Queries.Courses;

public record CourseDetailDto(
    Guid CourseId, 
    string Title, 
    string Description,
    InstructorDto Instructor,
    IEnumerable<SectionDto> Sections);

public record SectionDto(
    Guid SectionId,
    string Title,
    string Description,
    int Order);

public record InstructorDto(
    Guid InstructorId,
    string Name);

public record GetCourseDetailCommand(Guid CourseId) : IRequest<CourseDetailDto>
{
}

public class GetCourseDetailCommandHandler(ICourseQuery courseQuery) : IRequestHandler<GetCourseDetailCommand, CourseDetailDto>
{
    private readonly ICourseQuery _courseQuery = courseQuery;
    public async Task<CourseDetailDto> Handle(GetCourseDetailCommand request, CancellationToken cancellationToken)
    {
        return await _courseQuery.GetCourseDetailAsync(request.CourseId, cancellationToken);
    }
}
