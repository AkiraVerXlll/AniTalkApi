﻿using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.DTO.Auth;

public class RegisterForm
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Nickname { get; set; }

    [Required]
    public string Password { get; set; } 
}