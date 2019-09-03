using EmployeePortal.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Autofac;
using EmployeePortal.Infrastructure.Module;
using EmployeePortal.SkillManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeePortal.SkillManagement
{
    public class ModuleConfigurator : IModuleConfigurator
    {
        private readonly IConfiguration _configuration;
        private readonly ContainerBuilder _containerBuilder;
        private SkillConfiguration _skillConfiguration;

        public ModuleConfigurator(
            IConfiguration configuration,
            ContainerBuilder containerBuilder)
        {
            _configuration = configuration;
            _containerBuilder = containerBuilder;
        }

        public void ConfigureServices()
        {
            _skillConfiguration = new SkillConfiguration(_configuration);
            _containerBuilder.RegisterInstance(_skillConfiguration);

            _containerBuilder.Register(c =>
                {
                    var opt = new DbContextOptionsBuilder<SkillContext>();
                    opt.UseSqlServer(_skillConfiguration.ConnectionString);

                    return new SkillContext(opt.Options);
                })
                .AsSelf()
                .InstancePerLifetimeScope();
        }

        public void ConfigureModule(IApplicationBuilder applicationBuilder)
        {
            var timeSheetDirectory =
                Path.Combine(RootDirectoryService.Get(), "EmployeePortal.SkillManagement/Application/Skills");
            applicationBuilder.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(timeSheetDirectory),
                RequestPath = "/Skills"
            });
        }
    }
}
