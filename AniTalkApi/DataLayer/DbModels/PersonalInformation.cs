﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AniTalkApi.DataLayer.DbModels;

public class PersonalInformation
{
    [Key]
    public int Id { get; init; }

    [Required]
    public string? AvatarUrl { get; init; }

    [MaxLength(30)]
    public string? Name { get; set; }

    [MaxLength(30)]
    public string? Surname { get; set; }

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

    public User? User { get; init; }

    public Author? Author { get; init; } 

    #endregion
}