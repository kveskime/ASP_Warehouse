﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPurchaseUOW
    {
        //save pending changes to the data store
        void Commit();

        //get repository for type
        T GetRepository<T>() where T : class;

        //Standard repos, autogenerated
        //IEFRepository<Product> Products { get; }
        //IEFRepository<ProductInPurchase> ProductsInPurchases { get; }

        //Customs repos, manually implemented
        //add it also to EFRepositoryFactories.cs, in method GetCustomFactories
        //IPersonRepository Persons { get; }
        IProductRepository Products { get; }
        IProductInPurchaseRepository ProductInPurchases { get; }
        ISupplierRepository Suppliers { get; }
        IPurchaseRepository Purchases { get; }
        IProductTypeRepository ProductTypes{ get; }

    }
}
