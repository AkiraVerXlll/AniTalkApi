namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class GenresInTitle
{
    public int TitleId { get; init; }

    public int GenreId { get; init; }

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

    public Genre Genre { get; set; }

    public Title Title { get; set; }

    #endregion
}
