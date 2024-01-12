using AniTalkApi.Helpers;
using AniTalkApi.ServiceLayer.CryptoGeneratorServices;
using AniTalkApi.ServiceLayer.OAuthServices;
using AniTalkApi.ServiceLayer.PhotoServices.Implementations;
using AniTalkApi.ServiceLayer.PhotoServices.Interfaces;
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

    public static void AddHttpClientService(this IServiceCollection services)
    {
        services.AddSingleton<HttpClientHelper>();
    }
}