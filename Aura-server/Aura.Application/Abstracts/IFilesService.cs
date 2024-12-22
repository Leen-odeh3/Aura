using Aura.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Aura.Application.Abstracts;
public interface IFilesService
{
    Task<string> UploadImageAsync(IFormFile file, ImageFileType imageFileType);
}