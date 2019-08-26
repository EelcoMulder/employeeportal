using Autofac;
using EmployeePortal.Infrastructure.Logging;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.Infrastructure.Services;

namespace EmployeePortal.Infrastructure
{
    public class ContainerConfig
    {
        public static void Configure(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<LoggingInterceptor>();
            containerBuilder.RegisterType<RequestHandlerFactory>();
            containerBuilder.RegisterType<CurrentUserService>();
            containerBuilder
                .RegisterType<RequestHandlerFactory>()
                .As<IRequestHandlerFactory>();
        }
    }
}
