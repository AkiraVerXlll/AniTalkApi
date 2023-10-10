using AniTalkApi.DataLayer;
using AniTalkApi.DataLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AniTalkApi.Controllers;

[Route("/[controller]")]
public class TitleController : Controller
{
    private readonly AppDbContext _dbContext;

    public TitleController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("")]
    public IActionResult GetTitles([FromForm] TitleFromForm formData)
    {
        return null;
    }

    [HttpPost("")]
    public IActionResult AddTitle([FromForm] TitleFromForm formData)
    {
        return null;
    }
}
