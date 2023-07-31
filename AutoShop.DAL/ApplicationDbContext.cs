using AutoShop.Domain.Entity;
using AutoShop.Domain.Enum;
using AutoShop.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AutoShop.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Car> Cars { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasData(new User
                {
                    Id = 1,
                    Name = "Nikita_Ivakh",
                    Password = HashPasswordHelper.HashPassword("postgres"),
                    Role = Role.Admin,
                });

                builder.ToTable("Users").HasKey(key => key.Id);
                builder.Property(key => key.Id).ValueGeneratedOnAdd();
                builder.Property(key => key.Name).HasMaxLength(100).IsRequired();
                builder.Property(key => key.Password).IsRequired();
            });
        }
    }
}