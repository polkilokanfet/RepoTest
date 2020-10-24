using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.UI.Lookup
{
    public partial class PriceCalculationLookup
    {
        [Designation("Менеджер"), OrderStatus(-100)]
        public string Manager => this.Entity.PriceCalculationItems.FirstOrDefault()?.SalesUnits.FirstOrDefault()?.Project.Manager.ToString();

        [Designation("З/з"), OrderStatus(-200)]
        public string Orders => this.Entity.PriceCalculationItems.SelectMany(x => x.SalesUnits).Select(x => x.Order).Distinct().ToStringEnum();

        [Designation("Номенклатура"), OrderStatus(-300)]
        public string ProductsInCalculation => this.PriceCalculationItems.SelectMany(x => x.Entity.SalesUnits).Select(x => x.Product.Designation).Distinct().OrderBy(x => x).ToStringEnum();

    }
}