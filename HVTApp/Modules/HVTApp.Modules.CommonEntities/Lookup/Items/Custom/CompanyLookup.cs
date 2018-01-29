using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class CompanyLookup : LookupItem
    {
        private string _fullName;
        private string _shortName;

        public string FullName
        {
            get { return _fullName; }
            set { SetSimpleProperty(ref _fullName, value); }
        }

        public string ShortName
        {
            get { return _shortName; }
            set { SetSimpleProperty(ref _shortName, value); }
        }


        public CompanyFormLookup CompanyForm { get; set; }
        public CompanyLookup ParentCompany { get; set; }
    }
}