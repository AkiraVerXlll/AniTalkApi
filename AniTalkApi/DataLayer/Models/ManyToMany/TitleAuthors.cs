using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.Models.Enums;

namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class TitleAuthors
{
    [Required]
    public int AuthorId { get; init; }

    [Required]
    public int TitleId { get; init; }

    [Required]
    public AuthorType AuthorType { get; set; }

    #region Dependencies

    public Title? Title { get; init; } 

    public Author? Author { get; init; }

    #endregion
}
