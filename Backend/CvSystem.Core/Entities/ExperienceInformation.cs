namespace CvSystem.Core.Entities
{
    public class ExperienceInformation
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? CompanyField { get; set; }
        public int CVId { get; set; }
        public CV? CV { get; set; }

    }
}