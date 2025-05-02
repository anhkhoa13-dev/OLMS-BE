using Domain.Primatives;

namespace Domain.CourseAggregate;

public class Section : Entity
{
    #region Properties
    public string Tittle { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Order { get; private set; }
    public Guid CourseId { get; set; }
    #endregion

    private Section() : base() { }
    public Section(Guid id, string title, string description, int order, Guid courseId) : base(id)
    {
        Tittle = title;
        Description = description;
        Order = order;
        CourseId = courseId;
    }
}
