using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AniTalkApi.DataLayer.Models;

public class PersonalInformation
{
    [Key]
    public int Id { get; set; }

    public int AvatarId { get; set; }

    [MaxLength(512)]
    public string? AboutYourself { get; set; }

    [MaxLength(25)]
    public string? Country { get; set; }

    [MaxLength(60)]
    public string? City { get; set; }

    public int? Age { get; set; }

    [Column(TypeName = "date")]
    public DateTime? BirthDate { get; set; }

    #region Dependencies

    public User? User { get; set; }

    public Author? Author { get; set; } 

    public Image Avatar { get; set; }

    #endregion
}