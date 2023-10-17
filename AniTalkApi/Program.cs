using AniTalkApi.DataLayer;
using AniTalkApi.ServiceLayer;
#pragma warning disable ASP0014

namespace AniTalkApi;

public class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddPhotoValidatorService();
        builder.Services.AddCloudinaryPhotoLoaderService();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseEndpoints(configure => 
            configure.MapControllers());
        app.Run();
    }
}