using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models.Auth;

public struct SignUpFormModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [MinLength(4)]
    [MaxLength(20)]
    [RegularExpression(@"^[a-zA-Z0-9.]+$")]
    public string? Username { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\W).{6,32}$")]
    public string? Password { get; set; }
}
