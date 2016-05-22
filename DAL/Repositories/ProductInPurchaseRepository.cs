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
    public class ProductInPurchaseRepository : EFRepository<ProductInPurchase>, IProductInPurchaseRepository
    {
        public ProductInPurchaseRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public List<ProductInPurchase> GetByPurchaseId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
