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
        public ApplicationDbContext(DbContextOptions<Microsoft.EntityFrameworkCore.DbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=CATALIN; Database=HR_PLATFORM; Integrated Security=True; TrustServerCertificate=True;");
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
