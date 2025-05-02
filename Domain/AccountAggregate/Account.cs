using Domain.Primatives;

namespace Domain.AccountAggregate;

public class Account : AggregateRoot
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public Role Role { get; set; }
    private Account() : base() { }
    public Account(Guid id, string username, string password, Role role) : base(id) 
    {
        Username = username;
        Password = password;
        Role = role;
    }

}
