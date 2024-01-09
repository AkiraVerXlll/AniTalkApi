namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class UsersInDialog
{
    public string UserId { get; init; }

    public int DialogId { get; init; }

    #region Dependencies

    public User User { get; init; }

    public Dialog Dialog { get; init; }

    #endregion
}