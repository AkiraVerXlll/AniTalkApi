#pragma warning disable CS8604 
#pragma warning disable ASP0014

using AniTalkApi.DataLayer;
using AniTalkApi.Middleware;
using AniTalkApi.ServiceLayer;
using Serilog;

namespace AniTalkApi;

public class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Seq(builder
                .Configuration["SeqSettings:SeqUrl"]) 
            .CreateLogger();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddPhotoValidatorService();
        builder.Services.AddCloudinaryPhotoLoaderService();
        builder.Services.AddLogging(loggingBuilder => 
            loggingBuilder.AddSerilog());

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
        }

        app.UseExceptionHandler();
        app.UseExceptionLogger();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseEndpoints(configure => 
            configure.MapControllers());
        app.Run();
    }
}