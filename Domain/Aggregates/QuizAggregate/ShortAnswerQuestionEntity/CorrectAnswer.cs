using Domain.Primatives;

namespace Domain.Aggregates.QuizAggregate.ShortAnswerQuestionEntity;

public class CorrectAnswer : ValueObject
{
    public string Text { get; set; } = default!;

    public CorrectAnswer(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Correct answer cannot be empty.", nameof(text));
        }
        Text = text.ToLower();
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Text;
    }
}
