using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.repository.Data.Config
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> order)
        {

            order.OwnsOne(O => O.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());


            order.Property(O => O.OrderStatus)
                 .HasConversion(
                        OStatus => OStatus.ToString(), //تتخزن في الداتا بيز ك string       
                        OStatus => Enum.Parse<OrderStatus>(OStatus.ToString()) // انا هقراها من الداتابيز ك OrderStatus
                 );

            order.Property(O => O.SubTotal)
                     .HasColumnType("decimal(18,2)");

            order.HasOne(O => O.DeliveryMethod)
                  .WithMany()
                  .OnDelete(DeleteBehavior.SetNull); // عامل العلاقه كدا اصلا  EF ال

        }
    }
}
