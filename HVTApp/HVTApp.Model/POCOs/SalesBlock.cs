using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class SalesBlock : BaseEntity
    {
        public virtual List<SalesUnit> ParentSalesUnits { get; set; } = new List<SalesUnit>();
        public virtual List<SalesUnit> ChildSalesUnits { get; set; } = new List<SalesUnit>();
    }
}