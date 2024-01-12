namespace AniTalkApi.CRUD;

public static class ApplicationCrudExtension
{
    public static void AddCrud(this IServiceCollection services)
    {
        services.AddScoped<CrudManager, CrudManager>();
        services.AddSingleton<ImageCrud, ImageCrud>();
    }
}