using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class CompanyFormLookupDataService : LookupDataService<CompanyFormLookup, CompanyForm>, ICompanyFormLookupDataService
    {
        public CompanyFormLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}