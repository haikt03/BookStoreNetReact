using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Application.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        private readonly IOptions<SmsOptions> _smsOption;
        private readonly ILogger<SmsService> _logger;
        public SmsService(IOptions<SmsOptions> smsOption, ILogger<SmsService> logger)
        {
            _smsOption = smsOption;
            _logger = logger;
        }

        public async Task<bool> SendSmsAsync(string toPhoneNumber, string message)
        {
            try
            {
                var optionsValue = _smsOption.Value;
                var client = new TwilioRestClient(optionsValue.AccountSid, optionsValue.AuthToken);

                var messageResource = await MessageResource.CreateAsync(
                    body: message,
                    from: new PhoneNumber(optionsValue.PhoneNumber),
                    to: new PhoneNumber(toPhoneNumber)
                );
                if (messageResource != null && messageResource.ErrorCode == null)
                    return true;
                return false;
            } catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while sending sms");
                return false;
            }

        }
    }
}
