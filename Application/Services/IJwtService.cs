using System.Security.Claims;

namespace Application.Services;

public interface IJwtService
{
    string GenerateJwt(IReadOnlyCollection<Claim> claims);
}
