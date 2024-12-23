using Microsoft.AspNetCore.Http;

namespace Aura.Application.Abstracts.FileServices;
public interface IFileService
{
    Task<string> StoreImageToLocalFolder(IFormFile file);
    void DeleteFile(string filePath);
}