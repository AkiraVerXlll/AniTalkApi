using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.AuthServices.SignInServices;
using AniTalkApi.ServiceLayer.EmailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.AuthServices;

public class TwoFactorVerificationService
{
    private readonly UserManager<User> _userManager;

    private readonly IEmailSenderService _emailSender;

    private readonly SendGridSettings _sendGridSettings;

    private readonly OAuthSignInService _oAuthSignIn;

    public TwoFactorVerificationService(
        UserManager<User> userManager,
        IEmailSenderService emailSender,
        IOptions<SendGridSettings> sendGridOptions,
        OAuthSignInService oAuthSignIn)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _sendGridSettings = sendGridOptions.Value;
        _oAuthSignIn = oAuthSignIn;
    }

    public async Task SendTwoFactorCodeAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            throw new ArgumentException("User not found");

        var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
        await _emailSender.SendTemplateEmailAsync(
            user.Email!,
            _sendGridSettings.EmailTemplates!.TwoFactorVerification!,
            new { Code = token });
    }


    public async Task<TokenModel> TwoFactorVerificationValidateAsync(string email, string code)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var result = await _userManager.VerifyTwoFactorTokenAsync(user!, "Email", code);
        if (!result)
            throw new ArgumentException("Invalid two factor code");
        var claims = new Dictionary<string, string>
        {
            { "email", email }
        };
        return await _oAuthSignIn.SignInAsync(claims);
    }

    public async Task<bool> IsTwoFactorEnabledAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            throw new ArgumentException("User not found");

        return await _userManager.GetTwoFactorEnabledAsync(user);
    }
}