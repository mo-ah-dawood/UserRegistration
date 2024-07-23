using System.ComponentModel.DataAnnotations;

namespace UserRegistration.Api.Requests
{
    public class CreatePinRequest
    {
        [Required(ErrorMessage = SharedResources.Required)]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = SharedResources.StringLength)]
        [Range(100000, 999999, ErrorMessage = SharedResources.Range)]
        public string PinCode { get; set; } = string.Empty;
    }
}