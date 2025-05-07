using Domain.Aggregates.QuizAggregate;

namespace Domain.Aggregates.QuizAggregate.MultipleChoiceQuestionEntity;

public class MultipleChoiceQuestion : Question
{
    #region Properties
    private readonly List<Choice> _choices = [];
    public IReadOnlyCollection<Choice> Choices => _choices.AsReadOnly();

    #endregion
    private MultipleChoiceQuestion() : base() { }
    private MultipleChoiceQuestion(Guid id, string title, int order) : base(id, title, order, QuestionType.MultipleChoice)
    {
    }

    public static MultipleChoiceQuestion Create(Guid id, string title, int order, (string, bool)[] choices)
    {
        if (choices.Length < 2)
        {
            throw new InvalidOperationException("A question must have at least 2 choices.");
        }

        if (choices.Length > 4)
        {
            throw new InvalidOperationException("A question can have a maximum of 4 choices.");
        }

        MultipleChoiceQuestion question = new(id, title, order);
        foreach (var (text, isCorrect) in choices)
        {
            question.AddChoice(text, isCorrect);
        }

        // Ensure that at least one choice is correct
        if (!question.Choices.Any(c => c.IsCorrect))
        {
            throw new InvalidOperationException("At least one choice must be correct.");
        }

        return question;
    }

    public void AddChoice(string text, bool isCorrect)
    {
 
        if(_choices.Count >= 4)
        {
            throw new InvalidOperationException("A question can have a maximum of 4 choices.");
        }

        if(_choices.Any(c => c.Text == text))
        {
            throw new InvalidOperationException("A choice with the same text already exists.");
        }

        Choice choice = new(Guid.NewGuid(), text, isCorrect, Id);
        _choices.Add(choice);
    }
    public void RemoveChoice(Guid choiceId)
    {
        if (Choices.Count <= 2)
        {
            throw new InvalidOperationException("A question must have at least 2 choices.");
        }

        var choice = Choices.FirstOrDefault(c => c.Id == choiceId);

        if (choice is null) return;

        // If the removed choice was correct, ensure that at least one choice remains correct
        if (choice.IsCorrect && !Choices.Any(c => c.IsCorrect))
        {
            throw new InvalidOperationException("At least one choice must be correct.");
        }

        _choices.Remove(choice);


    }
}
