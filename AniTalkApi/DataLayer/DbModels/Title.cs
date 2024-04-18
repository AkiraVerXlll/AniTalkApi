using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.DbModels.ManyToMany;

namespace AniTalkApi.DataLayer.DbModels;

public class Title
{
    [Key]
    public int Id { get; init; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? CoverUrl { get; set; }

    [Required]
    public string? NormalizeName { get; set; }

    [Required]
    public string? Description { get; set; }

    #region Dependencies

    public List<TitleTypes>? TitleTypes { get; init; }

    public List<GenresInTitle>? Genres { get; init; }

    public List<FavoriteTitles>? FavoriteTitlesOf { get; init; }

    public List<Review>? Reviews { get; init; }

    public List<Forum>? Forums { get; init; }

    #endregion
}