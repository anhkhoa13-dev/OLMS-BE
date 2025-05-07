namespace Application.Queries.Quizzes;

public interface IQuizQuery
{
    Task<QuizResponse?> GetByIdAsync(Guid quizId, CancellationToken cancellationToken);
}
