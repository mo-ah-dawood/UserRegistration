using Microsoft.Extensions.DependencyInjection;
using UserRegistration.Application.Services;
using UserRegistration.Core.Services;

namespace UserRegistration.Application
{
    public static class Injection
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            return services.AddScoped<IUserService, UserService>().AddScoped<IPrivacyService, PrivacyService>();
        }

    }
}