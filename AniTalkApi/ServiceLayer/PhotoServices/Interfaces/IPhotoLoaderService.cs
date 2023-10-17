namespace AniTalkApi.ServiceLayer.PhotoServices.Interfaces;

public interface IPhotoLoaderService
{
    Task<string> Upload(IFormFile file);

    Task<string> Download(string publicId);
}
