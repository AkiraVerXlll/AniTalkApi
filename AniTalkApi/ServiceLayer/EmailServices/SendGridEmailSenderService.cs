using System.Text.RegularExpressions;
using AniTalkApi.DataLayer.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AniTalkApi.ServiceLayer.EmailServices;

public class SendGridEmailSenderService : IEmailSenderService
{
    private readonly IOptions<EmailSettings> _emailSettings;

    public SendGridEmailSenderService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings;
    }

    public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var fromEmail = _emailSettings.Value.SenderEmail;
        var fromName = _emailSettings.Value.SenderName;
        var apiKey = _emailSettings.Value.ApiKey;
        var sendGridClient = new SendGridClient(apiKey);
        var from = new EmailAddress(fromEmail, fromName);
        var to = new EmailAddress(email);
        var plainTextContent = Regex.Replace(htmlMessage, "<[^>]*>", "");
        var msg = MailHelper.CreateSingleEmail(from, to, subject,
            plainTextContent, htmlMessage);
        return (await sendGridClient.SendEmailAsync(msg)).IsSuccessStatusCode;
    }
}