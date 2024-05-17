using System.Text;
using AniTalkApi.DataLayer.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AniTalkApi;

public static class ConfigureApplicationExtension
{
    public static void MapSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<SendGridSettings>
            (options => builder.Configuration.GetSection("SendGridSettings").Bind(options));
        builder.Services.Configure<JwtSettings>
            (options => builder.Configuration.GetSection("JWT").Bind(options));
        builder.Services.Configure<CloudinarySettings>
            (options => builder.Configuration.GetSection("CloudinarySettings").Bind(options));
        builder.Services.Configure<GoogleOAuthSettings>
            (options => builder.Configuration.GetSection("GoogleOAuth2.0Settings").Bind(options));
        builder.Services.Configure<AvatarSettings>
            (options => builder.Configuration.GetSection("AvatarSettings").Bind(options));
        builder.Services.Configure<AuthSettings>(
            options => builder.Configuration.GetSection("Auth").Bind(options));
        builder.Services.Configure<CookieSettings>(
                       options => builder.Configuration.GetSection("CookieSettings").Bind(options));
    }

    public static void ConfigureCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }

    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
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
    }

    public static void ConfigureCookiePolicy(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            options.MinimumSameSitePolicy = SameSiteMode.None;
            options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
            options.Secure = CookieSecurePolicy.Always;
        });
    }
}
