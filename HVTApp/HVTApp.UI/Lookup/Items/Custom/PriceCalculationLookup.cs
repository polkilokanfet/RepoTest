using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.UI.Converter;

namespace HVTApp.UI.Lookup
{
    public partial class PriceCalculationLookup
    {
        [Designation("Менеджер"), OrderStatus(-100)]
        public string Manager => this.Entity.PriceCalculationItems.FirstOrDefault()?.SalesUnits.FirstOrDefault()?.Project.Manager.ToString();

        [Designation("З/з"), OrderStatus(-200)]
        public string Orders => this.Entity.PriceCalculationItems.SelectMany(item => item.SalesUnits).Select(salesUnit => salesUnit.Order).Distinct().ToStringEnum();

        public string Status
        {
            get
            {
                if (this.Entity.LastHistoryItem == null)
                {
                    return "Инициализация";
                }

                return (string)(new PriceCalculationHistoryItemTypeToStringConverter().Convert(this.Entity.LastHistoryItem.Type, null, null, null));
            }
        }

        [Designation("Номенклатура"), OrderStatus(-300)]
        public string ProductsInCalculation => this.PriceCalculationItems.SelectMany(x => x.Entity.SalesUnits).Select(x => x.Product.Designation).Distinct().OrderBy(x => x).ToStringEnum();
    }
}