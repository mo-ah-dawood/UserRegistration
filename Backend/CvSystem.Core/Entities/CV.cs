namespace CvSystem.Core.Entities
{
    public class CV
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int PersonalInformationId { get; set; }
        public PersonalInformation? PersonalInformation { get; set; }
        public ICollection<ExperienceInformation> Experiences { get; set; } = [];
    }
}