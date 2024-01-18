namespace AniTalkApi.DataLayer.Settings;

public class JwtSettings
{
    public string? ValidAudience { get; init; }

    public string? ValidIssuer { get; init; }

    public string? Secret { get; init; }

    public string? TokenValidityInMinutes { get; init; }

    public string? RefreshTokenValidityInDays { get; init; }

    public string? RefreshTokenLength { get; init; }

    public string? Scope { get; init; }
}