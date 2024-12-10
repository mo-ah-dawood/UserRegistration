using CvSystem.Core.Entities;
using CvSystem.Core.Services.Params;

namespace CvSystem.Core.Services
{
    public interface ICVService
    {
        Task<IEnumerable<CV>> LoadCVs(CvPagingParams parameters,Action<int> totalCallBack);
        Task<CV> Create(CV entity);
        Task<CV> Update(CV cv);
        Task AddExperiences(CV cv, IEnumerable<ExperienceInformation> experienceInformation);
        Task<CV> Delete(CV cv);
        Task<CV> Get(int id);
    }

}