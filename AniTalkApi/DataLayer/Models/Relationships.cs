using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AniTalkApi.DataLayer.Models.Enums;

namespace AniTalkApi.DataLayer.Models;

public class Relationships
{
    public int MainUserId { get; init; }

    public int RelationshipsWithUserId { get; init; }

    [Required]
    public RelationshipsStatus RelationshipsStatus { get; set; }

    #region Dependencies

    public User MainUser { get; set; }

    public User RelationshipsWithUser { get; set; }

    #endregion
}