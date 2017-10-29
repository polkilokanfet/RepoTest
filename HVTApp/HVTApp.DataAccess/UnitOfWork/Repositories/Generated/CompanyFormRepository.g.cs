using System.Data.Entity;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class CompanyFormRepository : BaseRepository<CompanyForm>, ICompanyFormRepository
    {
        public CompanyFormRepository(DbContext context) : base(context)
        {
        }
    }
}
