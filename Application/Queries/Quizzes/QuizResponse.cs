using Domain.Aggregates.QuizAggregate;

namespace Application.Queries.Quizzes;

public record QuizResponse(
    Guid Id, 
    string Title, 
    string Description, 
    List<QuestionResponse> Questions)
{
}

public abstract record QuestionResponse(Guid Id, string Title, int Order, QuestionType Type)
{
}

public record MultipleChoiceQuestionResponse(
    Guid Id, 
    string Title, 
    int Order, 
    List<ChoiceResponse> Choices) : QuestionResponse(Id, Title, Order, QuestionType.MultipleChoice)
{
}

public record ChoiceResponse(Guid Id, string Text)
{
}

public record ShortAnswerQuestionResponse(
    Guid Id,
    string Title,
    int Order,
    List<string> CorrectAnswers) : QuestionResponse(Id, Title, Order, QuestionType.ShortAnswer)
{
}
