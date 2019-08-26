using System.IO;
using Autofac;
using Autofac.Extras.DynamicProxy;
using EmployeePortal.Infrastructure.Logging;
using EmployeePortal.Infrastructure.Module;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.TimeRegistration.Domain.TimeSheets;
using EmployeePortal.TimeRegistration.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
// Configure
// - Config
// - Handlers
// - Services
// - Static Files
// - EntityFramework

namespace EmployeePortal.TimeRegistration
{
    public class ModuleConfigurator : IModuleConfigurator
    {
        public static void ConfigureServices(IServiceCollection serviceCollection, ContainerBuilder containerBuilder)
        {
            serviceCollection.AddTransient<TimeSheetConfiguration>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            RegisterHandlers(containerBuilder);
            RegisterEntityFramework(serviceCollection, serviceProvider);
        }

        public static void ConfigureModule(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "../EmployeePortal.TimeRegistration/Application/TimeSheet")),
                RequestPath = "/TimeSheets"
            });
        }

        private static void RegisterHandlers(ContainerBuilder containerBuilder)
        {
            RegisterWithLoggingInterceptor<GetTimeSheetHandler, RequestHandler<GetTimeSheetRequest, GetTimeSheetReponse>>(containerBuilder);
            RegisterWithLoggingInterceptor<TimeSheetOverviewHandler, RequestHandler<TimeSheetOverviewRequest, TimeSheetOverviewReponse>>(containerBuilder);
            RegisterWithLoggingInterceptor<DeleteHourLineHandler, RequestHandler<DeleteHourLineRequest, ResponseBase>>(containerBuilder);
            RegisterWithLoggingInterceptor<StoreTimeSheetHandler, RequestHandler<StoreTimeSheetRequest, ResponseBase>>(containerBuilder);
        }

        private static void RegisterWithLoggingInterceptor<T, TY>(ContainerBuilder containerBuilder)
        {
            // This could be a decorator
            containerBuilder
                .RegisterType<T>()
                .As<TY>()
                .EnableClassInterceptors()
                .InterceptedBy(typeof(LoggingInterceptor));
        }

        private static void RegisterEntityFramework(IServiceCollection services, ServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<TimeSheetConfiguration>();
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<TimeSheetContext>(options =>
                    options.UseSqlServer(configuration.ConnectionString));
        }
    }
}