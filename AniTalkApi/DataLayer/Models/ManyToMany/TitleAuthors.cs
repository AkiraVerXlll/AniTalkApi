using AniTalkApi.DataLayer.Models.Enums;

namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class TitleAuthors
{
    public int AuthorId { get; init; }

    public int TitleId { get; init; }

    public AuthorType AuthorType { get; set; }

    #region Dependencies

    public Title Title { get; set; }

    public Author Author { get; set; }

    #endregion
}
