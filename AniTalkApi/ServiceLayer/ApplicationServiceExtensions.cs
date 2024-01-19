﻿using AniTalkApi.ServiceLayer.AuthServices.OAuthServices;
using AniTalkApi.ServiceLayer.CryptoGeneratorServices;
using AniTalkApi.ServiceLayer.EmailServices;
using AniTalkApi.ServiceLayer.PhotoServices.PhotoUploaderServices;
using AniTalkApi.ServiceLayer.PhotoServices.PhotoValidatorServices;
using AniTalkApi.ServiceLayer.TokenManagerServices;

namespace AniTalkApi.ServiceLayer;

public static class ApplicationServiceExtensions
{
    public static void AddPhotoValidatorService(this IServiceCollection services)
    {
        services.AddSingleton<IPhotoValidatorService, PhotoValidatorService>();
    }

    public static void AddCloudinaryPhotoLoaderService(this IServiceCollection services)
    {
        services.AddSingleton<IPhotoUploaderService, CloudinaryPhotoUploaderService>();
    }

    public static void AddTokenManagerService(this IServiceCollection services)
    {
        services.AddSingleton<ITokenManagerService, BaseTokenManagerService>();
    }

    public static void AddCryptoGeneratorService(this IServiceCollection services)
    {
        services.AddSingleton<ICryptoGeneratorService, BaseCryptoGeneratorService>();
    }

    public static void AddGoogleOAuthService(this IServiceCollection services)
    {
        services.AddSingleton<GoogleOAuthService, GoogleOAuthService>();
    }

    public static void AddEmailSenderService(this IServiceCollection services)
    {
        services.AddSingleton<IEmailSenderService, SendGridEmailSenderService>();
    }
}