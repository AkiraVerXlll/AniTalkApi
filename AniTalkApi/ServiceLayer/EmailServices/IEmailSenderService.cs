namespace AniTalkApi.ServiceLayer.EmailServices;

public interface IEmailSenderService
{
    public Task SendEmailVerificationLinkAsync(string email, string verificationLink);
}