using AniTalkApi.DataLayer.Models;
using AniTalkApi.Helpers;
using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.PhotoServices.PhotoUploaderServices;
using Microsoft.Extensions.Options;

namespace AniTalkApi.CRUD;

public class ImageCrud : BaseCrud
{
    private readonly IPhotoUploaderService _photoUploader;

    private readonly HttpClientHelper _httpClient;

    private readonly CloudinarySettings _cloudinarySettings;

    public ImageCrud(
        AniTalkDbContext dbContext, 
        IPhotoUploaderService photoUploader,
        HttpClientHelper httpClient,
        IOptions<CloudinarySettings> cloudinaryOptions) : base(dbContext)
    {
        _photoUploader = photoUploader;
        _httpClient = httpClient;
        _cloudinarySettings = cloudinaryOptions.Value;
    }

    public async Task<string> CreateAsync(IFormFile uploadedFile, string folder = null!)
    {
       return await _photoUploader.UploadAsync(uploadedFile, folder);
    }

    public async Task<string> CreateAsync(string externalUrl, string folder = null!)
    {
        var avatar = await _httpClient
            .GetImageAsFormFileAsync(externalUrl);

        return await CreateAsync(avatar, folder);
    }

    public async Task<string> CreateAvatarAsync(string externalUrl)
    {
        return await CreateAsync(externalUrl, _cloudinarySettings.Paths!.Avatar!);
    }
}