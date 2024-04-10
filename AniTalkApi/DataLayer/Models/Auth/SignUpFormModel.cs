using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models.Auth;

public class SignUpFormModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(20)]
    [RegularExpression(@"^[a-zA-Z0-9.]+$")]
    public string? Username { get; set; }

    [Required]
    [MinLength(6)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*\\W)[a-zA-Z0-9\\W]{6,}$")]
    public string? Password { get; set; }
}
