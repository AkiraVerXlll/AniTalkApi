﻿using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.Models.Enums;

namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class TitleAuthors
{
    [Required]
    public int AuthorId { get; init; }

    [Required]
    public int TitleTypesId { get; init; }

    [Required]
    public int AuthorTypeId { get; init; }

    #region Dependencies
    public AuthorType AuthorType { get; set; }

    public TitleTypes? TitleTypes { get; init; } 

    public Author? Author { get; init; }

    #endregion
}
