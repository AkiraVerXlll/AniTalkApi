using SixLabors.ImageSharp;

namespace AniTalkApi.ServiceLayer.PhotoServices.PhotoValidatorServices;


public class PhotoValidatorService : IPhotoValidatorService
{

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