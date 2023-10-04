namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class Review
{
    public int UserID { get; init; }

    public int TitleId { get; init; }

    public int StarsCount { get; set; }

    public string Text { get; set; }

    #region Dependencies

    public User User { get; set; }

    public Title Title { get; set; }

    #endregion
}
