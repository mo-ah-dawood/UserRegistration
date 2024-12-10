using System.ComponentModel.DataAnnotations;

namespace CvSystem.Api.Requests
{

    public class PersonalInformationRequest
    {


        [Required(ErrorMessage = SharedResources.Required)]
        [Display(Name = "Full name")]
        public string FullName { get; set; } = string.Empty;
        [Display(Name = "City name")]
        public string? CityName { get; set; } = string.Empty;
        [Required(ErrorMessage = SharedResources.Required)]
        [RegularExpression("^01[0125]\\d{8}$", ErrorMessage = SharedResources.MobileNumber)]
        [Display(Name = "Mobile number")]
        public string MobileNumber { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = SharedResources.Email)]
        [Display(Name = "Email address")]
        
        public string? EmailAddress { get; set; }

    }

}