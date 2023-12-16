namespace AniTalkApi.ServiceLayer.PhotoServices.Interfaces;

/// <summary>
/// Services that implemented that interface upload photos to the cloud.
/// </summary>
public interface IPhotoUploaderService
{
    Task<string> UploadAsync(IFormFile file, string path);

}
