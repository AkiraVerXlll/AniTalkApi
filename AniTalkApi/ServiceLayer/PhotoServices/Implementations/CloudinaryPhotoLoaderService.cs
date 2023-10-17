using System.Net;
using AniTalkApi.ServiceLayer.PhotoServices.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace AniTalkApi.ServiceLayer.PhotoServices.Implementations;

public class CloudinaryPhotoLoaderService : IPhotoLoaderService
{
    private readonly IPhotoValidatorService _validator;

    private readonly Cloudinary _cloudinary;

    public CloudinaryPhotoLoaderService(
        IPhotoValidatorService validator, 
        IConfiguration configuration)
    {
        _validator = validator;

        var cloudName = configuration.GetValue<string>("CloudinarySettings:CloudName");
        var apiKey = configuration.GetValue<string>("CloudinarySettings:ApiKey");
        var apiSecret = configuration.GetValue<string>("CloudinarySettings:ApiSecret");
        _cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
    }

    public async Task<string> Upload(IFormFile file)
    {
        if (!_validator.IsImage(file)) 
            throw new ArgumentException();

        await using var stream = file.OpenReadStream();

        var imageUploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = $"test/{file.FileName.Split('.').First()}{Guid.NewGuid()}"
        };

        var imageUploadResult = 
            await _cloudinary.UploadAsync(imageUploadParams);

        if (imageUploadResult != null)
        {
            return imageUploadResult.PublicId;
        }

        throw new ArgumentException();
    }

    public Task<string> Download(string publicId)
    {
        var url = _cloudinary
            .GetResource(publicId)
            .SecureUrl;

        if (url is null)
            throw new ArgumentException();

        return new Task<string>(() => url);
    }
}