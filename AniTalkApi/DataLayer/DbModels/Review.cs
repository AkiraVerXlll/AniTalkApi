using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.DbModels;

public class Review
{
    [Required]
    public string? UserId { get; init; }

    [Required]
    public int TitleId { get; init; }

    [Required]
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

    public User? User { get; init; }

    public Title? Title { get; init; }

    #endregion
}
