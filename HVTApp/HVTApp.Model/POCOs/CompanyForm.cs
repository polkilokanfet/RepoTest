using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class CompanyForm : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }

        public override string ToString()
        {
            return $"{ShortName} ({FullName})";
        }
    }
}
