using Application.Queries.Quizzes;
using Infrastructure.Helpers.NavigationsLoaders;
using Infrastructure.Helpers.ResponseMapperApdapters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories.Queries;

public class QuizQuery(
    ApplicationDbContext context, 
    IEnumerable<IQuestionDtoMapper> mappers,
    IEnumerable<INavigationsLoader> loaders) : IQuizQuery
{
    private readonly ApplicationDbContext _context = context;
    private readonly IEnumerable<IQuestionDtoMapper> _mappers = mappers;
    private readonly IEnumerable<INavigationsLoader> _loaders = loaders;

    public async Task<QuizResponse?> GetByIdAsync(Guid quizId, CancellationToken cancellationToken)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
            .FirstOrDefaultAsync(q => q.Id == quizId, cancellationToken);

        if (quiz is null)
            return null;

        // Load navigations
        foreach (var loader in _loaders)
            await loader.LoadNavigationsAsync(quizId, _context, cancellationToken);

        // Map to DTO
        var quizDto = new QuizResponse(
            quiz.Id,
            quiz.Title,
            quiz.Description,
            quiz.Questions.Select(q =>
            {
                var mapper = _mappers.FirstOrDefault(m => m.CanHandle(q))
                        ?? throw new InvalidOperationException($"No mapper found for {q.GetType().Name}"); ;
                return mapper.Map(q);
            }).ToList()
        );

        return quizDto;
    }
}
