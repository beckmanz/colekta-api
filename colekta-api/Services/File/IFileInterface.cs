namespace colekta_api.Services.File;

public interface IFileInterface
{
    Task<string> UploadImageAsync(IFormFile file, string bucketName);
}