

namespace Application.Queries.Instructors;

public interface IInstructorQuery
{
    Task<IEnumerable<CoursesDTOForInstructor>> GetCoursesByInstructorIdAsync(Guid instructorId);

}
