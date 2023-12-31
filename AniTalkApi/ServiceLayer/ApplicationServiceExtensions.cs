﻿using AniTalkApi.ServiceLayer.PasswordHasherServices;
using AniTalkApi.ServiceLayer.PhotoServices.Implementations;
using AniTalkApi.ServiceLayer.PhotoServices.Interfaces;

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

    public static void AddPasswordHasherSha256Service(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasherService, PasswordHasherSHA256Service>();
    }
}