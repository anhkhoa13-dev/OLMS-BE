

using Domain.Aggregates.StudentAggregate;

namespace Infrastructure.Database.Repositories.Commands;

public sealed class StudentRepository(ApplicationDbContext context) : Repository<Student>(context), IStudentRepository
{
}

