using Domain.Primatives;

namespace Domain.Aggregates.QuizAggregate;

public class Quiz : AggregateRoot
{
    #region Properties
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    #endregion

    #region Navigations
    private readonly List<Question> _questions = [];
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();
    #endregion

    private Quiz() : base() { }
    private Quiz(Guid id, string title, string description) : base(id)
    {
        Title = title;
        Description = description;
    }

    public static Quiz Create(Guid id, string title, string description, Question[] questions)
    {
        if (questions.Length < 1)
        {
            throw new InvalidOperationException("A quiz must have at least 1 question.");
        }
        if (questions.Length > 10)
        {
            throw new InvalidOperationException("A quiz can have a maximum of 10 questions.");
        }
        Quiz quiz = new(id, title, description);
        foreach (var question in questions)
        {
            quiz.AddQuestion(question);
            question.QuizId = id;
        }
        return quiz;
    }

    public void AddQuestion(Question question)
    {
        if(_questions.Count >= 10)
        {
            throw new InvalidOperationException("A quiz can have a maximum of 10 questions.");
        }
        _questions.Add(question);
    }
    public void RemoveQuestion(Guid questionId)
    {
        if (_questions.Count <= 1)
        {
            throw new InvalidOperationException("A quiz must have at least 1 question.");
        }
        Question? question = _questions.FirstOrDefault(q => q.Id == questionId);

        if (question is null)
        {
            return;
        }
        _questions.Remove(question);

    }
}
