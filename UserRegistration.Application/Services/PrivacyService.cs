using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistration.Core.Entities;
using UserRegistration.Core.Repositories;
using UserRegistration.Core.Services;

namespace UserRegistration.Application.Services
{
    public class PrivacyService : IPrivacyService
    {
        private readonly IUnitOfWork unitOfWork;

        public PrivacyService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Privacy>> LoadPrivacy(LoadPrivacyParams parameters)
        {
            return unitOfWork.Privacy.QueryAsync((q) => q.Where((w) => parameters.Language == null || w.Language == parameters.Language).Skip(parameters.Skip).Take(parameters.PageSize));
        }
    }
}