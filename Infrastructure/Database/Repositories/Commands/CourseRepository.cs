

using Domain.CourseAggregate;

namespace Infrastructure.Database.Repositories.Commands;

public class CourseRepository(ApplicationDbContext context) : Repository<Course>(context), ICourseRepository
{
}
