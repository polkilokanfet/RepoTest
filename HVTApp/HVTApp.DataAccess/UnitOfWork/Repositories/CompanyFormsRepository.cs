using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class CompanyFormsRepository : BaseRepository<CompanyForm>, ICompanyFormsRepository
    {
        public CompanyFormsRepository(DbContext context) : base(context)
        {
        }
    }
}