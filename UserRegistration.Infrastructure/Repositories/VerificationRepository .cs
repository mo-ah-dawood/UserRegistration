using UserRegistration.Core.Entities;
using UserRegistration.Core.Enum;
using UserRegistration.Core.Repositories;
using UserRegistration.Infrastructure.Data;

namespace UserRegistration.Infrastructure.Repositories
{
    public class VerificationRepository : Repository<Verification>, IVerificationRepository
    {
        public VerificationCodeConfig config;
        public VerificationRepository(AppDbContext context, VerificationCodeConfig config) : base(context)
        {
            this.config = config;
        }

        public async Task<Verification> CreateNewVerification(string userId, VerificationType type)
        {
            Random _rdm = new Random();

            var old = await FirstOrDefaultAsync((v) => v.UserId == userId && v.Type == type);
            if (old != null)
                Delete(old);
            var min = Math.Pow(10, config.Count - 1);
            var max = Math.Pow(10, config.Count) - 1;
            var newObj = new Verification()
            {
                Type = type,
                Expiry = DateTime.UtcNow.AddMinutes(config.ExpirationMinutes),
                Code = _rdm.Next((int)min, (int)max).ToString(),
                UserId = userId,
            };
            await AddAsync(newObj);
            return newObj;
        }

        public async Task<Verification?> GetVerificationAsync(string userId, string verificationCode, VerificationType type)
        {
            return await FirstOrDefaultAsync((v) => v.UserId == userId && v.Code == verificationCode && v.Type == type && v.Expiry >= DateTime.UtcNow);
        }
    }

    public class VerificationCodeConfig
    {
        public int ExpirationMinutes { get; set; }
        public int Count { get; set; }
    }
}