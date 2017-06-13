using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class CompaniesRepository : BaseRepository<Company, CompanyWrapper>, ICompaniesRepository
    {
        public CompaniesRepository(DbContext context, Dictionary<IBaseEntity, IWrapper<IBaseEntity>> wrappersRepository) : base(context, wrappersRepository)
        {
        }
    }
}