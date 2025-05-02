using Application.Services;
using Domain.AccountAggregate;
using Domain.Results;
using MediatR;
using System.Security.Claims;

namespace Application.Commands;

public record LoginAccountCommand(string Username, string Password) : IRequest<Result<string>>
{
}

public class LoginAccountCommandHandler(
    IAccountRepository accountRepository,
    IJwtService jwtService
    ) : IRequestHandler<LoginAccountCommand, Result<string>>
{
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IJwtService _jwtService = jwtService;
    public async Task<Result<string>> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByUsername(request.Username, cancellationToken);

        if (account is null)
        {
            return new Error("Account not found");
        }

        if (account.Password != request.Password)
        {
            return new Error("Invalid password");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, account.Username),
            new(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new(ClaimTypes.Role, account.Role.ToString())
        };

        var token = _jwtService.GenerateJwt(claims);
        
        return token;
    }
}