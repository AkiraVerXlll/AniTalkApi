namespace AniTalkApi.DataLayer.Settings;

public class SendGridSettings
{
    public string? SenderEmail { get; init; }
    public string? SenderName { get; init; }
    public string? ApiKey { get; init; }

    public EmailTemplateSettings? EmailTemplates { get; init; }

    public class EmailTemplateSettings
    {
        public string? EmailConfirmation { get; init; }
        public string? TwoFactorVerification { get; init; }

        public string? ResetPassword { get; init; }
    }
}