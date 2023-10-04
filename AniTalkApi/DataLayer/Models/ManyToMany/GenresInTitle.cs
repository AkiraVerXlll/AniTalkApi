namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class GenresInTitle
{
    public int TitleId { get; set; }

    public int GenreId { get; set; }

    public int Order { get; set; }

    #region Dependencies

    public Genre Genre { get; set; }

    public Title Title { get; set; }

    #endregion
}
