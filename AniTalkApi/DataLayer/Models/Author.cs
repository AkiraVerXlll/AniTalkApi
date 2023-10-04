using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.Models.ManyToMany;

namespace AniTalkApi.DataLayer.Models;

public class Author
{
    [Key]
    public int Id { get; set; }

    public int PersonalInformationId { get; set; }

    #region Dependencies

    public PersonalInformation PersonalInformation { get; set; }

    public List<TitleAuthors> Works { get; set; }

    #endregion
}