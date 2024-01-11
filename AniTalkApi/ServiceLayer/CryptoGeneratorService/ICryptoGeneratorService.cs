namespace AniTalkApi.ServiceLayer.CryptoGeneratorService;

/// <summary>
/// Service for generating random sequence of characters
/// </summary>
public interface ICryptoGeneratorService
{
    public string GenerateRandomString(int length);
}
