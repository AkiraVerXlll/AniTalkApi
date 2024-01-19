namespace AniTalkApi.ServiceLayer.EmailServices;

public interface IEmailSenderService
{
    public Task SendEmailVerificationLinkAsync(string email, string verificationLink);

    public Task SendTwoFactorCodeAsync(string email, string code);
}