using AniTalkApi.DataLayer.Models.ManyToMany;
using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models;

public class Tag
{
    [Key]
    public int Id { get; set; }

    public int Name { get; set; }

    #region Dependencies

    public List<TagsInTitle> TagsInTitle { get; set; }

    #endregion
}
