using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models;

public class Message
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int DialogId { get; set; }

    [Required]
    public string? SenderId { get; set; }

    [Required]
    public string? Text { get; set; }

    [Required]
    [DataType("timestamp with time zone")]
    public DateTime SendingTime { get; set; }

    #region Dependencies
    
    public User? Sender { get; set; }

    public Dialog? Dialog { get; set; }

    #endregion
}