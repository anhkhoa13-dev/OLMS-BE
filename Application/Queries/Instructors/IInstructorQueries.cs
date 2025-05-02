

namespace Application.Queries.Instructors;

public interface IInstructorQueries
{
    Task<IEnumerable<CoursesDTOForInstructor>> GetCoursesByInstructorIdAsync(Guid instructorId);

}
