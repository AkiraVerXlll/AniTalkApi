#pragma warning disable ASP0014
#pragma warning disable CS8604
#pragma warning disable CS8601

using AniTalkApi.DataLayer;
using AniTalkApi.ServiceLayer;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace AniTalkApi;

public class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddPhotoValidatorService();
        builder.Services.AddCloudinaryPhotoLoaderService();
        builder.Services.AddAuth0WebAppAuthentication(options =>
        {
            options.Domain = builder.Configuration["Auth0:Domain"];
            options.ClientId = builder.Configuration["Auth0:ClientId"];
        });


        builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(configure => 
            configure.MapControllers());
        app.Run();
    }
}