namespace AniTalkApi.DataLayer.Settings;

public struct AuthSettings
{
    public string EmailConfirmationLink { get; init; }

    public string PasswordResetLink { get; init; }
}