using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class TitleTypes
{
    [Required]
    public int TitleTypeId { get; init; }

    [Required]
    public int TitleId { get; init; }

    #region Dependencies

    public Title? Title { get; init; } 

    public TitleType? TitleType { get; init; } 

    #endregion
}