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
    public class WorkTypeRepository : EFRepository<WorkType>, IWorkTypeRepository
    {
        public WorkTypeRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
