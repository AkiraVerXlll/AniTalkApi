using AniTalkApi.DataLayer.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AniTalkApi.ServiceLayer.EmailServices;

public class SendGridEmailSenderService : IEmailSenderService
{
    private readonly SendGridSettings.EmailTemplateSettings _emailTemplates;

    private readonly SendGridClient _sendGridClient;

    private readonly EmailAddress _from;

    public SendGridEmailSenderService(IOptions<SendGridSettings> emailOptions)
    {
        var emailSettings = emailOptions.Value;
        var fromEmail = emailSettings.SenderEmail;
        var fromName = emailSettings.SenderName;
        var apiKey = emailSettings.ApiKey;
        _emailTemplates = emailSettings.EmailTemplates!;
        _sendGridClient = new SendGridClient(apiKey);
        _from = new EmailAddress(fromEmail, fromName);
    }

    public async Task SendEmailVerificationLinkAsync(string email, string verificationLink)
    {
        var to = new EmailAddress(email);
        await _sendGridClient.SendEmailAsync(
            MailHelper.CreateSingleTemplateEmail(
                _from, to, 
                _emailTemplates.EmailConfirmation,
                new {Link = verificationLink}));
    }

    public Task SendTwoFactorCodeAsync(string email, string code)
    {
        var to = new EmailAddress(email);
        return _sendGridClient.SendEmailAsync(
            MailHelper.CreateSingleTemplateEmail(
                _from, to,
                _emailTemplates.TwoFactorVerification,
                new { Code = code}));
    }
}