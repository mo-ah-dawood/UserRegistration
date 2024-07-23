using Microsoft.Extensions.Logging;
using UserRegistration.Application.Interfaces;
using UserRegistration.Core.Entities;

namespace UserRegistration.Infrastructure.Providers
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }


        public Task SendVerificationEmail(User user, string code)
        {
            _logger.LogInformation("Email verification code is {0}", code);
            return Task.CompletedTask;
        }
    }
}