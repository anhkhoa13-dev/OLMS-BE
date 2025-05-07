

using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Helpers.NavigationsLoaders.QuestionLoaders;

public class ShortAnswerQuestionLoader : INavigationsLoader
{
    public async Task LoadNavigationsAsync(Guid quizId, ApplicationDbContext context, CancellationToken cancellationToken)
    {
        await context.ShortAnswerQuestions
            .Where(q => q.QuizId == quizId)
            .Include(q => q.CorrectAnswers)
            .ToListAsync(cancellationToken);
    }
}
