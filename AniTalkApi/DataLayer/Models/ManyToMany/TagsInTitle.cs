namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class TagsInTitle
{
    public int TitleId { get; init; }

    public int TagId { get; init; }

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

    public Tag Tag { get; init; }
    
    public Title Title { get; init; }
    
    #endregion
}