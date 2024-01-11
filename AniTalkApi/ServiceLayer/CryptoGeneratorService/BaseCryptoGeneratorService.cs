using System.Security.Cryptography;

namespace AniTalkApi.ServiceLayer.CryptoGeneratorService;

public class BaseCryptoGeneratorService : ICryptoGeneratorService
{
    public string GenerateRandomString(int length)
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
