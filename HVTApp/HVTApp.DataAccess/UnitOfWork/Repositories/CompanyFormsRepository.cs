using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.Wrapper;

namespace HVTApp.DataAccess
{
    public class CompanyFormsRepository : BaseRepository<CompanyForm, CompanyFormWrapper>, ICompanyFormsRepository
    {
        public CompanyFormsRepository(DbContext context, Dictionary<IBaseEntity, object> repository) : base(context, repository)
        {
        }
    }
}