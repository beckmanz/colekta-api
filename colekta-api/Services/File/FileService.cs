using System.Text;
using System.Text.RegularExpressions;
using Supabase;

namespace colekta_api.Services.File;

public class FileService : IFileInterface
{
    private readonly Supabase.Client _supabase;

    public FileService( IConfiguration config)
    {
        var url = config["Supabase:Url"];
        var key = config["Supabase:Key"];
        _supabase = new Supabase.Client(url, key);
    }

    public async Task<string> UploadImageAsync(IFormFile file, string bucketName)
    {
        var rawFileName = Path.GetFileNameWithoutExtension(file.FileName);
        var extension = Path.GetExtension(file.FileName);

        var cleanFileName = Regex.Replace(rawFileName.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9]", "");
        
        var fileName = $"{Guid.NewGuid()}_{cleanFileName}{extension}";
        
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        var fileData = stream.ToArray();

        await _supabase.Storage.From(bucketName).Upload(fileData, fileName);

        return _supabase.Storage.From(bucketName).GetPublicUrl(fileName);
    }
}