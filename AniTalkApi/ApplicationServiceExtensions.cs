﻿using AniTalkApi.ServiceLayer.AuthServices;
using AniTalkApi.ServiceLayer.AuthServices.OAuthServices;
using AniTalkApi.ServiceLayer.AuthServices.SignInServices;
using AniTalkApi.ServiceLayer.AuthServices.SignUpServices;
using AniTalkApi.ServiceLayer.CryptoGeneratorServices;
using AniTalkApi.ServiceLayer.EmailServices;
using AniTalkApi.ServiceLayer.PhotoServices.PhotoUploaderServices;
using AniTalkApi.ServiceLayer.PhotoServices.PhotoValidatorServices;
namespace AniTalkApi;

public static class ApplicationServiceExtensions
{
    public static void AddPhotoServices(this IServiceCollection services)
    {
        services.AddSingleton<IPhotoValidatorService, PhotoValidatorService>();
        services.AddSingleton<IPhotoUploaderService, CloudinaryPhotoUploaderService>();
    }

    public static void AddAuthServices(this IServiceCollection services)
    {
        services.AddScoped<OAuthSignUpService, OAuthSignUpService>();
        services.AddScoped<OAuthSignInService, OAuthSignInService>();
        services.AddScoped<TwoFactorVerificationService, TwoFactorVerificationService>();
        services.AddScoped<EmailVerificationService, EmailVerificationService>();
        services.AddScoped<ManualSignInService, ManualSignInService>();
        services.AddScoped<ManualSignUpService, ManualSignUpService>();
        services.AddScoped<GoogleOAuthService, GoogleOAuthService>();
        services.AddScoped<TokenManagerService, TokenManagerService>();
        services.AddScoped<ResetPasswordService, ResetPasswordService>();
    }

    public static void AddCryptoGeneratorService(this IServiceCollection services)
    {
        services.AddScoped<ICryptoGeneratorService, BaseCryptoGeneratorService>();
    }

    public static void AddEmailSenderService(this IServiceCollection services)
    {
        services.AddScoped<IEmailSenderService, SendGridEmailSenderService>();
    }
}