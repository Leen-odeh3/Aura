namespace Aura.Application.Abstracts.FileServices;
public interface ICloudinaryService
{
    Task DeleteImageFromCloudinary(string CloudinaryIdentifier);
    Task<Tuple<string, string>> UploadImageToCloudinary(string imagePath);
}
