namespace AniTalkApi.ServiceLayer.EmailServices;

public interface IEmailSenderService
{
    public Task SendTemplateEmailAsync(string email, string emailTemplate, object payload);
}