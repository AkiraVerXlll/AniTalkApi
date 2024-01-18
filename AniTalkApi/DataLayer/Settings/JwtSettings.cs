namespace AniTalkApi.DataLayer.Settings;

public class JwtSettings
{
    public string? Audience { get; init; }

    public string? Issuer { get; init; }

    public string? Secret { get; init; }

    public int TokenValidityInMinutes { get; init; }

    public int RefreshTokenValidityInDays { get; init; }

    public int RefreshTokenLength { get; init; }

    public string? Scope { get; init; }
}