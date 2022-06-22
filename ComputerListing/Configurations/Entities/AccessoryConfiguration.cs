using ComputerListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerListing.Configurations.Entities
{
    public class AccessoryConfiguration : IEntityTypeConfiguration<Accessory>
    {
        public void Configure(EntityTypeBuilder<Accessory> builder)
        {
            builder.HasData(
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
