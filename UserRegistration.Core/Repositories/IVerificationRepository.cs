using UserRegistration.Core.Entities;
using UserRegistration.Core.Enum;

namespace UserRegistration.Core.Repositories
{
    public interface IVerificationRepository : IRepository<Verification>
    {
        Task<Verification?> GetVerificationAsync(string userId, string verificationCode, VerificationType type);
        Task<Verification> CreateNewVerification(string userId, VerificationType type);
    }
}