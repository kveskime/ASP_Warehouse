using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;
using Domain.Models;

namespace DAL.Repositories
{
    public class ProductTypeRepository : EFRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
