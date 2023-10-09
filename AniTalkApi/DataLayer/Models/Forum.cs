using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models;

public class Forum
{
    [Key]
    public int DialogId { get; set; }

    public int TitleId { get; set; }

    [Required]
    public string Topic { get; set; }

    public bool IsFrozen { get; set; }

    #region Dependencies

    public Dialog Dialog { get; set; }

    public Title Title { get; set; }

    #endregion
}