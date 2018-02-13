using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.UI.Events
{
	public partial class AfterSelectAddressEvent : PubSubEvent<PubSubEventArgs<Address>> { }
	public partial class AfterSelectCountryEvent : PubSubEvent<PubSubEventArgs<Country>> { }
	public partial class AfterSelectDistrictEvent : PubSubEvent<PubSubEventArgs<District>> { }
	public partial class AfterSelectLocalityEvent : PubSubEvent<PubSubEventArgs<Locality>> { }
	public partial class AfterSelectLocalityTypeEvent : PubSubEvent<PubSubEventArgs<LocalityType>> { }
	public partial class AfterSelectRegionEvent : PubSubEvent<PubSubEventArgs<Region>> { }
	public partial class AfterSelectAdditionalSalesUnitsEvent : PubSubEvent<PubSubEventArgs<AdditionalSalesUnits>> { }
	public partial class AfterSelectBankDetailsEvent : PubSubEvent<PubSubEventArgs<BankDetails>> { }
	public partial class AfterSelectCompanyEvent : PubSubEvent<PubSubEventArgs<Company>> { }
	public partial class AfterSelectCompanyFormEvent : PubSubEvent<PubSubEventArgs<CompanyForm>> { }
	public partial class AfterSelectDocumentsRegistrationDetailsEvent : PubSubEvent<PubSubEventArgs<DocumentsRegistrationDetails>> { }
	public partial class AfterSelectEmployeesPositionEvent : PubSubEvent<PubSubEventArgs<EmployeesPosition>> { }
	public partial class AfterSelectFacilityTypeEvent : PubSubEvent<PubSubEventArgs<FacilityType>> { }
	public partial class AfterSelectActivityFieldEvent : PubSubEvent<PubSubEventArgs<ActivityField>> { }
	public partial class AfterSelectContractEvent : PubSubEvent<PubSubEventArgs<Contract>> { }
	public partial class AfterSelectMeasureEvent : PubSubEvent<PubSubEventArgs<Measure>> { }
	public partial class AfterSelectParameterEvent : PubSubEvent<PubSubEventArgs<Parameter>> { }
	public partial class AfterSelectParameterGroupEvent : PubSubEvent<PubSubEventArgs<ParameterGroup>> { }
	public partial class AfterSelectProductRelationEvent : PubSubEvent<PubSubEventArgs<ProductRelation>> { }
	public partial class AfterSelectStandartPaymentConditionsEvent : PubSubEvent<PubSubEventArgs<StandartPaymentConditions>> { }
	public partial class AfterSelectPersonEvent : PubSubEvent<PubSubEventArgs<Person>> { }
	public partial class AfterSelectPaymentPlannedEvent : PubSubEvent<PubSubEventArgs<PaymentPlanned>> { }
	public partial class AfterSelectPaymentActualEvent : PubSubEvent<PubSubEventArgs<PaymentActual>> { }
	public partial class AfterSelectParameterRelationEvent : PubSubEvent<PubSubEventArgs<ParameterRelation>> { }
	public partial class AfterSelectProjectUnitEvent : PubSubEvent<PubSubEventArgs<ProjectUnit>> { }
	public partial class AfterSelectShipmentUnitEvent : PubSubEvent<PubSubEventArgs<ShipmentUnit>> { }
	public partial class AfterSelectProductionUnitEvent : PubSubEvent<PubSubEventArgs<ProductionUnit>> { }
	public partial class AfterSelectSalesUnitEvent : PubSubEvent<PubSubEventArgs<SalesUnit>> { }
	public partial class AfterSelectTestFriendAddressEvent : PubSubEvent<PubSubEventArgs<TestFriendAddress>> { }
	public partial class AfterSelectTestFriendEvent : PubSubEvent<PubSubEventArgs<TestFriend>> { }
	public partial class AfterSelectTestFriendEmailEvent : PubSubEvent<PubSubEventArgs<TestFriendEmail>> { }
	public partial class AfterSelectTestFriendGroupEvent : PubSubEvent<PubSubEventArgs<TestFriendGroup>> { }
	public partial class AfterSelectDocumentEvent : PubSubEvent<PubSubEventArgs<Document>> { }
	public partial class AfterSelectTestEntityEvent : PubSubEvent<PubSubEventArgs<TestEntity>> { }
	public partial class AfterSelectTestHusbandEvent : PubSubEvent<PubSubEventArgs<TestHusband>> { }
	public partial class AfterSelectTestWifeEvent : PubSubEvent<PubSubEventArgs<TestWife>> { }
	public partial class AfterSelectTestChildEvent : PubSubEvent<PubSubEventArgs<TestChild>> { }
	public partial class AfterSelectCostOnDateEvent : PubSubEvent<PubSubEventArgs<CostOnDate>> { }
	public partial class AfterSelectCostEvent : PubSubEvent<PubSubEventArgs<Cost>> { }
	public partial class AfterSelectCurrencyEvent : PubSubEvent<PubSubEventArgs<Currency>> { }
	public partial class AfterSelectExchangeCurrencyRateEvent : PubSubEvent<PubSubEventArgs<ExchangeCurrencyRate>> { }
	public partial class AfterSelectProductEvent : PubSubEvent<PubSubEventArgs<Product>> { }
	public partial class AfterSelectOfferEvent : PubSubEvent<PubSubEventArgs<Offer>> { }
	public partial class AfterSelectEmployeeEvent : PubSubEvent<PubSubEventArgs<Employee>> { }
	public partial class AfterSelectOrderEvent : PubSubEvent<PubSubEventArgs<Order>> { }
	public partial class AfterSelectPaymentConditionEvent : PubSubEvent<PubSubEventArgs<PaymentCondition>> { }
	public partial class AfterSelectPaymentDocumentEvent : PubSubEvent<PubSubEventArgs<PaymentDocument>> { }
	public partial class AfterSelectFacilityEvent : PubSubEvent<PubSubEventArgs<Facility>> { }
	public partial class AfterSelectProjectEvent : PubSubEvent<PubSubEventArgs<Project>> { }
	public partial class AfterSelectUserRoleEvent : PubSubEvent<PubSubEventArgs<UserRole>> { }
	public partial class AfterSelectSpecificationEvent : PubSubEvent<PubSubEventArgs<Specification>> { }
	public partial class AfterSelectTenderEvent : PubSubEvent<PubSubEventArgs<Tender>> { }
	public partial class AfterSelectTenderTypeEvent : PubSubEvent<PubSubEventArgs<TenderType>> { }
	public partial class AfterSelectUserEvent : PubSubEvent<PubSubEventArgs<User>> { }
	public partial class AfterSelectOfferUnitEvent : PubSubEvent<PubSubEventArgs<OfferUnit>> { }
	public partial class AfterSelectProductBlockEvent : PubSubEvent<PubSubEventArgs<ProductBlock>> { }
}
