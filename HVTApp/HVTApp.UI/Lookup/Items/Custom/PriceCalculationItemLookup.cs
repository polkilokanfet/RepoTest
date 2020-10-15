using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class PriceCalculationItemLookup
    {
        public SalesUnit SalesUnit => SalesUnits.First()?.Entity;
        [Designation("Объект")]
        public Facility Facility => SalesUnit?.Facility;
        [Designation("Продукт")]
        public Product Product => SalesUnit?.Product;
        [Designation("Количество")]
        public int Amount => SalesUnits.Count;
        [Designation("Стоимость единицы")]
        public double? UnitPrice => this.StructureCosts.Sum(x => x.Total);
    }
}