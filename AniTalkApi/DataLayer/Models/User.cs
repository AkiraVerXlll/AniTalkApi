using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AniTalkApi.DataLayer.Models.Enums;
using AniTalkApi.DataLayer.Models.ManyToMany;

namespace AniTalkApi.DataLayer.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Nickname { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime DateOfRegistration { get; set; }

    [Required]
    public UserStatus Status { get; set; }

    [Required]
    public UserRoles Role { get; set; }

    public int PersonalInformationId { get; set; }

    #region Dependencies

    public PersonalInformation PersonalInformation { get; set; }

    public List<FavoriteTitles> FavoriteTitles { get; set; }

    public List<Relationships> Relationships { get; set; }

    public List<Review> Reviews { get; set; }

    public List<UsersInDialog> Dialogs { get; set; }

    #endregion
}