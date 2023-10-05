using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.Models.ManyToMany;

namespace AniTalkApi.DataLayer.Models;

public class Author
{
    [Key]
    public int Id { get; init; }

    public int PersonalInformationId { get; init; }

    #region Dependencies

    public PersonalInformation PersonalInformation { get; init; }

    public List<TitleAuthors> Works { get; init; }

    #endregion
}