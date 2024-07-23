namespace UserRegistration.Core.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CustomerName { get; set; } = string.Empty;
        public string ICNumber { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PinCode { get; set; } = string.Empty;
        public bool IsPhoneVerified { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool ISPrivacyAccepted { get; set; }
        public ICollection<Verification> Verifications { get; set; } = new HashSet<Verification>();
    }
}