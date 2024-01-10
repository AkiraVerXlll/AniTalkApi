using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AniTalkApi.ServiceLayer.TokenManagerService;

public interface ITokenManagerService
{
    public JwtSecurityToken CreateToken(IEnumerable<Claim> authClaims);

    public string GenerateRefreshToken();

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
}