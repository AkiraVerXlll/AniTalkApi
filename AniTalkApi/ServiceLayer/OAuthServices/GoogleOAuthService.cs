using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.Helpers;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.OAuthServices;

public class GoogleOAuthService : IOAuthService
{
    private readonly GoogleOAuthSettings _settings;

    private readonly HttpClientHelper _httpClient;

    public GoogleOAuthService(
        IOptions<GoogleOAuthSettings> options,
        HttpClientHelper httpClient)
    {
        _settings = options.Value;
        _httpClient = httpClient;
    }

    public string GetOAuthUrl(string scope, string codeChallenge)
    {
        var url = _settings.AuthorizationEndpoint;

        var parameters = new Dictionary<string, string>
        {
            {"client_id", _settings.ClientId!},
            {"redirect_uri", _settings.RedirectUrl!},
            {"response_type", "code"},
            {"scope", scope},
            {"code_challenge", codeChallenge},
            {"code_challenge_method", "S256"},
        };
        var encodedParameters = string.Join("&", parameters
            .Select(x => $"{x.Key}={x.Value}"));
        return $"{url}?{encodedParameters}";
    }

    public async Task<string> ExchangeCodeToIdTokenAsync(string code, string codeVerifier)
    {
        var url = _settings.TokenEndpoint!;

        var parameters = new Dictionary<string, string>
        {
            {"code", code},
            {"client_id", _settings.ClientId!},
            {"client_secret", _settings.ClientSecret!},
            {"code_verifier", codeVerifier},
            {"grant_type", "authorization_code"},
            {"redirect_uri", _settings.RedirectUrl!}
        };

        var token = await _httpClient.SendPostRequest<IdTokenModel>(url, parameters);
        return token is null ? 
            throw new Exception("Token is null") : 
            token.IdToken!;
    }
}