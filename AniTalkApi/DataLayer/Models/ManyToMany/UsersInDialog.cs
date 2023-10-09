namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class UsersInDialog
{
    public int UserId { get; set; }

    public int DialogId { get; set; }

    #region Dependencies

    public User User { get; set; }

    public Dialog Dialog { get; set; }

    #endregion
}