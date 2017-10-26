using System.Collections.Generic;

namespace HVTApp.UI.Lookup
{
    public class CompanyLookup : LookupItem
    {
        public CompanyFormLookup CompanyForm { get; set; }
        public CompanyLookup ParentCompany { get; set; }
        public IEnumerable<CompanyLookup> ChildCompanies { get; set; }
    }
}