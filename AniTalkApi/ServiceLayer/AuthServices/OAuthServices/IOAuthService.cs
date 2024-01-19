namespace AniTalkApi.ServiceLayer.AuthServices.OAuthServices;

public interface IOAuthService
{
    /// <summary>
    ///  Get url for redirecting user to OAuth server
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="codeChallenge"></param>
    /// <returns></returns>
    public string GetOAuthUrl(string scope, string codeChallenge);

    /// <summary>
    ///    Exchange authorization code to id_token with information about user
    /// </summary>
    /// <param name="code"></param>
    /// <param name="codeVerifier"></param>
    /// <returns></returns>
    public Task<string> ExchangeCodeToIdTokenAsync(string code, string codeVerifier);
}