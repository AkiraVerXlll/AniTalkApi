using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.Models.ManyToMany;

namespace AniTalkApi.DataLayer.Models;

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
