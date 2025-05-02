using Application.Queries.Instructors;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories.Queries;

public sealed class InstructorQueries(ApplicationDbContext context) : IInstructorQueries
{
    private readonly ApplicationDbContext _context = context;
    public async Task<IEnumerable<CoursesDTOForInstructor>> GetCoursesByInstructorIdAsync(Guid instructorId)
    {
        return await _context.Courses
            .AsNoTracking()
            .Where(c => c.InstructorId == instructorId)
            .Select(c => new CoursesDTOForInstructor(
                c.Id, 
                c.Title, 
                c.Description, 
                c.Code.Value))
            .ToListAsync();
    }
}
