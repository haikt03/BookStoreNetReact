using BookStoreNetReact.Application.Dtos.Image;
using Microsoft.AspNetCore.Http;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface ICloudUploadService
    {
        Task<ImageDto?> UploadImageAsync(IFormFile file, string folder);
        Task<bool> DeleteImageAsync(string publicId);
    }
}
