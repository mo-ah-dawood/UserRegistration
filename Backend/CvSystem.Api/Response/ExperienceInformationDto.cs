namespace CvSystem.Api.Response
{

    public class ExperienceInformationDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? CompanyField { get; set; }
    }
}