using System;
using System.IO;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmployeePortal.Infrastructure.Services;
using EmployeePortal.TimeRegistration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace EmployeePortal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie("Cookies")
                .AddOpenIdConnect
                (
                    options =>
                    {
                        options.ClientId = "566d7d6c-0802-4e51-98c1-74672a59556c";
                        options.ClientSecret = "RPcV+rP4nBuOqF4lYAuHWeeRng.g5@/1";
                        options.Authority = "https://login.microsoftonline.com/5992a427-2b8e-43f2-a467-7fdc724be4bd/";
                    }
                );

            services
                .AddMvc(o =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    o.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                )
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<RazorPagesOptions>(o => o.RootDirectory = "/Application");
            var builder = new ContainerBuilder();
            builder
                .RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>();

            Infrastructure.ContainerConfig.Configure(builder);
            TimeRegistration.ModuleConfigurator.ConfigureServices(services, builder);

            builder.Populate(services);
            var applicationContainer = builder.Build();

            return new AutofacServiceProvider(applicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            ModuleConfigurator.ConfigureModule(app);
            // TODO: Move to Module
            var skillsDirectory = Path.Combine(RootDirectoryService.Get(), "EmployeePortal.SkillManagement/Application/Skills");
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(skillsDirectory),
                RequestPath = "/Skills"
            });
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
