using Newtonsoft.Json;

namespace AniTalkApi.ServiceLayer.OAuthServices;

public class TokenResultModel
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