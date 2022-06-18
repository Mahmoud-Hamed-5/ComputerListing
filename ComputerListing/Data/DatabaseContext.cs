using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerListing.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Computer> Computers { set; get; }

        public DbSet<Accessory> Accessories { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Computer>().HasData(
                new Computer
                {
                    Id = 1,
                    Manufacturer = "MSI",
                    Model = "Crosshair 15",
                    Proccessor = "i7-11800",
                    RAM = 16
                }
                );

            builder.Entity<Accessory>().HasData(
                new Accessory
                {
                    Id = 1,
                    Name = "Gaming Mouse",
                    ComputerId = 1
                }
                );
        }


    }
}
