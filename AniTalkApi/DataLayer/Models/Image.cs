using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models;

public class Image
{
    [Key]
    public int Id { get; init; }

    [Required]
    public string Url { get; init; }

}