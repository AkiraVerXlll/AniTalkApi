using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AniTalkApi.DataLayer.Models.Enums;
using AniTalkApi.DataLayer.Models.ManyToMany;

namespace AniTalkApi.DataLayer.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [MaxLength(20)]
    public string Nickname { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    [Column(TypeName = "date")]
    public DateTime DateOfRegistration { get; set; }

    public UserStatus Status { get; set; }

    public UserRoles Role { get; set; }

    public int PersonalInformationId { get; set; }

    #region Dependencies

    public PersonalInformation PersonalInformation { get; set; }

    public List<FavoriteTitles> FavoriteTitles { get; set; }

    public List<Relationships> Relationships { get; set; }

    public List<Review> Reviews { get; set; }

    #endregion
}