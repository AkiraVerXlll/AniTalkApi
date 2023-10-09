﻿namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class ImagesInMessage
{
    public int MessageId { get; init; }

    public int ImageId { get; init; }

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

    public Message Message { get; init; }

    public Image Image { get; init; }

    #endregion
}
