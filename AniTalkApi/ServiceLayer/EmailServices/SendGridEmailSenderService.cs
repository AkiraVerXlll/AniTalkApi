using AniTalkApi.DataLayer.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AniTalkApi.ServiceLayer.EmailServices;

public class SendGridEmailSenderService : IEmailSenderService
{
    private readonly SendGridClient _sendGridClient;

    private readonly EmailAddress _from;

    public SendGridEmailSenderService(IOptions<EmailSettings> emailOptions)
    {
        var emailSettings = emailOptions.Value;
        var fromEmail = emailSettings.SenderEmail;
        var fromName = emailSettings.SenderName;
        var apiKey = emailSettings.ApiKey;
        _sendGridClient = new SendGridClient(apiKey);
        _from = new EmailAddress(fromEmail, fromName);
    }

    public async Task SendEmailVerificationLinkAsync(string email, string verificationLink)
    {
        var to = new EmailAddress(email);
        await _sendGridClient.SendEmailAsync(
            MailHelper.CreateSingleTemplateEmail(
                _from, to, 
                "d-56d2992d7db745fa85040e673215a537",
                new {Link = verificationLink}));
    }
}