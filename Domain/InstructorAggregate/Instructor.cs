using Domain.Primatives;

namespace Domain.InstructorAggregate;

public class Instructor : AggregateRoot
{
    private Instructor() : base() { } 
    public Instructor(Guid id) : base(id) { }
}
