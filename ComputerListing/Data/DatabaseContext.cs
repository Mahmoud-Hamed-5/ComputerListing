using ComputerListing.Configurations.Entities;
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
         
            builder.ApplyConfiguration(new ComputerConfiguration());

            builder.ApplyConfiguration(new AccessoryConfiguration());

            builder.ApplyConfiguration(new RoleConfiguration());
        }


    }
}
