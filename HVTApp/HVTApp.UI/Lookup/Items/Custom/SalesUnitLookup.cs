using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class SalesUnitLookup : IUnitLookup
    {
        public override async Task LoadOther(IUnitOfWork unitOfWork)
        {
            ProductsIncluded = Entity.ProductsIncluded.Select(x => new ProductIncludedLookup(x)).ToList();
            foreach (var productIncluded in ProductsIncluded)
                await productIncluded.LoadOther(unitOfWork);

            PaymentsActual = Entity.PaymentsActual.Select(x => new PaymentActualLookup(x)).ToList();
            foreach (var paymentActualLookup in PaymentsActual)
                await paymentActualLookup.LoadOther(unitOfWork);

            PaymentsPlanned = Entity.PaymentsPlanned.Select(x => new PaymentPlannedLookup(x)).ToList();
            foreach (var paymentPlannedLookup in PaymentsPlanned)
                await paymentPlannedLookup.LoadOther(unitOfWork);
            //проставляем в сохраненных платежах суммы
            PaymentsPlanned.ForEach(x => x.Sum = Cost * x.Part * x.Condition.Part);

            await PaymentConditionSet.LoadOther(unitOfWork);
        }

        [Designation("Совершённые платежи")]
        public List<PaymentActualLookup> PaymentsActual { get; private set; }

        [Designation("Планируемые платежи")]
        public List<PaymentPlannedLookup> PaymentsPlanned { get; private set; }

        [Designation("Включенные в стоимость продукты")]
        public List<ProductIncludedLookup> ProductsIncluded { get; private set; }
    }
}