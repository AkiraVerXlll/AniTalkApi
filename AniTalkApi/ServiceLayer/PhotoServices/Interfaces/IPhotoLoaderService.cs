namespace AniTalkApi.ServiceLayer.PhotoServices.Interfaces;

public interface IPhotoLoaderService
{
    Task<string> UploadAsync(IFormFile file);

    Task<string> DownloadAsync(string publicId);
}
