using AniTalkApi.ServiceLayer.PhotoServices.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace AniTalkApi.ServiceLayer.PhotoServices.Implementations;

/// <summary>
/// This service uploads photos to Cloudinary.
/// </summary>
public class CloudinaryPhotoUploaderService : IPhotoUploaderService
{
    private readonly IPhotoValidatorService _validator;

    private readonly Cloudinary _cloudinary;

    public CloudinaryPhotoUploaderService(
        IPhotoValidatorService validator, 
        IConfiguration configuration)
    {
        _validator = validator;

        var cloudName = configuration.GetValue<string>("CloudinarySettings:CloudName");
        var apiKey = configuration.GetValue<string>("CloudinarySettings:ApiKey");
        var apiSecret = configuration.GetValue<string>("CloudinarySettings:ApiSecret");
        _cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
    }

    /// <summary>
    /// Uploads a photo to Cloudinary.
    /// </summary>
    /// <param name="file">
    /// File to upload
    /// </param>
    /// <param name="path">
    /// Path to upload on Cloudinary
    /// </param>
    /// <returns>
    /// Url of uploaded photo
    /// </returns>
    /// <exception cref="ArgumentException">
    /// If the file is not an image
    /// </exception>
    public async Task<string> UploadAsync(IFormFile file, string path)
    {
        if (!_validator.IsImage(file)) 
            throw new ArgumentException();

        await using var stream = file.OpenReadStream();

        var imageUploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = $"{path}/{file.FileName.Split('.').First()}{Guid.NewGuid()}"
        };

        var imageUploadResult = 
            await _cloudinary.UploadAsync(imageUploadParams);

        if (imageUploadResult is null)
            throw new ArgumentException();
          
        return imageUploadResult.SecureUrl.ToString();
    }
}