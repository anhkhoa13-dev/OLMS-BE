using Application.Queries.Courses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories.Queries;

public class CourseQuery(ApplicationDbContext context) : ICourseQuery
{
    private readonly ApplicationDbContext _context = context;
    public async Task<CourseDetailDto> GetCourseDetailAsync(Guid courseId, CancellationToken cancellationToken)
    {
        var sections = await GetSectionsAsync(courseId, cancellationToken);
        var instructor = await GetInstructorAsync(courseId, cancellationToken);
        return await _context.Courses
            .AsNoTracking()
            .Where(c => c.Id == courseId)
            .Select(c => new CourseDetailDto(
                c.Id,
                c.Title,
                c.Description,
                instructor,
                sections))
            .SingleAsync(cancellationToken);
    }

    public async Task<InstructorDto> GetInstructorAsync(Guid courseId, CancellationToken cancellationToken)
    {
        var instructorId = await _context.Courses
            .AsNoTracking()
            .Where(c => c.Id == courseId)
            .Select(c => c.InstructorId)
            .SingleAsync(cancellationToken);

        return await _context.Accounts
            .AsNoTracking()
            .Where(a => a.Id == instructorId)
            .Select(a => new InstructorDto(
                a.Id, 
                a.FullName))
            .SingleAsync(cancellationToken);
    }

    public async Task<IEnumerable<SectionDto>> GetSectionsAsync(Guid courseId, CancellationToken cancellationToken)
    {
        return await _context.Sections
            .AsNoTracking()
            .Where(s => s.CourseId == courseId)
            .Select(s => new SectionDto(s.Id, s.Title, s.Description, s.Order))
            .ToListAsync(cancellationToken);
    }


}
