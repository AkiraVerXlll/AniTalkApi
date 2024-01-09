namespace AniTalkApi.DataLayer.Models;

public class Review
{
    public string UserId { get; init; }

    public int TitleId { get; init; }

    private int _starsCount;
    public int StarsCount
    {
        get => _starsCount;
        set
        {
            if (value is >= 0 and <= 10)
                _starsCount = value;
            else
                throw new ArgumentOutOfRangeException($"Invalid stars count value: \'{value}\'");
        }
    }

    public string? Text { get; set; }

    #region Dependencies

    public User User { get; init; }

    public Title Title { get; init; }

    #endregion
}
