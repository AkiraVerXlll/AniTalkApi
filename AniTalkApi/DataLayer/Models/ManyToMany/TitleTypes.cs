using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models.ManyToMany;

public class TitleTypes
{
    public int TitleTypeId { get; init; }

    public int TitleId { get; init; }

    #region Dependencies

    public Title Title { get; set; }

    public TitleType TitleType { get; set; }

    #endregion
}