using System.ComponentModel.DataAnnotations;

namespace CvSystem.Api.Requests
{
    public class CreateCVRequest
    {
        [Required(ErrorMessage = SharedResources.Required)]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

    }

    public class UpdateCVRequest : CreateCVRequest
    {
        [Required(ErrorMessage = SharedResources.Required)]
        [Display(Name = "Id")]
        public int Id { get; set; }
    }
}