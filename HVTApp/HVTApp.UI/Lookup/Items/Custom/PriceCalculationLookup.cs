using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class PriceCalculationLookup
    {
        [Designation("Менеджер"), OrderStatus(-100)]
        public string Manager => this.Entity.PriceCalculationItems.FirstOrDefault()?.SalesUnits.FirstOrDefault()?.Project.Manager.ToString();
    }
}