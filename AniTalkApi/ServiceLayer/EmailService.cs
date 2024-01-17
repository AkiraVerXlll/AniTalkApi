using System.Text.RegularExpressions;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AniTalkApi.ServiceLayer;

public class EmailService
{
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var fromEmail = _configuration["SenderEmail"];
        var fromName = _configuration["SenderName"];
        var apiKey = _configuration["ApiKey"];
        var sendGridClient = new SendGridClient(apiKey);
        var from = new EmailAddress(fromEmail, fromName);
        var to = new EmailAddress(email);
        var plainTextContent = Regex.Replace(htmlMessage, "<[^>]*>", "");
        var msg = MailHelper.CreateSingleEmail(from, to, subject,
            plainTextContent, htmlMessage);
        var response = await sendGridClient.SendEmailAsync(msg);
    }
}