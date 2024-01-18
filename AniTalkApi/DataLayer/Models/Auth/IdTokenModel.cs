using Newtonsoft.Json;

namespace AniTalkApi.DataLayer.Models.Auth;

public class IdTokenModel
{
    [JsonProperty("id_token")]
    public string? IdToken { get; set; }
}