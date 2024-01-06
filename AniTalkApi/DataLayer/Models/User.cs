using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AniTalkApi.DataLayer.Models.Enums;
using AniTalkApi.DataLayer.Models.ManyToMany;

namespace AniTalkApi.DataLayer.Models;

public class User
{
    [Key]
    public int Id { get; init; }

    [Required]
    [MaxLength(20)]
    public string Nickname { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public string Salt { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime DateOfRegistration { get; set; }

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    [Required]
    public UserStatus Status { get; set; }

    [Required]
    public UserRoles Role { get; set; }

    [Required]
    public bool IsEmailVerified { get; set; }

    public int PersonalInformationId { get; init; }

    #region Dependencies

    public PersonalInformation PersonalInformation { get; init; }

    public List<FavoriteTitles> FavoriteTitles { get; init; }

    public List<Relationships> RelationshipsAsMainUser { get; init; }

    public List<Relationships> RelationshipsAsSubjectUser { get; init; }

    public List<Review> Reviews { get; init; }

    public List<UsersInDialog> Dialogs { get; init; }

    #endregion
}