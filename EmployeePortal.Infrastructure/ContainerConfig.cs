using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeePortal.Infrastructure
{
    public class ContainerConfig
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<RequestHandlerFactory>();
            serviceCollection.AddTransient<CurrentUserService>();
        }
    }
}
