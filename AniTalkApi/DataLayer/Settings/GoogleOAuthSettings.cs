namespace AniTalkApi.DataLayer.Settings;

public class GoogleOAuthSettings
{
    public string? ClientId { get; init; }

    public string? ClientSecret { get; init; }

    public string? AuthorizationEndpoint { get; init; }

    public string? TokenEndpoint { get; init; }

    public string? RedirectUrl { get; init; }

    public string? Scope { get; init; }

    public int CodeVerifierLength { get; init; }
}