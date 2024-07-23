using UserRegistration.Core.Entities;

namespace UserRegistration.Core.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User?> GetUserByICNumberAsync(string icNumber);
    }
}