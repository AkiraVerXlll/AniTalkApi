using System.ComponentModel.DataAnnotations.Schema;
using AniTalkApi.DataLayer.Models.Enums;

namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class Relationships
{
    public int MainUserId { get; set; }

    public int RelationshipsWithUserId { get; set; }

    public RelationshipsStatus RelationshipsStatus { get; set; }

    #region Dependencies

    public User MainUser { get; set; }

    public User RelationshipsWithUser { get; set; }

    #endregion
}