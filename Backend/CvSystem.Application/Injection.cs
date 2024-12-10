using Microsoft.Extensions.DependencyInjection;
using CvSystem.Application.Services;
using CvSystem.Core.Services;

namespace CvSystem.Application
{
    public static class Injection
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            return services.AddTransient<ICVService, CVService>();
        }

    }
}