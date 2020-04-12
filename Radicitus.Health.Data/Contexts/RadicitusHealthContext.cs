using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Radicitus.Health.Data.Entities;
namespace Radicitus.Health.Data.Contexts
{
    public class RadicitusHealthContext : DbContext
    {
        private readonly string _connectionString;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString, builder =>
            {
                builder.MigrationsAssembly("Radicitus.Health");
            });
        }

        public RadicitusHealthContext() : base()
        {
            var configuration = new ConfigurationBuilder();
            var builtConfig = configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _connectionString = builtConfig.GetConnectionString("radicitus-health");
        }

        public RadicitusHealthContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<HealthInitiative> HealthInitiatives { get; set; }
        public DbSet<HealthParticipant> HealthParticipants { get; set; }
        public DbSet<ParticipantLog> ParticipantLogs { get; set; }
    }
}
