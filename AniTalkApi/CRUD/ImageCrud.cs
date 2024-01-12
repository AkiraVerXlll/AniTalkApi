using AniTalkApi.DataLayer;
using AniTalkApi.DataLayer.Models;
using AniTalkApi.ServiceLayer.PhotoServices.Interfaces;
using AniTalkApi.Helpers;

namespace AniTalkApi.CRUD;

public class ImageCrud : BaseCrud
{
    private readonly IPhotoUploaderService _photoUploader;

    private readonly HttpClientHelper _httpClient;

    public ImageCrud(
        AppDbContext dbContext, 
        IPhotoUploaderService photoUploader,
        HttpClientHelper httpClient) : base(dbContext)
    {
        _photoUploader = photoUploader;
        _httpClient = httpClient;
    }

    public async Task<Image> CreateAsync(IFormFile uploadedFile, string folder = null!)
    {
        var url = await _photoUploader.UploadAsync(uploadedFile, folder);

        var image = new Image()
        {
            Url = url,
        };

        await DbContext.Images!.AddAsync(image);
        await DbContext.SaveChangesAsync();

        return image;
    }

    public async Task<Image> CreateAsync(string externalUrl, string folder = null!)
    {
        var avatar = await _httpClient
            .GetImageAsFormFileAsync(externalUrl);

        return await CreateAsync(avatar, folder);
    }
}