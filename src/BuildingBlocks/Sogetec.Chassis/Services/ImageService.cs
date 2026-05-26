namespace Studaris.Chassis.Services;


public class ImageService : FileService
{
    private static bool IsImageFile(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        return AllowedImageExtensions.Contains(extension);
    }

    private static Stream? ResizeImage(Stream imageStream) =>
      // imageStream.Position = 0;
      // var outputStream = new MemoryStream();
      //
      // using (var image = new MagickImage(imageStream))
      // {
      //   image.Resize(200, 200); // Resize to smaller dimensions, adjust as needed
      //   image.Write(outputStream);
      // }
      //
      // outputStream.Position = 0;
      // return outputStream;
      null;

    public static Task<string[]> UploadImageAsync(IFormFile file)
    {
        throw new NotImplementedException();
    }
}
