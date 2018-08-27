














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
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class AddressDetailsViewModel : BaseDetailsViewModel<AddressWrapper, Address, AfterSaveAddressEvent>
    {
		private Func<Task<List<Locality>>> _getEntitiesForSelectLocalityCommand;
		public ICommand SelectLocalityCommand { get; private set; }
		public ICommand ClearLocalityCommand { get; private set; }


        public AddressDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectLocalityCommand == null) SelectLocalityCommand = new DelegateCommand(SelectLocalityCommand_Execute);
			if (ClearLocalityCommand == null) ClearLocalityCommand = new DelegateCommand(ClearLocalityCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectLocalityCommand = async () => { return await WrapperDataService.GetRepository<Locality>().GetAllAsync(); };

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
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class DistrictDetailsViewModel : BaseDetailsViewModel<DistrictWrapper, District, AfterSaveDistrictEvent>
    {
		private Func<Task<List<Country>>> _getEntitiesForSelectCountryCommand;
		public ICommand SelectCountryCommand { get; private set; }
		public ICommand ClearCountryCommand { get; private set; }


        public DistrictDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectCountryCommand == null) SelectCountryCommand = new DelegateCommand(SelectCountryCommand_Execute);
			if (ClearCountryCommand == null) ClearCountryCommand = new DelegateCommand(ClearCountryCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectCountryCommand = async () => { return await WrapperDataService.GetRepository<Country>().GetAllAsync(); };

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
		public ICommand SelectLocalityTypeCommand { get; private set; }
		public ICommand ClearLocalityTypeCommand { get; private set; }

		private Func<Task<List<Region>>> _getEntitiesForSelectRegionCommand;
		public ICommand SelectRegionCommand { get; private set; }
		public ICommand ClearRegionCommand { get; private set; }


        public LocalityDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectLocalityTypeCommand == null) SelectLocalityTypeCommand = new DelegateCommand(SelectLocalityTypeCommand_Execute);
			if (ClearLocalityTypeCommand == null) ClearLocalityTypeCommand = new DelegateCommand(ClearLocalityTypeCommand_Execute);

			if (SelectRegionCommand == null) SelectRegionCommand = new DelegateCommand(SelectRegionCommand_Execute);
			if (ClearRegionCommand == null) ClearRegionCommand = new DelegateCommand(ClearRegionCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectLocalityTypeCommand = async () => { return await WrapperDataService.GetRepository<LocalityType>().GetAllAsync(); };
            _getEntitiesForSelectRegionCommand = async () => { return await WrapperDataService.GetRepository<Region>().GetAllAsync(); };

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
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class RegionDetailsViewModel : BaseDetailsViewModel<RegionWrapper, Region, AfterSaveRegionEvent>
    {
		private Func<Task<List<District>>> _getEntitiesForSelectDistrictCommand;
		public ICommand SelectDistrictCommand { get; private set; }
		public ICommand ClearDistrictCommand { get; private set; }


        public RegionDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectDistrictCommand == null) SelectDistrictCommand = new DelegateCommand(SelectDistrictCommand_Execute);
			if (ClearDistrictCommand == null) ClearDistrictCommand = new DelegateCommand(ClearDistrictCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectDistrictCommand = async () => { return await WrapperDataService.GetRepository<District>().GetAllAsync(); };

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
		public ICommand SelectProductBlockCommand { get; private set; }
		public ICommand ClearProductBlockCommand { get; private set; }

		private Func<Task<List<Project>>> _getEntitiesForAddInProjectsCommand;
		public ICommand AddInProjectsCommand { get; }
		public ICommand RemoveFromProjectsCommand { get; }
		private ProjectWrapper _selectedProjectsItem;
		public ProjectWrapper SelectedProjectsItem 
		{ 
			get { return _selectedProjectsItem; }
			set 
			{ 
				if (Equals(_selectedProjectsItem, value)) return;
				_selectedProjectsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromProjectsCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<Offer>>> _getEntitiesForAddInOffersCommand;
		public ICommand AddInOffersCommand { get; }
		public ICommand RemoveFromOffersCommand { get; }
		private OfferWrapper _selectedOffersItem;
		public OfferWrapper SelectedOffersItem 
		{ 
			get { return _selectedOffersItem; }
			set 
			{ 
				if (Equals(_selectedOffersItem, value)) return;
				_selectedOffersItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromOffersCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<Specification>>> _getEntitiesForAddInSpecificationsCommand;
		public ICommand AddInSpecificationsCommand { get; }
		public ICommand RemoveFromSpecificationsCommand { get; }
		private SpecificationWrapper _selectedSpecificationsItem;
		public SpecificationWrapper SelectedSpecificationsItem 
		{ 
			get { return _selectedSpecificationsItem; }
			set 
			{ 
				if (Equals(_selectedSpecificationsItem, value)) return;
				_selectedSpecificationsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromSpecificationsCommand).RaiseCanExecuteChanged();
			}
		}


        public CalculatePriceTaskDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectProductBlockCommand == null) SelectProductBlockCommand = new DelegateCommand(SelectProductBlockCommand_Execute);
			if (ClearProductBlockCommand == null) ClearProductBlockCommand = new DelegateCommand(ClearProductBlockCommand_Execute);

			AddInProjectsCommand = new DelegateCommand(AddInProjectsCommand_Execute);
			RemoveFromProjectsCommand = new DelegateCommand(RemoveFromProjectsCommand_Execute, RemoveFromProjectsCommand_CanExecute);

			AddInOffersCommand = new DelegateCommand(AddInOffersCommand_Execute);
			RemoveFromOffersCommand = new DelegateCommand(RemoveFromOffersCommand_Execute, RemoveFromOffersCommand_CanExecute);

			AddInSpecificationsCommand = new DelegateCommand(AddInSpecificationsCommand_Execute);
			RemoveFromSpecificationsCommand = new DelegateCommand(RemoveFromSpecificationsCommand_Execute, RemoveFromSpecificationsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectProductBlockCommand = async () => { return await WrapperDataService.GetRepository<ProductBlock>().GetAllAsync(); };
			_getEntitiesForAddInProjectsCommand = async () => { return await WrapperDataService.GetRepository<Project>().GetAllAsync(); };;
			_getEntitiesForAddInOffersCommand = async () => { return await WrapperDataService.GetRepository<Offer>().GetAllAsync(); };;
			_getEntitiesForAddInSpecificationsCommand = async () => { return await WrapperDataService.GetRepository<Specification>().GetAllAsync(); };;

		}
		private async void SelectProductBlockCommand_Execute() 
		{
            SelectAndSetWrapper<ProductBlock, ProductBlockWrapper>(await _getEntitiesForSelectProductBlockCommand(), nameof(Item.ProductBlock), Item.ProductBlock?.Id);
		}

		private void ClearProductBlockCommand_Execute() 
		{
		Item.ProductBlock = null;
		    
		}

			private async void AddInProjectsCommand_Execute()
			{
				SelectAndAddInListWrapper<Project, ProjectWrapper>(await _getEntitiesForAddInProjectsCommand(), Item.Projects);
			}

			private void RemoveFromProjectsCommand_Execute()
			{
				Item.Projects.Remove(SelectedProjectsItem);
			}

			private bool RemoveFromProjectsCommand_CanExecute()
			{
				return SelectedProjectsItem != null;
			}

			private async void AddInOffersCommand_Execute()
			{
				SelectAndAddInListWrapper<Offer, OfferWrapper>(await _getEntitiesForAddInOffersCommand(), Item.Offers);
			}

			private void RemoveFromOffersCommand_Execute()
			{
				Item.Offers.Remove(SelectedOffersItem);
			}

			private bool RemoveFromOffersCommand_CanExecute()
			{
				return SelectedOffersItem != null;
			}

			private async void AddInSpecificationsCommand_Execute()
			{
				SelectAndAddInListWrapper<Specification, SpecificationWrapper>(await _getEntitiesForAddInSpecificationsCommand(), Item.Specifications);
			}

			private void RemoveFromSpecificationsCommand_Execute()
			{
				Item.Specifications.Remove(SelectedSpecificationsItem);
			}

			private bool RemoveFromSpecificationsCommand_CanExecute()
			{
				return SelectedSpecificationsItem != null;
			}



    }


    public partial class SumDetailsViewModel : BaseDetailsViewModel<SumWrapper, Sum, AfterSaveSumEvent>
    {

        public SumDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class CurrencyExchangeRateDetailsViewModel : BaseDetailsViewModel<CurrencyExchangeRateWrapper, CurrencyExchangeRate, AfterSaveCurrencyExchangeRateEvent>
    {

        public CurrencyExchangeRateDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class DescribeProductBlockTaskDetailsViewModel : BaseDetailsViewModel<DescribeProductBlockTaskWrapper, DescribeProductBlockTask, AfterSaveDescribeProductBlockTaskEvent>
    {
		private Func<Task<List<ProductBlock>>> _getEntitiesForSelectProductBlockCommand;
		public ICommand SelectProductBlockCommand { get; private set; }
		public ICommand ClearProductBlockCommand { get; private set; }

		private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; private set; }
		public ICommand ClearProductCommand { get; private set; }


        public DescribeProductBlockTaskDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectProductBlockCommand == null) SelectProductBlockCommand = new DelegateCommand(SelectProductBlockCommand_Execute);
			if (ClearProductBlockCommand == null) ClearProductBlockCommand = new DelegateCommand(ClearProductBlockCommand_Execute);

			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectProductBlockCommand = async () => { return await WrapperDataService.GetRepository<ProductBlock>().GetAllAsync(); };
            _getEntitiesForSelectProductCommand = async () => { return await WrapperDataService.GetRepository<Product>().GetAllAsync(); };

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


    public partial class NoteDetailsViewModel : BaseDetailsViewModel<NoteWrapper, Note, AfterSaveNoteEvent>
    {

        public NoteDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class OfferUnitDetailsViewModel : BaseDetailsViewModel<OfferUnitWrapper, OfferUnit, AfterSaveOfferUnitEvent>
    {
		private Func<Task<List<Offer>>> _getEntitiesForSelectOfferCommand;
		public ICommand SelectOfferCommand { get; private set; }
		public ICommand ClearOfferCommand { get; private set; }

		private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; private set; }
		public ICommand ClearProductCommand { get; private set; }

		private Func<Task<List<Facility>>> _getEntitiesForSelectFacilityCommand;
		public ICommand SelectFacilityCommand { get; private set; }
		public ICommand ClearFacilityCommand { get; private set; }

		private Func<Task<List<PaymentConditionSet>>> _getEntitiesForSelectPaymentConditionSetCommand;
		public ICommand SelectPaymentConditionSetCommand { get; private set; }
		public ICommand ClearPaymentConditionSetCommand { get; private set; }

		private Func<Task<List<ProductDependent>>> _getEntitiesForAddInDependentProductsCommand;
		public ICommand AddInDependentProductsCommand { get; }
		public ICommand RemoveFromDependentProductsCommand { get; }
		private ProductDependentWrapper _selectedDependentProductsItem;
		public ProductDependentWrapper SelectedDependentProductsItem 
		{ 
			get { return _selectedDependentProductsItem; }
			set 
			{ 
				if (Equals(_selectedDependentProductsItem, value)) return;
				_selectedDependentProductsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromDependentProductsCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<Service>>> _getEntitiesForAddInServicesCommand;
		public ICommand AddInServicesCommand { get; }
		public ICommand RemoveFromServicesCommand { get; }
		private ServiceWrapper _selectedServicesItem;
		public ServiceWrapper SelectedServicesItem 
		{ 
			get { return _selectedServicesItem; }
			set 
			{ 
				if (Equals(_selectedServicesItem, value)) return;
				_selectedServicesItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromServicesCommand).RaiseCanExecuteChanged();
			}
		}


        public OfferUnitDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectOfferCommand == null) SelectOfferCommand = new DelegateCommand(SelectOfferCommand_Execute);
			if (ClearOfferCommand == null) ClearOfferCommand = new DelegateCommand(ClearOfferCommand_Execute);

			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute);

			if (SelectFacilityCommand == null) SelectFacilityCommand = new DelegateCommand(SelectFacilityCommand_Execute);
			if (ClearFacilityCommand == null) ClearFacilityCommand = new DelegateCommand(ClearFacilityCommand_Execute);

			if (SelectPaymentConditionSetCommand == null) SelectPaymentConditionSetCommand = new DelegateCommand(SelectPaymentConditionSetCommand_Execute);
			if (ClearPaymentConditionSetCommand == null) ClearPaymentConditionSetCommand = new DelegateCommand(ClearPaymentConditionSetCommand_Execute);

			AddInDependentProductsCommand = new DelegateCommand(AddInDependentProductsCommand_Execute);
			RemoveFromDependentProductsCommand = new DelegateCommand(RemoveFromDependentProductsCommand_Execute, RemoveFromDependentProductsCommand_CanExecute);

			AddInServicesCommand = new DelegateCommand(AddInServicesCommand_Execute);
			RemoveFromServicesCommand = new DelegateCommand(RemoveFromServicesCommand_Execute, RemoveFromServicesCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectOfferCommand = async () => { return await WrapperDataService.GetRepository<Offer>().GetAllAsync(); };
            _getEntitiesForSelectProductCommand = async () => { return await WrapperDataService.GetRepository<Product>().GetAllAsync(); };
            _getEntitiesForSelectFacilityCommand = async () => { return await WrapperDataService.GetRepository<Facility>().GetAllAsync(); };
            _getEntitiesForSelectPaymentConditionSetCommand = async () => { return await WrapperDataService.GetRepository<PaymentConditionSet>().GetAllAsync(); };
			_getEntitiesForAddInDependentProductsCommand = async () => { return await WrapperDataService.GetRepository<ProductDependent>().GetAllAsync(); };;
			_getEntitiesForAddInServicesCommand = async () => { return await WrapperDataService.GetRepository<Service>().GetAllAsync(); };;

		}
		private async void SelectOfferCommand_Execute() 
		{
            SelectAndSetWrapper<Offer, OfferWrapper>(await _getEntitiesForSelectOfferCommand(), nameof(Item.Offer), Item.Offer?.Id);
		}

		private void ClearOfferCommand_Execute() 
		{
		Item.Offer = null;
		    
		}

		private async void SelectProductCommand_Execute() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(await _getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute() 
		{
		Item.Product = null;
		    
		}

		private async void SelectFacilityCommand_Execute() 
		{
            SelectAndSetWrapper<Facility, FacilityWrapper>(await _getEntitiesForSelectFacilityCommand(), nameof(Item.Facility), Item.Facility?.Id);
		}

		private void ClearFacilityCommand_Execute() 
		{
		Item.Facility = null;
		    
		}

		private async void SelectPaymentConditionSetCommand_Execute() 
		{
            SelectAndSetWrapper<PaymentConditionSet, PaymentConditionSetWrapper>(await _getEntitiesForSelectPaymentConditionSetCommand(), nameof(Item.PaymentConditionSet), Item.PaymentConditionSet?.Id);
		}

		private void ClearPaymentConditionSetCommand_Execute() 
		{
		Item.PaymentConditionSet = null;
		    
		}

			private async void AddInDependentProductsCommand_Execute()
			{
				SelectAndAddInListWrapper<ProductDependent, ProductDependentWrapper>(await _getEntitiesForAddInDependentProductsCommand(), Item.DependentProducts);
			}

			private void RemoveFromDependentProductsCommand_Execute()
			{
				Item.DependentProducts.Remove(SelectedDependentProductsItem);
			}

			private bool RemoveFromDependentProductsCommand_CanExecute()
			{
				return SelectedDependentProductsItem != null;
			}

			private async void AddInServicesCommand_Execute()
			{
				SelectAndAddInListWrapper<Service, ServiceWrapper>(await _getEntitiesForAddInServicesCommand(), Item.Services);
			}

			private void RemoveFromServicesCommand_Execute()
			{
				Item.Services.Remove(SelectedServicesItem);
			}

			private bool RemoveFromServicesCommand_CanExecute()
			{
				return SelectedServicesItem != null;
			}



    }


    public partial class PaymentConditionSetDetailsViewModel : BaseDetailsViewModel<PaymentConditionSetWrapper, PaymentConditionSet, AfterSavePaymentConditionSetEvent>
    {
		private Func<Task<List<PaymentCondition>>> _getEntitiesForAddInPaymentConditionsCommand;
		public ICommand AddInPaymentConditionsCommand { get; }
		public ICommand RemoveFromPaymentConditionsCommand { get; }
		private PaymentConditionWrapper _selectedPaymentConditionsItem;
		public PaymentConditionWrapper SelectedPaymentConditionsItem 
		{ 
			get { return _selectedPaymentConditionsItem; }
			set 
			{ 
				if (Equals(_selectedPaymentConditionsItem, value)) return;
				_selectedPaymentConditionsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromPaymentConditionsCommand).RaiseCanExecuteChanged();
			}
		}


        public PaymentConditionSetDetailsViewModel(IUnityContainer container) : base(container) 
		{
			AddInPaymentConditionsCommand = new DelegateCommand(AddInPaymentConditionsCommand_Execute);
			RemoveFromPaymentConditionsCommand = new DelegateCommand(RemoveFromPaymentConditionsCommand_Execute, RemoveFromPaymentConditionsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
			_getEntitiesForAddInPaymentConditionsCommand = async () => { return await WrapperDataService.GetRepository<PaymentCondition>().GetAllAsync(); };;

		}
			private async void AddInPaymentConditionsCommand_Execute()
			{
				SelectAndAddInListWrapper<PaymentCondition, PaymentConditionWrapper>(await _getEntitiesForAddInPaymentConditionsCommand(), Item.PaymentConditions);
			}

			private void RemoveFromPaymentConditionsCommand_Execute()
			{
				Item.PaymentConditions.Remove(SelectedPaymentConditionsItem);
			}

			private bool RemoveFromPaymentConditionsCommand_CanExecute()
			{
				return SelectedPaymentConditionsItem != null;
			}



    }


    public partial class ProductBlockDetailsViewModel : BaseDetailsViewModel<ProductBlockWrapper, ProductBlock, AfterSaveProductBlockEvent>
    {
		private Func<Task<List<Parameter>>> _getEntitiesForAddInParametersCommand;
		public ICommand AddInParametersCommand { get; }
		public ICommand RemoveFromParametersCommand { get; }
		private ParameterWrapper _selectedParametersItem;
		public ParameterWrapper SelectedParametersItem 
		{ 
			get { return _selectedParametersItem; }
			set 
			{ 
				if (Equals(_selectedParametersItem, value)) return;
				_selectedParametersItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromParametersCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<SumOnDate>>> _getEntitiesForAddInPricesCommand;
		public ICommand AddInPricesCommand { get; }
		public ICommand RemoveFromPricesCommand { get; }
		private SumOnDateWrapper _selectedPricesItem;
		public SumOnDateWrapper SelectedPricesItem 
		{ 
			get { return _selectedPricesItem; }
			set 
			{ 
				if (Equals(_selectedPricesItem, value)) return;
				_selectedPricesItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromPricesCommand).RaiseCanExecuteChanged();
			}
		}


        public ProductBlockDetailsViewModel(IUnityContainer container) : base(container) 
		{
			AddInParametersCommand = new DelegateCommand(AddInParametersCommand_Execute);
			RemoveFromParametersCommand = new DelegateCommand(RemoveFromParametersCommand_Execute, RemoveFromParametersCommand_CanExecute);

			AddInPricesCommand = new DelegateCommand(AddInPricesCommand_Execute);
			RemoveFromPricesCommand = new DelegateCommand(RemoveFromPricesCommand_Execute, RemoveFromPricesCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
			_getEntitiesForAddInParametersCommand = async () => { return await WrapperDataService.GetRepository<Parameter>().GetAllAsync(); };;
			_getEntitiesForAddInPricesCommand = async () => { return await WrapperDataService.GetRepository<SumOnDate>().GetAllAsync(); };;

		}
			private async void AddInParametersCommand_Execute()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(await _getEntitiesForAddInParametersCommand(), Item.Parameters);
			}

			private void RemoveFromParametersCommand_Execute()
			{
				Item.Parameters.Remove(SelectedParametersItem);
			}

			private bool RemoveFromParametersCommand_CanExecute()
			{
				return SelectedParametersItem != null;
			}

			private async void AddInPricesCommand_Execute()
			{
				SelectAndAddInListWrapper<SumOnDate, SumOnDateWrapper>(await _getEntitiesForAddInPricesCommand(), Item.Prices);
			}

			private void RemoveFromPricesCommand_Execute()
			{
				Item.Prices.Remove(SelectedPricesItem);
			}

			private bool RemoveFromPricesCommand_CanExecute()
			{
				return SelectedPricesItem != null;
			}



    }


    public partial class ProductDependentDetailsViewModel : BaseDetailsViewModel<ProductDependentWrapper, ProductDependent, AfterSaveProductDependentEvent>
    {
		private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; private set; }
		public ICommand ClearProductCommand { get; private set; }


        public ProductDependentDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectProductCommand = async () => { return await WrapperDataService.GetRepository<Product>().GetAllAsync(); };

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


    public partial class ProductionTaskDetailsViewModel : BaseDetailsViewModel<ProductionTaskWrapper, ProductionTask, AfterSaveProductionTaskEvent>
    {
		private Func<Task<List<SalesUnit>>> _getEntitiesForAddInSalesUnitsCommand;
		public ICommand AddInSalesUnitsCommand { get; }
		public ICommand RemoveFromSalesUnitsCommand { get; }
		private SalesUnitWrapper _selectedSalesUnitsItem;
		public SalesUnitWrapper SelectedSalesUnitsItem 
		{ 
			get { return _selectedSalesUnitsItem; }
			set 
			{ 
				if (Equals(_selectedSalesUnitsItem, value)) return;
				_selectedSalesUnitsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromSalesUnitsCommand).RaiseCanExecuteChanged();
			}
		}


        public ProductionTaskDetailsViewModel(IUnityContainer container) : base(container) 
		{
			AddInSalesUnitsCommand = new DelegateCommand(AddInSalesUnitsCommand_Execute);
			RemoveFromSalesUnitsCommand = new DelegateCommand(RemoveFromSalesUnitsCommand_Execute, RemoveFromSalesUnitsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
			_getEntitiesForAddInSalesUnitsCommand = async () => { return await WrapperDataService.GetRepository<SalesUnit>().GetAllAsync(); };;

		}
			private async void AddInSalesUnitsCommand_Execute()
			{
				SelectAndAddInListWrapper<SalesUnit, SalesUnitWrapper>(await _getEntitiesForAddInSalesUnitsCommand(), Item.SalesUnits);
			}

			private void RemoveFromSalesUnitsCommand_Execute()
			{
				Item.SalesUnits.Remove(SelectedSalesUnitsItem);
			}

			private bool RemoveFromSalesUnitsCommand_CanExecute()
			{
				return SelectedSalesUnitsItem != null;
			}



    }


    public partial class SalesBlockDetailsViewModel : BaseDetailsViewModel<SalesBlockWrapper, SalesBlock, AfterSaveSalesBlockEvent>
    {
		private Func<Task<List<SalesUnit>>> _getEntitiesForAddInParentSalesUnitsCommand;
		public ICommand AddInParentSalesUnitsCommand { get; }
		public ICommand RemoveFromParentSalesUnitsCommand { get; }
		private SalesUnitWrapper _selectedParentSalesUnitsItem;
		public SalesUnitWrapper SelectedParentSalesUnitsItem 
		{ 
			get { return _selectedParentSalesUnitsItem; }
			set 
			{ 
				if (Equals(_selectedParentSalesUnitsItem, value)) return;
				_selectedParentSalesUnitsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromParentSalesUnitsCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<SalesUnit>>> _getEntitiesForAddInChildSalesUnitsCommand;
		public ICommand AddInChildSalesUnitsCommand { get; }
		public ICommand RemoveFromChildSalesUnitsCommand { get; }
		private SalesUnitWrapper _selectedChildSalesUnitsItem;
		public SalesUnitWrapper SelectedChildSalesUnitsItem 
		{ 
			get { return _selectedChildSalesUnitsItem; }
			set 
			{ 
				if (Equals(_selectedChildSalesUnitsItem, value)) return;
				_selectedChildSalesUnitsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromChildSalesUnitsCommand).RaiseCanExecuteChanged();
			}
		}


        public SalesBlockDetailsViewModel(IUnityContainer container) : base(container) 
		{
			AddInParentSalesUnitsCommand = new DelegateCommand(AddInParentSalesUnitsCommand_Execute);
			RemoveFromParentSalesUnitsCommand = new DelegateCommand(RemoveFromParentSalesUnitsCommand_Execute, RemoveFromParentSalesUnitsCommand_CanExecute);

			AddInChildSalesUnitsCommand = new DelegateCommand(AddInChildSalesUnitsCommand_Execute);
			RemoveFromChildSalesUnitsCommand = new DelegateCommand(RemoveFromChildSalesUnitsCommand_Execute, RemoveFromChildSalesUnitsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
			_getEntitiesForAddInParentSalesUnitsCommand = async () => { return await WrapperDataService.GetRepository<SalesUnit>().GetAllAsync(); };;
			_getEntitiesForAddInChildSalesUnitsCommand = async () => { return await WrapperDataService.GetRepository<SalesUnit>().GetAllAsync(); };;

		}
			private async void AddInParentSalesUnitsCommand_Execute()
			{
				SelectAndAddInListWrapper<SalesUnit, SalesUnitWrapper>(await _getEntitiesForAddInParentSalesUnitsCommand(), Item.ParentSalesUnits);
			}

			private void RemoveFromParentSalesUnitsCommand_Execute()
			{
				Item.ParentSalesUnits.Remove(SelectedParentSalesUnitsItem);
			}

			private bool RemoveFromParentSalesUnitsCommand_CanExecute()
			{
				return SelectedParentSalesUnitsItem != null;
			}

			private async void AddInChildSalesUnitsCommand_Execute()
			{
				SelectAndAddInListWrapper<SalesUnit, SalesUnitWrapper>(await _getEntitiesForAddInChildSalesUnitsCommand(), Item.ChildSalesUnits);
			}

			private void RemoveFromChildSalesUnitsCommand_Execute()
			{
				Item.ChildSalesUnits.Remove(SelectedChildSalesUnitsItem);
			}

			private bool RemoveFromChildSalesUnitsCommand_CanExecute()
			{
				return SelectedChildSalesUnitsItem != null;
			}



    }


    public partial class BankDetailsDetailsViewModel : BaseDetailsViewModel<BankDetailsWrapper, BankDetails, AfterSaveBankDetailsEvent>
    {

        public BankDetailsDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class CompanyDetailsViewModel : BaseDetailsViewModel<CompanyWrapper, Company, AfterSaveCompanyEvent>
    {
		private Func<Task<List<CompanyForm>>> _getEntitiesForSelectFormCommand;
		public ICommand SelectFormCommand { get; private set; }
		public ICommand ClearFormCommand { get; private set; }

		private Func<Task<List<Company>>> _getEntitiesForSelectParentCompanyCommand;
		public ICommand SelectParentCompanyCommand { get; private set; }
		public ICommand ClearParentCompanyCommand { get; private set; }

		private Func<Task<List<Address>>> _getEntitiesForSelectAddressLegalCommand;
		public ICommand SelectAddressLegalCommand { get; private set; }
		public ICommand ClearAddressLegalCommand { get; private set; }

		private Func<Task<List<Address>>> _getEntitiesForSelectAddressPostCommand;
		public ICommand SelectAddressPostCommand { get; private set; }
		public ICommand ClearAddressPostCommand { get; private set; }

		private Func<Task<List<BankDetails>>> _getEntitiesForAddInBankDetailsListCommand;
		public ICommand AddInBankDetailsListCommand { get; }
		public ICommand RemoveFromBankDetailsListCommand { get; }
		private BankDetailsWrapper _selectedBankDetailsListItem;
		public BankDetailsWrapper SelectedBankDetailsListItem 
		{ 
			get { return _selectedBankDetailsListItem; }
			set 
			{ 
				if (Equals(_selectedBankDetailsListItem, value)) return;
				_selectedBankDetailsListItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromBankDetailsListCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<ActivityField>>> _getEntitiesForAddInActivityFildsCommand;
		public ICommand AddInActivityFildsCommand { get; }
		public ICommand RemoveFromActivityFildsCommand { get; }
		private ActivityFieldWrapper _selectedActivityFildsItem;
		public ActivityFieldWrapper SelectedActivityFildsItem 
		{ 
			get { return _selectedActivityFildsItem; }
			set 
			{ 
				if (Equals(_selectedActivityFildsItem, value)) return;
				_selectedActivityFildsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromActivityFildsCommand).RaiseCanExecuteChanged();
			}
		}


        public CompanyDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectFormCommand == null) SelectFormCommand = new DelegateCommand(SelectFormCommand_Execute);
			if (ClearFormCommand == null) ClearFormCommand = new DelegateCommand(ClearFormCommand_Execute);

			if (SelectParentCompanyCommand == null) SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_Execute);
			if (ClearParentCompanyCommand == null) ClearParentCompanyCommand = new DelegateCommand(ClearParentCompanyCommand_Execute);

			if (SelectAddressLegalCommand == null) SelectAddressLegalCommand = new DelegateCommand(SelectAddressLegalCommand_Execute);
			if (ClearAddressLegalCommand == null) ClearAddressLegalCommand = new DelegateCommand(ClearAddressLegalCommand_Execute);

			if (SelectAddressPostCommand == null) SelectAddressPostCommand = new DelegateCommand(SelectAddressPostCommand_Execute);
			if (ClearAddressPostCommand == null) ClearAddressPostCommand = new DelegateCommand(ClearAddressPostCommand_Execute);

			AddInBankDetailsListCommand = new DelegateCommand(AddInBankDetailsListCommand_Execute);
			RemoveFromBankDetailsListCommand = new DelegateCommand(RemoveFromBankDetailsListCommand_Execute, RemoveFromBankDetailsListCommand_CanExecute);

			AddInActivityFildsCommand = new DelegateCommand(AddInActivityFildsCommand_Execute);
			RemoveFromActivityFildsCommand = new DelegateCommand(RemoveFromActivityFildsCommand_Execute, RemoveFromActivityFildsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectFormCommand = async () => { return await WrapperDataService.GetRepository<CompanyForm>().GetAllAsync(); };
            _getEntitiesForSelectParentCompanyCommand = async () => { return await WrapperDataService.GetRepository<Company>().GetAllAsync(); };
            _getEntitiesForSelectAddressLegalCommand = async () => { return await WrapperDataService.GetRepository<Address>().GetAllAsync(); };
            _getEntitiesForSelectAddressPostCommand = async () => { return await WrapperDataService.GetRepository<Address>().GetAllAsync(); };
			_getEntitiesForAddInBankDetailsListCommand = async () => { return await WrapperDataService.GetRepository<BankDetails>().GetAllAsync(); };;
			_getEntitiesForAddInActivityFildsCommand = async () => { return await WrapperDataService.GetRepository<ActivityField>().GetAllAsync(); };;

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

			private async void AddInBankDetailsListCommand_Execute()
			{
				SelectAndAddInListWrapper<BankDetails, BankDetailsWrapper>(await _getEntitiesForAddInBankDetailsListCommand(), Item.BankDetailsList);
			}

			private void RemoveFromBankDetailsListCommand_Execute()
			{
				Item.BankDetailsList.Remove(SelectedBankDetailsListItem);
			}

			private bool RemoveFromBankDetailsListCommand_CanExecute()
			{
				return SelectedBankDetailsListItem != null;
			}

			private async void AddInActivityFildsCommand_Execute()
			{
				SelectAndAddInListWrapper<ActivityField, ActivityFieldWrapper>(await _getEntitiesForAddInActivityFildsCommand(), Item.ActivityFilds);
			}

			private void RemoveFromActivityFildsCommand_Execute()
			{
				Item.ActivityFilds.Remove(SelectedActivityFildsItem);
			}

			private bool RemoveFromActivityFildsCommand_CanExecute()
			{
				return SelectedActivityFildsItem != null;
			}



    }


    public partial class CompanyFormDetailsViewModel : BaseDetailsViewModel<CompanyFormWrapper, CompanyForm, AfterSaveCompanyFormEvent>
    {

        public CompanyFormDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class DocumentsRegistrationDetailsDetailsViewModel : BaseDetailsViewModel<DocumentsRegistrationDetailsWrapper, DocumentsRegistrationDetails, AfterSaveDocumentsRegistrationDetailsEvent>
    {

        public DocumentsRegistrationDetailsDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class EmployeesPositionDetailsViewModel : BaseDetailsViewModel<EmployeesPositionWrapper, EmployeesPosition, AfterSaveEmployeesPositionEvent>
    {

        public EmployeesPositionDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class FacilityTypeDetailsViewModel : BaseDetailsViewModel<FacilityTypeWrapper, FacilityType, AfterSaveFacilityTypeEvent>
    {

        public FacilityTypeDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class ActivityFieldDetailsViewModel : BaseDetailsViewModel<ActivityFieldWrapper, ActivityField, AfterSaveActivityFieldEvent>
    {

        public ActivityFieldDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class ContractDetailsViewModel : BaseDetailsViewModel<ContractWrapper, Contract, AfterSaveContractEvent>
    {
		private Func<Task<List<Company>>> _getEntitiesForSelectContragentCommand;
		public ICommand SelectContragentCommand { get; private set; }
		public ICommand ClearContragentCommand { get; private set; }


        public ContractDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectContragentCommand == null) SelectContragentCommand = new DelegateCommand(SelectContragentCommand_Execute);
			if (ClearContragentCommand == null) ClearContragentCommand = new DelegateCommand(ClearContragentCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectContragentCommand = async () => { return await WrapperDataService.GetRepository<Company>().GetAllAsync(); };

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
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class ParameterDetailsViewModel : BaseDetailsViewModel<ParameterWrapper, Parameter, AfterSaveParameterEvent>
    {
		private Func<Task<List<ParameterGroup>>> _getEntitiesForSelectParameterGroupCommand;
		public ICommand SelectParameterGroupCommand { get; private set; }
		public ICommand ClearParameterGroupCommand { get; private set; }

		private Func<Task<List<ParameterRelation>>> _getEntitiesForAddInParameterRelationsCommand;
		public ICommand AddInParameterRelationsCommand { get; }
		public ICommand RemoveFromParameterRelationsCommand { get; }
		private ParameterRelationWrapper _selectedParameterRelationsItem;
		public ParameterRelationWrapper SelectedParameterRelationsItem 
		{ 
			get { return _selectedParameterRelationsItem; }
			set 
			{ 
				if (Equals(_selectedParameterRelationsItem, value)) return;
				_selectedParameterRelationsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromParameterRelationsCommand).RaiseCanExecuteChanged();
			}
		}


        public ParameterDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectParameterGroupCommand == null) SelectParameterGroupCommand = new DelegateCommand(SelectParameterGroupCommand_Execute);
			if (ClearParameterGroupCommand == null) ClearParameterGroupCommand = new DelegateCommand(ClearParameterGroupCommand_Execute);

			AddInParameterRelationsCommand = new DelegateCommand(AddInParameterRelationsCommand_Execute);
			RemoveFromParameterRelationsCommand = new DelegateCommand(RemoveFromParameterRelationsCommand_Execute, RemoveFromParameterRelationsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectParameterGroupCommand = async () => { return await WrapperDataService.GetRepository<ParameterGroup>().GetAllAsync(); };
			_getEntitiesForAddInParameterRelationsCommand = async () => { return await WrapperDataService.GetRepository<ParameterRelation>().GetAllAsync(); };;

		}
		private async void SelectParameterGroupCommand_Execute() 
		{
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(await _getEntitiesForSelectParameterGroupCommand(), nameof(Item.ParameterGroup), Item.ParameterGroup?.Id);
		}

		private void ClearParameterGroupCommand_Execute() 
		{
		Item.ParameterGroup = null;
		    
		}

			private async void AddInParameterRelationsCommand_Execute()
			{
				SelectAndAddInListWrapper<ParameterRelation, ParameterRelationWrapper>(await _getEntitiesForAddInParameterRelationsCommand(), Item.ParameterRelations);
			}

			private void RemoveFromParameterRelationsCommand_Execute()
			{
				Item.ParameterRelations.Remove(SelectedParameterRelationsItem);
			}

			private bool RemoveFromParameterRelationsCommand_CanExecute()
			{
				return SelectedParameterRelationsItem != null;
			}



    }


    public partial class ParameterGroupDetailsViewModel : BaseDetailsViewModel<ParameterGroupWrapper, ParameterGroup, AfterSaveParameterGroupEvent>
    {
		private Func<Task<List<Measure>>> _getEntitiesForSelectMeasureCommand;
		public ICommand SelectMeasureCommand { get; private set; }
		public ICommand ClearMeasureCommand { get; private set; }


        public ParameterGroupDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectMeasureCommand == null) SelectMeasureCommand = new DelegateCommand(SelectMeasureCommand_Execute);
			if (ClearMeasureCommand == null) ClearMeasureCommand = new DelegateCommand(ClearMeasureCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectMeasureCommand = async () => { return await WrapperDataService.GetRepository<Measure>().GetAllAsync(); };

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
		private Func<Task<List<Parameter>>> _getEntitiesForAddInParentProductParametersCommand;
		public ICommand AddInParentProductParametersCommand { get; }
		public ICommand RemoveFromParentProductParametersCommand { get; }
		private ParameterWrapper _selectedParentProductParametersItem;
		public ParameterWrapper SelectedParentProductParametersItem 
		{ 
			get { return _selectedParentProductParametersItem; }
			set 
			{ 
				if (Equals(_selectedParentProductParametersItem, value)) return;
				_selectedParentProductParametersItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromParentProductParametersCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<Parameter>>> _getEntitiesForAddInChildProductParametersCommand;
		public ICommand AddInChildProductParametersCommand { get; }
		public ICommand RemoveFromChildProductParametersCommand { get; }
		private ParameterWrapper _selectedChildProductParametersItem;
		public ParameterWrapper SelectedChildProductParametersItem 
		{ 
			get { return _selectedChildProductParametersItem; }
			set 
			{ 
				if (Equals(_selectedChildProductParametersItem, value)) return;
				_selectedChildProductParametersItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromChildProductParametersCommand).RaiseCanExecuteChanged();
			}
		}


        public ProductRelationDetailsViewModel(IUnityContainer container) : base(container) 
		{
			AddInParentProductParametersCommand = new DelegateCommand(AddInParentProductParametersCommand_Execute);
			RemoveFromParentProductParametersCommand = new DelegateCommand(RemoveFromParentProductParametersCommand_Execute, RemoveFromParentProductParametersCommand_CanExecute);

			AddInChildProductParametersCommand = new DelegateCommand(AddInChildProductParametersCommand_Execute);
			RemoveFromChildProductParametersCommand = new DelegateCommand(RemoveFromChildProductParametersCommand_Execute, RemoveFromChildProductParametersCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
			_getEntitiesForAddInParentProductParametersCommand = async () => { return await WrapperDataService.GetRepository<Parameter>().GetAllAsync(); };;
			_getEntitiesForAddInChildProductParametersCommand = async () => { return await WrapperDataService.GetRepository<Parameter>().GetAllAsync(); };;

		}
			private async void AddInParentProductParametersCommand_Execute()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(await _getEntitiesForAddInParentProductParametersCommand(), Item.ParentProductParameters);
			}

			private void RemoveFromParentProductParametersCommand_Execute()
			{
				Item.ParentProductParameters.Remove(SelectedParentProductParametersItem);
			}

			private bool RemoveFromParentProductParametersCommand_CanExecute()
			{
				return SelectedParentProductParametersItem != null;
			}

			private async void AddInChildProductParametersCommand_Execute()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(await _getEntitiesForAddInChildProductParametersCommand(), Item.ChildProductParameters);
			}

			private void RemoveFromChildProductParametersCommand_Execute()
			{
				Item.ChildProductParameters.Remove(SelectedChildProductParametersItem);
			}

			private bool RemoveFromChildProductParametersCommand_CanExecute()
			{
				return SelectedChildProductParametersItem != null;
			}



    }


    public partial class PersonDetailsViewModel : BaseDetailsViewModel<PersonWrapper, Person, AfterSavePersonEvent>
    {

        public PersonDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class PaymentPlannedListDetailsViewModel : BaseDetailsViewModel<PaymentPlannedListWrapper, PaymentPlannedList, AfterSavePaymentPlannedListEvent>
    {
		private Func<Task<List<PaymentCondition>>> _getEntitiesForSelectConditionCommand;
		public ICommand SelectConditionCommand { get; private set; }
		public ICommand ClearConditionCommand { get; private set; }

		private Func<Task<List<PaymentPlanned>>> _getEntitiesForAddInPaymentsCommand;
		public ICommand AddInPaymentsCommand { get; }
		public ICommand RemoveFromPaymentsCommand { get; }
		private PaymentPlannedWrapper _selectedPaymentsItem;
		public PaymentPlannedWrapper SelectedPaymentsItem 
		{ 
			get { return _selectedPaymentsItem; }
			set 
			{ 
				if (Equals(_selectedPaymentsItem, value)) return;
				_selectedPaymentsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromPaymentsCommand).RaiseCanExecuteChanged();
			}
		}


        public PaymentPlannedListDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectConditionCommand == null) SelectConditionCommand = new DelegateCommand(SelectConditionCommand_Execute);
			if (ClearConditionCommand == null) ClearConditionCommand = new DelegateCommand(ClearConditionCommand_Execute);

			AddInPaymentsCommand = new DelegateCommand(AddInPaymentsCommand_Execute);
			RemoveFromPaymentsCommand = new DelegateCommand(RemoveFromPaymentsCommand_Execute, RemoveFromPaymentsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectConditionCommand = async () => { return await WrapperDataService.GetRepository<PaymentCondition>().GetAllAsync(); };
			_getEntitiesForAddInPaymentsCommand = async () => { return await WrapperDataService.GetRepository<PaymentPlanned>().GetAllAsync(); };;

		}
		private async void SelectConditionCommand_Execute() 
		{
            SelectAndSetWrapper<PaymentCondition, PaymentConditionWrapper>(await _getEntitiesForSelectConditionCommand(), nameof(Item.Condition), Item.Condition?.Id);
		}

		private void ClearConditionCommand_Execute() 
		{
		Item.Condition = null;
		    
		}

			private async void AddInPaymentsCommand_Execute()
			{
				SelectAndAddInListWrapper<PaymentPlanned, PaymentPlannedWrapper>(await _getEntitiesForAddInPaymentsCommand(), Item.Payments);
			}

			private void RemoveFromPaymentsCommand_Execute()
			{
				Item.Payments.Remove(SelectedPaymentsItem);
			}

			private bool RemoveFromPaymentsCommand_CanExecute()
			{
				return SelectedPaymentsItem != null;
			}



    }


    public partial class PaymentPlannedDetailsViewModel : BaseDetailsViewModel<PaymentPlannedWrapper, PaymentPlanned, AfterSavePaymentPlannedEvent>
    {

        public PaymentPlannedDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class PaymentActualDetailsViewModel : BaseDetailsViewModel<PaymentActualWrapper, PaymentActual, AfterSavePaymentActualEvent>
    {

        public PaymentActualDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class ParameterRelationDetailsViewModel : BaseDetailsViewModel<ParameterRelationWrapper, ParameterRelation, AfterSaveParameterRelationEvent>
    {
		private Func<Task<List<Parameter>>> _getEntitiesForAddInRequiredParametersCommand;
		public ICommand AddInRequiredParametersCommand { get; }
		public ICommand RemoveFromRequiredParametersCommand { get; }
		private ParameterWrapper _selectedRequiredParametersItem;
		public ParameterWrapper SelectedRequiredParametersItem 
		{ 
			get { return _selectedRequiredParametersItem; }
			set 
			{ 
				if (Equals(_selectedRequiredParametersItem, value)) return;
				_selectedRequiredParametersItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromRequiredParametersCommand).RaiseCanExecuteChanged();
			}
		}


        public ParameterRelationDetailsViewModel(IUnityContainer container) : base(container) 
		{
			AddInRequiredParametersCommand = new DelegateCommand(AddInRequiredParametersCommand_Execute);
			RemoveFromRequiredParametersCommand = new DelegateCommand(RemoveFromRequiredParametersCommand_Execute, RemoveFromRequiredParametersCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
			_getEntitiesForAddInRequiredParametersCommand = async () => { return await WrapperDataService.GetRepository<Parameter>().GetAllAsync(); };;

		}
			private async void AddInRequiredParametersCommand_Execute()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(await _getEntitiesForAddInRequiredParametersCommand(), Item.RequiredParameters);
			}

			private void RemoveFromRequiredParametersCommand_Execute()
			{
				Item.RequiredParameters.Remove(SelectedRequiredParametersItem);
			}

			private bool RemoveFromRequiredParametersCommand_CanExecute()
			{
				return SelectedRequiredParametersItem != null;
			}



    }


    public partial class SalesUnitDetailsViewModel : BaseDetailsViewModel<SalesUnitWrapper, SalesUnit, AfterSaveSalesUnitEvent>
    {
		private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; private set; }
		public ICommand ClearProductCommand { get; private set; }

		private Func<Task<List<Facility>>> _getEntitiesForSelectFacilityCommand;
		public ICommand SelectFacilityCommand { get; private set; }
		public ICommand ClearFacilityCommand { get; private set; }

		private Func<Task<List<PaymentConditionSet>>> _getEntitiesForSelectPaymentConditionSetCommand;
		public ICommand SelectPaymentConditionSetCommand { get; private set; }
		public ICommand ClearPaymentConditionSetCommand { get; private set; }

		private Func<Task<List<Project>>> _getEntitiesForSelectProjectCommand;
		public ICommand SelectProjectCommand { get; private set; }
		public ICommand ClearProjectCommand { get; private set; }

		private Func<Task<List<Company>>> _getEntitiesForSelectProducerCommand;
		public ICommand SelectProducerCommand { get; private set; }
		public ICommand ClearProducerCommand { get; private set; }

		private Func<Task<List<Order>>> _getEntitiesForSelectOrderCommand;
		public ICommand SelectOrderCommand { get; private set; }
		public ICommand ClearOrderCommand { get; private set; }

		private Func<Task<List<Specification>>> _getEntitiesForSelectSpecificationCommand;
		public ICommand SelectSpecificationCommand { get; private set; }
		public ICommand ClearSpecificationCommand { get; private set; }

		private Func<Task<List<Address>>> _getEntitiesForSelectAddressCommand;
		public ICommand SelectAddressCommand { get; private set; }
		public ICommand ClearAddressCommand { get; private set; }

		private Func<Task<List<ProductDependent>>> _getEntitiesForAddInDependentProductsCommand;
		public ICommand AddInDependentProductsCommand { get; }
		public ICommand RemoveFromDependentProductsCommand { get; }
		private ProductDependentWrapper _selectedDependentProductsItem;
		public ProductDependentWrapper SelectedDependentProductsItem 
		{ 
			get { return _selectedDependentProductsItem; }
			set 
			{ 
				if (Equals(_selectedDependentProductsItem, value)) return;
				_selectedDependentProductsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromDependentProductsCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<Service>>> _getEntitiesForAddInServicesCommand;
		public ICommand AddInServicesCommand { get; }
		public ICommand RemoveFromServicesCommand { get; }
		private ServiceWrapper _selectedServicesItem;
		public ServiceWrapper SelectedServicesItem 
		{ 
			get { return _selectedServicesItem; }
			set 
			{ 
				if (Equals(_selectedServicesItem, value)) return;
				_selectedServicesItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromServicesCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<PaymentActual>>> _getEntitiesForAddInPaymentsActualCommand;
		public ICommand AddInPaymentsActualCommand { get; }
		public ICommand RemoveFromPaymentsActualCommand { get; }
		private PaymentActualWrapper _selectedPaymentsActualItem;
		public PaymentActualWrapper SelectedPaymentsActualItem 
		{ 
			get { return _selectedPaymentsActualItem; }
			set 
			{ 
				if (Equals(_selectedPaymentsActualItem, value)) return;
				_selectedPaymentsActualItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromPaymentsActualCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<PaymentPlannedList>>> _getEntitiesForAddInPaymentsPlannedSavedCommand;
		public ICommand AddInPaymentsPlannedSavedCommand { get; }
		public ICommand RemoveFromPaymentsPlannedSavedCommand { get; }
		private PaymentPlannedListWrapper _selectedPaymentsPlannedSavedItem;
		public PaymentPlannedListWrapper SelectedPaymentsPlannedSavedItem 
		{ 
			get { return _selectedPaymentsPlannedSavedItem; }
			set 
			{ 
				if (Equals(_selectedPaymentsPlannedSavedItem, value)) return;
				_selectedPaymentsPlannedSavedItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromPaymentsPlannedSavedCommand).RaiseCanExecuteChanged();
			}
		}


        public SalesUnitDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute);

			if (SelectFacilityCommand == null) SelectFacilityCommand = new DelegateCommand(SelectFacilityCommand_Execute);
			if (ClearFacilityCommand == null) ClearFacilityCommand = new DelegateCommand(ClearFacilityCommand_Execute);

			if (SelectPaymentConditionSetCommand == null) SelectPaymentConditionSetCommand = new DelegateCommand(SelectPaymentConditionSetCommand_Execute);
			if (ClearPaymentConditionSetCommand == null) ClearPaymentConditionSetCommand = new DelegateCommand(ClearPaymentConditionSetCommand_Execute);

			if (SelectProjectCommand == null) SelectProjectCommand = new DelegateCommand(SelectProjectCommand_Execute);
			if (ClearProjectCommand == null) ClearProjectCommand = new DelegateCommand(ClearProjectCommand_Execute);

			if (SelectProducerCommand == null) SelectProducerCommand = new DelegateCommand(SelectProducerCommand_Execute);
			if (ClearProducerCommand == null) ClearProducerCommand = new DelegateCommand(ClearProducerCommand_Execute);

			if (SelectOrderCommand == null) SelectOrderCommand = new DelegateCommand(SelectOrderCommand_Execute);
			if (ClearOrderCommand == null) ClearOrderCommand = new DelegateCommand(ClearOrderCommand_Execute);

			if (SelectSpecificationCommand == null) SelectSpecificationCommand = new DelegateCommand(SelectSpecificationCommand_Execute);
			if (ClearSpecificationCommand == null) ClearSpecificationCommand = new DelegateCommand(ClearSpecificationCommand_Execute);

			if (SelectAddressCommand == null) SelectAddressCommand = new DelegateCommand(SelectAddressCommand_Execute);
			if (ClearAddressCommand == null) ClearAddressCommand = new DelegateCommand(ClearAddressCommand_Execute);

			AddInDependentProductsCommand = new DelegateCommand(AddInDependentProductsCommand_Execute);
			RemoveFromDependentProductsCommand = new DelegateCommand(RemoveFromDependentProductsCommand_Execute, RemoveFromDependentProductsCommand_CanExecute);

			AddInServicesCommand = new DelegateCommand(AddInServicesCommand_Execute);
			RemoveFromServicesCommand = new DelegateCommand(RemoveFromServicesCommand_Execute, RemoveFromServicesCommand_CanExecute);

			AddInPaymentsActualCommand = new DelegateCommand(AddInPaymentsActualCommand_Execute);
			RemoveFromPaymentsActualCommand = new DelegateCommand(RemoveFromPaymentsActualCommand_Execute, RemoveFromPaymentsActualCommand_CanExecute);

			AddInPaymentsPlannedSavedCommand = new DelegateCommand(AddInPaymentsPlannedSavedCommand_Execute);
			RemoveFromPaymentsPlannedSavedCommand = new DelegateCommand(RemoveFromPaymentsPlannedSavedCommand_Execute, RemoveFromPaymentsPlannedSavedCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectProductCommand = async () => { return await WrapperDataService.GetRepository<Product>().GetAllAsync(); };
            _getEntitiesForSelectFacilityCommand = async () => { return await WrapperDataService.GetRepository<Facility>().GetAllAsync(); };
            _getEntitiesForSelectPaymentConditionSetCommand = async () => { return await WrapperDataService.GetRepository<PaymentConditionSet>().GetAllAsync(); };
            _getEntitiesForSelectProjectCommand = async () => { return await WrapperDataService.GetRepository<Project>().GetAllAsync(); };
            _getEntitiesForSelectProducerCommand = async () => { return await WrapperDataService.GetRepository<Company>().GetAllAsync(); };
            _getEntitiesForSelectOrderCommand = async () => { return await WrapperDataService.GetRepository<Order>().GetAllAsync(); };
            _getEntitiesForSelectSpecificationCommand = async () => { return await WrapperDataService.GetRepository<Specification>().GetAllAsync(); };
            _getEntitiesForSelectAddressCommand = async () => { return await WrapperDataService.GetRepository<Address>().GetAllAsync(); };
			_getEntitiesForAddInDependentProductsCommand = async () => { return await WrapperDataService.GetRepository<ProductDependent>().GetAllAsync(); };;
			_getEntitiesForAddInServicesCommand = async () => { return await WrapperDataService.GetRepository<Service>().GetAllAsync(); };;
			_getEntitiesForAddInPaymentsActualCommand = async () => { return await WrapperDataService.GetRepository<PaymentActual>().GetAllAsync(); };;
			_getEntitiesForAddInPaymentsPlannedSavedCommand = async () => { return await WrapperDataService.GetRepository<PaymentPlannedList>().GetAllAsync(); };;

		}
		private async void SelectProductCommand_Execute() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(await _getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute() 
		{
		Item.Product = null;
		    
		}

		private async void SelectFacilityCommand_Execute() 
		{
            SelectAndSetWrapper<Facility, FacilityWrapper>(await _getEntitiesForSelectFacilityCommand(), nameof(Item.Facility), Item.Facility?.Id);
		}

		private void ClearFacilityCommand_Execute() 
		{
		Item.Facility = null;
		    
		}

		private async void SelectPaymentConditionSetCommand_Execute() 
		{
            SelectAndSetWrapper<PaymentConditionSet, PaymentConditionSetWrapper>(await _getEntitiesForSelectPaymentConditionSetCommand(), nameof(Item.PaymentConditionSet), Item.PaymentConditionSet?.Id);
		}

		private void ClearPaymentConditionSetCommand_Execute() 
		{
		Item.PaymentConditionSet = null;
		    
		}

		private async void SelectProjectCommand_Execute() 
		{
            SelectAndSetWrapper<Project, ProjectWrapper>(await _getEntitiesForSelectProjectCommand(), nameof(Item.Project), Item.Project?.Id);
		}

		private void ClearProjectCommand_Execute() 
		{
		Item.Project = null;
		    
		}

		private async void SelectProducerCommand_Execute() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectProducerCommand(), nameof(Item.Producer), Item.Producer?.Id);
		}

		private void ClearProducerCommand_Execute() 
		{
		Item.Producer = null;
		    
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

		private async void SelectAddressCommand_Execute() 
		{
            SelectAndSetWrapper<Address, AddressWrapper>(await _getEntitiesForSelectAddressCommand(), nameof(Item.Address), Item.Address?.Id);
		}

		private void ClearAddressCommand_Execute() 
		{
		Item.Address = null;
		    
		}

			private async void AddInDependentProductsCommand_Execute()
			{
				SelectAndAddInListWrapper<ProductDependent, ProductDependentWrapper>(await _getEntitiesForAddInDependentProductsCommand(), Item.DependentProducts);
			}

			private void RemoveFromDependentProductsCommand_Execute()
			{
				Item.DependentProducts.Remove(SelectedDependentProductsItem);
			}

			private bool RemoveFromDependentProductsCommand_CanExecute()
			{
				return SelectedDependentProductsItem != null;
			}

			private async void AddInServicesCommand_Execute()
			{
				SelectAndAddInListWrapper<Service, ServiceWrapper>(await _getEntitiesForAddInServicesCommand(), Item.Services);
			}

			private void RemoveFromServicesCommand_Execute()
			{
				Item.Services.Remove(SelectedServicesItem);
			}

			private bool RemoveFromServicesCommand_CanExecute()
			{
				return SelectedServicesItem != null;
			}

			private async void AddInPaymentsActualCommand_Execute()
			{
				SelectAndAddInListWrapper<PaymentActual, PaymentActualWrapper>(await _getEntitiesForAddInPaymentsActualCommand(), Item.PaymentsActual);
			}

			private void RemoveFromPaymentsActualCommand_Execute()
			{
				Item.PaymentsActual.Remove(SelectedPaymentsActualItem);
			}

			private bool RemoveFromPaymentsActualCommand_CanExecute()
			{
				return SelectedPaymentsActualItem != null;
			}

			private async void AddInPaymentsPlannedSavedCommand_Execute()
			{
				SelectAndAddInListWrapper<PaymentPlannedList, PaymentPlannedListWrapper>(await _getEntitiesForAddInPaymentsPlannedSavedCommand(), Item.PaymentsPlannedSaved);
			}

			private void RemoveFromPaymentsPlannedSavedCommand_Execute()
			{
				Item.PaymentsPlannedSaved.Remove(SelectedPaymentsPlannedSavedItem);
			}

			private bool RemoveFromPaymentsPlannedSavedCommand_CanExecute()
			{
				return SelectedPaymentsPlannedSavedItem != null;
			}



    }


    public partial class ServiceDetailsViewModel : BaseDetailsViewModel<ServiceWrapper, Service, AfterSaveServiceEvent>
    {

        public ServiceDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class TestFriendAddressDetailsViewModel : BaseDetailsViewModel<TestFriendAddressWrapper, TestFriendAddress, AfterSaveTestFriendAddressEvent>
    {

        public TestFriendAddressDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class TestFriendDetailsViewModel : BaseDetailsViewModel<TestFriendWrapper, TestFriend, AfterSaveTestFriendEvent>
    {
		private Func<Task<List<TestFriendAddress>>> _getEntitiesForSelectTestFriendAddressCommand;
		public ICommand SelectTestFriendAddressCommand { get; private set; }
		public ICommand ClearTestFriendAddressCommand { get; private set; }

		private Func<Task<List<TestFriendGroup>>> _getEntitiesForSelectTestFriendGroupCommand;
		public ICommand SelectTestFriendGroupCommand { get; private set; }
		public ICommand ClearTestFriendGroupCommand { get; private set; }

		private Func<Task<List<TestFriendEmail>>> _getEntitiesForSelectTestFriendEmailGetCommand;
		public ICommand SelectTestFriendEmailGetCommand { get; private set; }
		public ICommand ClearTestFriendEmailGetCommand { get; private set; }

		private Func<Task<List<TestFriendEmail>>> _getEntitiesForAddInEmailsCommand;
		public ICommand AddInEmailsCommand { get; }
		public ICommand RemoveFromEmailsCommand { get; }
		private TestFriendEmailWrapper _selectedEmailsItem;
		public TestFriendEmailWrapper SelectedEmailsItem 
		{ 
			get { return _selectedEmailsItem; }
			set 
			{ 
				if (Equals(_selectedEmailsItem, value)) return;
				_selectedEmailsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromEmailsCommand).RaiseCanExecuteChanged();
			}
		}


        public TestFriendDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectTestFriendAddressCommand == null) SelectTestFriendAddressCommand = new DelegateCommand(SelectTestFriendAddressCommand_Execute);
			if (ClearTestFriendAddressCommand == null) ClearTestFriendAddressCommand = new DelegateCommand(ClearTestFriendAddressCommand_Execute);

			if (SelectTestFriendGroupCommand == null) SelectTestFriendGroupCommand = new DelegateCommand(SelectTestFriendGroupCommand_Execute);
			if (ClearTestFriendGroupCommand == null) ClearTestFriendGroupCommand = new DelegateCommand(ClearTestFriendGroupCommand_Execute);

			if (SelectTestFriendEmailGetCommand == null) SelectTestFriendEmailGetCommand = new DelegateCommand(SelectTestFriendEmailGetCommand_Execute);
			if (ClearTestFriendEmailGetCommand == null) ClearTestFriendEmailGetCommand = new DelegateCommand(ClearTestFriendEmailGetCommand_Execute);

			AddInEmailsCommand = new DelegateCommand(AddInEmailsCommand_Execute);
			RemoveFromEmailsCommand = new DelegateCommand(RemoveFromEmailsCommand_Execute, RemoveFromEmailsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectTestFriendAddressCommand = async () => { return await WrapperDataService.GetRepository<TestFriendAddress>().GetAllAsync(); };
            _getEntitiesForSelectTestFriendGroupCommand = async () => { return await WrapperDataService.GetRepository<TestFriendGroup>().GetAllAsync(); };
            _getEntitiesForSelectTestFriendEmailGetCommand = async () => { return await WrapperDataService.GetRepository<TestFriendEmail>().GetAllAsync(); };
			_getEntitiesForAddInEmailsCommand = async () => { return await WrapperDataService.GetRepository<TestFriendEmail>().GetAllAsync(); };;

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
		
		    
		}

			private async void AddInEmailsCommand_Execute()
			{
				SelectAndAddInListWrapper<TestFriendEmail, TestFriendEmailWrapper>(await _getEntitiesForAddInEmailsCommand(), Item.Emails);
			}

			private void RemoveFromEmailsCommand_Execute()
			{
				Item.Emails.Remove(SelectedEmailsItem);
			}

			private bool RemoveFromEmailsCommand_CanExecute()
			{
				return SelectedEmailsItem != null;
			}



    }


    public partial class TestFriendEmailDetailsViewModel : BaseDetailsViewModel<TestFriendEmailWrapper, TestFriendEmail, AfterSaveTestFriendEmailEvent>
    {

        public TestFriendEmailDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class TestFriendGroupDetailsViewModel : BaseDetailsViewModel<TestFriendGroupWrapper, TestFriendGroup, AfterSaveTestFriendGroupEvent>
    {
		private Func<Task<List<TestFriend>>> _getEntitiesForAddInFriendTestsCommand;
		public ICommand AddInFriendTestsCommand { get; }
		public ICommand RemoveFromFriendTestsCommand { get; }
		private TestFriendWrapper _selectedFriendTestsItem;
		public TestFriendWrapper SelectedFriendTestsItem 
		{ 
			get { return _selectedFriendTestsItem; }
			set 
			{ 
				if (Equals(_selectedFriendTestsItem, value)) return;
				_selectedFriendTestsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromFriendTestsCommand).RaiseCanExecuteChanged();
			}
		}


        public TestFriendGroupDetailsViewModel(IUnityContainer container) : base(container) 
		{
			AddInFriendTestsCommand = new DelegateCommand(AddInFriendTestsCommand_Execute);
			RemoveFromFriendTestsCommand = new DelegateCommand(RemoveFromFriendTestsCommand_Execute, RemoveFromFriendTestsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
			_getEntitiesForAddInFriendTestsCommand = async () => { return await WrapperDataService.GetRepository<TestFriend>().GetAllAsync(); };;

		}
			private async void AddInFriendTestsCommand_Execute()
			{
				SelectAndAddInListWrapper<TestFriend, TestFriendWrapper>(await _getEntitiesForAddInFriendTestsCommand(), Item.FriendTests);
			}

			private void RemoveFromFriendTestsCommand_Execute()
			{
				Item.FriendTests.Remove(SelectedFriendTestsItem);
			}

			private bool RemoveFromFriendTestsCommand_CanExecute()
			{
				return SelectedFriendTestsItem != null;
			}



    }


    public partial class DocumentDetailsViewModel : BaseDetailsViewModel<DocumentWrapper, Document, AfterSaveDocumentEvent>
    {
		private Func<Task<List<Document>>> _getEntitiesForSelectRequestDocumentCommand;
		public ICommand SelectRequestDocumentCommand { get; private set; }
		public ICommand ClearRequestDocumentCommand { get; private set; }

		private Func<Task<List<Employee>>> _getEntitiesForSelectAuthorCommand;
		public ICommand SelectAuthorCommand { get; private set; }
		public ICommand ClearAuthorCommand { get; private set; }

		private Func<Task<List<Employee>>> _getEntitiesForSelectSenderEmployeeCommand;
		public ICommand SelectSenderEmployeeCommand { get; private set; }
		public ICommand ClearSenderEmployeeCommand { get; private set; }

		private Func<Task<List<Employee>>> _getEntitiesForSelectRecipientEmployeeCommand;
		public ICommand SelectRecipientEmployeeCommand { get; private set; }
		public ICommand ClearRecipientEmployeeCommand { get; private set; }

		private Func<Task<List<DocumentsRegistrationDetails>>> _getEntitiesForSelectRegistrationDetailsOfSenderCommand;
		public ICommand SelectRegistrationDetailsOfSenderCommand { get; private set; }
		public ICommand ClearRegistrationDetailsOfSenderCommand { get; private set; }

		private Func<Task<List<DocumentsRegistrationDetails>>> _getEntitiesForSelectRegistrationDetailsOfRecipientCommand;
		public ICommand SelectRegistrationDetailsOfRecipientCommand { get; private set; }
		public ICommand ClearRegistrationDetailsOfRecipientCommand { get; private set; }

		private Func<Task<List<Employee>>> _getEntitiesForAddInCopyToRecipientsCommand;
		public ICommand AddInCopyToRecipientsCommand { get; }
		public ICommand RemoveFromCopyToRecipientsCommand { get; }
		private EmployeeWrapper _selectedCopyToRecipientsItem;
		public EmployeeWrapper SelectedCopyToRecipientsItem 
		{ 
			get { return _selectedCopyToRecipientsItem; }
			set 
			{ 
				if (Equals(_selectedCopyToRecipientsItem, value)) return;
				_selectedCopyToRecipientsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromCopyToRecipientsCommand).RaiseCanExecuteChanged();
			}
		}


        public DocumentDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectRequestDocumentCommand == null) SelectRequestDocumentCommand = new DelegateCommand(SelectRequestDocumentCommand_Execute);
			if (ClearRequestDocumentCommand == null) ClearRequestDocumentCommand = new DelegateCommand(ClearRequestDocumentCommand_Execute);

			if (SelectAuthorCommand == null) SelectAuthorCommand = new DelegateCommand(SelectAuthorCommand_Execute);
			if (ClearAuthorCommand == null) ClearAuthorCommand = new DelegateCommand(ClearAuthorCommand_Execute);

			if (SelectSenderEmployeeCommand == null) SelectSenderEmployeeCommand = new DelegateCommand(SelectSenderEmployeeCommand_Execute);
			if (ClearSenderEmployeeCommand == null) ClearSenderEmployeeCommand = new DelegateCommand(ClearSenderEmployeeCommand_Execute);

			if (SelectRecipientEmployeeCommand == null) SelectRecipientEmployeeCommand = new DelegateCommand(SelectRecipientEmployeeCommand_Execute);
			if (ClearRecipientEmployeeCommand == null) ClearRecipientEmployeeCommand = new DelegateCommand(ClearRecipientEmployeeCommand_Execute);

			if (SelectRegistrationDetailsOfSenderCommand == null) SelectRegistrationDetailsOfSenderCommand = new DelegateCommand(SelectRegistrationDetailsOfSenderCommand_Execute);
			if (ClearRegistrationDetailsOfSenderCommand == null) ClearRegistrationDetailsOfSenderCommand = new DelegateCommand(ClearRegistrationDetailsOfSenderCommand_Execute);

			if (SelectRegistrationDetailsOfRecipientCommand == null) SelectRegistrationDetailsOfRecipientCommand = new DelegateCommand(SelectRegistrationDetailsOfRecipientCommand_Execute);
			if (ClearRegistrationDetailsOfRecipientCommand == null) ClearRegistrationDetailsOfRecipientCommand = new DelegateCommand(ClearRegistrationDetailsOfRecipientCommand_Execute);

			AddInCopyToRecipientsCommand = new DelegateCommand(AddInCopyToRecipientsCommand_Execute);
			RemoveFromCopyToRecipientsCommand = new DelegateCommand(RemoveFromCopyToRecipientsCommand_Execute, RemoveFromCopyToRecipientsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectRequestDocumentCommand = async () => { return await WrapperDataService.GetRepository<Document>().GetAllAsync(); };
            _getEntitiesForSelectAuthorCommand = async () => { return await WrapperDataService.GetRepository<Employee>().GetAllAsync(); };
            _getEntitiesForSelectSenderEmployeeCommand = async () => { return await WrapperDataService.GetRepository<Employee>().GetAllAsync(); };
            _getEntitiesForSelectRecipientEmployeeCommand = async () => { return await WrapperDataService.GetRepository<Employee>().GetAllAsync(); };
            _getEntitiesForSelectRegistrationDetailsOfSenderCommand = async () => { return await WrapperDataService.GetRepository<DocumentsRegistrationDetails>().GetAllAsync(); };
            _getEntitiesForSelectRegistrationDetailsOfRecipientCommand = async () => { return await WrapperDataService.GetRepository<DocumentsRegistrationDetails>().GetAllAsync(); };
			_getEntitiesForAddInCopyToRecipientsCommand = async () => { return await WrapperDataService.GetRepository<Employee>().GetAllAsync(); };;

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

			private async void AddInCopyToRecipientsCommand_Execute()
			{
				SelectAndAddInListWrapper<Employee, EmployeeWrapper>(await _getEntitiesForAddInCopyToRecipientsCommand(), Item.CopyToRecipients);
			}

			private void RemoveFromCopyToRecipientsCommand_Execute()
			{
				Item.CopyToRecipients.Remove(SelectedCopyToRecipientsItem);
			}

			private bool RemoveFromCopyToRecipientsCommand_CanExecute()
			{
				return SelectedCopyToRecipientsItem != null;
			}



    }


    public partial class TestEntityDetailsViewModel : BaseDetailsViewModel<TestEntityWrapper, TestEntity, AfterSaveTestEntityEvent>
    {

        public TestEntityDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class TestHusbandDetailsViewModel : BaseDetailsViewModel<TestHusbandWrapper, TestHusband, AfterSaveTestHusbandEvent>
    {
		private Func<Task<List<TestWife>>> _getEntitiesForSelectWifeCommand;
		public ICommand SelectWifeCommand { get; private set; }
		public ICommand ClearWifeCommand { get; private set; }

		private Func<Task<List<TestChild>>> _getEntitiesForAddInChildrenCommand;
		public ICommand AddInChildrenCommand { get; }
		public ICommand RemoveFromChildrenCommand { get; }
		private TestChildWrapper _selectedChildrenItem;
		public TestChildWrapper SelectedChildrenItem 
		{ 
			get { return _selectedChildrenItem; }
			set 
			{ 
				if (Equals(_selectedChildrenItem, value)) return;
				_selectedChildrenItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromChildrenCommand).RaiseCanExecuteChanged();
			}
		}


        public TestHusbandDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectWifeCommand == null) SelectWifeCommand = new DelegateCommand(SelectWifeCommand_Execute);
			if (ClearWifeCommand == null) ClearWifeCommand = new DelegateCommand(ClearWifeCommand_Execute);

			AddInChildrenCommand = new DelegateCommand(AddInChildrenCommand_Execute);
			RemoveFromChildrenCommand = new DelegateCommand(RemoveFromChildrenCommand_Execute, RemoveFromChildrenCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectWifeCommand = async () => { return await WrapperDataService.GetRepository<TestWife>().GetAllAsync(); };
			_getEntitiesForAddInChildrenCommand = async () => { return await WrapperDataService.GetRepository<TestChild>().GetAllAsync(); };;

		}
		private async void SelectWifeCommand_Execute() 
		{
            SelectAndSetWrapper<TestWife, TestWifeWrapper>(await _getEntitiesForSelectWifeCommand(), nameof(Item.Wife), Item.Wife?.Id);
		}

		private void ClearWifeCommand_Execute() 
		{
		Item.Wife = null;
		    
		}

			private async void AddInChildrenCommand_Execute()
			{
				SelectAndAddInListWrapper<TestChild, TestChildWrapper>(await _getEntitiesForAddInChildrenCommand(), Item.Children);
			}

			private void RemoveFromChildrenCommand_Execute()
			{
				Item.Children.Remove(SelectedChildrenItem);
			}

			private bool RemoveFromChildrenCommand_CanExecute()
			{
				return SelectedChildrenItem != null;
			}



    }


    public partial class TestWifeDetailsViewModel : BaseDetailsViewModel<TestWifeWrapper, TestWife, AfterSaveTestWifeEvent>
    {
		private Func<Task<List<TestHusband>>> _getEntitiesForSelectHusbandCommand;
		public ICommand SelectHusbandCommand { get; private set; }
		public ICommand ClearHusbandCommand { get; private set; }


        public TestWifeDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectHusbandCommand == null) SelectHusbandCommand = new DelegateCommand(SelectHusbandCommand_Execute);
			if (ClearHusbandCommand == null) ClearHusbandCommand = new DelegateCommand(ClearHusbandCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectHusbandCommand = async () => { return await WrapperDataService.GetRepository<TestHusband>().GetAllAsync(); };

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
		public ICommand SelectHusbandCommand { get; private set; }
		public ICommand ClearHusbandCommand { get; private set; }

		private Func<Task<List<TestWife>>> _getEntitiesForSelectWifeCommand;
		public ICommand SelectWifeCommand { get; private set; }
		public ICommand ClearWifeCommand { get; private set; }


        public TestChildDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectHusbandCommand == null) SelectHusbandCommand = new DelegateCommand(SelectHusbandCommand_Execute);
			if (ClearHusbandCommand == null) ClearHusbandCommand = new DelegateCommand(ClearHusbandCommand_Execute);

			if (SelectWifeCommand == null) SelectWifeCommand = new DelegateCommand(SelectWifeCommand_Execute);
			if (ClearWifeCommand == null) ClearWifeCommand = new DelegateCommand(ClearWifeCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectHusbandCommand = async () => { return await WrapperDataService.GetRepository<TestHusband>().GetAllAsync(); };
            _getEntitiesForSelectWifeCommand = async () => { return await WrapperDataService.GetRepository<TestWife>().GetAllAsync(); };

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


    public partial class SumOnDateDetailsViewModel : BaseDetailsViewModel<SumOnDateWrapper, SumOnDate, AfterSaveSumOnDateEvent>
    {
		private Func<Task<List<Sum>>> _getEntitiesForSelectSumCommand;
		public ICommand SelectSumCommand { get; private set; }
		public ICommand ClearSumCommand { get; private set; }


        public SumOnDateDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectSumCommand == null) SelectSumCommand = new DelegateCommand(SelectSumCommand_Execute);
			if (ClearSumCommand == null) ClearSumCommand = new DelegateCommand(ClearSumCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectSumCommand = async () => { return await WrapperDataService.GetRepository<Sum>().GetAllAsync(); };

		}
		private async void SelectSumCommand_Execute() 
		{
            SelectAndSetWrapper<Sum, SumWrapper>(await _getEntitiesForSelectSumCommand(), nameof(Item.Sum), Item.Sum?.Id);
		}

		private void ClearSumCommand_Execute() 
		{
		Item.Sum = null;
		    
		}



    }


    public partial class ProductDetailsViewModel : BaseDetailsViewModel<ProductWrapper, Product, AfterSaveProductEvent>
    {
		private Func<Task<List<ProductBlock>>> _getEntitiesForSelectProductBlockCommand;
		public ICommand SelectProductBlockCommand { get; private set; }
		public ICommand ClearProductBlockCommand { get; private set; }

		private Func<Task<List<Product>>> _getEntitiesForAddInDependentProductsCommand;
		public ICommand AddInDependentProductsCommand { get; }
		public ICommand RemoveFromDependentProductsCommand { get; }
		private ProductWrapper _selectedDependentProductsItem;
		public ProductWrapper SelectedDependentProductsItem 
		{ 
			get { return _selectedDependentProductsItem; }
			set 
			{ 
				if (Equals(_selectedDependentProductsItem, value)) return;
				_selectedDependentProductsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromDependentProductsCommand).RaiseCanExecuteChanged();
			}
		}


        public ProductDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectProductBlockCommand == null) SelectProductBlockCommand = new DelegateCommand(SelectProductBlockCommand_Execute);
			if (ClearProductBlockCommand == null) ClearProductBlockCommand = new DelegateCommand(ClearProductBlockCommand_Execute);

			AddInDependentProductsCommand = new DelegateCommand(AddInDependentProductsCommand_Execute);
			RemoveFromDependentProductsCommand = new DelegateCommand(RemoveFromDependentProductsCommand_Execute, RemoveFromDependentProductsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectProductBlockCommand = async () => { return await WrapperDataService.GetRepository<ProductBlock>().GetAllAsync(); };
			_getEntitiesForAddInDependentProductsCommand = async () => { return await WrapperDataService.GetRepository<Product>().GetAllAsync(); };;

		}
		private async void SelectProductBlockCommand_Execute() 
		{
            SelectAndSetWrapper<ProductBlock, ProductBlockWrapper>(await _getEntitiesForSelectProductBlockCommand(), nameof(Item.ProductBlock), Item.ProductBlock?.Id);
		}

		private void ClearProductBlockCommand_Execute() 
		{
		Item.ProductBlock = null;
		    
		}

			private async void AddInDependentProductsCommand_Execute()
			{
				SelectAndAddInListWrapper<Product, ProductWrapper>(await _getEntitiesForAddInDependentProductsCommand(), Item.DependentProducts);
			}

			private void RemoveFromDependentProductsCommand_Execute()
			{
				Item.DependentProducts.Remove(SelectedDependentProductsItem);
			}

			private bool RemoveFromDependentProductsCommand_CanExecute()
			{
				return SelectedDependentProductsItem != null;
			}



    }


    public partial class OfferDetailsViewModel : BaseDetailsViewModel<OfferWrapper, Offer, AfterSaveOfferEvent>
    {
		private Func<Task<List<Project>>> _getEntitiesForSelectProjectCommand;
		public ICommand SelectProjectCommand { get; private set; }
		public ICommand ClearProjectCommand { get; private set; }

		private Func<Task<List<Document>>> _getEntitiesForSelectRequestDocumentCommand;
		public ICommand SelectRequestDocumentCommand { get; private set; }
		public ICommand ClearRequestDocumentCommand { get; private set; }

		private Func<Task<List<Employee>>> _getEntitiesForSelectAuthorCommand;
		public ICommand SelectAuthorCommand { get; private set; }
		public ICommand ClearAuthorCommand { get; private set; }

		private Func<Task<List<Employee>>> _getEntitiesForSelectSenderEmployeeCommand;
		public ICommand SelectSenderEmployeeCommand { get; private set; }
		public ICommand ClearSenderEmployeeCommand { get; private set; }

		private Func<Task<List<Employee>>> _getEntitiesForSelectRecipientEmployeeCommand;
		public ICommand SelectRecipientEmployeeCommand { get; private set; }
		public ICommand ClearRecipientEmployeeCommand { get; private set; }

		private Func<Task<List<DocumentsRegistrationDetails>>> _getEntitiesForSelectRegistrationDetailsOfSenderCommand;
		public ICommand SelectRegistrationDetailsOfSenderCommand { get; private set; }
		public ICommand ClearRegistrationDetailsOfSenderCommand { get; private set; }

		private Func<Task<List<DocumentsRegistrationDetails>>> _getEntitiesForSelectRegistrationDetailsOfRecipientCommand;
		public ICommand SelectRegistrationDetailsOfRecipientCommand { get; private set; }
		public ICommand ClearRegistrationDetailsOfRecipientCommand { get; private set; }

		private Func<Task<List<Employee>>> _getEntitiesForAddInCopyToRecipientsCommand;
		public ICommand AddInCopyToRecipientsCommand { get; }
		public ICommand RemoveFromCopyToRecipientsCommand { get; }
		private EmployeeWrapper _selectedCopyToRecipientsItem;
		public EmployeeWrapper SelectedCopyToRecipientsItem 
		{ 
			get { return _selectedCopyToRecipientsItem; }
			set 
			{ 
				if (Equals(_selectedCopyToRecipientsItem, value)) return;
				_selectedCopyToRecipientsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromCopyToRecipientsCommand).RaiseCanExecuteChanged();
			}
		}


        public OfferDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectProjectCommand == null) SelectProjectCommand = new DelegateCommand(SelectProjectCommand_Execute);
			if (ClearProjectCommand == null) ClearProjectCommand = new DelegateCommand(ClearProjectCommand_Execute);

			if (SelectRequestDocumentCommand == null) SelectRequestDocumentCommand = new DelegateCommand(SelectRequestDocumentCommand_Execute);
			if (ClearRequestDocumentCommand == null) ClearRequestDocumentCommand = new DelegateCommand(ClearRequestDocumentCommand_Execute);

			if (SelectAuthorCommand == null) SelectAuthorCommand = new DelegateCommand(SelectAuthorCommand_Execute);
			if (ClearAuthorCommand == null) ClearAuthorCommand = new DelegateCommand(ClearAuthorCommand_Execute);

			if (SelectSenderEmployeeCommand == null) SelectSenderEmployeeCommand = new DelegateCommand(SelectSenderEmployeeCommand_Execute);
			if (ClearSenderEmployeeCommand == null) ClearSenderEmployeeCommand = new DelegateCommand(ClearSenderEmployeeCommand_Execute);

			if (SelectRecipientEmployeeCommand == null) SelectRecipientEmployeeCommand = new DelegateCommand(SelectRecipientEmployeeCommand_Execute);
			if (ClearRecipientEmployeeCommand == null) ClearRecipientEmployeeCommand = new DelegateCommand(ClearRecipientEmployeeCommand_Execute);

			if (SelectRegistrationDetailsOfSenderCommand == null) SelectRegistrationDetailsOfSenderCommand = new DelegateCommand(SelectRegistrationDetailsOfSenderCommand_Execute);
			if (ClearRegistrationDetailsOfSenderCommand == null) ClearRegistrationDetailsOfSenderCommand = new DelegateCommand(ClearRegistrationDetailsOfSenderCommand_Execute);

			if (SelectRegistrationDetailsOfRecipientCommand == null) SelectRegistrationDetailsOfRecipientCommand = new DelegateCommand(SelectRegistrationDetailsOfRecipientCommand_Execute);
			if (ClearRegistrationDetailsOfRecipientCommand == null) ClearRegistrationDetailsOfRecipientCommand = new DelegateCommand(ClearRegistrationDetailsOfRecipientCommand_Execute);

			AddInCopyToRecipientsCommand = new DelegateCommand(AddInCopyToRecipientsCommand_Execute);
			RemoveFromCopyToRecipientsCommand = new DelegateCommand(RemoveFromCopyToRecipientsCommand_Execute, RemoveFromCopyToRecipientsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectProjectCommand = async () => { return await WrapperDataService.GetRepository<Project>().GetAllAsync(); };
            _getEntitiesForSelectRequestDocumentCommand = async () => { return await WrapperDataService.GetRepository<Document>().GetAllAsync(); };
            _getEntitiesForSelectAuthorCommand = async () => { return await WrapperDataService.GetRepository<Employee>().GetAllAsync(); };
            _getEntitiesForSelectSenderEmployeeCommand = async () => { return await WrapperDataService.GetRepository<Employee>().GetAllAsync(); };
            _getEntitiesForSelectRecipientEmployeeCommand = async () => { return await WrapperDataService.GetRepository<Employee>().GetAllAsync(); };
            _getEntitiesForSelectRegistrationDetailsOfSenderCommand = async () => { return await WrapperDataService.GetRepository<DocumentsRegistrationDetails>().GetAllAsync(); };
            _getEntitiesForSelectRegistrationDetailsOfRecipientCommand = async () => { return await WrapperDataService.GetRepository<DocumentsRegistrationDetails>().GetAllAsync(); };
			_getEntitiesForAddInCopyToRecipientsCommand = async () => { return await WrapperDataService.GetRepository<Employee>().GetAllAsync(); };;

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

			private async void AddInCopyToRecipientsCommand_Execute()
			{
				SelectAndAddInListWrapper<Employee, EmployeeWrapper>(await _getEntitiesForAddInCopyToRecipientsCommand(), Item.CopyToRecipients);
			}

			private void RemoveFromCopyToRecipientsCommand_Execute()
			{
				Item.CopyToRecipients.Remove(SelectedCopyToRecipientsItem);
			}

			private bool RemoveFromCopyToRecipientsCommand_CanExecute()
			{
				return SelectedCopyToRecipientsItem != null;
			}



    }


    public partial class EmployeeDetailsViewModel : BaseDetailsViewModel<EmployeeWrapper, Employee, AfterSaveEmployeeEvent>
    {
		private Func<Task<List<Person>>> _getEntitiesForSelectPersonCommand;
		public ICommand SelectPersonCommand { get; private set; }
		public ICommand ClearPersonCommand { get; private set; }

		private Func<Task<List<Company>>> _getEntitiesForSelectCompanyCommand;
		public ICommand SelectCompanyCommand { get; private set; }
		public ICommand ClearCompanyCommand { get; private set; }

		private Func<Task<List<EmployeesPosition>>> _getEntitiesForSelectPositionCommand;
		public ICommand SelectPositionCommand { get; private set; }
		public ICommand ClearPositionCommand { get; private set; }


        public EmployeeDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectPersonCommand == null) SelectPersonCommand = new DelegateCommand(SelectPersonCommand_Execute);
			if (ClearPersonCommand == null) ClearPersonCommand = new DelegateCommand(ClearPersonCommand_Execute);

			if (SelectCompanyCommand == null) SelectCompanyCommand = new DelegateCommand(SelectCompanyCommand_Execute);
			if (ClearCompanyCommand == null) ClearCompanyCommand = new DelegateCommand(ClearCompanyCommand_Execute);

			if (SelectPositionCommand == null) SelectPositionCommand = new DelegateCommand(SelectPositionCommand_Execute);
			if (ClearPositionCommand == null) ClearPositionCommand = new DelegateCommand(ClearPositionCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectPersonCommand = async () => { return await WrapperDataService.GetRepository<Person>().GetAllAsync(); };
            _getEntitiesForSelectCompanyCommand = async () => { return await WrapperDataService.GetRepository<Company>().GetAllAsync(); };
            _getEntitiesForSelectPositionCommand = async () => { return await WrapperDataService.GetRepository<EmployeesPosition>().GetAllAsync(); };

		}
		private async void SelectPersonCommand_Execute() 
		{
            SelectAndSetWrapper<Person, PersonWrapper>(await _getEntitiesForSelectPersonCommand(), nameof(Item.Person), Item.Person?.Id);
		}

		private void ClearPersonCommand_Execute() 
		{
		Item.Person = null;
		    
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
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class PaymentConditionDetailsViewModel : BaseDetailsViewModel<PaymentConditionWrapper, PaymentCondition, AfterSavePaymentConditionEvent>
    {

        public PaymentConditionDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class PaymentDocumentDetailsViewModel : BaseDetailsViewModel<PaymentDocumentWrapper, PaymentDocument, AfterSavePaymentDocumentEvent>
    {
		private Func<Task<List<PaymentActual>>> _getEntitiesForAddInPaymentsCommand;
		public ICommand AddInPaymentsCommand { get; }
		public ICommand RemoveFromPaymentsCommand { get; }
		private PaymentActualWrapper _selectedPaymentsItem;
		public PaymentActualWrapper SelectedPaymentsItem 
		{ 
			get { return _selectedPaymentsItem; }
			set 
			{ 
				if (Equals(_selectedPaymentsItem, value)) return;
				_selectedPaymentsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromPaymentsCommand).RaiseCanExecuteChanged();
			}
		}


        public PaymentDocumentDetailsViewModel(IUnityContainer container) : base(container) 
		{
			AddInPaymentsCommand = new DelegateCommand(AddInPaymentsCommand_Execute);
			RemoveFromPaymentsCommand = new DelegateCommand(RemoveFromPaymentsCommand_Execute, RemoveFromPaymentsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
			_getEntitiesForAddInPaymentsCommand = async () => { return await WrapperDataService.GetRepository<PaymentActual>().GetAllAsync(); };;

		}
			private async void AddInPaymentsCommand_Execute()
			{
				SelectAndAddInListWrapper<PaymentActual, PaymentActualWrapper>(await _getEntitiesForAddInPaymentsCommand(), Item.Payments);
			}

			private void RemoveFromPaymentsCommand_Execute()
			{
				Item.Payments.Remove(SelectedPaymentsItem);
			}

			private bool RemoveFromPaymentsCommand_CanExecute()
			{
				return SelectedPaymentsItem != null;
			}



    }


    public partial class FacilityDetailsViewModel : BaseDetailsViewModel<FacilityWrapper, Facility, AfterSaveFacilityEvent>
    {
		private Func<Task<List<FacilityType>>> _getEntitiesForSelectTypeCommand;
		public ICommand SelectTypeCommand { get; private set; }
		public ICommand ClearTypeCommand { get; private set; }

		private Func<Task<List<Company>>> _getEntitiesForSelectOwnerCompanyCommand;
		public ICommand SelectOwnerCompanyCommand { get; private set; }
		public ICommand ClearOwnerCompanyCommand { get; private set; }

		private Func<Task<List<Address>>> _getEntitiesForSelectAddressCommand;
		public ICommand SelectAddressCommand { get; private set; }
		public ICommand ClearAddressCommand { get; private set; }


        public FacilityDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectTypeCommand == null) SelectTypeCommand = new DelegateCommand(SelectTypeCommand_Execute);
			if (ClearTypeCommand == null) ClearTypeCommand = new DelegateCommand(ClearTypeCommand_Execute);

			if (SelectOwnerCompanyCommand == null) SelectOwnerCompanyCommand = new DelegateCommand(SelectOwnerCompanyCommand_Execute);
			if (ClearOwnerCompanyCommand == null) ClearOwnerCompanyCommand = new DelegateCommand(ClearOwnerCompanyCommand_Execute);

			if (SelectAddressCommand == null) SelectAddressCommand = new DelegateCommand(SelectAddressCommand_Execute);
			if (ClearAddressCommand == null) ClearAddressCommand = new DelegateCommand(ClearAddressCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectTypeCommand = async () => { return await WrapperDataService.GetRepository<FacilityType>().GetAllAsync(); };
            _getEntitiesForSelectOwnerCompanyCommand = async () => { return await WrapperDataService.GetRepository<Company>().GetAllAsync(); };
            _getEntitiesForSelectAddressCommand = async () => { return await WrapperDataService.GetRepository<Address>().GetAllAsync(); };

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
		public ICommand SelectManagerCommand { get; private set; }
		public ICommand ClearManagerCommand { get; private set; }

		private Func<Task<List<Note>>> _getEntitiesForAddInNotesCommand;
		public ICommand AddInNotesCommand { get; }
		public ICommand RemoveFromNotesCommand { get; }
		private NoteWrapper _selectedNotesItem;
		public NoteWrapper SelectedNotesItem 
		{ 
			get { return _selectedNotesItem; }
			set 
			{ 
				if (Equals(_selectedNotesItem, value)) return;
				_selectedNotesItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromNotesCommand).RaiseCanExecuteChanged();
			}
		}


        public ProjectDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectManagerCommand == null) SelectManagerCommand = new DelegateCommand(SelectManagerCommand_Execute);
			if (ClearManagerCommand == null) ClearManagerCommand = new DelegateCommand(ClearManagerCommand_Execute);

			AddInNotesCommand = new DelegateCommand(AddInNotesCommand_Execute);
			RemoveFromNotesCommand = new DelegateCommand(RemoveFromNotesCommand_Execute, RemoveFromNotesCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectManagerCommand = async () => { return await WrapperDataService.GetRepository<User>().GetAllAsync(); };
			_getEntitiesForAddInNotesCommand = async () => { return await WrapperDataService.GetRepository<Note>().GetAllAsync(); };;

		}
		private async void SelectManagerCommand_Execute() 
		{
            SelectAndSetWrapper<User, UserWrapper>(await _getEntitiesForSelectManagerCommand(), nameof(Item.Manager), Item.Manager?.Id);
		}

		private void ClearManagerCommand_Execute() 
		{
		Item.Manager = null;
		    
		}

			private async void AddInNotesCommand_Execute()
			{
				SelectAndAddInListWrapper<Note, NoteWrapper>(await _getEntitiesForAddInNotesCommand(), Item.Notes);
			}

			private void RemoveFromNotesCommand_Execute()
			{
				Item.Notes.Remove(SelectedNotesItem);
			}

			private bool RemoveFromNotesCommand_CanExecute()
			{
				return SelectedNotesItem != null;
			}



    }


    public partial class UserRoleDetailsViewModel : BaseDetailsViewModel<UserRoleWrapper, UserRole, AfterSaveUserRoleEvent>
    {

        public UserRoleDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class SpecificationDetailsViewModel : BaseDetailsViewModel<SpecificationWrapper, Specification, AfterSaveSpecificationEvent>
    {
		private Func<Task<List<Contract>>> _getEntitiesForSelectContractCommand;
		public ICommand SelectContractCommand { get; private set; }
		public ICommand ClearContractCommand { get; private set; }


        public SpecificationDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectContractCommand == null) SelectContractCommand = new DelegateCommand(SelectContractCommand_Execute);
			if (ClearContractCommand == null) ClearContractCommand = new DelegateCommand(ClearContractCommand_Execute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectContractCommand = async () => { return await WrapperDataService.GetRepository<Contract>().GetAllAsync(); };

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
		public ICommand SelectProjectCommand { get; private set; }
		public ICommand ClearProjectCommand { get; private set; }

		private Func<Task<List<Company>>> _getEntitiesForSelectWinnerCommand;
		public ICommand SelectWinnerCommand { get; private set; }
		public ICommand ClearWinnerCommand { get; private set; }

		private Func<Task<List<TenderType>>> _getEntitiesForAddInTypesCommand;
		public ICommand AddInTypesCommand { get; }
		public ICommand RemoveFromTypesCommand { get; }
		private TenderTypeWrapper _selectedTypesItem;
		public TenderTypeWrapper SelectedTypesItem 
		{ 
			get { return _selectedTypesItem; }
			set 
			{ 
				if (Equals(_selectedTypesItem, value)) return;
				_selectedTypesItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromTypesCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<Company>>> _getEntitiesForAddInParticipantsCommand;
		public ICommand AddInParticipantsCommand { get; }
		public ICommand RemoveFromParticipantsCommand { get; }
		private CompanyWrapper _selectedParticipantsItem;
		public CompanyWrapper SelectedParticipantsItem 
		{ 
			get { return _selectedParticipantsItem; }
			set 
			{ 
				if (Equals(_selectedParticipantsItem, value)) return;
				_selectedParticipantsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromParticipantsCommand).RaiseCanExecuteChanged();
			}
		}


        public TenderDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectProjectCommand == null) SelectProjectCommand = new DelegateCommand(SelectProjectCommand_Execute);
			if (ClearProjectCommand == null) ClearProjectCommand = new DelegateCommand(ClearProjectCommand_Execute);

			if (SelectWinnerCommand == null) SelectWinnerCommand = new DelegateCommand(SelectWinnerCommand_Execute);
			if (ClearWinnerCommand == null) ClearWinnerCommand = new DelegateCommand(ClearWinnerCommand_Execute);

			AddInTypesCommand = new DelegateCommand(AddInTypesCommand_Execute);
			RemoveFromTypesCommand = new DelegateCommand(RemoveFromTypesCommand_Execute, RemoveFromTypesCommand_CanExecute);

			AddInParticipantsCommand = new DelegateCommand(AddInParticipantsCommand_Execute);
			RemoveFromParticipantsCommand = new DelegateCommand(RemoveFromParticipantsCommand_Execute, RemoveFromParticipantsCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectProjectCommand = async () => { return await WrapperDataService.GetRepository<Project>().GetAllAsync(); };
            _getEntitiesForSelectWinnerCommand = async () => { return await WrapperDataService.GetRepository<Company>().GetAllAsync(); };
			_getEntitiesForAddInTypesCommand = async () => { return await WrapperDataService.GetRepository<TenderType>().GetAllAsync(); };;
			_getEntitiesForAddInParticipantsCommand = async () => { return await WrapperDataService.GetRepository<Company>().GetAllAsync(); };;

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

			private async void AddInTypesCommand_Execute()
			{
				SelectAndAddInListWrapper<TenderType, TenderTypeWrapper>(await _getEntitiesForAddInTypesCommand(), Item.Types);
			}

			private void RemoveFromTypesCommand_Execute()
			{
				Item.Types.Remove(SelectedTypesItem);
			}

			private bool RemoveFromTypesCommand_CanExecute()
			{
				return SelectedTypesItem != null;
			}

			private async void AddInParticipantsCommand_Execute()
			{
				SelectAndAddInListWrapper<Company, CompanyWrapper>(await _getEntitiesForAddInParticipantsCommand(), Item.Participants);
			}

			private void RemoveFromParticipantsCommand_Execute()
			{
				Item.Participants.Remove(SelectedParticipantsItem);
			}

			private bool RemoveFromParticipantsCommand_CanExecute()
			{
				return SelectedParticipantsItem != null;
			}



    }


    public partial class TenderTypeDetailsViewModel : BaseDetailsViewModel<TenderTypeWrapper, TenderType, AfterSaveTenderTypeEvent>
    {

        public TenderTypeDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}


        protected override void InitDefaultGetMethods()
		{

		}


    }


    public partial class UserDetailsViewModel : BaseDetailsViewModel<UserWrapper, User, AfterSaveUserEvent>
    {
		private Func<Task<List<Employee>>> _getEntitiesForSelectEmployeeCommand;
		public ICommand SelectEmployeeCommand { get; private set; }
		public ICommand ClearEmployeeCommand { get; private set; }

		private Func<Task<List<UserRole>>> _getEntitiesForAddInRolesCommand;
		public ICommand AddInRolesCommand { get; }
		public ICommand RemoveFromRolesCommand { get; }
		private UserRoleWrapper _selectedRolesItem;
		public UserRoleWrapper SelectedRolesItem 
		{ 
			get { return _selectedRolesItem; }
			set 
			{ 
				if (Equals(_selectedRolesItem, value)) return;
				_selectedRolesItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromRolesCommand).RaiseCanExecuteChanged();
			}
		}


        public UserDetailsViewModel(IUnityContainer container) : base(container) 
		{
			if (SelectEmployeeCommand == null) SelectEmployeeCommand = new DelegateCommand(SelectEmployeeCommand_Execute);
			if (ClearEmployeeCommand == null) ClearEmployeeCommand = new DelegateCommand(ClearEmployeeCommand_Execute);

			AddInRolesCommand = new DelegateCommand(AddInRolesCommand_Execute);
			RemoveFromRolesCommand = new DelegateCommand(RemoveFromRolesCommand_Execute, RemoveFromRolesCommand_CanExecute);

		}


        protected override void InitDefaultGetMethods()
		{
            _getEntitiesForSelectEmployeeCommand = async () => { return await WrapperDataService.GetRepository<Employee>().GetAllAsync(); };
			_getEntitiesForAddInRolesCommand = async () => { return await WrapperDataService.GetRepository<UserRole>().GetAllAsync(); };;

		}
		private async void SelectEmployeeCommand_Execute() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectEmployeeCommand(), nameof(Item.Employee), Item.Employee?.Id);
		}

		private void ClearEmployeeCommand_Execute() 
		{
		Item.Employee = null;
		    
		}

			private async void AddInRolesCommand_Execute()
			{
				SelectAndAddInListWrapper<UserRole, UserRoleWrapper>(await _getEntitiesForAddInRolesCommand(), Item.Roles);
			}

			private void RemoveFromRolesCommand_Execute()
			{
				Item.Roles.Remove(SelectedRolesItem);
			}

			private bool RemoveFromRolesCommand_CanExecute()
			{
				return SelectedRolesItem != null;
			}



    }


}
