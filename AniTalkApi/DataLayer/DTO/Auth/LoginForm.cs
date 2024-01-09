using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.DTO.Auth;

public class LoginForm
{
    [Required]
    public string Login { get; init; }

    [Required]
    public string Password { get; init; }
}
