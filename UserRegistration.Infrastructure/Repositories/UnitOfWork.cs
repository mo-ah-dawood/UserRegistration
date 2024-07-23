using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using UserRegistration.Core.Entities;
using UserRegistration.Core.Repositories;
using UserRegistration.Infrastructure.Data;

namespace UserRegistration.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IOptions<VerificationCodeConfig> _options;
        private IUserRepository? _userRepository;
        private IVerificationRepository? _verificationRepository;
        private IRepository<Privacy>? _privacy;

        public UnitOfWork(AppDbContext context, IOptions<VerificationCodeConfig> options)
        {
            _context = context;
            _options = options;
        }

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);
        public IVerificationRepository Verification => _verificationRepository ??= new VerificationRepository(_context, _options.Value);
        public IRepository<Privacy> Privacy => _privacy ??= new Repository<Privacy>(_context);


        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<ITransition> GetTransition()
        {
            try
            {
                return new AppTransaction(await _context.Database.BeginTransactionAsync());
            }
            catch 
            {
                return new UnsupportedTransaction();
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
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

        public Task RollBack()
        {
            return Task.CompletedTask;
        }
    }

}