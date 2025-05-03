using Domain.Primatives;

namespace Domain.CourseAggregate;

public class Section : Entity
{
    #region Properties
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Order { get; set; }
    public Guid CourseId { get; set; }
    #endregion

    private Section() : base() { }
    public Section(Guid id, string title, string description, int order, Guid courseId) : base(id)
    {
        Title = title;
        Description = description;
        Order = order;
        CourseId = courseId;
    }
}
