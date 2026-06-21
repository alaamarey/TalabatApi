using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities;

namespace Talabat.repository.Data.Config
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name)
                   .IsRequired();


            builder.Property(P => P.Description)
                    .IsRequired();


            builder.Property(P => P.PictureUrl)
                   .IsRequired();


            builder.Property(P => P.Price)
                    .IsRequired()
                    .HasColumnType("DECIMAL(18,2)");

            builder.HasOne(P => P.ProductBrand)
                   .WithMany()
                   .HasForeignKey(P => P.BrandId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(P => P.ProductCategory)
                   .WithMany()
                   .HasForeignKey(P => P.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);






        }
    }
}
