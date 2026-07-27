using Microsoft.EntityFrameworkCore;
using E_CommerceDatabase.Models;

namespace E_CommerceDatabase.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Review> Reviews { get; set; }
    }
}
