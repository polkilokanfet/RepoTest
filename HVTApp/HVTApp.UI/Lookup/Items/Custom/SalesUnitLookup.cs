using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Lookup
{
    public partial class SalesUnitLookup : IUnitLookup
    {
        public List<PaymentActualLookup> PaymentsActual { get; set; }

        /// <summary>
        /// Оплаченная сумма
        /// </summary>
        public double SumPaid => PaymentsActual.Sum(x => x.Sum);

        /// <summary>
        /// Неоплаченная сумма
        /// </summary>
        public double SumNotPaid => Cost - SumPaid;

        public override async Task LoadOther(IUnitOfWork unitOfWork)
        {
            PaymentsActual = Entity.PaymentsActual.Select(x => new PaymentActualLookup(x)).ToList();
            foreach (var paymentActualLookup in PaymentsActual)
                await paymentActualLookup.LoadOther(unitOfWork);
        }
    }
}