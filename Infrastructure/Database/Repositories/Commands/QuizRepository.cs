using Domain.Aggregates.QuizAggregate;

namespace Infrastructure.Database.Repositories.Commands;

public class QuizRepository(ApplicationDbContext context) : Repository<Quiz>(context), IQuizRepository
{
}
