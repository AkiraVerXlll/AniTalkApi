using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models;

public class Forum
{
    [Key]
    public int DialogId { get; init; }

    [Required]
    public int TitleId { get; init; }

    [Required]
    public string? Topic { get; set; }

    [Required]
    public bool IsFrozen { get; set; }

    #region Dependencies

    public Dialog? Dialog { get; init; }

    public Title? Title { get; init; }

    #endregion
}