using UserRegistration.Core.Enum;

namespace UserRegistration.Core.Entities
{
    public class Privacy
    {
        public int Id { get; set;}
        public string Value { get; set; } = string.Empty;
        public AppLanguage Language { get; set; } = AppLanguage.AR;
    }
}