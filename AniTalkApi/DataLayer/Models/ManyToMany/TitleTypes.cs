using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class TitleTypes
{
    [Key]
    public int Id { get; init; }

    [Required]
    public int TitleTypeId { get; init; }

    [Required]
    public int TitleId { get; init; }

    public string? SpecialDescription { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }


    #region Dependencies

    public Title? Title { get; init; } 

    public TitleAuthors? TitleAuthors { get; init; }

    public TitleType? TitleType { get; init; } 

    #endregion
}