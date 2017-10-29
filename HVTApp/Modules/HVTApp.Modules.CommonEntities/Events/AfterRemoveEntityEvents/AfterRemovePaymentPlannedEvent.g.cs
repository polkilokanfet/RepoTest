using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.UI.Events
{
	public partial class AfterRemovePaymentPlannedEvent : PubSubEvent<PaymentPlanned> { }
}
