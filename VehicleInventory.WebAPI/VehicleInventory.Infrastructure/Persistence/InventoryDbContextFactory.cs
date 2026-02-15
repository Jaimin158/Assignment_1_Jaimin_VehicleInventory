using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace VehicleInventory.Infrastructure.Persistence
{

    public class InventoryDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
    {
        public InventoryDbContext CreateDbContext(string[] args)
        {
        

            var currentPath = Directory.GetCurrentDirectory();
            var checkPath = currentPath;

            for (int i = 0; i < 6; i++)
            {
                var appsettingsFile = Path.Combine(checkPath, "VehicleInventory.WebAPI", "appsettings.json");

                if (File.Exists(appsettingsFile))
                {
                    var config = new ConfigurationBuilder()
                        .SetBasePath(Path.Combine(checkPath, "VehicleInventory.WebAPI"))
                        .AddJsonFile("appsettings.json", optional: false)
                        .Build();

                    var optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
                    optionsBuilder.UseSqlServer(config.GetConnectionString("InventoryDb"));

                    return new InventoryDbContext(optionsBuilder.Options);
                }

                var parent = Directory.GetParent(checkPath);
                if (parent == null)
                    break;

                checkPath = parent.FullName;
            }

            var fallbackOptions = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseSqlServer(
                    "Server=(localdb)\\MSSQLLocalDB;Database=VehicleInventoryDb;Trusted_Connection=True;TrustServerCertificate=True"
                )
                .Options;

            return new InventoryDbContext(fallbackOptions);
        }
    }
}
