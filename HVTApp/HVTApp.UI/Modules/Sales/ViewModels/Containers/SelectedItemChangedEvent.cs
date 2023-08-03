using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class SelectedOfferChangedEvent : PubSubEvent<Offer> { }
    public class SelectedTenderChangedEvent : PubSubEvent<Tender> { }
    public class SelectedSpecificationChangedEvent : PubSubEvent<Specification> { }
}