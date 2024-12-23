using Aura.Application.Abstracts.FileServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Aura.Application.Services.FileServices;
public class FileService : IFileService
{
    private readonly IHostingEnvironment hostingEnvironment;
    public FileService(IHostingEnvironment hostingEnvironment)
    {
        this.hostingEnvironment = hostingEnvironment;
    }

    public async Task<string> StoreImageToLocalFolder(IFormFile file)
    {

        string uploadsFolder = Path.Combine(hostingEnvironment.ContentRootPath, "Uploads");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);
        return filePath;
    }

    public void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}