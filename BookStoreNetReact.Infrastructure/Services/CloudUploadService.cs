using BookStoreNetReact.Application.Dtos.Image;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Application.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class CloudUploadService : ICloudUploadService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IOptions<CloudOptions> _cloudOption;
        public CloudUploadService(IOptions<CloudOptions> cloudOption)
        {
            _cloudOption = cloudOption;
            var cloudinaryAccount = new Account(
                cloud: cloudOption.Value.CloudName,
                apiKey: cloudOption.Value.ApiKey,
                apiSecret: cloudOption.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account: cloudinaryAccount);
        }
        public async Task<ImageDto> UploadImageAsync(UploadImageDto uploadImageDto, string folder)
        {
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(uploadImageDto.FileName, uploadImageDto.FileStream),
                Folder = $"BookStoreNetReact/{folder}"
            };
            try
            {
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult == null)
                    throw new NullReferenceException(nameof(uploadResult));
                return new ImageDto
                {
                    PublicId = uploadResult.PublicId,
                    ImageUrl = uploadResult.SecureUrl.ToString()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Upload image failed");
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            try
            {
                var deleteResult = await _cloudinary.DestroyAsync(deleteParams);
                if (deleteResult == null)
                    throw new NullReferenceException(nameof(deleteResult));
                return deleteResult.Result == "ok";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete image failed");
                throw new Exception(ex.ToString());
            }
        }
    }
}
