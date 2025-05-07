using Domain.Primatives;

namespace Domain.Aggregates.AccountAggregate;

public class Account : AggregateRoot
{
    public string Username { get; } = default!;
    public string Password { get; set; } = default!;
    public Role Role { get; set; }
    public string FullName { get; set; } = default!;
    private Account() : base() { }
    public Account(Guid id, string username, string password, string fullname, Role role) : base(id) 
    {
        Username = username;
        Password = password;
        Role = role;
        FullName = fullname;
    }

}
