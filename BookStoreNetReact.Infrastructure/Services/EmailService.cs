using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Application.Options;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailOptions> _emailOptions;
        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var optionsValue = _emailOptions.Value;
            using var client = new SmtpClient(optionsValue.SmtpHost, int.Parse(optionsValue.SmtpPort));
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(optionsValue.SmtpUser, optionsValue.SmtpPass);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(optionsValue.SmtpFrom),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);
            await client.SendMailAsync(mailMessage);
        }
    }
}
