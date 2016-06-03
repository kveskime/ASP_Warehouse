using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAnnotations;
using DAL.EFConfiguration;
using DAL.Helpers;
using DAL.Interfaces;
using Domain;
using Domain.Identity;
using Domain.Models;
using Ninject;
using NLog;

namespace DAL
{
    public class WarehouseDbContext : DbContext, IDbContext
    {

        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();
        private readonly IUserNameResolver _userNameResolver;
        [Inject]
        public WarehouseDbContext(IUserNameResolver userNameResolver, ILogger logger) : base("DbConnectionString")
        {
            _logger = logger;
            _userNameResolver = userNameResolver;

            _logger.Debug("InstanceId: " + _instanceId);
            Database.SetInitializer(new DatabaseInitializer());
            //Database.SetInitializer(
            //  new MigrateDatabaseToLatestVersion<WarehouseDbContext, MigrationConfiguration>());
#if DEBUG
            Database.Log = s => Trace.Write(s);
#endif
        }
        //hack for mvc scaffolding, parameterless constructor is required

        public WarehouseDbContext() : this(new UserNameResolver(() => "Anonymous"), NLog.LogManager.GetCurrentClassLogger())
        {

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

        public DbSet<Translation> Translations { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<RoleInt> RolesInt { get; set; }
        public DbSet<UserClaimInt> UserClaimsInt { get; set; }
        public DbSet<UserLoginInt> UserLoginsInt { get; set; }
        public DbSet<UserInt> UsersInt { get; set; }
        public DbSet<UserRoleInt> UserRolesInt { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // remove cascade delete
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


            // Identity, PK - int 
            modelBuilder.Configurations.Add(new RoleIntMap());
            modelBuilder.Configurations.Add(new UserClaimIntMap());
            modelBuilder.Configurations.Add(new UserLoginIntMap());
            modelBuilder.Configurations.Add(new UserIntMap());
            modelBuilder.Configurations.Add(new UserRoleIntMap());

            Precision.ConfigureModelBuilder(modelBuilder);

            // convert all datetime and datetime? properties to datetime2 in ms sql
            // ms sql datetime has min value of 1753-01-01 00:00:00.000
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            // use Date type in ms sql, where [DataType(DataType.Date)] attribute is used
            modelBuilder.Properties<DateTime>()
        .Where(x => x.GetCustomAttributes(false).OfType<DataTypeAttribute>()
        .Any(a => a.DataType == DataType.Date))
        .Configure(c => c.HasColumnType("date"));
        }
        public override int SaveChanges()
        {

            // Update metafields in entitys, that implement IBaseEntity - CreatedAtDT, CreatedBy, etc
            var entities =
                ChangeTracker.Entries()
                .Where(x => x.Entity is IBaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IBaseEntity)entity.Entity).CreatedAtDT = DateTime.Now;
                    ((IBaseEntity)entity.Entity).CreatedBy = _userNameResolver.CurrentUserName;
                }

                ((IBaseEntity)entity.Entity).ModifiedAtDT = DateTime.Now;
                ((IBaseEntity)entity.Entity).ModifiedBy = _userNameResolver.CurrentUserName;
            }

            // Custom exception - gives much more details why EF Validation failed
            // or watch this inside exception ((System.Data.Entity.Validation.DbEntityValidationException)$exception).EntityValidationErrors
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }

        protected override void Dispose(bool disposing)
        {
            _logger.Info("Disposing: " + disposing + " _instanceId: " + _instanceId);
            base.Dispose(disposing);
        }

        // Ettevaatust kui automaatselt kontrollereid luua, siis siia võivad tekkida autogenereeritud Identity DbSetid
    }
}

