using UserRegistration.Core.Entities;

namespace UserRegistration.Core.Services
{
    public interface IUserService
    {
        Task<User> Login(string icNumber);
        Task<User> RegisterUserAsync(User user);
        Task<User> VerifyPhoneAsync(string icNumber, string verificationCode);
        Task<User> VerifyEmailAsync(string icNumber,string verificationCode);
        Task<User> CreatePinAsync(string userId, string pinCode);
        Task<User> GetUserProfileAsync(string userId);
        Task<User> AcceptPrivacy(string userId);
    }
}