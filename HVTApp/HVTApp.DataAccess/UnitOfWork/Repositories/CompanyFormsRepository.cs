using System.Data.Entity;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.Wrapper;

namespace HVTApp.DataAccess
{
    public class CompanyFormsRepository : BaseRepository<CompanyForm, CompanyFormWrapper>, ICompanyFormsRepository
    {
        public CompanyFormsRepository(DbContext context) : base(context)
        {
        }
    }
}