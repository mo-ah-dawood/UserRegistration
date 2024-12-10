using CvSystem.Application.Exceptions;
using CvSystem.Core.Common;
using CvSystem.Core.Entities;
using CvSystem.Core.Repositories;
using CvSystem.Core.Services;
using CvSystem.Core.Services.Params;

namespace CvSystem.Application.Services
{
    public class CVService(IUnitOfWork unitOfWork) : ICVService
    {


        public async Task<CV> Create(CV entity)
        {
            var result = await unitOfWork.CVS.AddAsync(entity);
            await unitOfWork.SaveChangesAsync();
            return result;
        }


        public async Task<CV> Delete(CV cv)
        {
            var result = await unitOfWork.CVS.DeleteAsync(cv);
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<CV> Get(int id)
        {
            return await unitOfWork.CVS
             .Query()
             .Include((i) => i.PersonalInformation)
             .Include((i) => i.Experiences)
             .FirstOrDefaultAsync((x) => x.Id == id) ?? throw new AppException("CVNotFound", "CV Not Found");
        }

        public async Task<IEnumerable<CV>> LoadCVs(CvPagingParams parameters, Action<int> totalCallBack)
        {
            IQueryableRepository<CV> queryable = unitOfWork.CVS.Query().Include((i) => i.PersonalInformation);

            if (!string.IsNullOrWhiteSpace(parameters.Query))
                queryable = queryable.Query((q) => q.Where((w) => w.Name.Contains(parameters.Query)));
            totalCallBack(await queryable.CountAsync());
            queryable = queryable.Query((q) => q.Skip(parameters.Skip).Take(parameters.PageSize));
            return await queryable.ToListAsync();
        }

        public async Task<CV> Update(CV cv)
        {
            var result = await unitOfWork.CVS.UpdateAsync(cv);
            await unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task AddExperiences(CV cv, IEnumerable<ExperienceInformation> info)
        {
            var toDelete = cv.Experiences.Where((ex) => !info.Any((a) => a.Id == ex.Id));
            foreach (var ex in toDelete)
                await unitOfWork.ExperienceInformation.DeleteAsync(ex);

            foreach (var ex in info.Where((x) => x.Id <= 0))
            {
                ex.CVId = cv.Id;
                await unitOfWork.ExperienceInformation.AddAsync(ex);
            }
            foreach (var ex in info.Where(x => x.Id >= 0))
            {
                var exist = cv.Experiences.Where((x) => x.Id == ex.Id).FirstOrDefault()!;
                exist.CompanyField = ex.CompanyField;
                exist.CompanyName = ex.CompanyName;
                exist.City = ex.City;
            }
            await unitOfWork.SaveChangesAsync();

        }
    }
}