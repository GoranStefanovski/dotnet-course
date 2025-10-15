using Microsoft.EntityFrameworkCore;
using DotnetAPI.Models;
namespace DotnetAPI.Data
{
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSalary> UserSalary { get; set; }
        public virtual DbSet<UserJobInfo> UserJobInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseMySql(_config.GetConnectionString("DefaultConnection"), 
                        new MySqlServerVersion(new Version(8, 0, 21)),
                        optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TUtorialAppSchema");

            modelBuilder.Entity<User>()
                .ToTable("Users", "TutorialAppSchema")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<UserSalary>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<UserJobInfo>()
                .HasKey(u => u.UserId);
        }
    }
    
}