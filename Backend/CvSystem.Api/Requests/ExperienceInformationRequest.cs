using System.ComponentModel.DataAnnotations;

namespace CvSystem.Api.Requests
{
    public class ExperienceInformationRequest
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = SharedResources.Required)]
        [MaxLength(20, ErrorMessage = SharedResources.StringLength)]
        [Display(Name = "Company name")]
        public string CompanyName { get; set; } = string.Empty;
        [Display(Name = "City")]
        public string? City { get; set; }
        [Display(Name = "Company field")]
        public string? CompanyField { get; set; }

    }


}