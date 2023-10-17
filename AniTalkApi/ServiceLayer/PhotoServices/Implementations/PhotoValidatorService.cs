using AniTalkApi.ServiceLayer.PhotoServices.Interfaces;

namespace AniTalkApi.ServiceLayer.PhotoServices.Implementations;

public class PhotoValidatorService : IPhotoValidatorService
{
    public bool IsImage(IFormFile formFile)
    {
        if(formFile is null)
            throw new ArgumentNullException("form");

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