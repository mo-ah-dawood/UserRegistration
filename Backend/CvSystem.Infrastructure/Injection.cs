using Microsoft.Extensions.DependencyInjection;
using CvSystem.Core.Repositories;
using CvSystem.Infrastructure.Repositories;
using CvSystem.Application;
namespace CvSystem.Infrastructure
{

    public static class Injection
    {
        public static IServiceCollection InjectInfra(this IServiceCollection services)
        {
            return services
            .AddTransient(typeof(IRepository<>), typeof(Repository<>))
            .InjectServices()
            .AddScoped<IUnitOfWork, UnitOfWork>();
        }

    }
}