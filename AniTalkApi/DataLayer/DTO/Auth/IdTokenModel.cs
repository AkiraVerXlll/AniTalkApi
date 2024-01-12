using Newtonsoft.Json;

namespace AniTalkApi.DataLayer.DTO.Auth;

public class IdTokenModel
{
    [JsonProperty("id_token")]
    public string? IdToken { get; set; }
}