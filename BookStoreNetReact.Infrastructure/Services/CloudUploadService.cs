using BookStoreNetReact.Application.Dtos.Image;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Application.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class CloudUploadService : ICloudUploadService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IOptions<CloudOptions> _cloudOption;
        private readonly ILogger<CloudUploadService> _logger;
        public CloudUploadService(IOptions<CloudOptions> cloudOption, ILogger<CloudUploadService> logger)
        {
            _cloudOption = cloudOption;
            var cloudinaryAccount = new Account(
                cloud: cloudOption.Value.CloudName,
                apiKey: cloudOption.Value.ApiKey,
                apiSecret: cloudOption.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account: cloudinaryAccount);
            _logger = logger;
        }
        public async Task<ImageDto?> UploadImageAsync(IFormFile file, string folder)
        {
            try
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill"),
                    Folder = $"BookStoreNetReact/{folder}"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult == null)
                    throw new NullReferenceException("Upload result not found");

                return new ImageDto
                {
                    PublicId = uploadResult.PublicId,
                    ImageUrl = uploadResult.SecureUrl.ToString()
                };
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Upload result not found");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while uploading image");
                return null;
            }
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            try
            {
                var deleteParams = new DeletionParams(publicId);
                var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

                if (deleteResult == null)
                    throw new NullReferenceException("Delete result not found");
                return deleteResult.Result == "ok";
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning(ex, "Delete result not found");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while deleting image");
                return false;
            }
        }
    }
}
