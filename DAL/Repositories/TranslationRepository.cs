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
    public class TranslationRepository : EFRepository<Translation>, ITranslationRepository
    {
        public TranslationRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
