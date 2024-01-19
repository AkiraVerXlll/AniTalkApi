namespace AniTalkApi.ServiceLayer.PhotoServices.PhotoValidatorServices;

/// <summary>
/// Services that implemented validate image.
/// </summary>
public interface IPhotoValidatorService
{
    bool IsImage(IFormFile formFile);
}