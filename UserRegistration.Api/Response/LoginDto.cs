namespace UserRegistration.Api.Response
{

    public class LoginDto
    {
        public string ICNumber { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
    }
}