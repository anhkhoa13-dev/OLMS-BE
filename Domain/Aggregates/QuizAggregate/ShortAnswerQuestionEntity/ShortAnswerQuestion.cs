

namespace Domain.Aggregates.QuizAggregate.ShortAnswerQuestionEntity;

public class ShortAnswerQuestion : Question
{
    #region Navigations
    private readonly List<CorrectAnswer> _correctAnswers = [];
    public IReadOnlyCollection<CorrectAnswer> CorrectAnswers => _correctAnswers.AsReadOnly();
    #endregion

    private ShortAnswerQuestion() : base() { } // For EF Core
    private ShortAnswerQuestion(Guid id, string title, int order) : base(id, title, order, QuestionType.ShortAnswer)
    {
    }

    public static ShortAnswerQuestion Create(Guid id, string title, int order, string[] correctAnswers)
    {
        if (correctAnswers.Length < 1)
        {
            throw new InvalidOperationException("A question must have at least 1 correct answer.");
        }
        ShortAnswerQuestion question = new(id, title, order);
        foreach (var answer in correctAnswers)
        {
            question.AddCorrectAnswer(answer);
        }
        return question;
    }

    public void AddCorrectAnswer(string text)
    {
        if (_correctAnswers.Any(c => c.Text.Equals(text, StringComparison.CurrentCultureIgnoreCase)))
        {
            return;
        }
        _correctAnswers.Add(new CorrectAnswer(text));
    }
}
