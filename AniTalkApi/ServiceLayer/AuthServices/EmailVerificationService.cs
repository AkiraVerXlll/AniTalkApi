using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.EmailServices;
using Microsoft.AspNetCore.Identity;
using System.Text;
using AniTalkApi.DataLayer.DbModels;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.AuthServices;

public class EmailVerificationService
{
    private readonly UserManager<User> _userManager;

    private readonly AuthSettings _modalAuthSettings;

    private readonly IEmailSenderService _emailSender;

    private readonly SendGridSettings.EmailTemplateSettings _emailTemplates;

    public EmailVerificationService(
        UserManager<User> userManager,
        IOptions<AuthSettings> modalAuthOptions,
        IOptions<SendGridSettings> sendGridOptions,
        IEmailSenderService emailSender)
    {
        _userManager = userManager;
        _modalAuthSettings = modalAuthOptions.Value;
        _emailTemplates = sendGridOptions.Value.EmailTemplates!;
        _emailSender = emailSender;
    }

    public async Task SendVerificationLink(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user!);
        token = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
        var confirmationLink = $"{_modalAuthSettings.EmailConfirmationLink}?email={email}&token={token}";

        await _emailSender.SendTemplateEmailAsync(
            email,
            _emailTemplates.EmailConfirmation!,
            new { Link = confirmationLink });
    }

    public async Task VerifyEmailAsync(string email, string token)
    {
        token = Encoding.UTF8.GetString(Convert.FromBase64String(token));
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            throw new ArgumentException("User not found");

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
            throw new ArgumentException("Email confirmation failed");
    }
}
