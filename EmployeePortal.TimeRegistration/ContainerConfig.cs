using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.TimeRegistration.Infrastructure;
using EmployeePortal.TimeRegistration.TimeSheets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeePortal.TimeRegistration
{
    public class ContainerConfig
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<RequestHandler<GetTimeSheetRequest, GetTimeSheetReponse>, GetTimeSheetHandler>();
            serviceCollection.AddTransient<RequestHandler<TimeSheetOverviewRequest, TimeSheetOverviewReponse>, TimeSheetOverviewHandler>();
            serviceCollection.AddTransient<TimeSheetConfiguration>();
            RegisterEntityFramework(serviceCollection);
        }

        private static void RegisterEntityFramework(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<TimeSheetConfiguration>();
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<TimeSheetContext>(options =>
                    options.UseSqlServer(configuration.ConnectionString));
        }
    }
}
