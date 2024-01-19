namespace AniTalkApi.Helpers;

public static class ApplicationHelpersExtension
{
    public static void AddHttpClientHelper(this IServiceCollection services)
    {
        services.AddSingleton<HttpClientHelper>();
    }
}