using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.Models.Enums;

namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class TitleAuthors
{
    [Required]
    public int AuthorId { get; init; }

    [Required]
    public int TitleTypesId { get; init; }

    [Required]
    public AuthorType AuthorType { get; set; }

    #region Dependencies

    public TitleTypes? TitleTypes { get; init; } 

    public Author? Author { get; init; }

    #endregion
}
