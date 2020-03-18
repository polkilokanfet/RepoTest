using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class PaymentConditionSetLookup
    {
        [Designation("Условия оплаты")]
        public string Name => Entity.ToString();
    }
}