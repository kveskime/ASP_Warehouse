﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Repositories;
using Domain;

namespace DAL.Helpers
{
    public class EFRepositoryFactories : IDisposable
    {
        // Func<T, TResult> Delegate
        // https://msdn.microsoft.com/en-us/library/bb549151(v=vs.110).aspx

        private readonly IDictionary<Type, Func<IDbContext, object>> _repositoryFactories;

        public EFRepositoryFactories()
        {
            _repositoryFactories = GetCustomFactories();
        }

        //this ctor is for testing only, you can give here an arbitrary list of repos
        public EFRepositoryFactories(IDictionary<Type, Func<IDbContext, object>> factories)
        {
            _repositoryFactories = factories;
        }

        //special repos with custom interfaces are registered here
        private static IDictionary<Type, Func<IDbContext, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<IDbContext, object>>
                {
                    {typeof(IProductRepository), dbContext => new ProductRepository(dbContext)},
                    {typeof(IProductTypeRepository), dbContext => new ProductTypeRepository(dbContext)},
                    {typeof(IProductInWorkRepository), dbContext => new ProductInWorkRepository(dbContext)},
                    {typeof(IWorkTypeRepository), dbContext => new WorkTypeRepository(dbContext)},
                    {typeof(IProductInWarehouseRepository), dbContext => new ProductInWarehouseRepository(dbContext)},
                    {typeof(IWarehouseRepository), dbContext => new WarehouseRepository(dbContext)},
                    {typeof(IProductInPurchaseRepository), dbContext => new ProductInPurchaseRepository(dbContext)},
                    {typeof(ISupplierRepository), dbContext => new SupplierRepository(dbContext)},
                    {typeof(IPurchaseRepository), dbContext => new PurchaseRepository(dbContext)}

                };
        }

        public Func<IDbContext, object> GetRepositoryFactory<T>()
        {

            Func<IDbContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        public Func<IDbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            // if we already have this repository in list, return it
            // if not, create new instance of EFRepository
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        protected virtual Func<IDbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            // create new instance of EFRepository<T>
            return dbContext => new EFRepository<T>(dbContext);
        }

        public void Dispose()
        {
        }

    }
}