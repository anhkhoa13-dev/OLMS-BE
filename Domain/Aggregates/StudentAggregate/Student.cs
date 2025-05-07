using Domain.Primatives;

namespace Domain.Aggregates.StudentAggregate;

public class Student : AggregateRoot
{
    private Student() : base() { }
    public Student(Guid id) : base(id) { }
    

}
