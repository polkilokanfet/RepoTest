using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class AdditionalSalesUnits : BaseEntity
    {
        public virtual List<SalesUnit> ParentSalesUnits { get; set; } = new List<SalesUnit>();
        public virtual SalesUnit AdditionalSalesUnit { get; set; }
    }
}