using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain.Models;

namespace DAL.Repositories
{
    public class MultiLangStringRepository : EFRepository<MultiLangString>, IMultiLangStringRepository
    {
        public MultiLangStringRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
