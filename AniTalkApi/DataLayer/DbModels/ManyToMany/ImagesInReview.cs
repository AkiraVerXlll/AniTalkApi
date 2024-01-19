using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.DbModels.ManyToMany;

public class ImagesInReview
{
    [Required]
    public int ReviewId { get; init; }

    [Required]
    public int ImageId { get; init; }

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

    public Review? Review { get; init; } 

    public Image? Image { get; init; } 

    #endregion
}
