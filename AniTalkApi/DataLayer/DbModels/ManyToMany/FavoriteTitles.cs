using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.DbModels.ManyToMany;

public class FavoriteTitles
{
    [Required]
    public string? UserId { get; init; }

    [Required]
    public int TitleId { get; init; }

    private int? _order;

    public int? Order
    {
        get => _order;
        set
        {
            if (value > 0)
                _order = value;
            else
                throw new ArgumentOutOfRangeException($"Invalid order value: \'{value}\'");
        }
    }

    #region Dependencies

    public Title? Title { get; init; }

    public User? User { get; init; } 

    #endregion
}
