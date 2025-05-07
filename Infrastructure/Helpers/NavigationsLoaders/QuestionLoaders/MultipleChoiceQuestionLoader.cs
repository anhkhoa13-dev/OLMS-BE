using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Helpers.NavigationsLoaders.QuestionLoaders;

public class MultipleChoiceQuestionLoader : INavigationsLoader
{
    public async Task LoadNavigationsAsync(Guid quizId, ApplicationDbContext context, CancellationToken cancellationToken)
    {
        await context.MultipleChoiceQuestions
            .Where(q => q.QuizId == quizId)
            .Include(q => q.Choices)
            .ToListAsync(cancellationToken);
    }
}
