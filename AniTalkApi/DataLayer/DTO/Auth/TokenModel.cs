using System.Text.Json.Serialization;

namespace AniTalkApi.DataLayer.DTO.Auth;

public class TokenModel
{
    [JsonPropertyName("access_token")] 
    public string? AccessToken { get; set; }

    [JsonPropertyName("refresh_token")] 
    public string? RefreshToken { get; set; } 
}
