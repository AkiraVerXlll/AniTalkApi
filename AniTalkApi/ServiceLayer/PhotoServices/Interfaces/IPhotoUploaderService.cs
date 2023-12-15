namespace AniTalkApi.ServiceLayer.PhotoServices.Interfaces;

public interface IPhotoUploaderService
{
    Task<string> UploadAsync(IFormFile file);

}
