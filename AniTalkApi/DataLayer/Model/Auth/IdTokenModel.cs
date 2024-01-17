using Newtonsoft.Json;

namespace AniTalkApi.DataLayer.Model.Auth;

public class IdTokenModel
{
    [JsonProperty("id_token")]
    public string? IdToken { get; set; }
}