using System.Security.Cryptography;
using System.Text;

namespace AniTalkApi.ServiceLayer.PasswordHasherServices;

public class PasswordHasherSHA256Service : IPasswordHasherService
{
    private readonly SHA256 _passwordHasher;

    private readonly RandomNumberGenerator _saltGenerator;

    public PasswordHasherSHA256Service()
    {
        
        _passwordHasher = SHA256.Create();
        _saltGenerator = RandomNumberGenerator.Create();
    }

    public string HashPassword(string password, string salt)
    {
        password += salt;
        var passwordHash = _passwordHasher
            .ComputeHash(Encoding.UTF8.GetBytes(password))
            .ToString();
        return passwordHash;
    }

    public bool VerifyPassword(string inputPassword, string hash, string salt)
    {
        return HashPassword(inputPassword, salt).Equals(hash);
    }

    public string GenerateSalt()
    {
        var salt = new byte[32];
        _saltGenerator.GetBytes(salt);
        return Encoding.UTF8.GetString(salt);
    }

    

}
