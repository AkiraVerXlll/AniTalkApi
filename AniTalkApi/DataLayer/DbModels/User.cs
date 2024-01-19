using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AniTalkApi.DataLayer.DbModels.Enums;
using AniTalkApi.DataLayer.DbModels.ManyToMany;
using Microsoft.AspNetCore.Identity;

namespace AniTalkApi.DataLayer.DbModels;

public class User : IdentityUser
{

    [Required]
    [Column(TypeName = "date")]
    public DateTime DateOfRegistration { get; init; }

    [Required]
    public UserStatus Status { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }

    public int PersonalInformationId { get; init; }

    #region Dependencies

    public PersonalInformation? PersonalInformation { get; init; }

    public List<FavoriteTitles>? FavoriteTitles { get; init; }

    public List<Relationships>? RelationshipsAsMainUser { get; init; }

    public List<Relationships>? RelationshipsAsSubjectUser { get; init; }

    public List<Review>? Reviews { get; init; }

    public List<UsersInDialog>? Dialogs { get; init; }

    #endregion
}

public class UserRoles
{
    public const string User = "User";
    public const string Moderator = "Moderator";
    public const string Admin = "Admin";
}