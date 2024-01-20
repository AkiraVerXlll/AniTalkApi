namespace AniTalkApi.CRUD;

public static class ApplicationCrudExtension
{
    public static void AddCrud(this IServiceCollection services)
    {
        services.AddScoped<ImageCrud, ImageCrud>();
        services.AddScoped<UserCrud, UserCrud>();
    }
}