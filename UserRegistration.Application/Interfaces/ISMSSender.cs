using UserRegistration.Core.Entities;

namespace UserRegistration.Application.Interfaces
{
    public interface ISMSSender
    {
        public Task SendVerificationSMS(User user, string code);
    }
}