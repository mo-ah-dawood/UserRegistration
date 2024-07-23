using UserRegistration.Core.Enum;

namespace UserRegistration.Core.Entities
{
    public class Verification
    {
        public VerificationType Type { get; set; } = VerificationType.PhoneNumber;
        public string UserId { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime Expiry { get; set; }
        public User? User { get; set; }
    }
}