using Newtonsoft.Json;

namespace AniTalkApi.ServiceLayer.OAuthServices;

public class TokenResultModel
{
    [JsonProperty("id_token")]
    public string? IdToken { get; set; }
}