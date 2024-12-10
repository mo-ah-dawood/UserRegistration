using CvSystem.Core.Entities;

namespace CvSystem.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {

        IRepository<CV> CVS { get; }
        IRepository<ExperienceInformation> ExperienceInformation { get; }
        public Task<int> SaveChangesAsync();
        Task<ITransition> GetTransition();
    }


    public interface ITransition : IDisposable
    {
        Task Commit();
        Task RollBack();
    }

}