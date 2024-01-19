using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.DbModels.ManyToMany;

public class UsersInDialog
{
    [Required]
    public string? UserId { get; init; } 

    [Required]
    public int DialogId { get; init; }

    #region Dependencies

    public User? User { get; init; } 

    public Dialog? Dialog { get; init; } 

    #endregion
}