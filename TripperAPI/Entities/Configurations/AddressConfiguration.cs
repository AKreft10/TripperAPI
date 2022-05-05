using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripperAPI.Entities.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(a => a.Continent)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(56);

            builder.Property(a => a.Latitude)
                .IsRequired();

            builder.Property(a => a.Longitude)
                .IsRequired();
        }
    }
}
