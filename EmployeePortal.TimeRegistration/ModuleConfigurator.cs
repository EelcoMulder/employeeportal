using System.IO;
using Autofac;
using Autofac.Extras.DynamicProxy;
using EmployeePortal.Infrastructure.Logging;
using EmployeePortal.Infrastructure.Module;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.Infrastructure.Services;
using EmployeePortal.TimeRegistration.Domain.TimeSheets;
using EmployeePortal.TimeRegistration.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly ContainerBuilder _containerBuilder;
        private TimeSheetConfiguration _timeSheetConfiguration;

        public ModuleConfigurator(
            IConfiguration configuration,
            ContainerBuilder containerBuilder)
        {
            _configuration = configuration;
            _containerBuilder = containerBuilder;
        }

        public void ConfigureServices()
        {
            _timeSheetConfiguration = new TimeSheetConfiguration(_configuration);
            _containerBuilder.RegisterInstance(_timeSheetConfiguration);
            RegisterHandlers(_containerBuilder);

            _containerBuilder.Register(c =>
            {
                var opt = new DbContextOptionsBuilder<TimeSheetContext>();
                opt.UseSqlServer(_timeSheetConfiguration.ConnectionString);

                return new TimeSheetContext(opt.Options);
            })
            .AsSelf()
            .InstancePerLifetimeScope();
        }

        public void ConfigureModule(IApplicationBuilder applicationBuilder)
        {
            var timeSheetDirectory = Path.Combine(RootDirectoryService.Get(), "EmployeePortal.TimeRegistration/Application/TimeSheets");
            applicationBuilder.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(timeSheetDirectory),
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
    }
}