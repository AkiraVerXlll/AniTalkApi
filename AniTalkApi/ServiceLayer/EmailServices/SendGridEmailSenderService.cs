using AniTalkApi.DataLayer.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AniTalkApi.ServiceLayer.EmailServices;

public class SendGridEmailSenderService : IEmailSenderService
{
    private readonly SendGridClient _sendGridClient;

    private readonly EmailAddress _from;

    public SendGridEmailSenderService(IOptions<SendGridSettings> emailOptions)
    {
        var emailSettings = emailOptions.Value;
        var fromEmail = emailSettings.SenderEmail;
        var fromName = emailSettings.SenderName;
        var apiKey = emailSettings.ApiKey;
        _sendGridClient = new SendGridClient(apiKey);
        _from = new EmailAddress(fromEmail, fromName);
    }

    public async Task SendTemplateEmailAsync(string email, string emailTemplate, object payload)
    {
        var to = new EmailAddress(email);
        await _sendGridClient.SendEmailAsync(
            MailHelper.CreateSingleTemplateEmail(
                _from, to, 
                emailTemplate,
                payload));
    }
}