namespace BookStoreNetReact.Application.Interfaces.Services
{
    public interface ISmsService
    {
        Task<bool> SendSmsAsync(string toPhoneNumber, string message);
    }
}
