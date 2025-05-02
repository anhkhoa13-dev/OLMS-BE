using Domain.CourseAggregate;
using Domain.Primatives;

namespace Domain.InstructorAggregate;

public class Instructor : AggregateRoot
{
    private readonly List<Guid> _courses = [];
    public IReadOnlyCollection<Guid> Courses => _courses.AsReadOnly();

    private Instructor() : base() { } 
    public Instructor(Guid id) : base(id) { }

    public Course CreateCourse(string title, string description)
    {
        Course course = new(Guid.NewGuid(), title, description, Id);
        _courses.Add(course.Id);
        return course;
    }
}
