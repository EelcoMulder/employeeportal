using System.IO;
using EmployeePortal.SkillManagement.Domain;
using EmployeePortal.SkillManagement.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EmployeePortal.SkillManagement.Infrastructure
{
    public class SkillContext : DbContext
    {
        public SkillContext(DbContextOptions<SkillContext> options) : base(options)
        {
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SkillContext>
        {
            public SkillContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<SkillContext>();
                ConfigureDbContext(builder);
                return new SkillContext(builder.Options);
            }
        }

        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Skill>()
                .ToTable("Skills")
                .HasKey(e => e.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConfigureDbContext(optionsBuilder);
        }

        private static void ConfigureDbContext(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("SkillContext");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
