

using Domain.Aggregates.InstructorAggregate;

namespace Infrastructure.Database.Repositories.Commands;

public class InstructorRepository(ApplicationDbContext context) : Repository<Instructor>(context), IInstructorRepository
{
}
