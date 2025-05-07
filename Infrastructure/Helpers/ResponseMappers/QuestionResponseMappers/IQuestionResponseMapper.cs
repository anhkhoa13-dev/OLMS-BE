using Application.Queries.Quizzes;
using Domain.Aggregates.QuizAggregate;

namespace Infrastructure.Helpers.ResponseMappers.QuestionResponseMappers;

public interface IQuestionResponseMapper<T> where T : Question
{
    QuestionResponse Map(T question);
    bool CanHandle(Question question) => question is T;
}