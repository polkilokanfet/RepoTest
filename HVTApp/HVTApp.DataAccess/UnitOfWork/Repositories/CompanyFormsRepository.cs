using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class CompanyFormsRepository : BaseRepository<CompanyForm, CompanyFormWrapper>, ICompanyFormsRepository
    {
        public CompanyFormsRepository(DbContext context, IGetWrapper wrappersGetter) : base(context, wrappersGetter)
        {
        }
    }
}