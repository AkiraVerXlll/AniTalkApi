using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models.Auth;

public class SignUpFormModel
{
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}
