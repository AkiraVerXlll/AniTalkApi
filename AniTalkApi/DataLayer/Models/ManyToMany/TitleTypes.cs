using AniTalkApi.DataLayer.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    [Column(TypeName = "date")]
    public DateTime ReleaseDate { get; set; }

    [Required]
    public int CoverId { get; set; }

    public TitleStatus TitleStatus { get; set; }

    #region Dependencies

    public Title? Title { get; init; } 

    public TitleAuthors? TitleAuthors { get; init; }

    public TitleType? TitleType { get; init; } 

    #endregion
}