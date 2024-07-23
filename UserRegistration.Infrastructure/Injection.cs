using Microsoft.Extensions.DependencyInjection;
using UserRegistration.Core.Repositories;
using UserRegistration.Infrastructure.Repositories;
using UserRegistration.Application;
using UserRegistration.Application.Interfaces;
using UserRegistration.Infrastructure.Providers;
namespace UserRegistration.Infrastructure
{

    public static class Injection
    {
        public static IServiceCollection InjectInfra(this IServiceCollection services)
        {
            return services
            .InjectServices()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IEmailSender, EmailSender>()
            .AddScoped<ISMSSender, SmsSender>();
        }

    }
}