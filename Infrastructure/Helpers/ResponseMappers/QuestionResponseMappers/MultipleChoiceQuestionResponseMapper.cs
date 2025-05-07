using Application.Queries.Quizzes;
using Domain.Aggregates.QuizAggregate.MultipleChoiceQuestionEntity;

namespace Infrastructure.Helpers.ResponseMappers.QuestionResponseMappers;

public class MultipleChoiceQuestionResponseMapper : IQuestionResponseMapper<MultipleChoiceQuestion>
{
    public QuestionResponse Map(MultipleChoiceQuestion question)
    {
        return new MultipleChoiceQuestionResponse(
            question.Id,
            question.Title,
            question.Order,
            question.Choices
                .Select(c => new ChoiceResponse(c.Id, c.Text))
                .ToList()
        );
    }

}

