using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.repository.Data.Config
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        void IEntityTypeConfiguration<OrderItem>.Configure(EntityTypeBuilder<OrderItem> orderItem)
        {
            orderItem.OwnsOne(OI => OI.Product, Product => Product.WithOwner());


            orderItem.Property(OI => OI.Price)
                     .HasColumnType("decimal(18,2)");   
        }

    }
}
