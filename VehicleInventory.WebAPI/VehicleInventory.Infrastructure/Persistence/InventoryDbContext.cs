using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleInventory.Domain.Entities;
namespace VehicleInventory.Infrastructure.Persistence
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
        }

        // Vehicles table
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>(entity =>
            {
                
                entity.ToTable("Vehicles");
                
                entity.HasKey(v => v.Id);
                
                entity.Property(v => v.VehicleCode)
                      .IsRequired()
                      .HasMaxLength(30);

                entity.HasIndex(v => v.VehicleCode)
                      .IsUnique();
                entity.Property(v => v.VehicleType)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(v => v.LocationId)
                      .IsRequired();

                entity.Property(v => v.Status)
                      .IsRequired();
            });
        }
    }
}
