﻿using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.DbModels;

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

    public string[]? ImageUrls { get; set; }

    [Required]
    public bool IsRead { get; set; }

    [Required]
    public bool IsEdited { get; set; }
    
    [DataType("timestamp with time zone")]
    public DateTime SendingTime { get; set; }

    #region Dependencies
    
    public User? Sender { get; set; }

    public Dialog? Dialog { get; set; }

    #endregion
}