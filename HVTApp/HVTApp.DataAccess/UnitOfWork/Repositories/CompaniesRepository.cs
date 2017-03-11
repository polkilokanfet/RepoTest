using System.Data.Entity;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;

namespace HVTApp.DataAccess
{
    public class CompaniesRepository : BaseRepository<Company>, ICompaniesRepository
    {
        public CompaniesRepository(DbContext context) : base(context)
        {
        }
    }
}