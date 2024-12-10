namespace CvSystem.Api.Response
{

    public class CVDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public PersonalInformationDto? PersonalInformation { get; set; }
        public ICollection<ExperienceInformationDto> Experiences { get; set; } = [];
    }
}