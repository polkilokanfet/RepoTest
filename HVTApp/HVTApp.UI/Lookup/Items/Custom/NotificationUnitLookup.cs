using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class NotificationUnitLookup
    {
        [Designation("Действие"), OrderStatus(-10)]
        public string ActionString => this.Entity.GetActionString();
    }
}