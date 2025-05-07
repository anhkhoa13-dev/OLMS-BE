using Application.Queries.Quizzes;
using Domain.Aggregates.QuizAggregate;
using Infrastructure.Helpers.ResponseMappers.QuestionResponseMappers;

namespace Infrastructure.Helpers.ResponseMapperApdapters;

public interface IQuestionDtoMapper
{
    QuestionResponse Map(Question question);
    bool CanHandle(Question question);
}

public class QuestionResponseMapperAdapter<T>(
    IQuestionResponseMapper<T> mappers) : IQuestionDtoMapper where T : Question
{
    private readonly IQuestionResponseMapper<T> _mappers = mappers;
    public bool CanHandle(Question question) => _mappers.CanHandle(question);
    public QuestionResponse Map(Question question) => _mappers.Map((T)question);
}
