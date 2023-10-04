using AniTalkApi.DataLayer.Models.Enums;

namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class TitleAuthors
{
    public int AuthorId { get; set; }

    public int TitleId { get; set; }

    public AuthorType AuthorType { get; set; }

    #region Dependencies

    public Title Title { get; set; }

    public Author Author { get; set; }

    #endregion
}
