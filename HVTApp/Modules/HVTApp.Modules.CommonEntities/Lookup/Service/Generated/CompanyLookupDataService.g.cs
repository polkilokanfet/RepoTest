using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class CompanyLookupDataService : LookupDataService<CompanyLookup, Company>, ICompanyLookupDataService
    {
        public CompanyLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
