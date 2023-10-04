using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.Models.ManyToMany;

namespace AniTalkApi.DataLayer.Models;

public class Genre
{
    [Key]
    public int Id { get; set; }

    public int Name { get; set; }

    #region Dependencies
    
    public List<GenresInTitle> GenresInTitle { get; set; }

    #endregion
}