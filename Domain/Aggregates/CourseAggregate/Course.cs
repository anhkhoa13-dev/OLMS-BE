using Domain.Aggregates.SectionAggregate;
using Domain.Primatives;

namespace Domain.Aggregates.CourseAggregate;

public class Course : AggregateRoot
{
    #region Properties
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Code Code { get; } = default!;
    public Guid InstructorId { get; set; }
    #endregion

    #region Navigations
    private readonly List<Section> _sections = [];
    public IReadOnlyCollection<Section> Sections => _sections.AsReadOnly();
    #endregion
    private Course() : base() { }
    public Course(Guid id, string title, string description, Guid instructorId) : base(id) 
    {
        Title = title;
        Description = description;
        InstructorId = instructorId;
        Code = Code.Generate(id);
    }

    public void AddSection(string title, string description)
    {
        Section section = new(Guid.NewGuid(), title, description, _sections.Count + 1, Id);
        _sections.Add(section);
    }

    public void RemoveSection(Guid sectionId)
    {
        Section? section = _sections.FirstOrDefault(s => s.Id == sectionId);
        if (section is null) return;
        _sections.Remove(section);

        int order = section.Order;

        foreach (var sec in _sections.Where(s => s.Order > order))
        {
            sec.Order --;
        }
    }
}
