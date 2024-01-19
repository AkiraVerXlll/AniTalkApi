using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models.Auth;

public class LoginFormModel
{
    [Required]
    public string? Login { get; init; }

    [Required]
    public string? Password { get; init; }
}
