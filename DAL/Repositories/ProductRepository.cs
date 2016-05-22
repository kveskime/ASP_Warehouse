using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
