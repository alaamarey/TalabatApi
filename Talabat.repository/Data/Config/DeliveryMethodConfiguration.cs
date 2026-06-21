using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.repository.Data.Config
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> deliveryMethod)
        {
            deliveryMethod.Property(D => D.Cost)
                             .HasColumnType("decimal(18,2)");
        }
    }
}
