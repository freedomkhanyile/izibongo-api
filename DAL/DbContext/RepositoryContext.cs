using izibongo.api.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace izibongo.api.DAL.DbContext
{
    public class RepositoryContext : IdentityDbContext
    {
        private IConfigurationRoot _config;

        public RepositoryContext(DbContextOptions<RepositoryContext> options, IConfigurationRoot config)
            : base(options)
        {
            _config = config;
        }

        public DbSet<Family> Families { get; set; }
        public DbSet<Isibongo> Izibongo { get; set; }
        public DbSet<Status> Statuses { get; set; }
 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["Data:ConnectionString"]);
        }
    }
}