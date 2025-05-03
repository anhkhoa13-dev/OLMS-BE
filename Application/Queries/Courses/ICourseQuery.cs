

namespace Application.Queries.Courses;

public interface ICourseQuery
{
    Task<CourseDetailDto> GetCourseDetailAsync(Guid courseId, CancellationToken cancellationToken);
    Task<IEnumerable<SectionDto>> GetSectionsAsync(Guid courseId, CancellationToken cancellationToken);
    Task<InstructorDto> GetInstructorAsync(Guid courseId, CancellationToken cancellationToken);
}
