using System.Text;
using System.Text.Unicode;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AniTalkApi.ServiceLayer;

public class GoogleOAuthService
{
    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public GoogleOAuthService(
        IConfiguration configuration,
        HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    public string GetGoogleOAuthUrl(string scope, string codeChallenge)
    {
        var url = _configuration["GoogleOAuth2.0:AuthorizationEndpoint"];

        var parameters = new Dictionary<string, string>
        {
            {"client_id", _configuration["GoogleOAuth2.0:ClientId"]!},
            {"redirect_uri", _configuration["GoogleOAuth2.0:RedirectUrl"]!},
            {"response_type", "code"},
            {"scope", scope},
            {"code_challenge", codeChallenge},
            {"code_challenge_method", "S256"}
        };
        var encodedParameters = string.Join("&", parameters
            .Select(x => $"{x.Key}={x.Value}"));
        return $"{url}?{encodedParameters}";
    }

    public string GenerateCodeVerifier()
    {
        var random = new Random();
        var bytes = new byte[32];
        random.NextBytes(bytes);
        return (bytes);
    }

    public string ExchangeCodeToToken(string code, string codeVerifier)
    {
        var url = _configuration["GoogleOAuth2.0:TokenEndpoint"];

        var parameters = new Dictionary<string, string>
        {
            {"code", code},
            {"client_id", _configuration["GoogleOAuth2.0:ClientId"]!},
            {"client_secret", _configuration["GoogleOAuth2.0:ClientSecret"]!},
            {"code_verifier", codeVerifier},
            {"grant_type", "authorization_code"},
            {"redirect_uri", _configuration["GoogleOAuth2.0:RedirectUrl"]!}
        };

        var encodedParameters = string
            .Join("&", parameters.Select(x => $"{x.Key}={x.Value}"));
        var response = _httpClient
            .PostAsync(url, new StringContent
                (encodedParameters, Encoding.UTF8, "application/x-www-form-urlencoded"))
            .Result;
        var content = response.Content.ReadAsStringAsync().Result;
        var token = JsonConvert.DeserializeObject<TokenResult>(content);

        return token is null ? 
            throw new Exception("TokenIsNull") : 
            token.AccessToken;
    }

    private class TokenResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = null!;

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; } = null!;

        [JsonProperty("scope")]
        public string Scope { get; set; } = null!;

        [JsonProperty("token_type")]
        public string TokenType { get; set; } = null!;
    }
}