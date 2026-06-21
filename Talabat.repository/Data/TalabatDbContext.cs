using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.repository.Data.Config;

namespace Talabat.repository.Data
{
    public class TalabatDbContext : DbContext
    {
        public TalabatDbContext(DbContextOptions<TalabatDbContext> options) : base(options) { }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Apply Configuration in Old Way 
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductBrandConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());

            #endregion


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }


        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    }
}
