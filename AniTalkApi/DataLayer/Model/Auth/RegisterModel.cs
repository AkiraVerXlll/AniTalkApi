using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Model.Auth;

public class RegisterModel
{
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}
