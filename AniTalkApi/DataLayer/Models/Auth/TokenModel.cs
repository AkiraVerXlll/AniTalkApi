using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AniTalkApi.DataLayer.Models.Auth;

public class TokenModel
{
    [Required]
    public string AccessToken { get; set; }

    [Required]
    public string RefreshToken { get; set; }
}
