using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.UI.Events
{
    public class AfterSaveCompanyEvent : PubSubEvent<Company> { }
    public class AfterSaveCompanyFormEvent : PubSubEvent<CompanyForm> { }
    public class AfterSaveFacilityEvent : PubSubEvent<Facility> { }
    public class AfterSaveFacilityTypeEvent : PubSubEvent<FacilityType> { }
    public class AfterSaveParameterEvent : PubSubEvent<Parameter> { }
    public class AfterSaveParameterGroupEvent : PubSubEvent<ParameterGroup> { }
    public class AfterSaveProductEvent : PubSubEvent<Product> { }
    public class AfterSaveActivityFieldEvent : PubSubEvent<ActivityField> { }
    public class AfterSaveOfferEvent : PubSubEvent<Offer> { }
    public class AfterSaveProjectEvent : PubSubEvent<Project> { }
    public class AfterSaveProjectUnitEvent : PubSubEvent<ProjectUnit> { }
    public class AfterSaveTenderEvent : PubSubEvent<Tender> { }
}
