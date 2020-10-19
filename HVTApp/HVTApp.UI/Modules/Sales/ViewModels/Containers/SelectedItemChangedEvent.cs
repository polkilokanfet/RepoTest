using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class SelectedProjectChangedEvent : PubSubEvent<Project> { }
    public class SelectedOfferChangedEvent : PubSubEvent<Offer> { }
    public class SelectedTenderChangedEvent : PubSubEvent<Tender> { }
    public class SelectedTechnicalRequrementsTaskChangedEvent : PubSubEvent<TechnicalRequrementsTask> { }
    public class SelectedPriceCalculationChangedEvent : PubSubEvent<PriceCalculation> { }
    public class SelectedSalesUnitChangedEvent : PubSubEvent<SalesUnit> { }
    public class SelectedSpecificationChangedEvent : PubSubEvent<Specification> { }
}