using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.DbModels.ManyToMany;

public class ImagesInMessage
{
    [Required]
    public int MessageId { get; init; }

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

    public Message? Message { get; init; }

    public Image? Image { get; init; } 

    #endregion
}
