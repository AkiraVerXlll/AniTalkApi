using AniTalkApi.DataLayer.Models.ManyToMany;
using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models;

public class TitleTypes
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public int TitleId { get; set; }

    #region Dependencies

    public Title Title { get; set; }

    #endregion
}