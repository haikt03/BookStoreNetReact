using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Application.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailOptions> _emailOptions;
        private readonly ILogger<EmailService> _logger;
        public EmailService(IOptions<EmailOptions> emailOptions, ILogger<EmailService> logger)
        {
            _emailOptions = emailOptions;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var optionsValue = _emailOptions.Value;
                using var client = new SmtpClient(optionsValue.SmtpHost, int.Parse(optionsValue.SmtpPort));
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(optionsValue.SmtpUser, optionsValue.SmtpPass);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(optionsValue.SmtpFrom, "HUCE Book Conner"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);
                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while sending confimation email");
                return false;
            }
        }
    }
}
