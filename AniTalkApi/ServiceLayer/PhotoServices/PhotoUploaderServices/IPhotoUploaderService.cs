namespace AniTalkApi.ServiceLayer.PhotoServices.PhotoUploaderServices;

public interface IPhotoUploaderService
{
    Task<string> UploadAsync(IFormFile file, string path);

}
