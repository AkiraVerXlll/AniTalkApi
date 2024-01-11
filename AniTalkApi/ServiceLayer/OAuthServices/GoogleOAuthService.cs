using System.Text;
using Newtonsoft.Json;

namespace AniTalkApi.ServiceLayer.OAuthServices;

public class GoogleOAuthService : IOAuthService
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

    public string GetOAuthUrl(string scope, string codeChallenge)
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

    public async Task<TokenResultModel> ExchangeCodeToTokenAsync(string code, string codeVerifier)
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
        var response = await _httpClient
            .PostAsync(url, new StringContent
                (encodedParameters, Encoding.UTF8, "application/x-www-form-urlencoded"));

        var content = response.Content.ReadAsStringAsync().Result;
        var token = JsonConvert.DeserializeObject<TokenResultModel>(content);

        return token ?? throw new Exception("TokenIsNull");
    }

    public async Task<TokenResultModel> RefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }
}