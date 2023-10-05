namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class FavoriteTitles
{
    public int UserId { get; init; }

    public int TitleId { get; init; }

    public int Order { get; set; }

    #region Dependencies

    public Title Title { get; set; }

    public User User { get; set; }

    #endregion
}
