using AniTalkApi.DataLayer.Models.ManyToMany;
using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models;

public class Tag
{
    [Key]
    public int Id { get; init; }

    [Required]
    public int Name { get; init; }

    #region Dependencies

    public List<TagsInTitle> TagsInTitle { get; init; }

    #endregion
}
