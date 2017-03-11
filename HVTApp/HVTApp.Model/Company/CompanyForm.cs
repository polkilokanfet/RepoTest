using System.Collections.Generic;

namespace HVTApp.Model
{
    public class CompanyForm : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }

        public override string ToString()
        {
            return $"{ShortName} ({FullName})";
        }
    }
}
