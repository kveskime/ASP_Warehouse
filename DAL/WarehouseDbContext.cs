using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Migrations;
using Domain;
using Domain.Models;

namespace DAL
{
    public class WarehouseDbContext : DbContext, IDbContext
    {
        public WarehouseDbContext() : base("DbConnectionString")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<WarehouseDbContext, MigrationConfiguration>());
#if DEBUG
            Database.Log = s => Trace.Write(s);
#endif
        }



        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInPurchase> ProductInPurchases { get; set; }
        public DbSet<ProductInWarehouse> ProductInWarehouses { get; set; }
        public DbSet<ProductInWork> ProductInWorks { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // convert all datetime and datetime? properties to datetime2 in ms sql
            // ms sql datetime has min value of 1753-01-01 00:00:00.000
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            // use Date type in ms sql, where [DataType(DataType.Date)] attribute is used
            modelBuilder.Properties<DateTime>()
        .Where(x => x.GetCustomAttributes(false).OfType<DataTypeAttribute>()
        .Any(a => a.DataType == DataType.Date))
        .Configure(c => c.HasColumnType("date"));
        }
    }
}
