using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class CompanyForm : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        //public virtual List<Company> Companies { get; set; } = new List<Company>();

        public override string ToString()
        {
            return $"{ShortName} ({FullName})";
        }
    }
}
