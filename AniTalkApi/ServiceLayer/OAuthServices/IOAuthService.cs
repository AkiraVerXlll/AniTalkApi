namespace AniTalkApi.ServiceLayer.OAuthServices;

public interface IOAuthService
{
    public string GetOAuthUrl(string scope, string codeChallenge);

    public Task<TokenResultModel> ExchangeCodeToTokenAsync(string code, string codeVerifier);

    public Task<TokenResultModel> RefreshTokenAsync(string refreshToken);
}