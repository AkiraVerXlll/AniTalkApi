namespace AniTalkApi.CRUD;

public static class ApplicationCrudExtension
{
    public static void AddCrudManager(this IServiceCollection services)
    {
        services.AddScoped<CrudManager, CrudManager>();
    }
}