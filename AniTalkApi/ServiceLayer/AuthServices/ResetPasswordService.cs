using System.Text;
using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.EmailServices;
using Microsoft.AspNetCore.Identity;

namespace AniTalkApi.ServiceLayer.AuthServices;

public class ResetPasswordService
{
    private readonly UserManager<User> _userManager;

    private readonly IEmailSenderService _emailSender;

    private readonly SendGridSettings.EmailTemplateSettings _emailTemplates;

    private readonly AuthSettings _authSettings;

    public ResetPasswordService(
        UserManager<User> userManager,
        IEmailSenderService emailSender, 
        SendGridSettings sendGridSettings, 
        AuthSettings authSettings)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _authSettings = authSettings;
        _emailTemplates = sendGridSettings.EmailTemplates!;
    }

    public async Task ResetPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            throw new ArgumentException("User with this email not found");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetPasswordLink = $"{_authSettings.ResetPasswordLink}?token={token}&email={email}";
        resetPasswordLink = Convert.ToBase64String(Encoding.UTF8.GetBytes(resetPasswordLink));

        await _emailSender.SendTemplateEmailAsync(
            email, 
            _emailTemplates.ResetPassword!, 
            resetPasswordLink);
    }

    public async Task ChangeForgottenPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            throw new ArgumentException("User with this email not found");

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        if (!result.Succeeded)
            throw new ArgumentException("Bad token");
    }

    public async Task ChangePasswordAsync(string email, string oldPassword, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            throw new ArgumentException("User with this email not found");

        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        if (!result.Succeeded)
            throw new ArgumentException("Bad password");
    }
}