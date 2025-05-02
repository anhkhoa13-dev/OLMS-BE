using Domain.Primatives;

namespace Domain.CourseAggregate;

public class Code(string value) : ValueObject
{
    public string Value { get; set; } = value;

    public static Code Generate(Guid id)
    {
        return new Code(id.ToString("N")[..6].ToUpper());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
