using System.Collections.Generic;

namespace HVTApp.DataAccess.Lookup
{
    public class CompanyLookup : LookupItem
    {
        public CompanyLookup ParentCompany { get; set; }
        public IEnumerable<CompanyLookup> ChildCompanies { get; set; }
    }
}