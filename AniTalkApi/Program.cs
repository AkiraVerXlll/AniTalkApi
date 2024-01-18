#pragma warning disable ASP0014

using System.Text;
using AniTalkApi.CRUD;
using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.Models;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.Helpers;
using AniTalkApi.ServiceLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AniTalkApi;

public class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession();
        builder.Services.AddHttpClient();

        builder.Services.AddDbContext<AniTalkDbContext>();
        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AniTalkDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddHttpClientHelper();
        builder.Services.AddAuthHelper();
        builder.Services.AddEmailSenderService();
        builder.Services.AddCrud();
        builder.Services.AddPhotoValidatorService();
        builder.Services.AddCloudinaryPhotoLoaderService();
        builder.Services.AddTokenManagerService();
        builder.Services.AddCryptoGeneratorService();
        builder.Services.AddGoogleOAuthService();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
                };
            }
        );
        builder.Services.AddAuthorization();

        builder.Services.Configure<EmailSettings>
            (options => builder.Configuration.GetSection("EmailSettings").Bind(options));
        builder.Services.Configure<JwtSettings>
            (options => builder.Configuration.GetSection("JWT").Bind(options));
        builder.Services.Configure<CloudinarySettings>
            (options => builder.Configuration.GetSection("CloudinarySettings").Bind(options));
        builder.Services.Configure<ConnectionStringSettings>
            (options => builder.Configuration.GetSection("ConnectionStrings").Bind(options));
        builder.Services.Configure<GoogleOAuthSettings>
            (options => builder.Configuration.GetSection("GoogleOAuth2.0Settings").Bind(options));
        builder.Services.Configure<AvatarSettings>
            (options => builder.Configuration.GetSection("AvatarSettings").Bind(options));

        var app = builder.Build();

        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSession();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(configure => 
            configure.MapControllers());
        app.Run();
    }
}