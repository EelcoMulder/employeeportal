using System.IO;
using EmployeePortal.TimeRegistration.Model;
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
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var builder = new DbContextOptionsBuilder<TimeSheetContext>();
                var connectionString = configuration.GetConnectionString("TimeSheetContext");
                builder.UseSqlServer(connectionString);
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
    }
}
