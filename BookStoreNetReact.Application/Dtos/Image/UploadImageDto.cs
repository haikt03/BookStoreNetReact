namespace BookStoreNetReact.Application.Dtos.Image
{
    public class UploadImageDto
    {
        public required Stream FileStream { get; set; }
        public required string FileName { get; set; }
    }
}
