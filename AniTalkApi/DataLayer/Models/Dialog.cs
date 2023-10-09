using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.Models.ManyToMany;

namespace AniTalkApi.DataLayer.Models;

public class Dialog
{
    public int Id { get; init; }

    [Required]
    public string Name { get; set; }

    public int AvatarId { get; set; }

    #region Dependencies

    public Image Avatar { get; set; }

    public List<UsersInDialog> UsersInDialogs;

    public List<Message> Messages;

    public Forum? Forum { get; set; }

    #endregion
}
