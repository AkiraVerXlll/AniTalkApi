#pragma warning disable ASP0014

using AniTalkApi.CRUD;
using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.OpenApi.Models;

namespace AniTalkApi;

public class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSpaStaticFiles(options => 
            options.RootPath = builder.Configuration["FrontPath"]! + "/dist");
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddHttpClient();
        builder.Services.AddDbContext<AniTalkDbContext>();
        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AniTalkDbContext>()
            .AddDefaultTokenProviders();
        builder.Services.AddSession();


        builder.Services.AddHttpClientHelper();
        builder.Services.AddEmailSenderService();
        builder.Services.AddCrud();
        builder.Services.AddCryptoGeneratorService();
        builder.Services.AddAuthServices();
        builder.Services.AddPhotoServices();

        builder.MapSettings();
        builder.ConfigureCors();

        builder.Services.AddAuthorization();
        builder.ConfigureAuthentication();
        builder.ConfigureCookiePolicy();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("MyPolicy");
        app.UseSession();
        app.UseCookiePolicy();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(configure =>
            configure.MapControllers());
        app.UseSpaStaticFiles();
        app.UseSpa(spa =>
        {
            spa.Options.SourcePath = builder.Configuration["FrontPath"]!;
            spa.Options.DevServerPort = int.Parse(builder.Configuration["FrontPort"]!);

            if (app.Environment.IsDevelopment())
            {
                spa.UseAngularCliServer(npmScript: "start"); 
                spa.UseProxyToSpaDevelopmentServer(builder.Configuration["FrontUrl"]!);
            }
        });
        app.Run();
    }
}