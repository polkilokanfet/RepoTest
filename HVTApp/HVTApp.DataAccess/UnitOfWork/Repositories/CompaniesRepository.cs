using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;

namespace HVTApp.DataAccess
{
    public class CompaniesRepository : BaseRepository<Company>, ICompaniesRepository
    {
        public CompaniesRepository(DbContext context) : base(context)
        {
        }

        public override List<Company> GetAll()
        {
            List<Company> companies =  base.GetAll();
            companies.ForEach(parent => parent.ChildCompanies = companies.Where(child => Equals(child.ParentCompany, parent)).ToList());
            return companies;
        }
    }
}