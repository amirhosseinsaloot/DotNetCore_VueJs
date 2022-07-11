using System.Security.Claims;

namespace Domain.Interfaces.Services;
public interface IAuthTokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}