using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.PhotoServices.PhotoValidatorServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.PhotoServices.PhotoUploaderServices;

/// <summary>
/// This service uploads photos to Cloudinary.
/// </summary>
public class CloudinaryPhotoUploaderService : IPhotoUploaderService
{
    private readonly IPhotoValidatorService _validator;

    private readonly Cloudinary _cloudinary;

    public CloudinaryPhotoUploaderService(
        IPhotoValidatorService validator,
        IOptions<CloudinarySettings> cloudinaryOptions)
    {
        _validator = validator;

        var cloudinarySettings = cloudinaryOptions.Value;

        _cloudinary = new Cloudinary(new Account(
            cloudinarySettings.CloudName,
            cloudinarySettings.ApiKey,
            cloudinarySettings.ApiSecret));
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
    public async Task<string> UploadAsync(IFormFile file, string? path)
    {
        if (!_validator.IsImage(file))
            throw new ArgumentException("Form file isn`t a image");

        await using var stream = file.OpenReadStream();

        path = path is null ?
            $"{path}/" : "";

        var imageUploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = $"{path}{file.FileName.Split('.').First()}{Guid.NewGuid()}"
        };

        var imageUploadResult =
            await _cloudinary.UploadAsync(imageUploadParams);

        if (imageUploadResult is null)
            throw new Exception("Unable to upload an image");

        return imageUploadResult.SecureUrl.ToString();
    }
}