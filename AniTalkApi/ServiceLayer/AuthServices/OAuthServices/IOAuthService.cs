namespace AniTalkApi.ServiceLayer.AuthServices.OAuthServices;

public interface IOAuthService
{
    public string GetOAuthUrl(string codeChallenge);

    public Task<string> ExchangeCodeToIdTokenAsync(string code, string codeVerifier);

    public string GenerateCodeVerifier();

    public string GenerateCodeChallenge(string codeVerifier);
}