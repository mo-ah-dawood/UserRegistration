using UserRegistration.Core.Entities;

namespace UserRegistration.Application.Interfaces
{
    public interface IEmailSender
    {
        public Task SendVerificationEmail(User user, string code);
    }
}