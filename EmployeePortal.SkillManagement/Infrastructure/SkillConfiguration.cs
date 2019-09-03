using Microsoft.Extensions.Configuration;

namespace EmployeePortal.SkillManagement.Infrastructure
{
    internal class SkillConfiguration
    {
        private readonly IConfiguration _configuration;

        public SkillConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString => _configuration["ConnectionStrings:SkillContext"];
    }
}