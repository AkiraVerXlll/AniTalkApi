using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.DbModels.Enums;

namespace AniTalkApi.DataLayer.DTO;

public record TitleFromForm
{
    [Required]
    public string? Name { get; init; } 

    [Required]
    public string? Description { get; init; } 

    [Required]
    public FormFile? Cover { get; init; } 

    [Required]
    public DateTime ReleaseDate { get; init; } 

    [Required]
    public TitleStatus TitleStatus { get; init; } 
}
