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

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(key => key.Id);

                builder.HasData(new User
                {
                    Id = 1,
                    Name = "Nikita_Ivakh",
                    Password = HashPasswordHelper.HashPassword("postgres"),
                    Role = Role.Admin,
                });

                builder.Property(key => key.Id).ValueGeneratedOnAdd();
                builder.Property(key => key.Name).HasMaxLength(100).IsRequired();
                builder.Property(key => key.Password).IsRequired();

                builder.HasOne(key => key.Profile).WithOne(key => key.User).HasPrincipalKey<User>(key => key.Id).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Profile>(builder =>
            {
                builder.HasData(new Profile()
                {
                    Id = 1,
                    UserId = 1,
                });

                builder.ToTable("Profiles").HasKey(key => key.Id);
                builder.Property(key => key.Id).ValueGeneratedOnAdd();

                builder.Property(key => key.Age);
                builder.Property(key => key.Address).HasMaxLength(250).IsRequired(false);
            });
        }
    }
}