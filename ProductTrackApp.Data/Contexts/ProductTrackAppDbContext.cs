using Microsoft.EntityFrameworkCore;
using ProductTrackApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Data.Contexts
{
    public class ProductTrackAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ProductTrackAppDbContext(DbContextOptions<ProductTrackAppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(50);
                entity.Property(e => e.Username).HasMaxLength(50);
                entity.Property(e => e.Password).HasMaxLength(50);
                entity.Property(e => e.Role).HasMaxLength(25);
                entity.Property(e => e.Role).HasDefaultValue("User");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Brand).HasMaxLength(50);
                entity.Property(e => e.Model).HasMaxLength(50);
                entity.Property(e => e.Category).HasMaxLength(50);
                entity.Property(e => e.ProductCode).HasMaxLength(50);
                entity.Property(e => e.Status).HasDefaultValue("true");
            });
        }
    }
}
