using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistration.Application.Exceptions;
using UserRegistration.Core.Entities;
using UserRegistration.Core.Repositories;
using UserRegistration.Infrastructure.Data;

namespace UserRegistration.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(AppDbContext context):base(context){}

        public async Task<User?> GetUserByICNumberAsync(string icNumber)
        {
           return await FirstOrDefaultAsync((x)=>x.ICNumber == icNumber);
        }
    }
}