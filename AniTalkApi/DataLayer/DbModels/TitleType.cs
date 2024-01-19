using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.DbModels.ManyToMany;

namespace AniTalkApi.DataLayer.DbModels;

public class TitleType
{
    [Key]
    public int Id { get; init; }

    [Required]
    public string? Name { get; set; }

    [Required] 
    public string? NormalizeName { get; set; }

    #region Dependencies

    public List<TitleTypes>? TitleTypes { get; init; }

    #endregion
}
