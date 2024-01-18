using AniTalkApi.DataLayer.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AniTalkApi.ServiceLayer.EmailServices;

public class SendGridEmailSenderService : IEmailSenderService
{
    private readonly EmailSettings _emailSettings;

    public SendGridEmailSenderService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var fromEmail = _emailSettings.SenderEmail;
        var fromName = _emailSettings.SenderName;
        var apiKey = _emailSettings.ApiKey;
        var sendGridClient = new SendGridClient(apiKey);
        var from = new EmailAddress(fromEmail, fromName);
        var to = new EmailAddress(email);
        var m = MailHelper.CreateSingleTemplateEmail(from, to, "d-ea5228c967b94dcbb189732e0ccd6f84", null);
        return (await sendGridClient.SendEmailAsync(m)).IsSuccessStatusCode;
    }
}