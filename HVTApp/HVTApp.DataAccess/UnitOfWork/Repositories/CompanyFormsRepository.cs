using System.Data.Entity;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;

namespace HVTApp.DataAccess
{
    public class CompanyFormsRepository : BaseRepository<CompanyForm>, ICompanyFormsRepository
    {
        public CompanyFormsRepository(DbContext context) : base(context)
        {
        }
    }
}