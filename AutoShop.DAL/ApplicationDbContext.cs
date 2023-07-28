using AutoShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AutoShop.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Car> Cars { get; set; }
    }
}