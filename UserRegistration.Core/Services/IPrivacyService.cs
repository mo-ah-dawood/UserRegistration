using UserRegistration.Core.Common;
using UserRegistration.Core.Entities;
using UserRegistration.Core.Enum;

namespace UserRegistration.Core.Services
{
    public interface IPrivacyService
    {
        Task<IEnumerable<Privacy>> LoadPrivacy(LoadPrivacyParams parameters);
    }

    public class LoadPrivacyParams : PagingParameters
    {
        public AppLanguage? Language { get; set; }
    }
}