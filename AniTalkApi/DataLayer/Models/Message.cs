using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models;

public class Message
{
    public int Id { get; set; }

    public int DialogId { get; set; }

    public int SenderId { get; set; }

    public string? Text { get; set; }

    [Required]
    [DataType("timestamp with time zone")]
    public DateTime SendingTime { get; set; }

    #region Dependencies
    
    public User Sender { get; set; }

    public Dialog Dialog { get; set; }

    #endregion
}