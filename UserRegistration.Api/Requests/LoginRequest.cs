using System.ComponentModel.DataAnnotations;

namespace UserRegistration.Api.Requests
{
    public class LoginRequest
    {
             [Required(ErrorMessage = SharedResources.Required)]
        public string ICNumber { get; set; } = string.Empty;
    }
}