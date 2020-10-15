using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class PriceCalculationItemLookup
    {
        public SalesUnit SalesUnit => SalesUnits.First()?.Entity;
        [Designation("������")]
        public Facility Facility => SalesUnit?.Facility;
        [Designation("�������")]
        public Product Product => SalesUnit?.Product;
        [Designation("����������")]
        public int Amount => SalesUnits.Count;
        [Designation("��������� �������")]
        public double? UnitPrice => this.StructureCosts.Sum(x => x.Total);
    }
}