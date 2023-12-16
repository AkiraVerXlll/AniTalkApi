namespace AniTalkApi.ServiceLayer.PhotoServices.Interfaces;

/// <summary>
/// Services that implemented validate image.
/// </summary>
public interface IPhotoValidatorService
{
    bool IsImage(IFormFile formFile);
}