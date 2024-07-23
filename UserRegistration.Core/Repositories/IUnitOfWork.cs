using UserRegistration.Core.Entities;

namespace UserRegistration.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IVerificationRepository Verification { get; }
        IUserRepository Users { get; }
        IRepository<Privacy> Privacy { get; }
        public Task<int> SaveChangesAsync();
        Task<ITransition> GetTransition();
    }


    public interface ITransition
    {
        Task Commit();
        Task RollBack();
    }

}