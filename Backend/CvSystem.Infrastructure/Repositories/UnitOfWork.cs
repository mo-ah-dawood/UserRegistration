using Microsoft.EntityFrameworkCore.Storage;
using CvSystem.Core.Repositories;
using CvSystem.Infrastructure.Data;
using CvSystem.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CvSystem.Infrastructure.Repositories
{
    public class UnitOfWork(AppDbContext context, IServiceProvider provider) : IUnitOfWork
    {
        public IRepository<CV> CVS => provider.GetRequiredService<IRepository<CV>>();


        public IRepository<ExperienceInformation> ExperienceInformation => provider.GetRequiredService<IRepository<ExperienceInformation>>();


        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<ITransition> GetTransition()
        {
            try
            {
                return new AppTransaction(await context.Database.BeginTransactionAsync());
            }
            catch
            {
                return new UnsupportedTransaction();
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }

    class AppTransaction : ITransition
    {
        private readonly IDbContextTransaction _transaction;

        public AppTransaction(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _transaction.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task RollBack()
        {
            await _transaction.RollbackAsync();
        }
    }


    class UnsupportedTransaction : ITransition
    {

        public Task Commit()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task RollBack()
        {

            return Task.CompletedTask;
        }
    }

}