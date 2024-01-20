namespace AniTalkApi.ServiceLayer.PhotoServices.PhotoValidatorServices;


public interface IPhotoValidatorService
{
    bool IsImage(IFormFile formFile);
}