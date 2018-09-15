













using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.Model.Events
{
	public partial class AfterSaveCreateNewProductTaskEvent : PubSubEvent<CreateNewProductTask> { }
	public partial class AfterSaveDocumentIncomingNumberEvent : PubSubEvent<DocumentIncomingNumber> { }
	public partial class AfterSaveDocumentOutgoingNumberEvent : PubSubEvent<DocumentOutgoingNumber> { }
	public partial class AfterSavePaymentActualEvent : PubSubEvent<PaymentActual> { }
	public partial class AfterSavePaymentPlannedEvent : PubSubEvent<PaymentPlanned> { }
	public partial class AfterSaveProductBlockIsServiceEvent : PubSubEvent<ProductBlockIsService> { }
	public partial class AfterSaveProductIncludedEvent : PubSubEvent<ProductIncluded> { }
	public partial class AfterSaveProductDesignationEvent : PubSubEvent<ProductDesignation> { }
	public partial class AfterSaveProductTypeEvent : PubSubEvent<ProductType> { }
	public partial class AfterSaveProductTypeDesignationEvent : PubSubEvent<ProductTypeDesignation> { }
	public partial class AfterSaveProjectTypeEvent : PubSubEvent<ProjectType> { }
	public partial class AfterSaveCommonOptionEvent : PubSubEvent<CommonOption> { }
	public partial class AfterSaveAddressEvent : PubSubEvent<Address> { }
	public partial class AfterSaveCountryEvent : PubSubEvent<Country> { }
	public partial class AfterSaveDistrictEvent : PubSubEvent<District> { }
	public partial class AfterSaveLocalityEvent : PubSubEvent<Locality> { }
	public partial class AfterSaveLocalityTypeEvent : PubSubEvent<LocalityType> { }
	public partial class AfterSaveRegionEvent : PubSubEvent<Region> { }
	public partial class AfterSaveCalculatePriceTaskEvent : PubSubEvent<CalculatePriceTask> { }
	public partial class AfterSaveSumEvent : PubSubEvent<Sum> { }
	public partial class AfterSaveCurrencyExchangeRateEvent : PubSubEvent<CurrencyExchangeRate> { }
	public partial class AfterSaveDescribeProductBlockTaskEvent : PubSubEvent<DescribeProductBlockTask> { }
	public partial class AfterSaveNoteEvent : PubSubEvent<Note> { }
	public partial class AfterSaveOfferUnitEvent : PubSubEvent<OfferUnit> { }
	public partial class AfterSavePaymentConditionSetEvent : PubSubEvent<PaymentConditionSet> { }
	public partial class AfterSaveProductBlockEvent : PubSubEvent<ProductBlock> { }
	public partial class AfterSaveProductDependentEvent : PubSubEvent<ProductDependent> { }
	public partial class AfterSaveProductionTaskEvent : PubSubEvent<ProductionTask> { }
	public partial class AfterSaveSalesBlockEvent : PubSubEvent<SalesBlock> { }
	public partial class AfterSaveBankDetailsEvent : PubSubEvent<BankDetails> { }
	public partial class AfterSaveCompanyEvent : PubSubEvent<Company> { }
	public partial class AfterSaveCompanyFormEvent : PubSubEvent<CompanyForm> { }
	public partial class AfterSaveDocumentsRegistrationDetailsEvent : PubSubEvent<DocumentsRegistrationDetails> { }
	public partial class AfterSaveEmployeesPositionEvent : PubSubEvent<EmployeesPosition> { }
	public partial class AfterSaveFacilityTypeEvent : PubSubEvent<FacilityType> { }
	public partial class AfterSaveActivityFieldEvent : PubSubEvent<ActivityField> { }
	public partial class AfterSaveContractEvent : PubSubEvent<Contract> { }
	public partial class AfterSaveMeasureEvent : PubSubEvent<Measure> { }
	public partial class AfterSaveParameterEvent : PubSubEvent<Parameter> { }
	public partial class AfterSaveParameterGroupEvent : PubSubEvent<ParameterGroup> { }
	public partial class AfterSaveProductRelationEvent : PubSubEvent<ProductRelation> { }
	public partial class AfterSavePersonEvent : PubSubEvent<Person> { }
	public partial class AfterSaveParameterRelationEvent : PubSubEvent<ParameterRelation> { }
	public partial class AfterSaveSalesUnitEvent : PubSubEvent<SalesUnit> { }
	public partial class AfterSaveTestFriendAddressEvent : PubSubEvent<TestFriendAddress> { }
	public partial class AfterSaveTestFriendEvent : PubSubEvent<TestFriend> { }
	public partial class AfterSaveTestFriendEmailEvent : PubSubEvent<TestFriendEmail> { }
	public partial class AfterSaveTestFriendGroupEvent : PubSubEvent<TestFriendGroup> { }
	public partial class AfterSaveDocumentEvent : PubSubEvent<Document> { }
	public partial class AfterSaveDocumentNumberEvent : PubSubEvent<DocumentNumber> { }
	public partial class AfterSaveTestEntityEvent : PubSubEvent<TestEntity> { }
	public partial class AfterSaveTestHusbandEvent : PubSubEvent<TestHusband> { }
	public partial class AfterSaveTestWifeEvent : PubSubEvent<TestWife> { }
	public partial class AfterSaveTestChildEvent : PubSubEvent<TestChild> { }
	public partial class AfterSaveSumOnDateEvent : PubSubEvent<SumOnDate> { }
	public partial class AfterSaveProductEvent : PubSubEvent<Product> { }
	public partial class AfterSaveOfferEvent : PubSubEvent<Offer> { }
	public partial class AfterSaveEmployeeEvent : PubSubEvent<Employee> { }
	public partial class AfterSaveOrderEvent : PubSubEvent<Order> { }
	public partial class AfterSavePaymentConditionEvent : PubSubEvent<PaymentCondition> { }
	public partial class AfterSavePaymentDocumentEvent : PubSubEvent<PaymentDocument> { }
	public partial class AfterSaveFacilityEvent : PubSubEvent<Facility> { }
	public partial class AfterSaveProjectEvent : PubSubEvent<Project> { }
	public partial class AfterSaveUserRoleEvent : PubSubEvent<UserRole> { }
	public partial class AfterSaveSpecificationEvent : PubSubEvent<Specification> { }
	public partial class AfterSaveTenderEvent : PubSubEvent<Tender> { }
	public partial class AfterSaveTenderTypeEvent : PubSubEvent<TenderType> { }
	public partial class AfterSaveUserEvent : PubSubEvent<User> { }
}
