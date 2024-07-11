using HR_PLATFORM_INFRASTRUCTURE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HR_PLATFORM_INFRASTRUCTURE.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext()
        {
        }
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<VacationDetailsEntity> VacationsDetails { get; set; }
        public DbSet<VacationTypeEntity> VacationTypes { get; set; }
        public DbSet<VacationEntity> Vacations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
