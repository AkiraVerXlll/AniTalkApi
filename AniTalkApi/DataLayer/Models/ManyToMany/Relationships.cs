using System.ComponentModel.DataAnnotations.Schema;
using AniTalkApi.DataLayer.Models.Enums;

namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class Relationships
{
    public int MainUserId { get; init; }

    public int RelationshipsWithUserId { get; init; }

    public RelationshipsStatus RelationshipsStatus { get; set; }

    #region Dependencies

    public User MainUser { get; set; }

    public User RelationshipsWithUser { get; set; }

    #endregion
}