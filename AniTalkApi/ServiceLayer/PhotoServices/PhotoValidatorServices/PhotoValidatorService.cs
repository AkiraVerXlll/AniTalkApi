using SixLabors.ImageSharp;

namespace AniTalkApi.ServiceLayer.PhotoServices.PhotoValidatorServices;

/// <summary>
/// This service validates image.
/// </summary>
public class PhotoValidatorService : IPhotoValidatorService
{
    /// <summary>
    /// Checks if the file is an image.
    /// </summary>
    /// <param name="formFile">
    /// File to check
    /// </param>
    /// <returns>
    /// Boolean value indicating if the given file is an image
    /// </returns>
    /// <exception cref="ArgumentNullException"></exception>
    public bool IsImage(IFormFile formFile)
    {
        if (formFile is null)
            throw new ArgumentNullException(nameof(formFile));

        using var stream = formFile.OpenReadStream();
        try
        {
            Image.Identify(stream);
        }
        catch
        {
            return false;
        }
        return true;
    }
}