using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.DbModels.ManyToMany;

namespace AniTalkApi.DataLayer.DbModels;

public class Dialog
{
    [Key]
    public int Id { get; init; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public int AvatarId { get; set; }

    #region Dependencies

    public Image? Avatar { get; init; }

    public List<UsersInDialog>? UsersInDialogs { get; init; }

    public List<Message>? Messages { get; init; }

    public Forum? Forum { get; init; }

    #endregion
}
