using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Talabat.Core.Entities.Identity.Configure
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Adddress>
    {
        public void Configure(EntityTypeBuilder<Adddress> address)
        {
            address.ToTable("Addresses");

        }
    }
}
