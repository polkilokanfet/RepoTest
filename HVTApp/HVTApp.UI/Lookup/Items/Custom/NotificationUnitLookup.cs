using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class NotificationUnitLookup
    {
        [Designation("��������"), OrderStatus(-10)]
        public string ActionString => this.Entity.GetActionString();
    }
}