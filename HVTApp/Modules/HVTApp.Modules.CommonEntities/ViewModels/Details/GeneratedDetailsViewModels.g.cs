using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using Prism.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace HVTApp.UI.ViewModels
{

    public partial class CommonOptionDetailsViewModel : BaseDetailsViewModel<CommonOptionWrapper, CommonOption, AfterSaveCommonOptionEvent>
    {

        public CommonOptionDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class AddressDetailsViewModel : BaseDetailsViewModel<AddressWrapper, Address, AfterSaveAddressEvent>
    {
		private Func<Task<List<Locality>>> _getEntitiesForSelectLocalityCommand;
		public ICommand SelectLocalityCommand { get; }
		public ICommand ClearLocalityCommand { get; }


        public AddressDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectLocalityCommand = async () => { return await UnitOfWork.GetRepository<Locality>().GetAllAsync(); };
			SelectLocalityCommand = new DelegateCommand(SelectLocalityCommand_Execute);
			ClearLocalityCommand = new DelegateCommand(ClearLocalityCommand_Execute);


			InitGetMethods();
		}
		private async void SelectLocalityCommand_Execute() 
		{
            SelectAndSetWrapper<Locality, LocalityWrapper>(await _getEntitiesForSelectLocalityCommand(), nameof(Item.Locality), Item.Locality?.Id);
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

			InitGetMethods();
		}


    }


    public partial class DistrictDetailsViewModel : BaseDetailsViewModel<DistrictWrapper, District, AfterSaveDistrictEvent>
    {
		private Func<Task<List<Country>>> _getEntitiesForSelectCountryCommand;
		public ICommand SelectCountryCommand { get; }
		public ICommand ClearCountryCommand { get; }


        public DistrictDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectCountryCommand = async () => { return await UnitOfWork.GetRepository<Country>().GetAllAsync(); };
			SelectCountryCommand = new DelegateCommand(SelectCountryCommand_Execute);
			ClearCountryCommand = new DelegateCommand(ClearCountryCommand_Execute);


			InitGetMethods();
		}
		private async void SelectCountryCommand_Execute() 
		{
            SelectAndSetWrapper<Country, CountryWrapper>(await _getEntitiesForSelectCountryCommand(), nameof(Item.Country), Item.Country?.Id);
		}

		private void ClearCountryCommand_Execute() 
		{
		    Item.Country = null;
		}



    }


    public partial class LocalityDetailsViewModel : BaseDetailsViewModel<LocalityWrapper, Locality, AfterSaveLocalityEvent>
    {
		private Func<Task<List<LocalityType>>> _getEntitiesForSelectLocalityTypeCommand;
		public ICommand SelectLocalityTypeCommand { get; }
		public ICommand ClearLocalityTypeCommand { get; }

		private Func<Task<List<Region>>> _getEntitiesForSelectRegionCommand;
		public ICommand SelectRegionCommand { get; }
		public ICommand ClearRegionCommand { get; }


        public LocalityDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectLocalityTypeCommand = async () => { return await UnitOfWork.GetRepository<LocalityType>().GetAllAsync(); };
			SelectLocalityTypeCommand = new DelegateCommand(SelectLocalityTypeCommand_Execute);
			ClearLocalityTypeCommand = new DelegateCommand(ClearLocalityTypeCommand_Execute);

          _getEntitiesForSelectRegionCommand = async () => { return await UnitOfWork.GetRepository<Region>().GetAllAsync(); };
			SelectRegionCommand = new DelegateCommand(SelectRegionCommand_Execute);
			ClearRegionCommand = new DelegateCommand(ClearRegionCommand_Execute);


			InitGetMethods();
		}
		private async void SelectLocalityTypeCommand_Execute() 
		{
            SelectAndSetWrapper<LocalityType, LocalityTypeWrapper>(await _getEntitiesForSelectLocalityTypeCommand(), nameof(Item.LocalityType), Item.LocalityType?.Id);
		}

		private void ClearLocalityTypeCommand_Execute() 
		{
		    Item.LocalityType = null;
		}

		private async void SelectRegionCommand_Execute() 
		{
            SelectAndSetWrapper<Region, RegionWrapper>(await _getEntitiesForSelectRegionCommand(), nameof(Item.Region), Item.Region?.Id);
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

			InitGetMethods();
		}


    }


    public partial class RegionDetailsViewModel : BaseDetailsViewModel<RegionWrapper, Region, AfterSaveRegionEvent>
    {
		private Func<Task<List<District>>> _getEntitiesForSelectDistrictCommand;
		public ICommand SelectDistrictCommand { get; }
		public ICommand ClearDistrictCommand { get; }


        public RegionDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectDistrictCommand = async () => { return await UnitOfWork.GetRepository<District>().GetAllAsync(); };
			SelectDistrictCommand = new DelegateCommand(SelectDistrictCommand_Execute);
			ClearDistrictCommand = new DelegateCommand(ClearDistrictCommand_Execute);


			InitGetMethods();
		}
		private async void SelectDistrictCommand_Execute() 
		{
            SelectAndSetWrapper<District, DistrictWrapper>(await _getEntitiesForSelectDistrictCommand(), nameof(Item.District), Item.District?.Id);
		}

		private void ClearDistrictCommand_Execute() 
		{
		    Item.District = null;
		}



    }


    public partial class CalculatePriceTaskDetailsViewModel : BaseDetailsViewModel<CalculatePriceTaskWrapper, CalculatePriceTask, AfterSaveCalculatePriceTaskEvent>
    {
		private Func<Task<List<ProductBlock>>> _getEntitiesForSelectProductBlockCommand;
		public ICommand SelectProductBlockCommand { get; }
		public ICommand ClearProductBlockCommand { get; }


        public CalculatePriceTaskDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectProductBlockCommand = async () => { return await UnitOfWork.GetRepository<ProductBlock>().GetAllAsync(); };
			SelectProductBlockCommand = new DelegateCommand(SelectProductBlockCommand_Execute);
			ClearProductBlockCommand = new DelegateCommand(ClearProductBlockCommand_Execute);


			InitGetMethods();
		}
		private async void SelectProductBlockCommand_Execute() 
		{
            SelectAndSetWrapper<ProductBlock, ProductBlockWrapper>(await _getEntitiesForSelectProductBlockCommand(), nameof(Item.ProductBlock), Item.ProductBlock?.Id);
		}

		private void ClearProductBlockCommand_Execute() 
		{
		    Item.ProductBlock = null;
		}



    }


    public partial class CostDetailsViewModel : BaseDetailsViewModel<CostWrapper, Cost, AfterSaveCostEvent>
    {
		private Func<Task<List<Currency>>> _getEntitiesForSelectCurrencyCommand;
		public ICommand SelectCurrencyCommand { get; }
		public ICommand ClearCurrencyCommand { get; }


        public CostDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectCurrencyCommand = async () => { return await UnitOfWork.GetRepository<Currency>().GetAllAsync(); };
			SelectCurrencyCommand = new DelegateCommand(SelectCurrencyCommand_Execute);
			ClearCurrencyCommand = new DelegateCommand(ClearCurrencyCommand_Execute);


			InitGetMethods();
		}
		private async void SelectCurrencyCommand_Execute() 
		{
            SelectAndSetWrapper<Currency, CurrencyWrapper>(await _getEntitiesForSelectCurrencyCommand(), nameof(Item.Currency), Item.Currency?.Id);
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

			InitGetMethods();
		}


    }


    public partial class CurrencyExchangeRateDetailsViewModel : BaseDetailsViewModel<CurrencyExchangeRateWrapper, CurrencyExchangeRate, AfterSaveCurrencyExchangeRateEvent>
    {
		private Func<Task<List<Currency>>> _getEntitiesForSelectFirstCurrencyCommand;
		public ICommand SelectFirstCurrencyCommand { get; }
		public ICommand ClearFirstCurrencyCommand { get; }

		private Func<Task<List<Currency>>> _getEntitiesForSelectSecondCurrencyCommand;
		public ICommand SelectSecondCurrencyCommand { get; }
		public ICommand ClearSecondCurrencyCommand { get; }


        public CurrencyExchangeRateDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectFirstCurrencyCommand = async () => { return await UnitOfWork.GetRepository<Currency>().GetAllAsync(); };
			SelectFirstCurrencyCommand = new DelegateCommand(SelectFirstCurrencyCommand_Execute);
			ClearFirstCurrencyCommand = new DelegateCommand(ClearFirstCurrencyCommand_Execute);

          _getEntitiesForSelectSecondCurrencyCommand = async () => { return await UnitOfWork.GetRepository<Currency>().GetAllAsync(); };
			SelectSecondCurrencyCommand = new DelegateCommand(SelectSecondCurrencyCommand_Execute);
			ClearSecondCurrencyCommand = new DelegateCommand(ClearSecondCurrencyCommand_Execute);


			InitGetMethods();
		}
		private async void SelectFirstCurrencyCommand_Execute() 
		{
            SelectAndSetWrapper<Currency, CurrencyWrapper>(await _getEntitiesForSelectFirstCurrencyCommand(), nameof(Item.FirstCurrency), Item.FirstCurrency?.Id);
		}

		private void ClearFirstCurrencyCommand_Execute() 
		{
		    Item.FirstCurrency = null;
		}

		private async void SelectSecondCurrencyCommand_Execute() 
		{
            SelectAndSetWrapper<Currency, CurrencyWrapper>(await _getEntitiesForSelectSecondCurrencyCommand(), nameof(Item.SecondCurrency), Item.SecondCurrency?.Id);
		}

		private void ClearSecondCurrencyCommand_Execute() 
		{
		    Item.SecondCurrency = null;
		}



    }


    public partial class DescribeProductBlockTaskDetailsViewModel : BaseDetailsViewModel<DescribeProductBlockTaskWrapper, DescribeProductBlockTask, AfterSaveDescribeProductBlockTaskEvent>
    {
		private Func<Task<List<ProductBlock>>> _getEntitiesForSelectProductBlockCommand;
		public ICommand SelectProductBlockCommand { get; }
		public ICommand ClearProductBlockCommand { get; }

		private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; }
		public ICommand ClearProductCommand { get; }


        public DescribeProductBlockTaskDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectProductBlockCommand = async () => { return await UnitOfWork.GetRepository<ProductBlock>().GetAllAsync(); };
			SelectProductBlockCommand = new DelegateCommand(SelectProductBlockCommand_Execute);
			ClearProductBlockCommand = new DelegateCommand(ClearProductBlockCommand_Execute);

          _getEntitiesForSelectProductCommand = async () => { return await UnitOfWork.GetRepository<Product>().GetAllAsync(); };
			SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute);
			ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute);


			InitGetMethods();
		}
		private async void SelectProductBlockCommand_Execute() 
		{
            SelectAndSetWrapper<ProductBlock, ProductBlockWrapper>(await _getEntitiesForSelectProductBlockCommand(), nameof(Item.ProductBlock), Item.ProductBlock?.Id);
		}

		private void ClearProductBlockCommand_Execute() 
		{
		    Item.ProductBlock = null;
		}

		private async void SelectProductCommand_Execute() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(await _getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute() 
		{
		    Item.Product = null;
		}



    }


    public partial class PaymentConditionSetDetailsViewModel : BaseDetailsViewModel<PaymentConditionSetWrapper, PaymentConditionSet, AfterSavePaymentConditionSetEvent>
    {

        public PaymentConditionSetDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class ProductBlockDetailsViewModel : BaseDetailsViewModel<ProductBlockWrapper, ProductBlock, AfterSaveProductBlockEvent>
    {

        public ProductBlockDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class SalesBlockDetailsViewModel : BaseDetailsViewModel<SalesBlockWrapper, SalesBlock, AfterSaveSalesBlockEvent>
    {

        public SalesBlockDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class BankDetailsDetailsViewModel : BaseDetailsViewModel<BankDetailsWrapper, BankDetails, AfterSaveBankDetailsEvent>
    {

        public BankDetailsDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class CompanyDetailsViewModel : BaseDetailsViewModel<CompanyWrapper, Company, AfterSaveCompanyEvent>
    {
		private Func<Task<List<CompanyForm>>> _getEntitiesForSelectFormCommand;
		public ICommand SelectFormCommand { get; }
		public ICommand ClearFormCommand { get; }

		private Func<Task<List<Company>>> _getEntitiesForSelectParentCompanyCommand;
		public ICommand SelectParentCompanyCommand { get; }
		public ICommand ClearParentCompanyCommand { get; }

		private Func<Task<List<Address>>> _getEntitiesForSelectAddressLegalCommand;
		public ICommand SelectAddressLegalCommand { get; }
		public ICommand ClearAddressLegalCommand { get; }

		private Func<Task<List<Address>>> _getEntitiesForSelectAddressPostCommand;
		public ICommand SelectAddressPostCommand { get; }
		public ICommand ClearAddressPostCommand { get; }


        public CompanyDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectFormCommand = async () => { return await UnitOfWork.GetRepository<CompanyForm>().GetAllAsync(); };
			SelectFormCommand = new DelegateCommand(SelectFormCommand_Execute);
			ClearFormCommand = new DelegateCommand(ClearFormCommand_Execute);

          _getEntitiesForSelectParentCompanyCommand = async () => { return await UnitOfWork.GetRepository<Company>().GetAllAsync(); };
			SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_Execute);
			ClearParentCompanyCommand = new DelegateCommand(ClearParentCompanyCommand_Execute);

          _getEntitiesForSelectAddressLegalCommand = async () => { return await UnitOfWork.GetRepository<Address>().GetAllAsync(); };
			SelectAddressLegalCommand = new DelegateCommand(SelectAddressLegalCommand_Execute);
			ClearAddressLegalCommand = new DelegateCommand(ClearAddressLegalCommand_Execute);

          _getEntitiesForSelectAddressPostCommand = async () => { return await UnitOfWork.GetRepository<Address>().GetAllAsync(); };
			SelectAddressPostCommand = new DelegateCommand(SelectAddressPostCommand_Execute);
			ClearAddressPostCommand = new DelegateCommand(ClearAddressPostCommand_Execute);


			InitGetMethods();
		}
		private async void SelectFormCommand_Execute() 
		{
            SelectAndSetWrapper<CompanyForm, CompanyFormWrapper>(await _getEntitiesForSelectFormCommand(), nameof(Item.Form), Item.Form?.Id);
		}

		private void ClearFormCommand_Execute() 
		{
		    Item.Form = null;
		}

		private async void SelectParentCompanyCommand_Execute() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectParentCompanyCommand(), nameof(Item.ParentCompany), Item.ParentCompany?.Id);
		}

		private void ClearParentCompanyCommand_Execute() 
		{
		    Item.ParentCompany = null;
		}

		private async void SelectAddressLegalCommand_Execute() 
		{
            SelectAndSetWrapper<Address, AddressWrapper>(await _getEntitiesForSelectAddressLegalCommand(), nameof(Item.AddressLegal), Item.AddressLegal?.Id);
		}

		private void ClearAddressLegalCommand_Execute() 
		{
		    Item.AddressLegal = null;
		}

		private async void SelectAddressPostCommand_Execute() 
		{
            SelectAndSetWrapper<Address, AddressWrapper>(await _getEntitiesForSelectAddressPostCommand(), nameof(Item.AddressPost), Item.AddressPost?.Id);
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

			InitGetMethods();
		}


    }


    public partial class DocumentsRegistrationDetailsDetailsViewModel : BaseDetailsViewModel<DocumentsRegistrationDetailsWrapper, DocumentsRegistrationDetails, AfterSaveDocumentsRegistrationDetailsEvent>
    {

        public DocumentsRegistrationDetailsDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class EmployeesPositionDetailsViewModel : BaseDetailsViewModel<EmployeesPositionWrapper, EmployeesPosition, AfterSaveEmployeesPositionEvent>
    {

        public EmployeesPositionDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class FacilityTypeDetailsViewModel : BaseDetailsViewModel<FacilityTypeWrapper, FacilityType, AfterSaveFacilityTypeEvent>
    {

        public FacilityTypeDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class ActivityFieldDetailsViewModel : BaseDetailsViewModel<ActivityFieldWrapper, ActivityField, AfterSaveActivityFieldEvent>
    {

        public ActivityFieldDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class ContractDetailsViewModel : BaseDetailsViewModel<ContractWrapper, Contract, AfterSaveContractEvent>
    {
		private Func<Task<List<Company>>> _getEntitiesForSelectContragentCommand;
		public ICommand SelectContragentCommand { get; }
		public ICommand ClearContragentCommand { get; }


        public ContractDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectContragentCommand = async () => { return await UnitOfWork.GetRepository<Company>().GetAllAsync(); };
			SelectContragentCommand = new DelegateCommand(SelectContragentCommand_Execute);
			ClearContragentCommand = new DelegateCommand(ClearContragentCommand_Execute);


			InitGetMethods();
		}
		private async void SelectContragentCommand_Execute() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectContragentCommand(), nameof(Item.Contragent), Item.Contragent?.Id);
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

			InitGetMethods();
		}


    }


    public partial class ParameterDetailsViewModel : BaseDetailsViewModel<ParameterWrapper, Parameter, AfterSaveParameterEvent>
    {
		private Func<Task<List<ParameterGroup>>> _getEntitiesForSelectParameterGroupCommand;
		public ICommand SelectParameterGroupCommand { get; }
		public ICommand ClearParameterGroupCommand { get; }


        public ParameterDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectParameterGroupCommand = async () => { return await UnitOfWork.GetRepository<ParameterGroup>().GetAllAsync(); };
			SelectParameterGroupCommand = new DelegateCommand(SelectParameterGroupCommand_Execute);
			ClearParameterGroupCommand = new DelegateCommand(ClearParameterGroupCommand_Execute);


			InitGetMethods();
		}
		private async void SelectParameterGroupCommand_Execute() 
		{
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(await _getEntitiesForSelectParameterGroupCommand(), nameof(Item.ParameterGroup), Item.ParameterGroup?.Id);
		}

		private void ClearParameterGroupCommand_Execute() 
		{
		    Item.ParameterGroup = null;
		}



    }


    public partial class ParameterGroupDetailsViewModel : BaseDetailsViewModel<ParameterGroupWrapper, ParameterGroup, AfterSaveParameterGroupEvent>
    {
		private Func<Task<List<Measure>>> _getEntitiesForSelectMeasureCommand;
		public ICommand SelectMeasureCommand { get; }
		public ICommand ClearMeasureCommand { get; }


        public ParameterGroupDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectMeasureCommand = async () => { return await UnitOfWork.GetRepository<Measure>().GetAllAsync(); };
			SelectMeasureCommand = new DelegateCommand(SelectMeasureCommand_Execute);
			ClearMeasureCommand = new DelegateCommand(ClearMeasureCommand_Execute);


			InitGetMethods();
		}
		private async void SelectMeasureCommand_Execute() 
		{
            SelectAndSetWrapper<Measure, MeasureWrapper>(await _getEntitiesForSelectMeasureCommand(), nameof(Item.Measure), Item.Measure?.Id);
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

			InitGetMethods();
		}


    }


    public partial class PersonDetailsViewModel : BaseDetailsViewModel<PersonWrapper, Person, AfterSavePersonEvent>
    {

        public PersonDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class PaymentPlannedDetailsViewModel : BaseDetailsViewModel<PaymentPlannedWrapper, PaymentPlanned, AfterSavePaymentPlannedEvent>
    {
		private Func<Task<List<PaymentCondition>>> _getEntitiesForSelectConditionCommand;
		public ICommand SelectConditionCommand { get; }
		public ICommand ClearConditionCommand { get; }


        public PaymentPlannedDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectConditionCommand = async () => { return await UnitOfWork.GetRepository<PaymentCondition>().GetAllAsync(); };
			SelectConditionCommand = new DelegateCommand(SelectConditionCommand_Execute);
			ClearConditionCommand = new DelegateCommand(ClearConditionCommand_Execute);


			InitGetMethods();
		}
		private async void SelectConditionCommand_Execute() 
		{
            SelectAndSetWrapper<PaymentCondition, PaymentConditionWrapper>(await _getEntitiesForSelectConditionCommand(), nameof(Item.Condition), Item.Condition?.Id);
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

			InitGetMethods();
		}


    }


    public partial class ParameterRelationDetailsViewModel : BaseDetailsViewModel<ParameterRelationWrapper, ParameterRelation, AfterSaveParameterRelationEvent>
    {

        public ParameterRelationDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class SalesUnitDetailsViewModel : BaseDetailsViewModel<SalesUnitWrapper, SalesUnit, AfterSaveSalesUnitEvent>
    {
		private Func<Task<List<Facility>>> _getEntitiesForSelectFacilityCommand;
		public ICommand SelectFacilityCommand { get; }
		public ICommand ClearFacilityCommand { get; }

		private Func<Task<List<Company>>> _getEntitiesForSelectProducerCommand;
		public ICommand SelectProducerCommand { get; }
		public ICommand ClearProducerCommand { get; }

		private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; }
		public ICommand ClearProductCommand { get; }

		private Func<Task<List<Order>>> _getEntitiesForSelectOrderCommand;
		public ICommand SelectOrderCommand { get; }
		public ICommand ClearOrderCommand { get; }

		private Func<Task<List<Specification>>> _getEntitiesForSelectSpecificationCommand;
		public ICommand SelectSpecificationCommand { get; }
		public ICommand ClearSpecificationCommand { get; }

		private Func<Task<List<PaymentConditionSet>>> _getEntitiesForSelectPaymentsConditionSetCommand;
		public ICommand SelectPaymentsConditionSetCommand { get; }
		public ICommand ClearPaymentsConditionSetCommand { get; }

		private Func<Task<List<Address>>> _getEntitiesForSelectAddressCommand;
		public ICommand SelectAddressCommand { get; }
		public ICommand ClearAddressCommand { get; }


        public SalesUnitDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectFacilityCommand = async () => { return await UnitOfWork.GetRepository<Facility>().GetAllAsync(); };
			SelectFacilityCommand = new DelegateCommand(SelectFacilityCommand_Execute);
			ClearFacilityCommand = new DelegateCommand(ClearFacilityCommand_Execute);

          _getEntitiesForSelectProducerCommand = async () => { return await UnitOfWork.GetRepository<Company>().GetAllAsync(); };
			SelectProducerCommand = new DelegateCommand(SelectProducerCommand_Execute);
			ClearProducerCommand = new DelegateCommand(ClearProducerCommand_Execute);

          _getEntitiesForSelectProductCommand = async () => { return await UnitOfWork.GetRepository<Product>().GetAllAsync(); };
			SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute);
			ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute);

          _getEntitiesForSelectOrderCommand = async () => { return await UnitOfWork.GetRepository<Order>().GetAllAsync(); };
			SelectOrderCommand = new DelegateCommand(SelectOrderCommand_Execute);
			ClearOrderCommand = new DelegateCommand(ClearOrderCommand_Execute);

          _getEntitiesForSelectSpecificationCommand = async () => { return await UnitOfWork.GetRepository<Specification>().GetAllAsync(); };
			SelectSpecificationCommand = new DelegateCommand(SelectSpecificationCommand_Execute);
			ClearSpecificationCommand = new DelegateCommand(ClearSpecificationCommand_Execute);

          _getEntitiesForSelectPaymentsConditionSetCommand = async () => { return await UnitOfWork.GetRepository<PaymentConditionSet>().GetAllAsync(); };
			SelectPaymentsConditionSetCommand = new DelegateCommand(SelectPaymentsConditionSetCommand_Execute);
			ClearPaymentsConditionSetCommand = new DelegateCommand(ClearPaymentsConditionSetCommand_Execute);

          _getEntitiesForSelectAddressCommand = async () => { return await UnitOfWork.GetRepository<Address>().GetAllAsync(); };
			SelectAddressCommand = new DelegateCommand(SelectAddressCommand_Execute);
			ClearAddressCommand = new DelegateCommand(ClearAddressCommand_Execute);


			InitGetMethods();
		}
		private async void SelectFacilityCommand_Execute() 
		{
            SelectAndSetWrapper<Facility, FacilityWrapper>(await _getEntitiesForSelectFacilityCommand(), nameof(Item.Facility), Item.Facility?.Id);
		}

		private void ClearFacilityCommand_Execute() 
		{
		    Item.Facility = null;
		}

		private async void SelectProducerCommand_Execute() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectProducerCommand(), nameof(Item.Producer), Item.Producer?.Id);
		}

		private void ClearProducerCommand_Execute() 
		{
		    Item.Producer = null;
		}

		private async void SelectProductCommand_Execute() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(await _getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute() 
		{
		    Item.Product = null;
		}

		private async void SelectOrderCommand_Execute() 
		{
            SelectAndSetWrapper<Order, OrderWrapper>(await _getEntitiesForSelectOrderCommand(), nameof(Item.Order), Item.Order?.Id);
		}

		private void ClearOrderCommand_Execute() 
		{
		    Item.Order = null;
		}

		private async void SelectSpecificationCommand_Execute() 
		{
            SelectAndSetWrapper<Specification, SpecificationWrapper>(await _getEntitiesForSelectSpecificationCommand(), nameof(Item.Specification), Item.Specification?.Id);
		}

		private void ClearSpecificationCommand_Execute() 
		{
		    Item.Specification = null;
		}

		private async void SelectPaymentsConditionSetCommand_Execute() 
		{
            SelectAndSetWrapper<PaymentConditionSet, PaymentConditionSetWrapper>(await _getEntitiesForSelectPaymentsConditionSetCommand(), nameof(Item.PaymentsConditionSet), Item.PaymentsConditionSet?.Id);
		}

		private void ClearPaymentsConditionSetCommand_Execute() 
		{
		    Item.PaymentsConditionSet = null;
		}

		private async void SelectAddressCommand_Execute() 
		{
            SelectAndSetWrapper<Address, AddressWrapper>(await _getEntitiesForSelectAddressCommand(), nameof(Item.Address), Item.Address?.Id);
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

			InitGetMethods();
		}


    }


    public partial class TestFriendDetailsViewModel : BaseDetailsViewModel<TestFriendWrapper, TestFriend, AfterSaveTestFriendEvent>
    {
		private Func<Task<List<TestFriendAddress>>> _getEntitiesForSelectTestFriendAddressCommand;
		public ICommand SelectTestFriendAddressCommand { get; }
		public ICommand ClearTestFriendAddressCommand { get; }

		private Func<Task<List<TestFriendGroup>>> _getEntitiesForSelectTestFriendGroupCommand;
		public ICommand SelectTestFriendGroupCommand { get; }
		public ICommand ClearTestFriendGroupCommand { get; }

		private Func<Task<List<TestFriendEmail>>> _getEntitiesForSelectTestFriendEmailGetCommand;
		public ICommand SelectTestFriendEmailGetCommand { get; }
		public ICommand ClearTestFriendEmailGetCommand { get; }


        public TestFriendDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectTestFriendAddressCommand = async () => { return await UnitOfWork.GetRepository<TestFriendAddress>().GetAllAsync(); };
			SelectTestFriendAddressCommand = new DelegateCommand(SelectTestFriendAddressCommand_Execute);
			ClearTestFriendAddressCommand = new DelegateCommand(ClearTestFriendAddressCommand_Execute);

          _getEntitiesForSelectTestFriendGroupCommand = async () => { return await UnitOfWork.GetRepository<TestFriendGroup>().GetAllAsync(); };
			SelectTestFriendGroupCommand = new DelegateCommand(SelectTestFriendGroupCommand_Execute);
			ClearTestFriendGroupCommand = new DelegateCommand(ClearTestFriendGroupCommand_Execute);

          _getEntitiesForSelectTestFriendEmailGetCommand = async () => { return await UnitOfWork.GetRepository<TestFriendEmail>().GetAllAsync(); };
			SelectTestFriendEmailGetCommand = new DelegateCommand(SelectTestFriendEmailGetCommand_Execute);
			ClearTestFriendEmailGetCommand = new DelegateCommand(ClearTestFriendEmailGetCommand_Execute);


			InitGetMethods();
		}
		private async void SelectTestFriendAddressCommand_Execute() 
		{
            SelectAndSetWrapper<TestFriendAddress, TestFriendAddressWrapper>(await _getEntitiesForSelectTestFriendAddressCommand(), nameof(Item.TestFriendAddress), Item.TestFriendAddress?.Id);
		}

		private void ClearTestFriendAddressCommand_Execute() 
		{
		    Item.TestFriendAddress = null;
		}

		private async void SelectTestFriendGroupCommand_Execute() 
		{
            SelectAndSetWrapper<TestFriendGroup, TestFriendGroupWrapper>(await _getEntitiesForSelectTestFriendGroupCommand(), nameof(Item.TestFriendGroup), Item.TestFriendGroup?.Id);
		}

		private void ClearTestFriendGroupCommand_Execute() 
		{
		    Item.TestFriendGroup = null;
		}

		private async void SelectTestFriendEmailGetCommand_Execute() 
		{
            SelectAndSetWrapper<TestFriendEmail, TestFriendEmailWrapper>(await _getEntitiesForSelectTestFriendEmailGetCommand(), nameof(Item.TestFriendEmailGet), Item.TestFriendEmailGet?.Id);
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

			InitGetMethods();
		}


    }


    public partial class TestFriendGroupDetailsViewModel : BaseDetailsViewModel<TestFriendGroupWrapper, TestFriendGroup, AfterSaveTestFriendGroupEvent>
    {

        public TestFriendGroupDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class DocumentDetailsViewModel : BaseDetailsViewModel<DocumentWrapper, Document, AfterSaveDocumentEvent>
    {
		private Func<Task<List<Document>>> _getEntitiesForSelectRequestDocumentCommand;
		public ICommand SelectRequestDocumentCommand { get; }
		public ICommand ClearRequestDocumentCommand { get; }

		private Func<Task<List<Employee>>> _getEntitiesForSelectAuthorCommand;
		public ICommand SelectAuthorCommand { get; }
		public ICommand ClearAuthorCommand { get; }

		private Func<Task<List<Employee>>> _getEntitiesForSelectSenderEmployeeCommand;
		public ICommand SelectSenderEmployeeCommand { get; }
		public ICommand ClearSenderEmployeeCommand { get; }

		private Func<Task<List<Employee>>> _getEntitiesForSelectRecipientEmployeeCommand;
		public ICommand SelectRecipientEmployeeCommand { get; }
		public ICommand ClearRecipientEmployeeCommand { get; }

		private Func<Task<List<DocumentsRegistrationDetails>>> _getEntitiesForSelectRegistrationDetailsOfSenderCommand;
		public ICommand SelectRegistrationDetailsOfSenderCommand { get; }
		public ICommand ClearRegistrationDetailsOfSenderCommand { get; }

		private Func<Task<List<DocumentsRegistrationDetails>>> _getEntitiesForSelectRegistrationDetailsOfRecipientCommand;
		public ICommand SelectRegistrationDetailsOfRecipientCommand { get; }
		public ICommand ClearRegistrationDetailsOfRecipientCommand { get; }


        public DocumentDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectRequestDocumentCommand = async () => { return await UnitOfWork.GetRepository<Document>().GetAllAsync(); };
			SelectRequestDocumentCommand = new DelegateCommand(SelectRequestDocumentCommand_Execute);
			ClearRequestDocumentCommand = new DelegateCommand(ClearRequestDocumentCommand_Execute);

          _getEntitiesForSelectAuthorCommand = async () => { return await UnitOfWork.GetRepository<Employee>().GetAllAsync(); };
			SelectAuthorCommand = new DelegateCommand(SelectAuthorCommand_Execute);
			ClearAuthorCommand = new DelegateCommand(ClearAuthorCommand_Execute);

          _getEntitiesForSelectSenderEmployeeCommand = async () => { return await UnitOfWork.GetRepository<Employee>().GetAllAsync(); };
			SelectSenderEmployeeCommand = new DelegateCommand(SelectSenderEmployeeCommand_Execute);
			ClearSenderEmployeeCommand = new DelegateCommand(ClearSenderEmployeeCommand_Execute);

          _getEntitiesForSelectRecipientEmployeeCommand = async () => { return await UnitOfWork.GetRepository<Employee>().GetAllAsync(); };
			SelectRecipientEmployeeCommand = new DelegateCommand(SelectRecipientEmployeeCommand_Execute);
			ClearRecipientEmployeeCommand = new DelegateCommand(ClearRecipientEmployeeCommand_Execute);

          _getEntitiesForSelectRegistrationDetailsOfSenderCommand = async () => { return await UnitOfWork.GetRepository<DocumentsRegistrationDetails>().GetAllAsync(); };
			SelectRegistrationDetailsOfSenderCommand = new DelegateCommand(SelectRegistrationDetailsOfSenderCommand_Execute);
			ClearRegistrationDetailsOfSenderCommand = new DelegateCommand(ClearRegistrationDetailsOfSenderCommand_Execute);

          _getEntitiesForSelectRegistrationDetailsOfRecipientCommand = async () => { return await UnitOfWork.GetRepository<DocumentsRegistrationDetails>().GetAllAsync(); };
			SelectRegistrationDetailsOfRecipientCommand = new DelegateCommand(SelectRegistrationDetailsOfRecipientCommand_Execute);
			ClearRegistrationDetailsOfRecipientCommand = new DelegateCommand(ClearRegistrationDetailsOfRecipientCommand_Execute);


			InitGetMethods();
		}
		private async void SelectRequestDocumentCommand_Execute() 
		{
            SelectAndSetWrapper<Document, DocumentWrapper>(await _getEntitiesForSelectRequestDocumentCommand(), nameof(Item.RequestDocument), Item.RequestDocument?.Id);
		}

		private void ClearRequestDocumentCommand_Execute() 
		{
		    Item.RequestDocument = null;
		}

		private async void SelectAuthorCommand_Execute() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectAuthorCommand(), nameof(Item.Author), Item.Author?.Id);
		}

		private void ClearAuthorCommand_Execute() 
		{
		    Item.Author = null;
		}

		private async void SelectSenderEmployeeCommand_Execute() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectSenderEmployeeCommand(), nameof(Item.SenderEmployee), Item.SenderEmployee?.Id);
		}

		private void ClearSenderEmployeeCommand_Execute() 
		{
		    Item.SenderEmployee = null;
		}

		private async void SelectRecipientEmployeeCommand_Execute() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectRecipientEmployeeCommand(), nameof(Item.RecipientEmployee), Item.RecipientEmployee?.Id);
		}

		private void ClearRecipientEmployeeCommand_Execute() 
		{
		    Item.RecipientEmployee = null;
		}

		private async void SelectRegistrationDetailsOfSenderCommand_Execute() 
		{
            SelectAndSetWrapper<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(await _getEntitiesForSelectRegistrationDetailsOfSenderCommand(), nameof(Item.RegistrationDetailsOfSender), Item.RegistrationDetailsOfSender?.Id);
		}

		private void ClearRegistrationDetailsOfSenderCommand_Execute() 
		{
		    Item.RegistrationDetailsOfSender = null;
		}

		private async void SelectRegistrationDetailsOfRecipientCommand_Execute() 
		{
            SelectAndSetWrapper<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(await _getEntitiesForSelectRegistrationDetailsOfRecipientCommand(), nameof(Item.RegistrationDetailsOfRecipient), Item.RegistrationDetailsOfRecipient?.Id);
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

			InitGetMethods();
		}


    }


    public partial class TestHusbandDetailsViewModel : BaseDetailsViewModel<TestHusbandWrapper, TestHusband, AfterSaveTestHusbandEvent>
    {
		private Func<Task<List<TestWife>>> _getEntitiesForSelectWifeCommand;
		public ICommand SelectWifeCommand { get; }
		public ICommand ClearWifeCommand { get; }


        public TestHusbandDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectWifeCommand = async () => { return await UnitOfWork.GetRepository<TestWife>().GetAllAsync(); };
			SelectWifeCommand = new DelegateCommand(SelectWifeCommand_Execute);
			ClearWifeCommand = new DelegateCommand(ClearWifeCommand_Execute);


			InitGetMethods();
		}
		private async void SelectWifeCommand_Execute() 
		{
            SelectAndSetWrapper<TestWife, TestWifeWrapper>(await _getEntitiesForSelectWifeCommand(), nameof(Item.Wife), Item.Wife?.Id);
		}

		private void ClearWifeCommand_Execute() 
		{
		    Item.Wife = null;
		}



    }


    public partial class TestWifeDetailsViewModel : BaseDetailsViewModel<TestWifeWrapper, TestWife, AfterSaveTestWifeEvent>
    {
		private Func<Task<List<TestHusband>>> _getEntitiesForSelectHusbandCommand;
		public ICommand SelectHusbandCommand { get; }
		public ICommand ClearHusbandCommand { get; }


        public TestWifeDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectHusbandCommand = async () => { return await UnitOfWork.GetRepository<TestHusband>().GetAllAsync(); };
			SelectHusbandCommand = new DelegateCommand(SelectHusbandCommand_Execute);
			ClearHusbandCommand = new DelegateCommand(ClearHusbandCommand_Execute);


			InitGetMethods();
		}
		private async void SelectHusbandCommand_Execute() 
		{
            SelectAndSetWrapper<TestHusband, TestHusbandWrapper>(await _getEntitiesForSelectHusbandCommand(), nameof(Item.Husband), Item.Husband?.Id);
		}

		private void ClearHusbandCommand_Execute() 
		{
		    Item.Husband = null;
		}



    }


    public partial class TestChildDetailsViewModel : BaseDetailsViewModel<TestChildWrapper, TestChild, AfterSaveTestChildEvent>
    {
		private Func<Task<List<TestHusband>>> _getEntitiesForSelectHusbandCommand;
		public ICommand SelectHusbandCommand { get; }
		public ICommand ClearHusbandCommand { get; }

		private Func<Task<List<TestWife>>> _getEntitiesForSelectWifeCommand;
		public ICommand SelectWifeCommand { get; }
		public ICommand ClearWifeCommand { get; }


        public TestChildDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectHusbandCommand = async () => { return await UnitOfWork.GetRepository<TestHusband>().GetAllAsync(); };
			SelectHusbandCommand = new DelegateCommand(SelectHusbandCommand_Execute);
			ClearHusbandCommand = new DelegateCommand(ClearHusbandCommand_Execute);

          _getEntitiesForSelectWifeCommand = async () => { return await UnitOfWork.GetRepository<TestWife>().GetAllAsync(); };
			SelectWifeCommand = new DelegateCommand(SelectWifeCommand_Execute);
			ClearWifeCommand = new DelegateCommand(ClearWifeCommand_Execute);


			InitGetMethods();
		}
		private async void SelectHusbandCommand_Execute() 
		{
            SelectAndSetWrapper<TestHusband, TestHusbandWrapper>(await _getEntitiesForSelectHusbandCommand(), nameof(Item.Husband), Item.Husband?.Id);
		}

		private void ClearHusbandCommand_Execute() 
		{
		    Item.Husband = null;
		}

		private async void SelectWifeCommand_Execute() 
		{
            SelectAndSetWrapper<TestWife, TestWifeWrapper>(await _getEntitiesForSelectWifeCommand(), nameof(Item.Wife), Item.Wife?.Id);
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

			InitGetMethods();
		}


    }


    public partial class ProductDetailsViewModel : BaseDetailsViewModel<ProductWrapper, Product, AfterSaveProductEvent>
    {
		private Func<Task<List<ProductBlock>>> _getEntitiesForSelectProductBlockCommand;
		public ICommand SelectProductBlockCommand { get; }
		public ICommand ClearProductBlockCommand { get; }


        public ProductDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectProductBlockCommand = async () => { return await UnitOfWork.GetRepository<ProductBlock>().GetAllAsync(); };
			SelectProductBlockCommand = new DelegateCommand(SelectProductBlockCommand_Execute);
			ClearProductBlockCommand = new DelegateCommand(ClearProductBlockCommand_Execute);


			InitGetMethods();
		}
		private async void SelectProductBlockCommand_Execute() 
		{
            SelectAndSetWrapper<ProductBlock, ProductBlockWrapper>(await _getEntitiesForSelectProductBlockCommand(), nameof(Item.ProductBlock), Item.ProductBlock?.Id);
		}

		private void ClearProductBlockCommand_Execute() 
		{
		    Item.ProductBlock = null;
		}



    }


    public partial class OfferDetailsViewModel : BaseDetailsViewModel<OfferWrapper, Offer, AfterSaveOfferEvent>
    {
		private Func<Task<List<Project>>> _getEntitiesForSelectProjectCommand;
		public ICommand SelectProjectCommand { get; }
		public ICommand ClearProjectCommand { get; }

		private Func<Task<List<Document>>> _getEntitiesForSelectRequestDocumentCommand;
		public ICommand SelectRequestDocumentCommand { get; }
		public ICommand ClearRequestDocumentCommand { get; }

		private Func<Task<List<Employee>>> _getEntitiesForSelectAuthorCommand;
		public ICommand SelectAuthorCommand { get; }
		public ICommand ClearAuthorCommand { get; }

		private Func<Task<List<Employee>>> _getEntitiesForSelectSenderEmployeeCommand;
		public ICommand SelectSenderEmployeeCommand { get; }
		public ICommand ClearSenderEmployeeCommand { get; }

		private Func<Task<List<Employee>>> _getEntitiesForSelectRecipientEmployeeCommand;
		public ICommand SelectRecipientEmployeeCommand { get; }
		public ICommand ClearRecipientEmployeeCommand { get; }

		private Func<Task<List<DocumentsRegistrationDetails>>> _getEntitiesForSelectRegistrationDetailsOfSenderCommand;
		public ICommand SelectRegistrationDetailsOfSenderCommand { get; }
		public ICommand ClearRegistrationDetailsOfSenderCommand { get; }

		private Func<Task<List<DocumentsRegistrationDetails>>> _getEntitiesForSelectRegistrationDetailsOfRecipientCommand;
		public ICommand SelectRegistrationDetailsOfRecipientCommand { get; }
		public ICommand ClearRegistrationDetailsOfRecipientCommand { get; }


        public OfferDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectProjectCommand = async () => { return await UnitOfWork.GetRepository<Project>().GetAllAsync(); };
			SelectProjectCommand = new DelegateCommand(SelectProjectCommand_Execute);
			ClearProjectCommand = new DelegateCommand(ClearProjectCommand_Execute);

          _getEntitiesForSelectRequestDocumentCommand = async () => { return await UnitOfWork.GetRepository<Document>().GetAllAsync(); };
			SelectRequestDocumentCommand = new DelegateCommand(SelectRequestDocumentCommand_Execute);
			ClearRequestDocumentCommand = new DelegateCommand(ClearRequestDocumentCommand_Execute);

          _getEntitiesForSelectAuthorCommand = async () => { return await UnitOfWork.GetRepository<Employee>().GetAllAsync(); };
			SelectAuthorCommand = new DelegateCommand(SelectAuthorCommand_Execute);
			ClearAuthorCommand = new DelegateCommand(ClearAuthorCommand_Execute);

          _getEntitiesForSelectSenderEmployeeCommand = async () => { return await UnitOfWork.GetRepository<Employee>().GetAllAsync(); };
			SelectSenderEmployeeCommand = new DelegateCommand(SelectSenderEmployeeCommand_Execute);
			ClearSenderEmployeeCommand = new DelegateCommand(ClearSenderEmployeeCommand_Execute);

          _getEntitiesForSelectRecipientEmployeeCommand = async () => { return await UnitOfWork.GetRepository<Employee>().GetAllAsync(); };
			SelectRecipientEmployeeCommand = new DelegateCommand(SelectRecipientEmployeeCommand_Execute);
			ClearRecipientEmployeeCommand = new DelegateCommand(ClearRecipientEmployeeCommand_Execute);

          _getEntitiesForSelectRegistrationDetailsOfSenderCommand = async () => { return await UnitOfWork.GetRepository<DocumentsRegistrationDetails>().GetAllAsync(); };
			SelectRegistrationDetailsOfSenderCommand = new DelegateCommand(SelectRegistrationDetailsOfSenderCommand_Execute);
			ClearRegistrationDetailsOfSenderCommand = new DelegateCommand(ClearRegistrationDetailsOfSenderCommand_Execute);

          _getEntitiesForSelectRegistrationDetailsOfRecipientCommand = async () => { return await UnitOfWork.GetRepository<DocumentsRegistrationDetails>().GetAllAsync(); };
			SelectRegistrationDetailsOfRecipientCommand = new DelegateCommand(SelectRegistrationDetailsOfRecipientCommand_Execute);
			ClearRegistrationDetailsOfRecipientCommand = new DelegateCommand(ClearRegistrationDetailsOfRecipientCommand_Execute);


			InitGetMethods();
		}
		private async void SelectProjectCommand_Execute() 
		{
            SelectAndSetWrapper<Project, ProjectWrapper>(await _getEntitiesForSelectProjectCommand(), nameof(Item.Project), Item.Project?.Id);
		}

		private void ClearProjectCommand_Execute() 
		{
		    Item.Project = null;
		}

		private async void SelectRequestDocumentCommand_Execute() 
		{
            SelectAndSetWrapper<Document, DocumentWrapper>(await _getEntitiesForSelectRequestDocumentCommand(), nameof(Item.RequestDocument), Item.RequestDocument?.Id);
		}

		private void ClearRequestDocumentCommand_Execute() 
		{
		    Item.RequestDocument = null;
		}

		private async void SelectAuthorCommand_Execute() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectAuthorCommand(), nameof(Item.Author), Item.Author?.Id);
		}

		private void ClearAuthorCommand_Execute() 
		{
		    Item.Author = null;
		}

		private async void SelectSenderEmployeeCommand_Execute() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectSenderEmployeeCommand(), nameof(Item.SenderEmployee), Item.SenderEmployee?.Id);
		}

		private void ClearSenderEmployeeCommand_Execute() 
		{
		    Item.SenderEmployee = null;
		}

		private async void SelectRecipientEmployeeCommand_Execute() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectRecipientEmployeeCommand(), nameof(Item.RecipientEmployee), Item.RecipientEmployee?.Id);
		}

		private void ClearRecipientEmployeeCommand_Execute() 
		{
		    Item.RecipientEmployee = null;
		}

		private async void SelectRegistrationDetailsOfSenderCommand_Execute() 
		{
            SelectAndSetWrapper<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(await _getEntitiesForSelectRegistrationDetailsOfSenderCommand(), nameof(Item.RegistrationDetailsOfSender), Item.RegistrationDetailsOfSender?.Id);
		}

		private void ClearRegistrationDetailsOfSenderCommand_Execute() 
		{
		    Item.RegistrationDetailsOfSender = null;
		}

		private async void SelectRegistrationDetailsOfRecipientCommand_Execute() 
		{
            SelectAndSetWrapper<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(await _getEntitiesForSelectRegistrationDetailsOfRecipientCommand(), nameof(Item.RegistrationDetailsOfRecipient), Item.RegistrationDetailsOfRecipient?.Id);
		}

		private void ClearRegistrationDetailsOfRecipientCommand_Execute() 
		{
		    Item.RegistrationDetailsOfRecipient = null;
		}



    }


    public partial class EmployeeDetailsViewModel : BaseDetailsViewModel<EmployeeWrapper, Employee, AfterSaveEmployeeEvent>
    {
		private Func<Task<List<Company>>> _getEntitiesForSelectCompanyCommand;
		public ICommand SelectCompanyCommand { get; }
		public ICommand ClearCompanyCommand { get; }

		private Func<Task<List<EmployeesPosition>>> _getEntitiesForSelectPositionCommand;
		public ICommand SelectPositionCommand { get; }
		public ICommand ClearPositionCommand { get; }


        public EmployeeDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectCompanyCommand = async () => { return await UnitOfWork.GetRepository<Company>().GetAllAsync(); };
			SelectCompanyCommand = new DelegateCommand(SelectCompanyCommand_Execute);
			ClearCompanyCommand = new DelegateCommand(ClearCompanyCommand_Execute);

          _getEntitiesForSelectPositionCommand = async () => { return await UnitOfWork.GetRepository<EmployeesPosition>().GetAllAsync(); };
			SelectPositionCommand = new DelegateCommand(SelectPositionCommand_Execute);
			ClearPositionCommand = new DelegateCommand(ClearPositionCommand_Execute);


			InitGetMethods();
		}
		private async void SelectCompanyCommand_Execute() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectCompanyCommand(), nameof(Item.Company), Item.Company?.Id);
		}

		private void ClearCompanyCommand_Execute() 
		{
		    Item.Company = null;
		}

		private async void SelectPositionCommand_Execute() 
		{
            SelectAndSetWrapper<EmployeesPosition, EmployeesPositionWrapper>(await _getEntitiesForSelectPositionCommand(), nameof(Item.Position), Item.Position?.Id);
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

			InitGetMethods();
		}


    }


    public partial class PaymentConditionDetailsViewModel : BaseDetailsViewModel<PaymentConditionWrapper, PaymentCondition, AfterSavePaymentConditionEvent>
    {

        public PaymentConditionDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class PaymentDocumentDetailsViewModel : BaseDetailsViewModel<PaymentDocumentWrapper, PaymentDocument, AfterSavePaymentDocumentEvent>
    {

        public PaymentDocumentDetailsViewModel(IUnityContainer container) : base(container) 
		{

			InitGetMethods();
		}


    }


    public partial class FacilityDetailsViewModel : BaseDetailsViewModel<FacilityWrapper, Facility, AfterSaveFacilityEvent>
    {
		private Func<Task<List<FacilityType>>> _getEntitiesForSelectTypeCommand;
		public ICommand SelectTypeCommand { get; }
		public ICommand ClearTypeCommand { get; }

		private Func<Task<List<Company>>> _getEntitiesForSelectOwnerCompanyCommand;
		public ICommand SelectOwnerCompanyCommand { get; }
		public ICommand ClearOwnerCompanyCommand { get; }

		private Func<Task<List<Address>>> _getEntitiesForSelectAddressCommand;
		public ICommand SelectAddressCommand { get; }
		public ICommand ClearAddressCommand { get; }


        public FacilityDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectTypeCommand = async () => { return await UnitOfWork.GetRepository<FacilityType>().GetAllAsync(); };
			SelectTypeCommand = new DelegateCommand(SelectTypeCommand_Execute);
			ClearTypeCommand = new DelegateCommand(ClearTypeCommand_Execute);

          _getEntitiesForSelectOwnerCompanyCommand = async () => { return await UnitOfWork.GetRepository<Company>().GetAllAsync(); };
			SelectOwnerCompanyCommand = new DelegateCommand(SelectOwnerCompanyCommand_Execute);
			ClearOwnerCompanyCommand = new DelegateCommand(ClearOwnerCompanyCommand_Execute);

          _getEntitiesForSelectAddressCommand = async () => { return await UnitOfWork.GetRepository<Address>().GetAllAsync(); };
			SelectAddressCommand = new DelegateCommand(SelectAddressCommand_Execute);
			ClearAddressCommand = new DelegateCommand(ClearAddressCommand_Execute);


			InitGetMethods();
		}
		private async void SelectTypeCommand_Execute() 
		{
            SelectAndSetWrapper<FacilityType, FacilityTypeWrapper>(await _getEntitiesForSelectTypeCommand(), nameof(Item.Type), Item.Type?.Id);
		}

		private void ClearTypeCommand_Execute() 
		{
		    Item.Type = null;
		}

		private async void SelectOwnerCompanyCommand_Execute() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectOwnerCompanyCommand(), nameof(Item.OwnerCompany), Item.OwnerCompany?.Id);
		}

		private void ClearOwnerCompanyCommand_Execute() 
		{
		    Item.OwnerCompany = null;
		}

		private async void SelectAddressCommand_Execute() 
		{
            SelectAndSetWrapper<Address, AddressWrapper>(await _getEntitiesForSelectAddressCommand(), nameof(Item.Address), Item.Address?.Id);
		}

		private void ClearAddressCommand_Execute() 
		{
		    Item.Address = null;
		}



    }


    public partial class ProjectDetailsViewModel : BaseDetailsViewModel<ProjectWrapper, Project, AfterSaveProjectEvent>
    {
		private Func<Task<List<User>>> _getEntitiesForSelectManagerCommand;
		public ICommand SelectManagerCommand { get; }
		public ICommand ClearManagerCommand { get; }


        public ProjectDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectManagerCommand = async () => { return await UnitOfWork.GetRepository<User>().GetAllAsync(); };
			SelectManagerCommand = new DelegateCommand(SelectManagerCommand_Execute);
			ClearManagerCommand = new DelegateCommand(ClearManagerCommand_Execute);


			InitGetMethods();
		}
		private async void SelectManagerCommand_Execute() 
		{
            SelectAndSetWrapper<User, UserWrapper>(await _getEntitiesForSelectManagerCommand(), nameof(Item.Manager), Item.Manager?.Id);
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

			InitGetMethods();
		}


    }


    public partial class SpecificationDetailsViewModel : BaseDetailsViewModel<SpecificationWrapper, Specification, AfterSaveSpecificationEvent>
    {
		private Func<Task<List<Contract>>> _getEntitiesForSelectContractCommand;
		public ICommand SelectContractCommand { get; }
		public ICommand ClearContractCommand { get; }


        public SpecificationDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectContractCommand = async () => { return await UnitOfWork.GetRepository<Contract>().GetAllAsync(); };
			SelectContractCommand = new DelegateCommand(SelectContractCommand_Execute);
			ClearContractCommand = new DelegateCommand(ClearContractCommand_Execute);


			InitGetMethods();
		}
		private async void SelectContractCommand_Execute() 
		{
            SelectAndSetWrapper<Contract, ContractWrapper>(await _getEntitiesForSelectContractCommand(), nameof(Item.Contract), Item.Contract?.Id);
		}

		private void ClearContractCommand_Execute() 
		{
		    Item.Contract = null;
		}



    }


    public partial class TenderDetailsViewModel : BaseDetailsViewModel<TenderWrapper, Tender, AfterSaveTenderEvent>
    {
		private Func<Task<List<Project>>> _getEntitiesForSelectProjectCommand;
		public ICommand SelectProjectCommand { get; }
		public ICommand ClearProjectCommand { get; }

		private Func<Task<List<Company>>> _getEntitiesForSelectWinnerCommand;
		public ICommand SelectWinnerCommand { get; }
		public ICommand ClearWinnerCommand { get; }


        public TenderDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectProjectCommand = async () => { return await UnitOfWork.GetRepository<Project>().GetAllAsync(); };
			SelectProjectCommand = new DelegateCommand(SelectProjectCommand_Execute);
			ClearProjectCommand = new DelegateCommand(ClearProjectCommand_Execute);

          _getEntitiesForSelectWinnerCommand = async () => { return await UnitOfWork.GetRepository<Company>().GetAllAsync(); };
			SelectWinnerCommand = new DelegateCommand(SelectWinnerCommand_Execute);
			ClearWinnerCommand = new DelegateCommand(ClearWinnerCommand_Execute);


			InitGetMethods();
		}
		private async void SelectProjectCommand_Execute() 
		{
            SelectAndSetWrapper<Project, ProjectWrapper>(await _getEntitiesForSelectProjectCommand(), nameof(Item.Project), Item.Project?.Id);
		}

		private void ClearProjectCommand_Execute() 
		{
		    Item.Project = null;
		}

		private async void SelectWinnerCommand_Execute() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectWinnerCommand(), nameof(Item.Winner), Item.Winner?.Id);
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

			InitGetMethods();
		}


    }


    public partial class UserDetailsViewModel : BaseDetailsViewModel<UserWrapper, User, AfterSaveUserEvent>
    {
		private Func<Task<List<Employee>>> _getEntitiesForSelectEmployeeCommand;
		public ICommand SelectEmployeeCommand { get; }
		public ICommand ClearEmployeeCommand { get; }


        public UserDetailsViewModel(IUnityContainer container) : base(container) 
		{
          _getEntitiesForSelectEmployeeCommand = async () => { return await UnitOfWork.GetRepository<Employee>().GetAllAsync(); };
			SelectEmployeeCommand = new DelegateCommand(SelectEmployeeCommand_Execute);
			ClearEmployeeCommand = new DelegateCommand(ClearEmployeeCommand_Execute);


			InitGetMethods();
		}
		private async void SelectEmployeeCommand_Execute() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectEmployeeCommand(), nameof(Item.Employee), Item.Employee?.Id);
		}

		private void ClearEmployeeCommand_Execute() 
		{
		    Item.Employee = null;
		}



    }


}
