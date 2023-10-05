using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models;

public class Image
{
    [Key]
    public int Id { get; init; }

    public string Url { get; init; }
}