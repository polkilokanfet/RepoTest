using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Lookup
{
    public partial class PaymentConditionSetLookup
    {
        public List<PaymentConditionLookup> PaymentConditions { get; set; }

        public override async Task LoadOther(IUnitOfWork unitOfWork)
        {
            PaymentConditions = Entity.PaymentConditions.Select(x => new PaymentConditionLookup(x)).ToList();
            foreach (var paymentConditionLookup in PaymentConditions)
                await paymentConditionLookup.LoadOther(unitOfWork);
        }
    }
}