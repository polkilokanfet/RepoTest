using System.Collections.Generic;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class SalesUnitLookup : IUnitLookup
    {
        [Designation("Совершённые платежи")]
        public List<PaymentActualLookup> PaymentsActual { get; private set; }

        [Designation("Планируемые платежи")]
        public List<PaymentPlannedLookup> PaymentsPlanned { get; private set; }

        [Designation("Включенные в стоимость продукты")]
        public List<ProductIncludedLookup> ProductsIncluded { get; private set; }
    }
}