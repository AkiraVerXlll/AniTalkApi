namespace AniTalkApi.ServiceLayer.OAuthServices;

public interface IOAuthService
{
    public string GetOAuthUrl(string scope, string codeChallenge);

    public Task<string> ExchangeCodeToIdTokenAsync(string code, string codeVerifier);
}