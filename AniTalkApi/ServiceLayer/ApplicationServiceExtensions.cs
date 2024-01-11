using AniTalkApi.ServiceLayer.CryptoGeneratorService;
using AniTalkApi.ServiceLayer.PhotoServices.Implementations;
using AniTalkApi.ServiceLayer.PhotoServices.Interfaces;
using AniTalkApi.ServiceLayer.TokenManagerService;

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
        services.AddSingleton<ITokenManagerService, TokenManagerService.TokenManagerService>();
    }

    public static void AddCryptoGeneratorService(this IServiceCollection services)
    {
        services.AddSingleton<ICryptoGeneratorService, BaseCryptoGeneratorService>();
    }
}