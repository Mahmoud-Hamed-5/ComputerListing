using ComputerListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerListing.Configurations.Entities
{
    public class ComputerConfiguration : IEntityTypeConfiguration<Computer>
    {
        public void Configure(EntityTypeBuilder<Computer> builder)
        {
            builder.HasData(
               new Computer
               {
                   Id = 1,
                   Manufacturer = "MSI",
                   Model = "Crosshair 15",
                   Proccessor = "i7-11800",
                   RAM = 16
               }
               );
        }
    }
}
