using System.ComponentModel.DataAnnotations;

namespace UserRegistration.Api.Requests
{
    public class RegisterNewUser
    {
        [Required(ErrorMessage = SharedResources.Required)]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = SharedResources.Required)]
        [RegularExpression("^[23]\\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\\d|3[01])\\d{7}$", ErrorMessage = SharedResources.ICNumber)]
        public string ICNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = SharedResources.Required)]
        [RegularExpression("^01[0125]\\d{8}$", ErrorMessage = SharedResources.MobileNumber)]
        public string MobileNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = SharedResources.Required)]
        [EmailAddress(ErrorMessage = SharedResources.Email)]
        public string EmailAddress { get; set; } = string.Empty;
    }


    
}