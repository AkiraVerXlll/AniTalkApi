using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AniTalkApi.ServiceLayer.TokenManagerServices;

public interface IAccessTokenManagerService
{
    public JwtSecurityToken CreateToken(IEnumerable<Claim> authClaims);

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
}