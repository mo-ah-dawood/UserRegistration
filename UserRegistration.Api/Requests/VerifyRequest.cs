using System.ComponentModel.DataAnnotations;

namespace UserRegistration.Api.Requests
{
    public class VerifyRequest
    {
        [Required(ErrorMessage = SharedResources.Required)]
        public string ICNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = SharedResources.Required)]
        [StringLength(4, MinimumLength = 4, ErrorMessage = SharedResources.StringLength)]
        [Range(1000, 9999, ErrorMessage = SharedResources.Range)]
        public string VerificationCode { get; set; } = string.Empty;
    }
}