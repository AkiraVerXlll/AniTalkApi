using AniTalkApi.DataLayer;
using AniTalkApi.DataLayer.DTO.Auth;
using AniTalkApi.DataLayer.Models;
using AniTalkApi.DataLayer.Models.Enums;
using AniTalkApi.ServiceLayer.PasswordHasherServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AniTalkApi.Controllers.Auth;
[Route("/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly AppDbContext _context;

    private readonly IPasswordHasherService _passwordHasher;

    public RegisterController(
        AppDbContext context, 
        IPasswordHasherService passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromForm] RegisterForm formData)
    {
        if (await EmailIsUnavailable(formData.Email))
            return BadRequest("User with this email already exists");

        if (await NicknameIsUnavailable(formData.Nickname))
            return BadRequest("User with this nickname already exists");
        
        var salt = _passwordHasher.GenerateSalt();
        var hash = _passwordHasher
            .HashPassword(formData.Password, salt);

        var newUser = new User
        {
            Email = formData.Email,
            PasswordHash = hash, 
            Salt = salt,
            Nickname = formData.Nickname,
            DateOfRegistration = DateTime.Now,
            Status = UserStatus.Offline,
            Role = UserRoles.User,
            IsEmailVerified = false,
            PersonalInformation = new PersonalInformation()
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return Ok();
    }

    private async Task<bool> EmailIsUnavailable(string email)
    {
        return await _context
            .Users
            .AnyAsync(u => u.Email == email);
    }

    private async Task<bool> NicknameIsUnavailable(string nickname)
    {
        return await _context
            .Users
            .AnyAsync(u => u.Nickname == nickname);
    }
}