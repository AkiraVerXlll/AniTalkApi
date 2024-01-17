namespace AniTalkApi.ServiceLayer.EmailServices;

public interface IEmailSenderService
{
    public Task<bool> SendEmailAsync(string email, string subject, string htmlMessage);
}