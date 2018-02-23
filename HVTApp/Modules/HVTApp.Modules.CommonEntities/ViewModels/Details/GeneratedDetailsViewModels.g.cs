using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{

    public partial class CommonOptionDetailsViewModel : BaseDetailsViewModel<CommonOptionWrapper, CommonOption, AfterSaveCommonOptionEvent>
    {

        public CommonOptionDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class AddressDetailsViewModel : BaseDetailsViewModel<AddressWrapper, Address, AfterSaveAddressEvent>
    {
		public ICommand SelectLocalityCommand { get; }
		public ICommand ClearLocalityCommand { get; }


        public AddressDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectLocalityCommand = new DelegateCommand(SelectLocalityCommand_Execute);
			ClearLocalityCommand = new DelegateCommand(ClearLocalityCommand_Execute);


		}
		private async void SelectLocalityCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Locality>().GetAllAsync();
            SelectAndSetWrapper<Locality, LocalityWrapper>(entities, nameof(Item.Locality), Item.Locality?.Id);
		}

		private void ClearLocalityCommand_Execute() 
		{
		    Item.Locality = null;
		}



    }


    public partial class CountryDetailsViewModel : BaseDetailsViewModel<CountryWrapper, Country, AfterSaveCountryEvent>
    {

        public CountryDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class DistrictDetailsViewModel : BaseDetailsViewModel<DistrictWrapper, District, AfterSaveDistrictEvent>
    {
		public ICommand SelectCountryCommand { get; }
		public ICommand ClearCountryCommand { get; }


        public DistrictDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectCountryCommand = new DelegateCommand(SelectCountryCommand_Execute);
			ClearCountryCommand = new DelegateCommand(ClearCountryCommand_Execute);


		}
		private async void SelectCountryCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Country>().GetAllAsync();
            SelectAndSetWrapper<Country, CountryWrapper>(entities, nameof(Item.Country), Item.Country?.Id);
		}

		private void ClearCountryCommand_Execute() 
		{
		    Item.Country = null;
		}



    }


    public partial class LocalityDetailsViewModel : BaseDetailsViewModel<LocalityWrapper, Locality, AfterSaveLocalityEvent>
    {
		public ICommand SelectLocalityTypeCommand { get; }
		public ICommand ClearLocalityTypeCommand { get; }

		public ICommand SelectRegionCommand { get; }
		public ICommand ClearRegionCommand { get; }


        public LocalityDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectLocalityTypeCommand = new DelegateCommand(SelectLocalityTypeCommand_Execute);
			ClearLocalityTypeCommand = new DelegateCommand(ClearLocalityTypeCommand_Execute);

			SelectRegionCommand = new DelegateCommand(SelectRegionCommand_Execute);
			ClearRegionCommand = new DelegateCommand(ClearRegionCommand_Execute);


		}
		private async void SelectLocalityTypeCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<LocalityType>().GetAllAsync();
            SelectAndSetWrapper<LocalityType, LocalityTypeWrapper>(entities, nameof(Item.LocalityType), Item.LocalityType?.Id);
		}

		private void ClearLocalityTypeCommand_Execute() 
		{
		    Item.LocalityType = null;
		}

		private async void SelectRegionCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Region>().GetAllAsync();
            SelectAndSetWrapper<Region, RegionWrapper>(entities, nameof(Item.Region), Item.Region?.Id);
		}

		private void ClearRegionCommand_Execute() 
		{
		    Item.Region = null;
		}



    }


    public partial class LocalityTypeDetailsViewModel : BaseDetailsViewModel<LocalityTypeWrapper, LocalityType, AfterSaveLocalityTypeEvent>
    {

        public LocalityTypeDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class RegionDetailsViewModel : BaseDetailsViewModel<RegionWrapper, Region, AfterSaveRegionEvent>
    {
		public ICommand SelectDistrictCommand { get; }
		public ICommand ClearDistrictCommand { get; }


        public RegionDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectDistrictCommand = new DelegateCommand(SelectDistrictCommand_Execute);
			ClearDistrictCommand = new DelegateCommand(ClearDistrictCommand_Execute);


		}
		private async void SelectDistrictCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<District>().GetAllAsync();
            SelectAndSetWrapper<District, DistrictWrapper>(entities, nameof(Item.District), Item.District?.Id);
		}

		private void ClearDistrictCommand_Execute() 
		{
		    Item.District = null;
		}



    }


    public partial class CalculatePriceTaskDetailsViewModel : BaseDetailsViewModel<CalculatePriceTaskWrapper, CalculatePriceTask, AfterSaveCalculatePriceTaskEvent>
    {
		public ICommand SelectProductBlockCommand { get; }
		public ICommand ClearProductBlockCommand { get; }


        public CalculatePriceTaskDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectProductBlockCommand = new DelegateCommand(SelectProductBlockCommand_Execute);
			ClearProductBlockCommand = new DelegateCommand(ClearProductBlockCommand_Execute);


		}
		private async void SelectProductBlockCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<ProductBlock>().GetAllAsync();
            SelectAndSetWrapper<ProductBlock, ProductBlockWrapper>(entities, nameof(Item.ProductBlock), Item.ProductBlock?.Id);
		}

		private void ClearProductBlockCommand_Execute() 
		{
		    Item.ProductBlock = null;
		}



    }


    public partial class DescribeProductBlockTaskDetailsViewModel : BaseDetailsViewModel<DescribeProductBlockTaskWrapper, DescribeProductBlockTask, AfterSaveDescribeProductBlockTaskEvent>
    {
		public ICommand SelectProductBlockCommand { get; }
		public ICommand ClearProductBlockCommand { get; }

		public ICommand SelectProductCommand { get; }
		public ICommand ClearProductCommand { get; }


        public DescribeProductBlockTaskDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectProductBlockCommand = new DelegateCommand(SelectProductBlockCommand_Execute);
			ClearProductBlockCommand = new DelegateCommand(ClearProductBlockCommand_Execute);

			SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute);
			ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute);


		}
		private async void SelectProductBlockCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<ProductBlock>().GetAllAsync();
            SelectAndSetWrapper<ProductBlock, ProductBlockWrapper>(entities, nameof(Item.ProductBlock), Item.ProductBlock?.Id);
		}

		private void ClearProductBlockCommand_Execute() 
		{
		    Item.ProductBlock = null;
		}

		private async void SelectProductCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Product>().GetAllAsync();
            SelectAndSetWrapper<Product, ProductWrapper>(entities, nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute() 
		{
		    Item.Product = null;
		}



    }


    public partial class SalesBlockDetailsViewModel : BaseDetailsViewModel<SalesBlockWrapper, SalesBlock, AfterSaveSalesBlockEvent>
    {

        public SalesBlockDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class BankDetailsDetailsViewModel : BaseDetailsViewModel<BankDetailsWrapper, BankDetails, AfterSaveBankDetailsEvent>
    {

        public BankDetailsDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class CompanyDetailsViewModel : BaseDetailsViewModel<CompanyWrapper, Company, AfterSaveCompanyEvent>
    {
		public ICommand SelectFormCommand { get; }
		public ICommand ClearFormCommand { get; }

		public ICommand SelectParentCompanyCommand { get; }
		public ICommand ClearParentCompanyCommand { get; }

		public ICommand SelectAddressLegalCommand { get; }
		public ICommand ClearAddressLegalCommand { get; }

		public ICommand SelectAddressPostCommand { get; }
		public ICommand ClearAddressPostCommand { get; }


        public CompanyDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectFormCommand = new DelegateCommand(SelectFormCommand_Execute);
			ClearFormCommand = new DelegateCommand(ClearFormCommand_Execute);

			SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_Execute);
			ClearParentCompanyCommand = new DelegateCommand(ClearParentCompanyCommand_Execute);

			SelectAddressLegalCommand = new DelegateCommand(SelectAddressLegalCommand_Execute);
			ClearAddressLegalCommand = new DelegateCommand(ClearAddressLegalCommand_Execute);

			SelectAddressPostCommand = new DelegateCommand(SelectAddressPostCommand_Execute);
			ClearAddressPostCommand = new DelegateCommand(ClearAddressPostCommand_Execute);


		}
		private async void SelectFormCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<CompanyForm>().GetAllAsync();
            SelectAndSetWrapper<CompanyForm, CompanyFormWrapper>(entities, nameof(Item.Form), Item.Form?.Id);
		}

		private void ClearFormCommand_Execute() 
		{
		    Item.Form = null;
		}

		private async void SelectParentCompanyCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Company>().GetAllAsync();
            SelectAndSetWrapper<Company, CompanyWrapper>(entities, nameof(Item.ParentCompany), Item.ParentCompany?.Id);
		}

		private void ClearParentCompanyCommand_Execute() 
		{
		    Item.ParentCompany = null;
		}

		private async void SelectAddressLegalCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Address>().GetAllAsync();
            SelectAndSetWrapper<Address, AddressWrapper>(entities, nameof(Item.AddressLegal), Item.AddressLegal?.Id);
		}

		private void ClearAddressLegalCommand_Execute() 
		{
		    Item.AddressLegal = null;
		}

		private async void SelectAddressPostCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Address>().GetAllAsync();
            SelectAndSetWrapper<Address, AddressWrapper>(entities, nameof(Item.AddressPost), Item.AddressPost?.Id);
		}

		private void ClearAddressPostCommand_Execute() 
		{
		    Item.AddressPost = null;
		}



    }


    public partial class CompanyFormDetailsViewModel : BaseDetailsViewModel<CompanyFormWrapper, CompanyForm, AfterSaveCompanyFormEvent>
    {

        public CompanyFormDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class DocumentsRegistrationDetailsDetailsViewModel : BaseDetailsViewModel<DocumentsRegistrationDetailsWrapper, DocumentsRegistrationDetails, AfterSaveDocumentsRegistrationDetailsEvent>
    {

        public DocumentsRegistrationDetailsDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class EmployeesPositionDetailsViewModel : BaseDetailsViewModel<EmployeesPositionWrapper, EmployeesPosition, AfterSaveEmployeesPositionEvent>
    {

        public EmployeesPositionDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class FacilityTypeDetailsViewModel : BaseDetailsViewModel<FacilityTypeWrapper, FacilityType, AfterSaveFacilityTypeEvent>
    {

        public FacilityTypeDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class ActivityFieldDetailsViewModel : BaseDetailsViewModel<ActivityFieldWrapper, ActivityField, AfterSaveActivityFieldEvent>
    {

        public ActivityFieldDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class ContractDetailsViewModel : BaseDetailsViewModel<ContractWrapper, Contract, AfterSaveContractEvent>
    {
		public ICommand SelectContragentCommand { get; }
		public ICommand ClearContragentCommand { get; }


        public ContractDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectContragentCommand = new DelegateCommand(SelectContragentCommand_Execute);
			ClearContragentCommand = new DelegateCommand(ClearContragentCommand_Execute);


		}
		private async void SelectContragentCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Company>().GetAllAsync();
            SelectAndSetWrapper<Company, CompanyWrapper>(entities, nameof(Item.Contragent), Item.Contragent?.Id);
		}

		private void ClearContragentCommand_Execute() 
		{
		    Item.Contragent = null;
		}



    }


    public partial class MeasureDetailsViewModel : BaseDetailsViewModel<MeasureWrapper, Measure, AfterSaveMeasureEvent>
    {

        public MeasureDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class ParameterDetailsViewModel : BaseDetailsViewModel<ParameterWrapper, Parameter, AfterSaveParameterEvent>
    {
		public ICommand SelectParameterGroupCommand { get; }
		public ICommand ClearParameterGroupCommand { get; }


        public ParameterDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectParameterGroupCommand = new DelegateCommand(SelectParameterGroupCommand_Execute);
			ClearParameterGroupCommand = new DelegateCommand(ClearParameterGroupCommand_Execute);


		}
		private async void SelectParameterGroupCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<ParameterGroup>().GetAllAsync();
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(entities, nameof(Item.ParameterGroup), Item.ParameterGroup?.Id);
		}

		private void ClearParameterGroupCommand_Execute() 
		{
		    Item.ParameterGroup = null;
		}



    }


    public partial class ParameterGroupDetailsViewModel : BaseDetailsViewModel<ParameterGroupWrapper, ParameterGroup, AfterSaveParameterGroupEvent>
    {
		public ICommand SelectMeasureCommand { get; }
		public ICommand ClearMeasureCommand { get; }


        public ParameterGroupDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectMeasureCommand = new DelegateCommand(SelectMeasureCommand_Execute);
			ClearMeasureCommand = new DelegateCommand(ClearMeasureCommand_Execute);


		}
		private async void SelectMeasureCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Measure>().GetAllAsync();
            SelectAndSetWrapper<Measure, MeasureWrapper>(entities, nameof(Item.Measure), Item.Measure?.Id);
		}

		private void ClearMeasureCommand_Execute() 
		{
		    Item.Measure = null;
		}



    }


    public partial class ProductRelationDetailsViewModel : BaseDetailsViewModel<ProductRelationWrapper, ProductRelation, AfterSaveProductRelationEvent>
    {

        public ProductRelationDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class PersonDetailsViewModel : BaseDetailsViewModel<PersonWrapper, Person, AfterSavePersonEvent>
    {

        public PersonDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class PaymentPlannedDetailsViewModel : BaseDetailsViewModel<PaymentPlannedWrapper, PaymentPlanned, AfterSavePaymentPlannedEvent>
    {
		public ICommand SelectConditionCommand { get; }
		public ICommand ClearConditionCommand { get; }


        public PaymentPlannedDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectConditionCommand = new DelegateCommand(SelectConditionCommand_Execute);
			ClearConditionCommand = new DelegateCommand(ClearConditionCommand_Execute);


		}
		private async void SelectConditionCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<PaymentCondition>().GetAllAsync();
            SelectAndSetWrapper<PaymentCondition, PaymentConditionWrapper>(entities, nameof(Item.Condition), Item.Condition?.Id);
		}

		private void ClearConditionCommand_Execute() 
		{
		    Item.Condition = null;
		}



    }


    public partial class PaymentActualDetailsViewModel : BaseDetailsViewModel<PaymentActualWrapper, PaymentActual, AfterSavePaymentActualEvent>
    {

        public PaymentActualDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class ParameterRelationDetailsViewModel : BaseDetailsViewModel<ParameterRelationWrapper, ParameterRelation, AfterSaveParameterRelationEvent>
    {

        public ParameterRelationDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class SalesUnitDetailsViewModel : BaseDetailsViewModel<SalesUnitWrapper, SalesUnit, AfterSaveSalesUnitEvent>
    {
		public ICommand SelectFacilityCommand { get; }
		public ICommand ClearFacilityCommand { get; }

		public ICommand SelectProducerCommand { get; }
		public ICommand ClearProducerCommand { get; }

		public ICommand SelectProductCommand { get; }
		public ICommand ClearProductCommand { get; }

		public ICommand SelectOrderCommand { get; }
		public ICommand ClearOrderCommand { get; }

		public ICommand SelectSpecificationCommand { get; }
		public ICommand ClearSpecificationCommand { get; }

		public ICommand SelectPaymentsConditionSetCommand { get; }
		public ICommand ClearPaymentsConditionSetCommand { get; }

		public ICommand SelectAddressCommand { get; }
		public ICommand ClearAddressCommand { get; }


        public SalesUnitDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectFacilityCommand = new DelegateCommand(SelectFacilityCommand_Execute);
			ClearFacilityCommand = new DelegateCommand(ClearFacilityCommand_Execute);

			SelectProducerCommand = new DelegateCommand(SelectProducerCommand_Execute);
			ClearProducerCommand = new DelegateCommand(ClearProducerCommand_Execute);

			SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute);
			ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute);

			SelectOrderCommand = new DelegateCommand(SelectOrderCommand_Execute);
			ClearOrderCommand = new DelegateCommand(ClearOrderCommand_Execute);

			SelectSpecificationCommand = new DelegateCommand(SelectSpecificationCommand_Execute);
			ClearSpecificationCommand = new DelegateCommand(ClearSpecificationCommand_Execute);

			SelectPaymentsConditionSetCommand = new DelegateCommand(SelectPaymentsConditionSetCommand_Execute);
			ClearPaymentsConditionSetCommand = new DelegateCommand(ClearPaymentsConditionSetCommand_Execute);

			SelectAddressCommand = new DelegateCommand(SelectAddressCommand_Execute);
			ClearAddressCommand = new DelegateCommand(ClearAddressCommand_Execute);


		}
		private async void SelectFacilityCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Facility>().GetAllAsync();
            SelectAndSetWrapper<Facility, FacilityWrapper>(entities, nameof(Item.Facility), Item.Facility?.Id);
		}

		private void ClearFacilityCommand_Execute() 
		{
		    Item.Facility = null;
		}

		private async void SelectProducerCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Company>().GetAllAsync();
            SelectAndSetWrapper<Company, CompanyWrapper>(entities, nameof(Item.Producer), Item.Producer?.Id);
		}

		private void ClearProducerCommand_Execute() 
		{
		    Item.Producer = null;
		}

		private async void SelectProductCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Product>().GetAllAsync();
            SelectAndSetWrapper<Product, ProductWrapper>(entities, nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute() 
		{
		    Item.Product = null;
		}

		private async void SelectOrderCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Order>().GetAllAsync();
            SelectAndSetWrapper<Order, OrderWrapper>(entities, nameof(Item.Order), Item.Order?.Id);
		}

		private void ClearOrderCommand_Execute() 
		{
		    Item.Order = null;
		}

		private async void SelectSpecificationCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Specification>().GetAllAsync();
            SelectAndSetWrapper<Specification, SpecificationWrapper>(entities, nameof(Item.Specification), Item.Specification?.Id);
		}

		private void ClearSpecificationCommand_Execute() 
		{
		    Item.Specification = null;
		}

		private async void SelectPaymentsConditionSetCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<PaymentConditionSet>().GetAllAsync();
            SelectAndSetWrapper<PaymentConditionSet, PaymentConditionSetWrapper>(entities, nameof(Item.PaymentsConditionSet), Item.PaymentsConditionSet?.Id);
		}

		private void ClearPaymentsConditionSetCommand_Execute() 
		{
		    Item.PaymentsConditionSet = null;
		}

		private async void SelectAddressCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Address>().GetAllAsync();
            SelectAndSetWrapper<Address, AddressWrapper>(entities, nameof(Item.Address), Item.Address?.Id);
		}

		private void ClearAddressCommand_Execute() 
		{
		    Item.Address = null;
		}



    }


    public partial class TestFriendAddressDetailsViewModel : BaseDetailsViewModel<TestFriendAddressWrapper, TestFriendAddress, AfterSaveTestFriendAddressEvent>
    {

        public TestFriendAddressDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class TestFriendDetailsViewModel : BaseDetailsViewModel<TestFriendWrapper, TestFriend, AfterSaveTestFriendEvent>
    {
		public ICommand SelectTestFriendAddressCommand { get; }
		public ICommand ClearTestFriendAddressCommand { get; }

		public ICommand SelectTestFriendGroupCommand { get; }
		public ICommand ClearTestFriendGroupCommand { get; }

		public ICommand SelectTestFriendEmailGetCommand { get; }
		public ICommand ClearTestFriendEmailGetCommand { get; }


        public TestFriendDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectTestFriendAddressCommand = new DelegateCommand(SelectTestFriendAddressCommand_Execute);
			ClearTestFriendAddressCommand = new DelegateCommand(ClearTestFriendAddressCommand_Execute);

			SelectTestFriendGroupCommand = new DelegateCommand(SelectTestFriendGroupCommand_Execute);
			ClearTestFriendGroupCommand = new DelegateCommand(ClearTestFriendGroupCommand_Execute);

			SelectTestFriendEmailGetCommand = new DelegateCommand(SelectTestFriendEmailGetCommand_Execute);
			ClearTestFriendEmailGetCommand = new DelegateCommand(ClearTestFriendEmailGetCommand_Execute);


		}
		private async void SelectTestFriendAddressCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<TestFriendAddress>().GetAllAsync();
            SelectAndSetWrapper<TestFriendAddress, TestFriendAddressWrapper>(entities, nameof(Item.TestFriendAddress), Item.TestFriendAddress?.Id);
		}

		private void ClearTestFriendAddressCommand_Execute() 
		{
		    Item.TestFriendAddress = null;
		}

		private async void SelectTestFriendGroupCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<TestFriendGroup>().GetAllAsync();
            SelectAndSetWrapper<TestFriendGroup, TestFriendGroupWrapper>(entities, nameof(Item.TestFriendGroup), Item.TestFriendGroup?.Id);
		}

		private void ClearTestFriendGroupCommand_Execute() 
		{
		    Item.TestFriendGroup = null;
		}

		private async void SelectTestFriendEmailGetCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<TestFriendEmail>().GetAllAsync();
            SelectAndSetWrapper<TestFriendEmail, TestFriendEmailWrapper>(entities, nameof(Item.TestFriendEmailGet), Item.TestFriendEmailGet?.Id);
		}

		private void ClearTestFriendEmailGetCommand_Execute() 
		{
		    //Item.TestFriendEmailGet = null;
		}



    }


    public partial class TestFriendEmailDetailsViewModel : BaseDetailsViewModel<TestFriendEmailWrapper, TestFriendEmail, AfterSaveTestFriendEmailEvent>
    {

        public TestFriendEmailDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class TestFriendGroupDetailsViewModel : BaseDetailsViewModel<TestFriendGroupWrapper, TestFriendGroup, AfterSaveTestFriendGroupEvent>
    {

        public TestFriendGroupDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class DocumentDetailsViewModel : BaseDetailsViewModel<DocumentWrapper, Document, AfterSaveDocumentEvent>
    {
		public ICommand SelectRequestDocumentCommand { get; }
		public ICommand ClearRequestDocumentCommand { get; }

		public ICommand SelectAuthorCommand { get; }
		public ICommand ClearAuthorCommand { get; }

		public ICommand SelectSenderEmployeeCommand { get; }
		public ICommand ClearSenderEmployeeCommand { get; }

		public ICommand SelectRecipientEmployeeCommand { get; }
		public ICommand ClearRecipientEmployeeCommand { get; }

		public ICommand SelectRegistrationDetailsOfSenderCommand { get; }
		public ICommand ClearRegistrationDetailsOfSenderCommand { get; }

		public ICommand SelectRegistrationDetailsOfRecipientCommand { get; }
		public ICommand ClearRegistrationDetailsOfRecipientCommand { get; }


        public DocumentDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectRequestDocumentCommand = new DelegateCommand(SelectRequestDocumentCommand_Execute);
			ClearRequestDocumentCommand = new DelegateCommand(ClearRequestDocumentCommand_Execute);

			SelectAuthorCommand = new DelegateCommand(SelectAuthorCommand_Execute);
			ClearAuthorCommand = new DelegateCommand(ClearAuthorCommand_Execute);

			SelectSenderEmployeeCommand = new DelegateCommand(SelectSenderEmployeeCommand_Execute);
			ClearSenderEmployeeCommand = new DelegateCommand(ClearSenderEmployeeCommand_Execute);

			SelectRecipientEmployeeCommand = new DelegateCommand(SelectRecipientEmployeeCommand_Execute);
			ClearRecipientEmployeeCommand = new DelegateCommand(ClearRecipientEmployeeCommand_Execute);

			SelectRegistrationDetailsOfSenderCommand = new DelegateCommand(SelectRegistrationDetailsOfSenderCommand_Execute);
			ClearRegistrationDetailsOfSenderCommand = new DelegateCommand(ClearRegistrationDetailsOfSenderCommand_Execute);

			SelectRegistrationDetailsOfRecipientCommand = new DelegateCommand(SelectRegistrationDetailsOfRecipientCommand_Execute);
			ClearRegistrationDetailsOfRecipientCommand = new DelegateCommand(ClearRegistrationDetailsOfRecipientCommand_Execute);


		}
		private async void SelectRequestDocumentCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Document>().GetAllAsync();
            SelectAndSetWrapper<Document, DocumentWrapper>(entities, nameof(Item.RequestDocument), Item.RequestDocument?.Id);
		}

		private void ClearRequestDocumentCommand_Execute() 
		{
		    Item.RequestDocument = null;
		}

		private async void SelectAuthorCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Employee>().GetAllAsync();
            SelectAndSetWrapper<Employee, EmployeeWrapper>(entities, nameof(Item.Author), Item.Author?.Id);
		}

		private void ClearAuthorCommand_Execute() 
		{
		    Item.Author = null;
		}

		private async void SelectSenderEmployeeCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Employee>().GetAllAsync();
            SelectAndSetWrapper<Employee, EmployeeWrapper>(entities, nameof(Item.SenderEmployee), Item.SenderEmployee?.Id);
		}

		private void ClearSenderEmployeeCommand_Execute() 
		{
		    Item.SenderEmployee = null;
		}

		private async void SelectRecipientEmployeeCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Employee>().GetAllAsync();
            SelectAndSetWrapper<Employee, EmployeeWrapper>(entities, nameof(Item.RecipientEmployee), Item.RecipientEmployee?.Id);
		}

		private void ClearRecipientEmployeeCommand_Execute() 
		{
		    Item.RecipientEmployee = null;
		}

		private async void SelectRegistrationDetailsOfSenderCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<DocumentsRegistrationDetails>().GetAllAsync();
            SelectAndSetWrapper<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(entities, nameof(Item.RegistrationDetailsOfSender), Item.RegistrationDetailsOfSender?.Id);
		}

		private void ClearRegistrationDetailsOfSenderCommand_Execute() 
		{
		    Item.RegistrationDetailsOfSender = null;
		}

		private async void SelectRegistrationDetailsOfRecipientCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<DocumentsRegistrationDetails>().GetAllAsync();
            SelectAndSetWrapper<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(entities, nameof(Item.RegistrationDetailsOfRecipient), Item.RegistrationDetailsOfRecipient?.Id);
		}

		private void ClearRegistrationDetailsOfRecipientCommand_Execute() 
		{
		    Item.RegistrationDetailsOfRecipient = null;
		}



    }


    public partial class TestEntityDetailsViewModel : BaseDetailsViewModel<TestEntityWrapper, TestEntity, AfterSaveTestEntityEvent>
    {

        public TestEntityDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class TestHusbandDetailsViewModel : BaseDetailsViewModel<TestHusbandWrapper, TestHusband, AfterSaveTestHusbandEvent>
    {
		public ICommand SelectWifeCommand { get; }
		public ICommand ClearWifeCommand { get; }


        public TestHusbandDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectWifeCommand = new DelegateCommand(SelectWifeCommand_Execute);
			ClearWifeCommand = new DelegateCommand(ClearWifeCommand_Execute);


		}
		private async void SelectWifeCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<TestWife>().GetAllAsync();
            SelectAndSetWrapper<TestWife, TestWifeWrapper>(entities, nameof(Item.Wife), Item.Wife?.Id);
		}

		private void ClearWifeCommand_Execute() 
		{
		    Item.Wife = null;
		}



    }


    public partial class TestWifeDetailsViewModel : BaseDetailsViewModel<TestWifeWrapper, TestWife, AfterSaveTestWifeEvent>
    {
		public ICommand SelectHusbandCommand { get; }
		public ICommand ClearHusbandCommand { get; }


        public TestWifeDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectHusbandCommand = new DelegateCommand(SelectHusbandCommand_Execute);
			ClearHusbandCommand = new DelegateCommand(ClearHusbandCommand_Execute);


		}
		private async void SelectHusbandCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<TestHusband>().GetAllAsync();
            SelectAndSetWrapper<TestHusband, TestHusbandWrapper>(entities, nameof(Item.Husband), Item.Husband?.Id);
		}

		private void ClearHusbandCommand_Execute() 
		{
		    Item.Husband = null;
		}



    }


    public partial class TestChildDetailsViewModel : BaseDetailsViewModel<TestChildWrapper, TestChild, AfterSaveTestChildEvent>
    {
		public ICommand SelectHusbandCommand { get; }
		public ICommand ClearHusbandCommand { get; }

		public ICommand SelectWifeCommand { get; }
		public ICommand ClearWifeCommand { get; }


        public TestChildDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectHusbandCommand = new DelegateCommand(SelectHusbandCommand_Execute);
			ClearHusbandCommand = new DelegateCommand(ClearHusbandCommand_Execute);

			SelectWifeCommand = new DelegateCommand(SelectWifeCommand_Execute);
			ClearWifeCommand = new DelegateCommand(ClearWifeCommand_Execute);


		}
		private async void SelectHusbandCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<TestHusband>().GetAllAsync();
            SelectAndSetWrapper<TestHusband, TestHusbandWrapper>(entities, nameof(Item.Husband), Item.Husband?.Id);
		}

		private void ClearHusbandCommand_Execute() 
		{
		    Item.Husband = null;
		}

		private async void SelectWifeCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<TestWife>().GetAllAsync();
            SelectAndSetWrapper<TestWife, TestWifeWrapper>(entities, nameof(Item.Wife), Item.Wife?.Id);
		}

		private void ClearWifeCommand_Execute() 
		{
		    Item.Wife = null;
		}



    }


    public partial class CostOnDateDetailsViewModel : BaseDetailsViewModel<CostOnDateWrapper, CostOnDate, AfterSaveCostOnDateEvent>
    {

        public CostOnDateDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class CostDetailsViewModel : BaseDetailsViewModel<CostWrapper, Cost, AfterSaveCostEvent>
    {
		public ICommand SelectCurrencyCommand { get; }
		public ICommand ClearCurrencyCommand { get; }


        public CostDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectCurrencyCommand = new DelegateCommand(SelectCurrencyCommand_Execute);
			ClearCurrencyCommand = new DelegateCommand(ClearCurrencyCommand_Execute);


		}
		private async void SelectCurrencyCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Currency>().GetAllAsync();
            SelectAndSetWrapper<Currency, CurrencyWrapper>(entities, nameof(Item.Currency), Item.Currency?.Id);
		}

		private void ClearCurrencyCommand_Execute() 
		{
		    Item.Currency = null;
		}



    }


    public partial class CurrencyDetailsViewModel : BaseDetailsViewModel<CurrencyWrapper, Currency, AfterSaveCurrencyEvent>
    {

        public CurrencyDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class ExchangeCurrencyRateDetailsViewModel : BaseDetailsViewModel<ExchangeCurrencyRateWrapper, CurrencyExchangeRate, AfterSaveExchangeCurrencyRateEvent>
    {
		public ICommand SelectFirstCurrencyCommand { get; }
		public ICommand ClearFirstCurrencyCommand { get; }

		public ICommand SelectSecondCurrencyCommand { get; }
		public ICommand ClearSecondCurrencyCommand { get; }


        public ExchangeCurrencyRateDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectFirstCurrencyCommand = new DelegateCommand(SelectFirstCurrencyCommand_Execute);
			ClearFirstCurrencyCommand = new DelegateCommand(ClearFirstCurrencyCommand_Execute);

			SelectSecondCurrencyCommand = new DelegateCommand(SelectSecondCurrencyCommand_Execute);
			ClearSecondCurrencyCommand = new DelegateCommand(ClearSecondCurrencyCommand_Execute);


		}
		private async void SelectFirstCurrencyCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Currency>().GetAllAsync();
            SelectAndSetWrapper<Currency, CurrencyWrapper>(entities, nameof(Item.FirstCurrency), Item.FirstCurrency?.Id);
		}

		private void ClearFirstCurrencyCommand_Execute() 
		{
		    Item.FirstCurrency = null;
		}

		private async void SelectSecondCurrencyCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Currency>().GetAllAsync();
            SelectAndSetWrapper<Currency, CurrencyWrapper>(entities, nameof(Item.SecondCurrency), Item.SecondCurrency?.Id);
		}

		private void ClearSecondCurrencyCommand_Execute() 
		{
		    Item.SecondCurrency = null;
		}



    }


    public partial class ProductDetailsViewModel : BaseDetailsViewModel<ProductWrapper, Product, AfterSaveProductEvent>
    {
		public ICommand SelectProductBlockCommand { get; }
		public ICommand ClearProductBlockCommand { get; }


        public ProductDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectProductBlockCommand = new DelegateCommand(SelectProductBlockCommand_Execute);
			ClearProductBlockCommand = new DelegateCommand(ClearProductBlockCommand_Execute);


		}
		private async void SelectProductBlockCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<ProductBlock>().GetAllAsync();
            SelectAndSetWrapper<ProductBlock, ProductBlockWrapper>(entities, nameof(Item.ProductBlock), Item.ProductBlock?.Id);
		}

		private void ClearProductBlockCommand_Execute() 
		{
		    Item.ProductBlock = null;
		}



    }


    public partial class OfferDetailsViewModel : BaseDetailsViewModel<OfferWrapper, Offer, AfterSaveOfferEvent>
    {
		public ICommand SelectProjectCommand { get; }
		public ICommand ClearProjectCommand { get; }

		public ICommand SelectRequestDocumentCommand { get; }
		public ICommand ClearRequestDocumentCommand { get; }

		public ICommand SelectAuthorCommand { get; }
		public ICommand ClearAuthorCommand { get; }

		public ICommand SelectSenderEmployeeCommand { get; }
		public ICommand ClearSenderEmployeeCommand { get; }

		public ICommand SelectRecipientEmployeeCommand { get; }
		public ICommand ClearRecipientEmployeeCommand { get; }

		public ICommand SelectRegistrationDetailsOfSenderCommand { get; }
		public ICommand ClearRegistrationDetailsOfSenderCommand { get; }

		public ICommand SelectRegistrationDetailsOfRecipientCommand { get; }
		public ICommand ClearRegistrationDetailsOfRecipientCommand { get; }


        public OfferDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectProjectCommand = new DelegateCommand(SelectProjectCommand_Execute);
			ClearProjectCommand = new DelegateCommand(ClearProjectCommand_Execute);

			SelectRequestDocumentCommand = new DelegateCommand(SelectRequestDocumentCommand_Execute);
			ClearRequestDocumentCommand = new DelegateCommand(ClearRequestDocumentCommand_Execute);

			SelectAuthorCommand = new DelegateCommand(SelectAuthorCommand_Execute);
			ClearAuthorCommand = new DelegateCommand(ClearAuthorCommand_Execute);

			SelectSenderEmployeeCommand = new DelegateCommand(SelectSenderEmployeeCommand_Execute);
			ClearSenderEmployeeCommand = new DelegateCommand(ClearSenderEmployeeCommand_Execute);

			SelectRecipientEmployeeCommand = new DelegateCommand(SelectRecipientEmployeeCommand_Execute);
			ClearRecipientEmployeeCommand = new DelegateCommand(ClearRecipientEmployeeCommand_Execute);

			SelectRegistrationDetailsOfSenderCommand = new DelegateCommand(SelectRegistrationDetailsOfSenderCommand_Execute);
			ClearRegistrationDetailsOfSenderCommand = new DelegateCommand(ClearRegistrationDetailsOfSenderCommand_Execute);

			SelectRegistrationDetailsOfRecipientCommand = new DelegateCommand(SelectRegistrationDetailsOfRecipientCommand_Execute);
			ClearRegistrationDetailsOfRecipientCommand = new DelegateCommand(ClearRegistrationDetailsOfRecipientCommand_Execute);


		}
		private async void SelectProjectCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Project>().GetAllAsync();
            SelectAndSetWrapper<Project, ProjectWrapper>(entities, nameof(Item.Project), Item.Project?.Id);
		}

		private void ClearProjectCommand_Execute() 
		{
		    Item.Project = null;
		}

		private async void SelectRequestDocumentCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Document>().GetAllAsync();
            SelectAndSetWrapper<Document, DocumentWrapper>(entities, nameof(Item.RequestDocument), Item.RequestDocument?.Id);
		}

		private void ClearRequestDocumentCommand_Execute() 
		{
		    Item.RequestDocument = null;
		}

		private async void SelectAuthorCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Employee>().GetAllAsync();
            SelectAndSetWrapper<Employee, EmployeeWrapper>(entities, nameof(Item.Author), Item.Author?.Id);
		}

		private void ClearAuthorCommand_Execute() 
		{
		    Item.Author = null;
		}

		private async void SelectSenderEmployeeCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Employee>().GetAllAsync();
            SelectAndSetWrapper<Employee, EmployeeWrapper>(entities, nameof(Item.SenderEmployee), Item.SenderEmployee?.Id);
		}

		private void ClearSenderEmployeeCommand_Execute() 
		{
		    Item.SenderEmployee = null;
		}

		private async void SelectRecipientEmployeeCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Employee>().GetAllAsync();
            SelectAndSetWrapper<Employee, EmployeeWrapper>(entities, nameof(Item.RecipientEmployee), Item.RecipientEmployee?.Id);
		}

		private void ClearRecipientEmployeeCommand_Execute() 
		{
		    Item.RecipientEmployee = null;
		}

		private async void SelectRegistrationDetailsOfSenderCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<DocumentsRegistrationDetails>().GetAllAsync();
            SelectAndSetWrapper<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(entities, nameof(Item.RegistrationDetailsOfSender), Item.RegistrationDetailsOfSender?.Id);
		}

		private void ClearRegistrationDetailsOfSenderCommand_Execute() 
		{
		    Item.RegistrationDetailsOfSender = null;
		}

		private async void SelectRegistrationDetailsOfRecipientCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<DocumentsRegistrationDetails>().GetAllAsync();
            SelectAndSetWrapper<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(entities, nameof(Item.RegistrationDetailsOfRecipient), Item.RegistrationDetailsOfRecipient?.Id);
		}

		private void ClearRegistrationDetailsOfRecipientCommand_Execute() 
		{
		    Item.RegistrationDetailsOfRecipient = null;
		}



    }


    public partial class EmployeeDetailsViewModel : BaseDetailsViewModel<EmployeeWrapper, Employee, AfterSaveEmployeeEvent>
    {
		public ICommand SelectCompanyCommand { get; }
		public ICommand ClearCompanyCommand { get; }

		public ICommand SelectPositionCommand { get; }
		public ICommand ClearPositionCommand { get; }


        public EmployeeDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectCompanyCommand = new DelegateCommand(SelectCompanyCommand_Execute);
			ClearCompanyCommand = new DelegateCommand(ClearCompanyCommand_Execute);

			SelectPositionCommand = new DelegateCommand(SelectPositionCommand_Execute);
			ClearPositionCommand = new DelegateCommand(ClearPositionCommand_Execute);


		}
		private async void SelectCompanyCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Company>().GetAllAsync();
            SelectAndSetWrapper<Company, CompanyWrapper>(entities, nameof(Item.Company), Item.Company?.Id);
		}

		private void ClearCompanyCommand_Execute() 
		{
		    Item.Company = null;
		}

		private async void SelectPositionCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<EmployeesPosition>().GetAllAsync();
            SelectAndSetWrapper<EmployeesPosition, EmployeesPositionWrapper>(entities, nameof(Item.Position), Item.Position?.Id);
		}

		private void ClearPositionCommand_Execute() 
		{
		    Item.Position = null;
		}



    }


    public partial class OrderDetailsViewModel : BaseDetailsViewModel<OrderWrapper, Order, AfterSaveOrderEvent>
    {

        public OrderDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class PaymentConditionDetailsViewModel : BaseDetailsViewModel<PaymentConditionWrapper, PaymentCondition, AfterSavePaymentConditionEvent>
    {

        public PaymentConditionDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class PaymentDocumentDetailsViewModel : BaseDetailsViewModel<PaymentDocumentWrapper, PaymentDocument, AfterSavePaymentDocumentEvent>
    {

        public PaymentDocumentDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class FacilityDetailsViewModel : BaseDetailsViewModel<FacilityWrapper, Facility, AfterSaveFacilityEvent>
    {
		public ICommand SelectTypeCommand { get; }
		public ICommand ClearTypeCommand { get; }

		public ICommand SelectOwnerCompanyCommand { get; }
		public ICommand ClearOwnerCompanyCommand { get; }

		public ICommand SelectAddressCommand { get; }
		public ICommand ClearAddressCommand { get; }


        public FacilityDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectTypeCommand = new DelegateCommand(SelectTypeCommand_Execute);
			ClearTypeCommand = new DelegateCommand(ClearTypeCommand_Execute);

			SelectOwnerCompanyCommand = new DelegateCommand(SelectOwnerCompanyCommand_Execute);
			ClearOwnerCompanyCommand = new DelegateCommand(ClearOwnerCompanyCommand_Execute);

			SelectAddressCommand = new DelegateCommand(SelectAddressCommand_Execute);
			ClearAddressCommand = new DelegateCommand(ClearAddressCommand_Execute);


		}
		private async void SelectTypeCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<FacilityType>().GetAllAsync();
            SelectAndSetWrapper<FacilityType, FacilityTypeWrapper>(entities, nameof(Item.Type), Item.Type?.Id);
		}

		private void ClearTypeCommand_Execute() 
		{
		    Item.Type = null;
		}

		private async void SelectOwnerCompanyCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Company>().GetAllAsync();
            SelectAndSetWrapper<Company, CompanyWrapper>(entities, nameof(Item.OwnerCompany), Item.OwnerCompany?.Id);
		}

		private void ClearOwnerCompanyCommand_Execute() 
		{
		    Item.OwnerCompany = null;
		}

		private async void SelectAddressCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Address>().GetAllAsync();
            SelectAndSetWrapper<Address, AddressWrapper>(entities, nameof(Item.Address), Item.Address?.Id);
		}

		private void ClearAddressCommand_Execute() 
		{
		    Item.Address = null;
		}



    }


    public partial class ProjectDetailsViewModel : BaseDetailsViewModel<ProjectWrapper, Project, AfterSaveProjectEvent>
    {
		public ICommand SelectManagerCommand { get; }
		public ICommand ClearManagerCommand { get; }


        public ProjectDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectManagerCommand = new DelegateCommand(SelectManagerCommand_Execute);
			ClearManagerCommand = new DelegateCommand(ClearManagerCommand_Execute);


		}
		private async void SelectManagerCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<User>().GetAllAsync();
            SelectAndSetWrapper<User, UserWrapper>(entities, nameof(Item.Manager), Item.Manager?.Id);
		}

		private void ClearManagerCommand_Execute() 
		{
		    Item.Manager = null;
		}



    }


    public partial class UserRoleDetailsViewModel : BaseDetailsViewModel<UserRoleWrapper, UserRole, AfterSaveUserRoleEvent>
    {

        public UserRoleDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class SpecificationDetailsViewModel : BaseDetailsViewModel<SpecificationWrapper, Specification, AfterSaveSpecificationEvent>
    {
		public ICommand SelectContractCommand { get; }
		public ICommand ClearContractCommand { get; }


        public SpecificationDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectContractCommand = new DelegateCommand(SelectContractCommand_Execute);
			ClearContractCommand = new DelegateCommand(ClearContractCommand_Execute);


		}
		private async void SelectContractCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Contract>().GetAllAsync();
            SelectAndSetWrapper<Contract, ContractWrapper>(entities, nameof(Item.Contract), Item.Contract?.Id);
		}

		private void ClearContractCommand_Execute() 
		{
		    Item.Contract = null;
		}



    }


    public partial class TenderDetailsViewModel : BaseDetailsViewModel<TenderWrapper, Tender, AfterSaveTenderEvent>
    {
		public ICommand SelectProjectCommand { get; }
		public ICommand ClearProjectCommand { get; }

		public ICommand SelectWinnerCommand { get; }
		public ICommand ClearWinnerCommand { get; }


        public TenderDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectProjectCommand = new DelegateCommand(SelectProjectCommand_Execute);
			ClearProjectCommand = new DelegateCommand(ClearProjectCommand_Execute);

			SelectWinnerCommand = new DelegateCommand(SelectWinnerCommand_Execute);
			ClearWinnerCommand = new DelegateCommand(ClearWinnerCommand_Execute);


		}
		private async void SelectProjectCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Project>().GetAllAsync();
            SelectAndSetWrapper<Project, ProjectWrapper>(entities, nameof(Item.Project), Item.Project?.Id);
		}

		private void ClearProjectCommand_Execute() 
		{
		    Item.Project = null;
		}

		private async void SelectWinnerCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Company>().GetAllAsync();
            SelectAndSetWrapper<Company, CompanyWrapper>(entities, nameof(Item.Winner), Item.Winner?.Id);
		}

		private void ClearWinnerCommand_Execute() 
		{
		    Item.Winner = null;
		}



    }


    public partial class TenderTypeDetailsViewModel : BaseDetailsViewModel<TenderTypeWrapper, TenderType, AfterSaveTenderTypeEvent>
    {

        public TenderTypeDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class UserDetailsViewModel : BaseDetailsViewModel<UserWrapper, User, AfterSaveUserEvent>
    {
		public ICommand SelectEmployeeCommand { get; }
		public ICommand ClearEmployeeCommand { get; }


        public UserDetailsViewModel(IUnityContainer container) : base(container) 
		{
			SelectEmployeeCommand = new DelegateCommand(SelectEmployeeCommand_Execute);
			ClearEmployeeCommand = new DelegateCommand(ClearEmployeeCommand_Execute);


		}
		private async void SelectEmployeeCommand_Execute() 
		{
            var entities = await UnitOfWork.GetRepository<Employee>().GetAllAsync();
            SelectAndSetWrapper<Employee, EmployeeWrapper>(entities, nameof(Item.Employee), Item.Employee?.Id);
		}

		private void ClearEmployeeCommand_Execute() 
		{
		    Item.Employee = null;
		}



    }


    public partial class ProductBlockDetailsViewModel : BaseDetailsViewModel<ProductBlockWrapper, ProductBlock, AfterSaveProductBlockEvent>
    {

        public ProductBlockDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


    public partial class PaymentConditionSetDetailsViewModel : BaseDetailsViewModel<PaymentConditionSetWrapper, PaymentConditionSet, AfterSavePaymentConditionSetEvent>
    {

        public PaymentConditionSetDetailsViewModel(IUnityContainer container) : base(container) 
		{

		}


    }


}
