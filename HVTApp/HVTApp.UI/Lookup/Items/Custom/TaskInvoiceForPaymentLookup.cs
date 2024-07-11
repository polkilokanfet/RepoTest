using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;

namespace HVTApp.UI.Lookup
{
    public partial class TaskInvoiceForPaymentLookup
    {
        [Designation("Объект"), OrderStatus(100)]
        public string Facilities =>
            this.Entity.Items
                .SelectMany(item => item.PriceEngineeringTask.SalesUnits)
                .Select(salesUnit => salesUnit.Facility)
                .Distinct()
                .ToStringEnum();

        [Designation("Заказ"), OrderStatus(90)]
        public string Orders =>
            this.Entity.Items
                .SelectMany(item => item.PriceEngineeringTask.SalesUnits)
                .Select(salesUnit => salesUnit.Order?.Number)
                .Distinct()
                .ToStringEnum();

        public bool IsActual
        {
            get
            {
                if (GlobalAppProperties.UserIsBackManagerBoss)
                    return this.Entity.MomentStart != null && this.Entity.BackManager == null;

                if (GlobalAppProperties.UserIsBackManager)
                    return this.Entity.MomentStart != null && this.Entity.MomentFinish == null;

                return true;
            }
        }
    }
}