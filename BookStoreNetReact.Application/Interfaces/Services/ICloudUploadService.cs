using BookStoreNetReact.Application.Dtos.Image;

namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface ICloudUploadService
    {
        Task<ImageDto> UploadImageAsync(UploadImageDto uploadImageDto);
        Task<bool> DeleteImageAsync(string publicId);
    }
}
