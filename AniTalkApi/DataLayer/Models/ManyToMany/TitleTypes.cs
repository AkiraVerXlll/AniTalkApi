using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class TitleTypes
{
    public int TitleTypeId { get; init; }

    public int TitleId { get; init; }

    #region Dependencies

    public Title Title { get; init; }

    public TitleType TitleType { get; init; }

    #endregion
}