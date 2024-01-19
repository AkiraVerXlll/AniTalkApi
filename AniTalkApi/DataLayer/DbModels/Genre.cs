using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.DbModels.ManyToMany;

namespace AniTalkApi.DataLayer.DbModels;

public class Genre
{
    [Key]
    public int Id { get; init; }

    [Required]
    public int Name { get; init; }

    [Required]
    public string? NormalizeName { get; init; }

    #region Dependencies
    
    public List<GenresInTitle>? GenresInTitle { get; init; }

    #endregion
}