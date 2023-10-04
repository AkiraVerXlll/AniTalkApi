using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AniTalkApi.DataLayer.Models.Enums;
using AniTalkApi.DataLayer.Models.ManyToMany;

namespace AniTalkApi.DataLayer.Models;

public class Title
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string CoverId { get; set; }

    [Column(TypeName = "date")]
    public DateTime ReleaseDate { get; set; }
    
    public TitleStatus TitleStatus { get; set; }

    #region Dependencies

    public Image Cover { get; set; }

    public List<TitleTypes> TitleTypes { get; set; }

    public List<GenresInTitle> Genres { get; set; }

    public List<TagsInTitle> Tags { get; set; }

    public List<TitleAuthors> TitleAuthors { get; set; }

    public List<FavoriteTitles> FavoriteTitlesOf { get; set; }

    public List<Review> Reviews { get; set; }

    #endregion
}