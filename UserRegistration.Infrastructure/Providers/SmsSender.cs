using Microsoft.Extensions.Logging;
using UserRegistration.Application.Interfaces;
using UserRegistration.Core.Entities;

namespace UserRegistration.Infrastructure.Providers
{
    public class SmsSender : ISMSSender
    {

        private readonly ILogger<SmsSender> _logger;

        public SmsSender(ILogger<SmsSender> logger)
        {
            _logger = logger;
        }

        public Task SendVerificationSMS(User user, string code)
        {
            _logger.LogInformation("SMS verification code is {0}", code);
            return Task.CompletedTask;
        }
    }
}