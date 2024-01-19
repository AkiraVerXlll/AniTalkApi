using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.DbModels.ManyToMany;

public class TitleAuthors
{
    [Required]
    public int AuthorId { get; init; }

    [Required]
    public int TitleTypesId { get; init; }

    [Required]
    public int AuthorTypeId { get; init; }

    #region Dependencies
    public AuthorType? AuthorType { get; set; }

    public TitleTypes? TitleTypes { get; init; } 

    public Author? Author { get; init; }

    #endregion
}
