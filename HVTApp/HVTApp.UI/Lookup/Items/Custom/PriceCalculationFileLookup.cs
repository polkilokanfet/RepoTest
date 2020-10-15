using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class PriceCalculationFileLookup
    {
        [Designation("�����"), OrderStatus(10)]
        public string Time => this.Entity.CreationMoment.ToShortTimeString();
    }
}