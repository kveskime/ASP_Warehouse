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
    public class WarehouseRepository : EFRepository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
