namespace Domain.Results;

public sealed record Error(string Code, string? ErrorMessage = null)
{
    public static readonly Error None = new(string.Empty);
}

