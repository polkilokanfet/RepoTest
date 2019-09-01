using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SelectedProjectChangedEvent : PubSubEvent<Project> { }
    public class SelectedOfferChangedEvent : PubSubEvent<Offer> { }
    public class SelectedTenderChangedEvent : PubSubEvent<Tender> { }
    public class SelectedSalesUnitChangedEvent : PubSubEvent<SalesUnit> { }
    public class SelectedSpecificationChangedEvent : PubSubEvent<Specification> { }
}