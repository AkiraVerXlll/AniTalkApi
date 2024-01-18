namespace AniTalkApi.DataLayer.Settings;

public class CloudinarySettings
{
    public string? CloudName { get; init; }

    public string? ApiKey { get; init; }

    public string? ApiSecret { get; init; }

    public PathsSettings? Paths { get; init; }

    public class PathsSettings
    {
        public string? Avatar { get; init; }

        public string? Post { get; init; }

        public string? Background { get; init; }
    }
}