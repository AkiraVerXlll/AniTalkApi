using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.DbModels.ManyToMany;

public class GenresInTitle
{
    [Required]
    public int TitleId { get; init; }

    [Required]
    public int GenreId { get; init; }

    [Required]
    private int _order;

    public int Order
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

    public Genre? Genre { get; init; }

    public Title? Title { get; init; } 

    #endregion
}
