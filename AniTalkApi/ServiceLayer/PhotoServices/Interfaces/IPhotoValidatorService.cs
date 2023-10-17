namespace AniTalkApi.ServiceLayer.PhotoServices.Interfaces;

public interface IPhotoValidatorService
{
    bool IsImage(IFormFile formFile);
}