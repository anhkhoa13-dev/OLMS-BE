using Domain.Primatives;

namespace Domain.Aggregates.QuizAggregate;

public abstract class Question : Entity
{
    #region Properties
    public string Title { get; set; } = default!;
    public int Order { get; set; }
    public QuestionType Type { get; set; }
    public Guid QuizId { get; set; }
    #endregion
    protected Question() : base() { }
    protected Question(Guid id, string title, int order, QuestionType type) : base(id)
    {
        Title = title;
        Order = order;
        Type = type;
    }
}
