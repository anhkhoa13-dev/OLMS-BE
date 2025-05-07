using Microsoft.EntityFrameworkCore;
using Domain.Aggregates.CourseAggregate;

namespace Infrastructure.Database.Repositories.Commands;

public class CourseRepository(ApplicationDbContext context) : Repository<Course>(context), ICourseRepository
{
    public override async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Courses
            .Include(c => c.Sections)
            .SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
