using AniTalkApi.ServiceLayer.PhotoServices.Implementations;
using AniTalkApi.ServiceLayer.PhotoServices.Interfaces;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;

namespace AniTalkApi.ServiceLayer;

public static class ApplicationServiceExtensions
{
    public static void AddPhotoValidatorService(this IServiceCollection services)
    {
        services.AddSingleton<IPhotoValidatorService, PhotoValidatorService>();
    }

    public static void AddCloudinaryPhotoLoaderService(this IServiceCollection services)
    {
        services.AddSingleton<IPhotoLoaderService, CloudinaryPhotoLoaderService>();
    }
}