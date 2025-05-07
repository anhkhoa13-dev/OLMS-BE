

using Application.Queries.Quizzes;
using Domain.Aggregates.QuizAggregate.ShortAnswerQuestionEntity;

namespace Infrastructure.Helpers.ResponseMappers.QuestionResponseMappers;

public class ShortAnswerQuestionResponseMapper : IQuestionResponseMapper<ShortAnswerQuestion>
{
    public QuestionResponse Map(ShortAnswerQuestion question)
    {
        return new ShortAnswerQuestionResponse(
            question.Id,
            question.Title,
            question.Order,
            question.CorrectAnswers
                .Select(c => c.Text)
                .ToList()
        );
    }
}
