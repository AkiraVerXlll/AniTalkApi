using System.ComponentModel.DataAnnotations;

namespace AniTalkApi.DataLayer.Models;

public class Image
{
    [Key]
    public int Id { get; set; }

    public string Url { get; set; }
}