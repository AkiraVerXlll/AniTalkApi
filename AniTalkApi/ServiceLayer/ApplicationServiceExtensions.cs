using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;

namespace AniTalkApi.ServiceLayer;

public static class ApplicationServiceExtensions
{
    public static void AddCloudinary(this IServiceCollection services, IConfiguration configuration)
    {
        var cloudName = configuration.GetValue<string>("CloudinarySettings:CloudName");
        var apiKey = configuration.GetValue<string>("CloudinarySettings:ApiKey");
        var apiSecret = configuration.GetValue<string>("CloudinarySettings:ApiSecret");
        var cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
        services.AddSingleton(cloudinary);
    }
}