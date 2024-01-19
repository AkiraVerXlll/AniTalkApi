using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.DbModels.ManyToMany;

public class MessageReadBy
{
    [Required]
    public int MessageId { get; init; }

    [Required]
    public string? UserId { get; init; }

    [Required]
    [DataType("timestamp with time zone")]
    public DateTime ReadingTime { get; init; }

    #region Dependencies

    public Message? Message { get; init; }

    public User? User { get; init; }


    #endregion
}