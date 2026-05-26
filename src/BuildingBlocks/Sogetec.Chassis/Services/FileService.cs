namespace Studaris.Chassis.Services;

public record FileServiceResult(bool IsSuccess, FileServiceOperationCode OperationCode, string? FilePath = null);

public class FileService
{
    protected static readonly List<string> AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp"];
    protected static readonly List<string> AllowedFileExtensions = [".doc", ".docx", ".csv", "", ".pdf", ".txt"];
    protected static readonly List<string> AllowedExtensions = [.. AllowedFileExtensions, .. AllowedImageExtensions];

    protected static Task<string> UploadFileAsync(IFormFile file)
    {
        throw new NotImplementedException();
    }

    private static string GenerateFilePath(IFormFile file)
    {
        var fileName = file.FileName;
        var name = Path.GetFileNameWithoutExtension(fileName);
        var prefix = name.Length < 64 ? name : name[..64];
        var ext = Path.GetExtension(fileName).ToLower();

        return $"{prefix}-{Guid.CreateVersion7().ToString().Replace("-", "")}{ext}";
    }
}

public enum FileServiceOperationCode
{
    Uploaded,
    Downloaded,
    FileNotFound,
    ExtensionNotAllowed
}
