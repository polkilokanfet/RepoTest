using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class PaymentConditionSetLookup
    {
        [Designation("������� ������")]
        public string Name => Entity.ToString();
    }
}