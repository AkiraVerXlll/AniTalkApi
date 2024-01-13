using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AniTalkApi.DataLayer.Models.Enums;
using AniTalkApi.DataLayer.Models.ManyToMany;

namespace AniTalkApi.DataLayer.Models;

public class Title
{
    [Key]
    public int Id { get; init; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public int CoverId { get; set; }

    [Column(TypeName = "date")]
    [Required]
    public DateTime ReleaseDate { get; set; }
    
    public TitleStatus TitleStatus { get; set; }

    #region Dependencies

    public Image? Cover { get; init; }

    public List<TitleTypes>? TitleTypes { get; init; }

    public List<GenresInTitle>? Genres { get; init; }

    public List<TagsInTitle>? Tags { get; init; }

    public List<FavoriteTitles>? FavoriteTitlesOf { get; init; }

    public List<Review>? Reviews { get; init; }

    public List<Forum>? Forums { get; init; }

    #endregion
}