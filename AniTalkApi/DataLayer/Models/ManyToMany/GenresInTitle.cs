namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class GenresInTitle
{
    public int TitleId { get; init; }

    public int GenreId { get; init; }

    public int Order { get; set; }

    #region Dependencies

    public Genre Genre { get; set; }

    public Title Title { get; set; }

    #endregion
}
