using Microsoft.Extensions.Configuration;

namespace EmployeePortal.TimeRegistration.Infrastructure
{
    internal class TimeSheetConfiguration
    {
        private readonly IConfiguration _configuration;

        public TimeSheetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString => _configuration["ConnectionStrings:TimeSheetContext"];
    }
}
