﻿using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.Models.ManyToMany;

namespace AniTalkApi.DataLayer.Models;

public class TitleType
{
    [Key]
    public int Id { get; init; }

    [Required]
    public string Name { get; set; }

    #region Dependencies

    public List<TitleTypes> TitleTypes { get; init; }

    #endregion
}
