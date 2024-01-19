using System.ComponentModel.DataAnnotations;
using AniTalkApi.DataLayer.DbModels.ManyToMany;

namespace AniTalkApi.DataLayer.DbModels;

public class Author
{
    [Key]
    public int Id { get; init; }

    [Required]
    public int PersonalInformationId { get; init; }

    #region Dependencies

    public PersonalInformation? PersonalInformation { get; init; }

    public List<TitleAuthors>? Works { get; init; }

    #endregion
}