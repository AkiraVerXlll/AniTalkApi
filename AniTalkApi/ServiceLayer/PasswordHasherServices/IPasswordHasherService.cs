using AniTalkApi.DataLayer.Models;

namespace AniTalkApi.ServiceLayer.PasswordHasherServices;

public interface IPasswordHasherService
{
    public string HashPassword(string password, string salt);

    public string GenerateSalt();

    public bool VerifyPassword(string inputPassword,  string hash, string salt);

}