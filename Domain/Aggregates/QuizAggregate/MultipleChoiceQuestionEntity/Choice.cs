using Domain.Primatives;

namespace Domain.Aggregates.QuizAggregate.MultipleChoiceQuestionEntity;

public class Choice : Entity
{
    public string Text { get; set; } = default!;
    public bool IsCorrect { get; set; } = default!;
    public Guid MultipleChoiceQuestionId { get; set; } = default!;

    private Choice() : base() {}// For EF Core

    public Choice(Guid id, string text, bool isCorrect, Guid multipleChoiceQuestionId) : base(id)
    {
        Text = text;
        IsCorrect = isCorrect;
        MultipleChoiceQuestionId = multipleChoiceQuestionId;
    }

}
