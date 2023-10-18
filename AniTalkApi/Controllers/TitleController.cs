using AniTalkApi.DataLayer;
using AniTalkApi.DataLayer.DTO;
using AniTalkApi.ServiceLayer.PhotoServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AniTalkApi.Controllers;

[Route("/[controller]")]
public class TitleController : Controller
{
    private readonly AppDbContext _dbContext;

    private IPhotoLoaderService _photoLoaderService;

    public TitleController(AppDbContext dbContext, 
        IPhotoLoaderService photoLoaderService)
    {
        _dbContext = dbContext;
        _photoLoaderService = photoLoaderService;
    }

    [HttpGet("")]
    public IActionResult GetTitles([FromForm] TitleFromForm formData)
    {
        var а = formData as TitleFromForm;
        return null;
    }

    [HttpPost("")]
    public IActionResult AddTitle([FromForm] TitleFromForm formData)
    {
        return null;
    }
}
