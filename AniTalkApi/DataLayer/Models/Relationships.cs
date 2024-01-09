using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AniTalkApi.DataLayer.Models.Enums;

namespace AniTalkApi.DataLayer.Models;

public class Relationships
{
    public string MainUserId { get; init; }

    public string RelationshipsWithUserId { get; init; }

    [Required]
    public RelationshipsStatus RelationshipsStatus { get; set; }

    #region Dependencies

    public User MainUser { get; init; }

    public User RelationshipsWithUser { get; init; }

    #endregion
}