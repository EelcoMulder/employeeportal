using Microsoft.AspNetCore.Builder;

namespace EmployeePortal.Infrastructure.Module
{
    public interface IModuleConfigurator
    {
        void ConfigureServices();
        void ConfigureModule(IApplicationBuilder applicationBuilder);// TODO
    }
}