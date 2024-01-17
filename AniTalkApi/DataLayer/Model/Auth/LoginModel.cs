using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Model.Auth;

public class LoginModel
{
    [Required]
    public string? Login { get; init; }

    [Required]
    public string? Password { get; init; }
}
