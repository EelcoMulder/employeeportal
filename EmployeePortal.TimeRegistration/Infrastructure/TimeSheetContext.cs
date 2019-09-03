using System.IO;
using EmployeePortal.TimeRegistration.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EmployeePortal.TimeRegistration.Infrastructure
{
    public class TimeSheetContext : DbContext
    {
        public TimeSheetContext(DbContextOptions<TimeSheetContext> options) : base(options)
        {
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TimeSheetContext>
        {
            public TimeSheetContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<TimeSheetContext>();
                ConfigureDbContext(builder);
                return new TimeSheetContext(builder.Options);
            }
        }

        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<HourLine> HourLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<TimeSheet>()
                .ToTable("TimeSheets")
                .HasKey(e => e.Id);
            modelBuilder
                .Entity<HourLine>()
                .ToTable("HourLines")
                .HasKey(e => e.Id);
            modelBuilder.Entity<HourLine>()
                .HasOne(p => p.TimeSheet)
                .WithMany(b => b.HourLines);
        }

        private static void ConfigureDbContext(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("TimeSheetContext");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
