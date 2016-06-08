using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;
using Domain.Models;

namespace DAL
{
    public class WarehouseUOW : IWarehouseUOW, IDisposable
    {

        private IDbContext DbContext { get; set; }
        protected IEFRepositoryProvider RepositoryProvider { get; set; }

        public WarehouseUOW(IEFRepositoryProvider repositoryProvider, IDbContext dbContext)
        {

            DbContext = dbContext;

            repositoryProvider.DbContext = dbContext;
            RepositoryProvider = repositoryProvider;
        }

        // UoW main feature - atomic commit at the end of work
        public void Commit()
        {
            ((DbContext)DbContext).SaveChanges();
        }


        //standard repos
        //public IEFRepository<Product> Products => GetStandardRepo<Product>();


        public IEFRepository<ProductInPurchase> ProductsInPurchases => GetStandardRepo<ProductInPurchase>();

        // repo with custom methods
        // add it also in EFRepositoryFactories.cs, in method GetCustomFactories
        public IProductRepository Products => GetRepo<IProductRepository>();
        public IProductTypeRepository ProductTypes => GetRepo<IProductTypeRepository>();
        public IProductInWorkRepository ProductInWorks => GetRepo<IProductInWorkRepository>();
        public IWorkTypeRepository WorkTypes => GetRepo<IWorkTypeRepository>();
        public IProductInWarehouseRepository ProductInWarehouses => GetRepo<IProductInWarehouseRepository>();
        public IWarehouseRepository Warehouses => GetRepo<IWarehouseRepository>();
        //articles
        public IArticleRepository Articles => GetRepo<IArticleRepository>();

        public IMultiLangStringRepository MultiLangStrings => GetRepo<IMultiLangStringRepository>();
        public ITranslationRepository Translations => GetRepo<ITranslationRepository>();


        //Identity
        public IUserIntRepository UsersInt => GetRepo<IUserIntRepository>();
        public IUserRoleIntRepository UserRolesInt => GetRepo<IUserRoleIntRepository>();
        public IRoleIntRepository RolesInt => GetRepo<IRoleIntRepository>();
        public IUserClaimIntRepository UserClaimsInt => GetRepo<IUserClaimIntRepository>();
        public IUserLoginIntRepository UserLoginsInt => GetRepo<IUserLoginIntRepository>();

        // calling standard EF repo provider
        private IEFRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        // calling custom repo provier
        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        // try to find repository for type T
        public T GetRepository<T>() where T : class
        {
            var res = GetRepo<T>() ?? GetStandardRepo<T>() as T;
            if (res == null)
            {
                throw new NotImplementedException("No repository for type, " + typeof(T).FullName);
            }
            return res;
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        #endregion

    }
}