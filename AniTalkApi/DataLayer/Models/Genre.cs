using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.Models.ManyToMany;

namespace AniTalkApi.DataLayer.Models;

public class Genre
{
    [Key]
    public int Id { get; init; }

    public int Name { get; init; }

    #region Dependencies
    
    public List<GenresInTitle> GenresInTitle { get; init; }

    #endregion
}