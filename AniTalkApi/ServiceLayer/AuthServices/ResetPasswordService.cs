using System.Text;
using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.EmailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.AuthServices;

public class ResetPasswordService
{
    private readonly UserManager<User> _userManager;

    private readonly IEmailSenderService _emailSender;

    private readonly SendGridSettings.EmailTemplateSettings _emailTemplates;

    private readonly AuthSettings _authOptions;

    public ResetPasswordService(
        UserManager<User> userManager,
        IEmailSenderService emailSender, 
        IOptions<SendGridSettings> sendGridOptions,
        IOptions<AuthSettings> authOptions)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _authOptions = authOptions.Value;
        _emailTemplates = sendGridOptions.Value.EmailTemplates!;
    }

    public async Task ResetPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            throw new ArgumentException("User with this email not found");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        token = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
        var resetPasswordLink = $"{_authOptions.PasswordResetLink}?token={token}&email={email}";

        await _emailSender.SendTemplateEmailAsync(
            email, 
            _emailTemplates.PasswordReset!,
            new{ Link = resetPasswordLink });
    }

    public async Task ChangeForgottenPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            throw new ArgumentException("User with this email not found");

        token = Encoding.UTF8.GetString(Convert.FromBase64String(token));
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