using Domain.Aggregates.QuizAggregate;

namespace Application.Commands.Quizzes;

public abstract record QuestionRequest(string Title, QuestionType Type, int Order);
public record MultipleChoiceQuestionRequest(string Title, int Order, List<ChoiceRequest> Choices) : QuestionRequest(Title, QuestionType.MultipleChoice, Order);
public record ChoiceRequest(string Text, bool IsCorrect);

public record ShortAnswerQuestionRequest(string Title, int Order, List<string> CorrectAnswers) : QuestionRequest(Title, QuestionType.ShortAnswer, Order);