using AniTalkApi.DataLayer.Models.ManyToMany;
using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models;

public class AuthorType
{
    [Key]
    public int Id { get; init; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? NormalizeName { get; set; }

    #region Dependencies

    public List<TitleAuthors>?  TitleAuthors { get; init; }

    #endregion
}