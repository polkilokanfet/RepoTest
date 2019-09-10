














using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using HVTApp.Model.Events;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using Prism.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace HVTApp.UI.ViewModels
{

    public partial class CountryUnionDetailsViewModel : BaseDetailsViewModel<CountryUnionWrapper, CountryUnion, AfterSaveCountryUnionEvent>
    {
		private Func<Task<List<Country>>> _getEntitiesForAddInCountriesCommand;
		public ICommand AddInCountriesCommand { get; }
		public ICommand RemoveFromCountriesCommand { get; }
		private CountryWrapper _selectedCountriesItem;
		public CountryWrapper SelectedCountriesItem 
		{ 
			get { return _selectedCountriesItem; }
			set 
			{ 
				if (Equals(_selectedCountriesItem, value)) return;
				_selectedCountriesItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromCountriesCommand).RaiseCanExecuteChanged();
			}
		}


        public CountryUnionDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForAddInCountriesCommand == null) _getEntitiesForAddInCountriesCommand = async () => { return await UnitOfWork.Repository<Country>().GetAllAsync(); };;
			if (AddInCountriesCommand == null) AddInCountriesCommand = new DelegateCommand(AddInCountriesCommand_Execute_Default);
			if (RemoveFromCountriesCommand == null) RemoveFromCountriesCommand = new DelegateCommand(RemoveFromCountriesCommand_Execute_Default, RemoveFromCountriesCommand_CanExecute_Default);

		}

			private async void AddInCountriesCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Country, CountryWrapper>(await _getEntitiesForAddInCountriesCommand(), Item.Countries);
			}

			private void RemoveFromCountriesCommand_Execute_Default()
			{
				Item.Countries.Remove(SelectedCountriesItem);
			}

			private bool RemoveFromCountriesCommand_CanExecute_Default()
			{
				return SelectedCountriesItem != null;
			}



    }


    public partial class CreateNewProductTaskDetailsViewModel : BaseDetailsViewModel<CreateNewProductTaskWrapper, CreateNewProductTask, AfterSaveCreateNewProductTaskEvent>
    {
		private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; private set; }
		public ICommand ClearProductCommand { get; private set; }


        public CreateNewProductTaskDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectProductCommand == null) _getEntitiesForSelectProductCommand = async () => { return await UnitOfWork.Repository<Product>().GetAllAsync(); };
			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute_Default);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute_Default);

		}

		private async void SelectProductCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(await _getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute_Default() 
		{
						Item.Product = null;
		    
		}



    }


    public partial class DocumentNumberDetailsViewModel : BaseDetailsViewModel<DocumentNumberWrapper, DocumentNumber, AfterSaveDocumentNumberEvent>
    {

        public DocumentNumberDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class MarketFieldDetailsViewModel : BaseDetailsViewModel<MarketFieldWrapper, MarketField, AfterSaveMarketFieldEvent>
    {
		private Func<Task<List<ActivityField>>> _getEntitiesForAddInActivityFieldsCommand;
		public ICommand AddInActivityFieldsCommand { get; }
		public ICommand RemoveFromActivityFieldsCommand { get; }
		private ActivityFieldWrapper _selectedActivityFieldsItem;
		public ActivityFieldWrapper SelectedActivityFieldsItem 
		{ 
			get { return _selectedActivityFieldsItem; }
			set 
			{ 
				if (Equals(_selectedActivityFieldsItem, value)) return;
				_selectedActivityFieldsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromActivityFieldsCommand).RaiseCanExecuteChanged();
			}
		}


        public MarketFieldDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForAddInActivityFieldsCommand == null) _getEntitiesForAddInActivityFieldsCommand = async () => { return await UnitOfWork.Repository<ActivityField>().GetAllAsync(); };;
			if (AddInActivityFieldsCommand == null) AddInActivityFieldsCommand = new DelegateCommand(AddInActivityFieldsCommand_Execute_Default);
			if (RemoveFromActivityFieldsCommand == null) RemoveFromActivityFieldsCommand = new DelegateCommand(RemoveFromActivityFieldsCommand_Execute_Default, RemoveFromActivityFieldsCommand_CanExecute_Default);

		}

			private async void AddInActivityFieldsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ActivityField, ActivityFieldWrapper>(await _getEntitiesForAddInActivityFieldsCommand(), Item.ActivityFields);
			}

			private void RemoveFromActivityFieldsCommand_Execute_Default()
			{
				Item.ActivityFields.Remove(SelectedActivityFieldsItem);
			}

			private bool RemoveFromActivityFieldsCommand_CanExecute_Default()
			{
				return SelectedActivityFieldsItem != null;
			}



    }


    public partial class PaymentActualDetailsViewModel : BaseDetailsViewModel<PaymentActualWrapper, PaymentActual, AfterSavePaymentActualEvent>
    {

        public PaymentActualDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class PaymentPlannedDetailsViewModel : BaseDetailsViewModel<PaymentPlannedWrapper, PaymentPlanned, AfterSavePaymentPlannedEvent>
    {
		private Func<Task<List<PaymentCondition>>> _getEntitiesForSelectConditionCommand;
		public ICommand SelectConditionCommand { get; private set; }
		public ICommand ClearConditionCommand { get; private set; }


        public PaymentPlannedDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectConditionCommand == null) _getEntitiesForSelectConditionCommand = async () => { return await UnitOfWork.Repository<PaymentCondition>().GetAllAsync(); };
			if (SelectConditionCommand == null) SelectConditionCommand = new DelegateCommand(SelectConditionCommand_Execute_Default);
			if (ClearConditionCommand == null) ClearConditionCommand = new DelegateCommand(ClearConditionCommand_Execute_Default);

		}

		private async void SelectConditionCommand_Execute_Default() 
		{
            SelectAndSetWrapper<PaymentCondition, PaymentConditionWrapper>(await _getEntitiesForSelectConditionCommand(), nameof(Item.Condition), Item.Condition?.Id);
		}

		private void ClearConditionCommand_Execute_Default() 
		{
						Item.Condition = null;
		    
		}



    }


    public partial class ProductIncludedDetailsViewModel : BaseDetailsViewModel<ProductIncludedWrapper, ProductIncluded, AfterSaveProductIncludedEvent>
    {
		private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; private set; }
		public ICommand ClearProductCommand { get; private set; }


        public ProductIncludedDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectProductCommand == null) _getEntitiesForSelectProductCommand = async () => { return await UnitOfWork.Repository<Product>().GetAllAsync(); };
			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute_Default);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute_Default);

		}

		private async void SelectProductCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(await _getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute_Default() 
		{
						Item.Product = null;
		    
		}



    }


    public partial class ProductDesignationDetailsViewModel : BaseDetailsViewModel<ProductDesignationWrapper, ProductDesignation, AfterSaveProductDesignationEvent>
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

		private Func<Task<List<ProductDesignation>>> _getEntitiesForAddInParentsCommand;
		public ICommand AddInParentsCommand { get; }
		public ICommand RemoveFromParentsCommand { get; }
		private ProductDesignationWrapper _selectedParentsItem;
		public ProductDesignationWrapper SelectedParentsItem 
		{ 
			get { return _selectedParentsItem; }
			set 
			{ 
				if (Equals(_selectedParentsItem, value)) return;
				_selectedParentsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromParentsCommand).RaiseCanExecuteChanged();
			}
		}


        public ProductDesignationDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForAddInParametersCommand == null) _getEntitiesForAddInParametersCommand = async () => { return await UnitOfWork.Repository<Parameter>().GetAllAsync(); };;
			if (AddInParametersCommand == null) AddInParametersCommand = new DelegateCommand(AddInParametersCommand_Execute_Default);
			if (RemoveFromParametersCommand == null) RemoveFromParametersCommand = new DelegateCommand(RemoveFromParametersCommand_Execute_Default, RemoveFromParametersCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInParentsCommand == null) _getEntitiesForAddInParentsCommand = async () => { return await UnitOfWork.Repository<ProductDesignation>().GetAllAsync(); };;
			if (AddInParentsCommand == null) AddInParentsCommand = new DelegateCommand(AddInParentsCommand_Execute_Default);
			if (RemoveFromParentsCommand == null) RemoveFromParentsCommand = new DelegateCommand(RemoveFromParentsCommand_Execute_Default, RemoveFromParentsCommand_CanExecute_Default);

		}

			private async void AddInParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(await _getEntitiesForAddInParametersCommand(), Item.Parameters);
			}

			private void RemoveFromParametersCommand_Execute_Default()
			{
				Item.Parameters.Remove(SelectedParametersItem);
			}

			private bool RemoveFromParametersCommand_CanExecute_Default()
			{
				return SelectedParametersItem != null;
			}

			private async void AddInParentsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ProductDesignation, ProductDesignationWrapper>(await _getEntitiesForAddInParentsCommand(), Item.Parents);
			}

			private void RemoveFromParentsCommand_Execute_Default()
			{
				Item.Parents.Remove(SelectedParentsItem);
			}

			private bool RemoveFromParentsCommand_CanExecute_Default()
			{
				return SelectedParentsItem != null;
			}



    }


    public partial class ProductTypeDetailsViewModel : BaseDetailsViewModel<ProductTypeWrapper, ProductType, AfterSaveProductTypeEvent>
    {

        public ProductTypeDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class ProductTypeDesignationDetailsViewModel : BaseDetailsViewModel<ProductTypeDesignationWrapper, ProductTypeDesignation, AfterSaveProductTypeDesignationEvent>
    {
		private Func<Task<List<ProductType>>> _getEntitiesForSelectProductTypeCommand;
		public ICommand SelectProductTypeCommand { get; private set; }
		public ICommand ClearProductTypeCommand { get; private set; }

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


        public ProductTypeDesignationDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectProductTypeCommand == null) _getEntitiesForSelectProductTypeCommand = async () => { return await UnitOfWork.Repository<ProductType>().GetAllAsync(); };
			if (SelectProductTypeCommand == null) SelectProductTypeCommand = new DelegateCommand(SelectProductTypeCommand_Execute_Default);
			if (ClearProductTypeCommand == null) ClearProductTypeCommand = new DelegateCommand(ClearProductTypeCommand_Execute_Default);

			
			if (_getEntitiesForAddInParametersCommand == null) _getEntitiesForAddInParametersCommand = async () => { return await UnitOfWork.Repository<Parameter>().GetAllAsync(); };;
			if (AddInParametersCommand == null) AddInParametersCommand = new DelegateCommand(AddInParametersCommand_Execute_Default);
			if (RemoveFromParametersCommand == null) RemoveFromParametersCommand = new DelegateCommand(RemoveFromParametersCommand_Execute_Default, RemoveFromParametersCommand_CanExecute_Default);

		}

		private async void SelectProductTypeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ProductType, ProductTypeWrapper>(await _getEntitiesForSelectProductTypeCommand(), nameof(Item.ProductType), Item.ProductType?.Id);
		}

		private void ClearProductTypeCommand_Execute_Default() 
		{
						Item.ProductType = null;
		    
		}

			private async void AddInParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(await _getEntitiesForAddInParametersCommand(), Item.Parameters);
			}

			private void RemoveFromParametersCommand_Execute_Default()
			{
				Item.Parameters.Remove(SelectedParametersItem);
			}

			private bool RemoveFromParametersCommand_CanExecute_Default()
			{
				return SelectedParametersItem != null;
			}



    }


    public partial class ProjectTypeDetailsViewModel : BaseDetailsViewModel<ProjectTypeWrapper, ProjectType, AfterSaveProjectTypeEvent>
    {

        public ProjectTypeDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class StandartMarginalIncomeDetailsViewModel : BaseDetailsViewModel<StandartMarginalIncomeWrapper, StandartMarginalIncome, AfterSaveStandartMarginalIncomeEvent>
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


        public StandartMarginalIncomeDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForAddInParametersCommand == null) _getEntitiesForAddInParametersCommand = async () => { return await UnitOfWork.Repository<Parameter>().GetAllAsync(); };;
			if (AddInParametersCommand == null) AddInParametersCommand = new DelegateCommand(AddInParametersCommand_Execute_Default);
			if (RemoveFromParametersCommand == null) RemoveFromParametersCommand = new DelegateCommand(RemoveFromParametersCommand_Execute_Default, RemoveFromParametersCommand_CanExecute_Default);

		}

			private async void AddInParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(await _getEntitiesForAddInParametersCommand(), Item.Parameters);
			}

			private void RemoveFromParametersCommand_Execute_Default()
			{
				Item.Parameters.Remove(SelectedParametersItem);
			}

			private bool RemoveFromParametersCommand_CanExecute_Default()
			{
				return SelectedParametersItem != null;
			}



    }


    public partial class StandartProductionTermDetailsViewModel : BaseDetailsViewModel<StandartProductionTermWrapper, StandartProductionTerm, AfterSaveStandartProductionTermEvent>
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


        public StandartProductionTermDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForAddInParametersCommand == null) _getEntitiesForAddInParametersCommand = async () => { return await UnitOfWork.Repository<Parameter>().GetAllAsync(); };;
			if (AddInParametersCommand == null) AddInParametersCommand = new DelegateCommand(AddInParametersCommand_Execute_Default);
			if (RemoveFromParametersCommand == null) RemoveFromParametersCommand = new DelegateCommand(RemoveFromParametersCommand_Execute_Default, RemoveFromParametersCommand_CanExecute_Default);

		}

			private async void AddInParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(await _getEntitiesForAddInParametersCommand(), Item.Parameters);
			}

			private void RemoveFromParametersCommand_Execute_Default()
			{
				Item.Parameters.Remove(SelectedParametersItem);
			}

			private bool RemoveFromParametersCommand_CanExecute_Default()
			{
				return SelectedParametersItem != null;
			}



    }


    public partial class GlobalPropertiesDetailsViewModel : BaseDetailsViewModel<GlobalPropertiesWrapper, GlobalProperties, AfterSaveGlobalPropertiesEvent>
    {
		private Func<Task<List<Company>>> _getEntitiesForSelectOurCompanyCommand;
		public ICommand SelectOurCompanyCommand { get; private set; }
		public ICommand ClearOurCompanyCommand { get; private set; }

		private Func<Task<List<PaymentConditionSet>>> _getEntitiesForSelectStandartPaymentsConditionSetCommand;
		public ICommand SelectStandartPaymentsConditionSetCommand { get; private set; }
		public ICommand ClearStandartPaymentsConditionSetCommand { get; private set; }

		private Func<Task<List<Parameter>>> _getEntitiesForSelectNewProductParameterCommand;
		public ICommand SelectNewProductParameterCommand { get; private set; }
		public ICommand ClearNewProductParameterCommand { get; private set; }

		private Func<Task<List<ParameterGroup>>> _getEntitiesForSelectNewProductParameterGroupCommand;
		public ICommand SelectNewProductParameterGroupCommand { get; private set; }
		public ICommand ClearNewProductParameterGroupCommand { get; private set; }

		private Func<Task<List<ParameterGroup>>> _getEntitiesForSelectVoltageGroupCommand;
		public ICommand SelectVoltageGroupCommand { get; private set; }
		public ICommand ClearVoltageGroupCommand { get; private set; }

		private Func<Task<List<Parameter>>> _getEntitiesForSelectServiceParameterCommand;
		public ICommand SelectServiceParameterCommand { get; private set; }
		public ICommand ClearServiceParameterCommand { get; private set; }

		private Func<Task<List<Parameter>>> _getEntitiesForSelectSupervisionParameterCommand;
		public ICommand SelectSupervisionParameterCommand { get; private set; }
		public ICommand ClearSupervisionParameterCommand { get; private set; }


        public GlobalPropertiesDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectOurCompanyCommand == null) _getEntitiesForSelectOurCompanyCommand = async () => { return await UnitOfWork.Repository<Company>().GetAllAsync(); };
			if (SelectOurCompanyCommand == null) SelectOurCompanyCommand = new DelegateCommand(SelectOurCompanyCommand_Execute_Default);
			if (ClearOurCompanyCommand == null) ClearOurCompanyCommand = new DelegateCommand(ClearOurCompanyCommand_Execute_Default);

			
			if (_getEntitiesForSelectStandartPaymentsConditionSetCommand == null) _getEntitiesForSelectStandartPaymentsConditionSetCommand = async () => { return await UnitOfWork.Repository<PaymentConditionSet>().GetAllAsync(); };
			if (SelectStandartPaymentsConditionSetCommand == null) SelectStandartPaymentsConditionSetCommand = new DelegateCommand(SelectStandartPaymentsConditionSetCommand_Execute_Default);
			if (ClearStandartPaymentsConditionSetCommand == null) ClearStandartPaymentsConditionSetCommand = new DelegateCommand(ClearStandartPaymentsConditionSetCommand_Execute_Default);

			
			if (_getEntitiesForSelectNewProductParameterCommand == null) _getEntitiesForSelectNewProductParameterCommand = async () => { return await UnitOfWork.Repository<Parameter>().GetAllAsync(); };
			if (SelectNewProductParameterCommand == null) SelectNewProductParameterCommand = new DelegateCommand(SelectNewProductParameterCommand_Execute_Default);
			if (ClearNewProductParameterCommand == null) ClearNewProductParameterCommand = new DelegateCommand(ClearNewProductParameterCommand_Execute_Default);

			
			if (_getEntitiesForSelectNewProductParameterGroupCommand == null) _getEntitiesForSelectNewProductParameterGroupCommand = async () => { return await UnitOfWork.Repository<ParameterGroup>().GetAllAsync(); };
			if (SelectNewProductParameterGroupCommand == null) SelectNewProductParameterGroupCommand = new DelegateCommand(SelectNewProductParameterGroupCommand_Execute_Default);
			if (ClearNewProductParameterGroupCommand == null) ClearNewProductParameterGroupCommand = new DelegateCommand(ClearNewProductParameterGroupCommand_Execute_Default);

			
			if (_getEntitiesForSelectVoltageGroupCommand == null) _getEntitiesForSelectVoltageGroupCommand = async () => { return await UnitOfWork.Repository<ParameterGroup>().GetAllAsync(); };
			if (SelectVoltageGroupCommand == null) SelectVoltageGroupCommand = new DelegateCommand(SelectVoltageGroupCommand_Execute_Default);
			if (ClearVoltageGroupCommand == null) ClearVoltageGroupCommand = new DelegateCommand(ClearVoltageGroupCommand_Execute_Default);

			
			if (_getEntitiesForSelectServiceParameterCommand == null) _getEntitiesForSelectServiceParameterCommand = async () => { return await UnitOfWork.Repository<Parameter>().GetAllAsync(); };
			if (SelectServiceParameterCommand == null) SelectServiceParameterCommand = new DelegateCommand(SelectServiceParameterCommand_Execute_Default);
			if (ClearServiceParameterCommand == null) ClearServiceParameterCommand = new DelegateCommand(ClearServiceParameterCommand_Execute_Default);

			
			if (_getEntitiesForSelectSupervisionParameterCommand == null) _getEntitiesForSelectSupervisionParameterCommand = async () => { return await UnitOfWork.Repository<Parameter>().GetAllAsync(); };
			if (SelectSupervisionParameterCommand == null) SelectSupervisionParameterCommand = new DelegateCommand(SelectSupervisionParameterCommand_Execute_Default);
			if (ClearSupervisionParameterCommand == null) ClearSupervisionParameterCommand = new DelegateCommand(ClearSupervisionParameterCommand_Execute_Default);

		}

		private async void SelectOurCompanyCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectOurCompanyCommand(), nameof(Item.OurCompany), Item.OurCompany?.Id);
		}

		private void ClearOurCompanyCommand_Execute_Default() 
		{
						Item.OurCompany = null;
		    
		}

		private async void SelectStandartPaymentsConditionSetCommand_Execute_Default() 
		{
            SelectAndSetWrapper<PaymentConditionSet, PaymentConditionSetWrapper>(await _getEntitiesForSelectStandartPaymentsConditionSetCommand(), nameof(Item.StandartPaymentsConditionSet), Item.StandartPaymentsConditionSet?.Id);
		}

		private void ClearStandartPaymentsConditionSetCommand_Execute_Default() 
		{
						Item.StandartPaymentsConditionSet = null;
		    
		}

		private async void SelectNewProductParameterCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Parameter, ParameterWrapper>(await _getEntitiesForSelectNewProductParameterCommand(), nameof(Item.NewProductParameter), Item.NewProductParameter?.Id);
		}

		private void ClearNewProductParameterCommand_Execute_Default() 
		{
						Item.NewProductParameter = null;
		    
		}

		private async void SelectNewProductParameterGroupCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(await _getEntitiesForSelectNewProductParameterGroupCommand(), nameof(Item.NewProductParameterGroup), Item.NewProductParameterGroup?.Id);
		}

		private void ClearNewProductParameterGroupCommand_Execute_Default() 
		{
						Item.NewProductParameterGroup = null;
		    
		}

		private async void SelectVoltageGroupCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(await _getEntitiesForSelectVoltageGroupCommand(), nameof(Item.VoltageGroup), Item.VoltageGroup?.Id);
		}

		private void ClearVoltageGroupCommand_Execute_Default() 
		{
						Item.VoltageGroup = null;
		    
		}

		private async void SelectServiceParameterCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Parameter, ParameterWrapper>(await _getEntitiesForSelectServiceParameterCommand(), nameof(Item.ServiceParameter), Item.ServiceParameter?.Id);
		}

		private void ClearServiceParameterCommand_Execute_Default() 
		{
						Item.ServiceParameter = null;
		    
		}

		private async void SelectSupervisionParameterCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Parameter, ParameterWrapper>(await _getEntitiesForSelectSupervisionParameterCommand(), nameof(Item.SupervisionParameter), Item.SupervisionParameter?.Id);
		}

		private void ClearSupervisionParameterCommand_Execute_Default() 
		{
						Item.SupervisionParameter = null;
		    
		}



    }


    public partial class AddressDetailsViewModel : BaseDetailsViewModel<AddressWrapper, Address, AfterSaveAddressEvent>
    {
		private Func<Task<List<Locality>>> _getEntitiesForSelectLocalityCommand;
		public ICommand SelectLocalityCommand { get; private set; }
		public ICommand ClearLocalityCommand { get; private set; }


        public AddressDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectLocalityCommand == null) _getEntitiesForSelectLocalityCommand = async () => { return await UnitOfWork.Repository<Locality>().GetAllAsync(); };
			if (SelectLocalityCommand == null) SelectLocalityCommand = new DelegateCommand(SelectLocalityCommand_Execute_Default);
			if (ClearLocalityCommand == null) ClearLocalityCommand = new DelegateCommand(ClearLocalityCommand_Execute_Default);

		}

		private async void SelectLocalityCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Locality, LocalityWrapper>(await _getEntitiesForSelectLocalityCommand(), nameof(Item.Locality), Item.Locality?.Id);
		}

		private void ClearLocalityCommand_Execute_Default() 
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
		private Func<Task<List<Country>>> _getEntitiesForSelectCountryCommand;
		public ICommand SelectCountryCommand { get; private set; }
		public ICommand ClearCountryCommand { get; private set; }


        public DistrictDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectCountryCommand == null) _getEntitiesForSelectCountryCommand = async () => { return await UnitOfWork.Repository<Country>().GetAllAsync(); };
			if (SelectCountryCommand == null) SelectCountryCommand = new DelegateCommand(SelectCountryCommand_Execute_Default);
			if (ClearCountryCommand == null) ClearCountryCommand = new DelegateCommand(ClearCountryCommand_Execute_Default);

		}

		private async void SelectCountryCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Country, CountryWrapper>(await _getEntitiesForSelectCountryCommand(), nameof(Item.Country), Item.Country?.Id);
		}

		private void ClearCountryCommand_Execute_Default() 
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
			
			if (_getEntitiesForSelectLocalityTypeCommand == null) _getEntitiesForSelectLocalityTypeCommand = async () => { return await UnitOfWork.Repository<LocalityType>().GetAllAsync(); };
			if (SelectLocalityTypeCommand == null) SelectLocalityTypeCommand = new DelegateCommand(SelectLocalityTypeCommand_Execute_Default);
			if (ClearLocalityTypeCommand == null) ClearLocalityTypeCommand = new DelegateCommand(ClearLocalityTypeCommand_Execute_Default);

			
			if (_getEntitiesForSelectRegionCommand == null) _getEntitiesForSelectRegionCommand = async () => { return await UnitOfWork.Repository<Region>().GetAllAsync(); };
			if (SelectRegionCommand == null) SelectRegionCommand = new DelegateCommand(SelectRegionCommand_Execute_Default);
			if (ClearRegionCommand == null) ClearRegionCommand = new DelegateCommand(ClearRegionCommand_Execute_Default);

		}

		private async void SelectLocalityTypeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<LocalityType, LocalityTypeWrapper>(await _getEntitiesForSelectLocalityTypeCommand(), nameof(Item.LocalityType), Item.LocalityType?.Id);
		}

		private void ClearLocalityTypeCommand_Execute_Default() 
		{
						Item.LocalityType = null;
		    
		}

		private async void SelectRegionCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Region, RegionWrapper>(await _getEntitiesForSelectRegionCommand(), nameof(Item.Region), Item.Region?.Id);
		}

		private void ClearRegionCommand_Execute_Default() 
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
		private Func<Task<List<District>>> _getEntitiesForSelectDistrictCommand;
		public ICommand SelectDistrictCommand { get; private set; }
		public ICommand ClearDistrictCommand { get; private set; }


        public RegionDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectDistrictCommand == null) _getEntitiesForSelectDistrictCommand = async () => { return await UnitOfWork.Repository<District>().GetAllAsync(); };
			if (SelectDistrictCommand == null) SelectDistrictCommand = new DelegateCommand(SelectDistrictCommand_Execute_Default);
			if (ClearDistrictCommand == null) ClearDistrictCommand = new DelegateCommand(ClearDistrictCommand_Execute_Default);

		}

		private async void SelectDistrictCommand_Execute_Default() 
		{
            SelectAndSetWrapper<District, DistrictWrapper>(await _getEntitiesForSelectDistrictCommand(), nameof(Item.District), Item.District?.Id);
		}

		private void ClearDistrictCommand_Execute_Default() 
		{
						Item.District = null;
		    
		}



    }


    public partial class SumDetailsViewModel : BaseDetailsViewModel<SumWrapper, Sum, AfterSaveSumEvent>
    {

        public SumDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class CurrencyExchangeRateDetailsViewModel : BaseDetailsViewModel<CurrencyExchangeRateWrapper, CurrencyExchangeRate, AfterSaveCurrencyExchangeRateEvent>
    {

        public CurrencyExchangeRateDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class NoteDetailsViewModel : BaseDetailsViewModel<NoteWrapper, Note, AfterSaveNoteEvent>
    {

        public NoteDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class OfferUnitDetailsViewModel : BaseDetailsViewModel<OfferUnitWrapper, OfferUnit, AfterSaveOfferUnitEvent>
    {
		private Func<Task<List<Offer>>> _getEntitiesForSelectOfferCommand;
		public ICommand SelectOfferCommand { get; private set; }
		public ICommand ClearOfferCommand { get; private set; }

		private Func<Task<List<Facility>>> _getEntitiesForSelectFacilityCommand;
		public ICommand SelectFacilityCommand { get; private set; }
		public ICommand ClearFacilityCommand { get; private set; }

		private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; private set; }
		public ICommand ClearProductCommand { get; private set; }

		private Func<Task<List<PaymentConditionSet>>> _getEntitiesForSelectPaymentConditionSetCommand;
		public ICommand SelectPaymentConditionSetCommand { get; private set; }
		public ICommand ClearPaymentConditionSetCommand { get; private set; }

		private Func<Task<List<ProductIncluded>>> _getEntitiesForAddInProductsIncludedCommand;
		public ICommand AddInProductsIncludedCommand { get; }
		public ICommand RemoveFromProductsIncludedCommand { get; }
		private ProductIncludedWrapper _selectedProductsIncludedItem;
		public ProductIncludedWrapper SelectedProductsIncludedItem 
		{ 
			get { return _selectedProductsIncludedItem; }
			set 
			{ 
				if (Equals(_selectedProductsIncludedItem, value)) return;
				_selectedProductsIncludedItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromProductsIncludedCommand).RaiseCanExecuteChanged();
			}
		}


        public OfferUnitDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectOfferCommand == null) _getEntitiesForSelectOfferCommand = async () => { return await UnitOfWork.Repository<Offer>().GetAllAsync(); };
			if (SelectOfferCommand == null) SelectOfferCommand = new DelegateCommand(SelectOfferCommand_Execute_Default);
			if (ClearOfferCommand == null) ClearOfferCommand = new DelegateCommand(ClearOfferCommand_Execute_Default);

			
			if (_getEntitiesForSelectFacilityCommand == null) _getEntitiesForSelectFacilityCommand = async () => { return await UnitOfWork.Repository<Facility>().GetAllAsync(); };
			if (SelectFacilityCommand == null) SelectFacilityCommand = new DelegateCommand(SelectFacilityCommand_Execute_Default);
			if (ClearFacilityCommand == null) ClearFacilityCommand = new DelegateCommand(ClearFacilityCommand_Execute_Default);

			
			if (_getEntitiesForSelectProductCommand == null) _getEntitiesForSelectProductCommand = async () => { return await UnitOfWork.Repository<Product>().GetAllAsync(); };
			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute_Default);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute_Default);

			
			if (_getEntitiesForSelectPaymentConditionSetCommand == null) _getEntitiesForSelectPaymentConditionSetCommand = async () => { return await UnitOfWork.Repository<PaymentConditionSet>().GetAllAsync(); };
			if (SelectPaymentConditionSetCommand == null) SelectPaymentConditionSetCommand = new DelegateCommand(SelectPaymentConditionSetCommand_Execute_Default);
			if (ClearPaymentConditionSetCommand == null) ClearPaymentConditionSetCommand = new DelegateCommand(ClearPaymentConditionSetCommand_Execute_Default);

			
			if (_getEntitiesForAddInProductsIncludedCommand == null) _getEntitiesForAddInProductsIncludedCommand = async () => { return await UnitOfWork.Repository<ProductIncluded>().GetAllAsync(); };;
			if (AddInProductsIncludedCommand == null) AddInProductsIncludedCommand = new DelegateCommand(AddInProductsIncludedCommand_Execute_Default);
			if (RemoveFromProductsIncludedCommand == null) RemoveFromProductsIncludedCommand = new DelegateCommand(RemoveFromProductsIncludedCommand_Execute_Default, RemoveFromProductsIncludedCommand_CanExecute_Default);

		}

		private async void SelectOfferCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Offer, OfferWrapper>(await _getEntitiesForSelectOfferCommand(), nameof(Item.Offer), Item.Offer?.Id);
		}

		private void ClearOfferCommand_Execute_Default() 
		{
						Item.Offer = null;
		    
		}

		private async void SelectFacilityCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Facility, FacilityWrapper>(await _getEntitiesForSelectFacilityCommand(), nameof(Item.Facility), Item.Facility?.Id);
		}

		private void ClearFacilityCommand_Execute_Default() 
		{
						Item.Facility = null;
		    
		}

		private async void SelectProductCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(await _getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute_Default() 
		{
						Item.Product = null;
		    
		}

		private async void SelectPaymentConditionSetCommand_Execute_Default() 
		{
            SelectAndSetWrapper<PaymentConditionSet, PaymentConditionSetWrapper>(await _getEntitiesForSelectPaymentConditionSetCommand(), nameof(Item.PaymentConditionSet), Item.PaymentConditionSet?.Id);
		}

		private void ClearPaymentConditionSetCommand_Execute_Default() 
		{
						Item.PaymentConditionSet = null;
		    
		}

			private async void AddInProductsIncludedCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ProductIncluded, ProductIncludedWrapper>(await _getEntitiesForAddInProductsIncludedCommand(), Item.ProductsIncluded);
			}

			private void RemoveFromProductsIncludedCommand_Execute_Default()
			{
				Item.ProductsIncluded.Remove(SelectedProductsIncludedItem);
			}

			private bool RemoveFromProductsIncludedCommand_CanExecute_Default()
			{
				return SelectedProductsIncludedItem != null;
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
			
			if (_getEntitiesForAddInPaymentConditionsCommand == null) _getEntitiesForAddInPaymentConditionsCommand = async () => { return await UnitOfWork.Repository<PaymentCondition>().GetAllAsync(); };;
			if (AddInPaymentConditionsCommand == null) AddInPaymentConditionsCommand = new DelegateCommand(AddInPaymentConditionsCommand_Execute_Default);
			if (RemoveFromPaymentConditionsCommand == null) RemoveFromPaymentConditionsCommand = new DelegateCommand(RemoveFromPaymentConditionsCommand_Execute_Default, RemoveFromPaymentConditionsCommand_CanExecute_Default);

		}

			private async void AddInPaymentConditionsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentCondition, PaymentConditionWrapper>(await _getEntitiesForAddInPaymentConditionsCommand(), Item.PaymentConditions);
			}

			private void RemoveFromPaymentConditionsCommand_Execute_Default()
			{
				Item.PaymentConditions.Remove(SelectedPaymentConditionsItem);
			}

			private bool RemoveFromPaymentConditionsCommand_CanExecute_Default()
			{
				return SelectedPaymentConditionsItem != null;
			}



    }


    public partial class ProductBlockDetailsViewModel : BaseDetailsViewModel<ProductBlockWrapper, ProductBlock, AfterSaveProductBlockEvent>
    {
		private Func<Task<List<ProductType>>> _getEntitiesForSelectProductTypeCommand;
		public ICommand SelectProductTypeCommand { get; private set; }
		public ICommand ClearProductTypeCommand { get; private set; }

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

		private Func<Task<List<SumOnDate>>> _getEntitiesForAddInFixedCostsCommand;
		public ICommand AddInFixedCostsCommand { get; }
		public ICommand RemoveFromFixedCostsCommand { get; }
		private SumOnDateWrapper _selectedFixedCostsItem;
		public SumOnDateWrapper SelectedFixedCostsItem 
		{ 
			get { return _selectedFixedCostsItem; }
			set 
			{ 
				if (Equals(_selectedFixedCostsItem, value)) return;
				_selectedFixedCostsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromFixedCostsCommand).RaiseCanExecuteChanged();
			}
		}


        public ProductBlockDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectProductTypeCommand == null) _getEntitiesForSelectProductTypeCommand = async () => { return await UnitOfWork.Repository<ProductType>().GetAllAsync(); };
			if (SelectProductTypeCommand == null) SelectProductTypeCommand = new DelegateCommand(SelectProductTypeCommand_Execute_Default);
			if (ClearProductTypeCommand == null) ClearProductTypeCommand = new DelegateCommand(ClearProductTypeCommand_Execute_Default);

			
			if (_getEntitiesForAddInParametersCommand == null) _getEntitiesForAddInParametersCommand = async () => { return await UnitOfWork.Repository<Parameter>().GetAllAsync(); };;
			if (AddInParametersCommand == null) AddInParametersCommand = new DelegateCommand(AddInParametersCommand_Execute_Default);
			if (RemoveFromParametersCommand == null) RemoveFromParametersCommand = new DelegateCommand(RemoveFromParametersCommand_Execute_Default, RemoveFromParametersCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInPricesCommand == null) _getEntitiesForAddInPricesCommand = async () => { return await UnitOfWork.Repository<SumOnDate>().GetAllAsync(); };;
			if (AddInPricesCommand == null) AddInPricesCommand = new DelegateCommand(AddInPricesCommand_Execute_Default);
			if (RemoveFromPricesCommand == null) RemoveFromPricesCommand = new DelegateCommand(RemoveFromPricesCommand_Execute_Default, RemoveFromPricesCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInFixedCostsCommand == null) _getEntitiesForAddInFixedCostsCommand = async () => { return await UnitOfWork.Repository<SumOnDate>().GetAllAsync(); };;
			if (AddInFixedCostsCommand == null) AddInFixedCostsCommand = new DelegateCommand(AddInFixedCostsCommand_Execute_Default);
			if (RemoveFromFixedCostsCommand == null) RemoveFromFixedCostsCommand = new DelegateCommand(RemoveFromFixedCostsCommand_Execute_Default, RemoveFromFixedCostsCommand_CanExecute_Default);

		}

		private async void SelectProductTypeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ProductType, ProductTypeWrapper>(await _getEntitiesForSelectProductTypeCommand(), nameof(Item.ProductType), Item.ProductType?.Id);
		}

		private void ClearProductTypeCommand_Execute_Default() 
		{
		
		    
		}

			private async void AddInParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(await _getEntitiesForAddInParametersCommand(), Item.Parameters);
			}

			private void RemoveFromParametersCommand_Execute_Default()
			{
				Item.Parameters.Remove(SelectedParametersItem);
			}

			private bool RemoveFromParametersCommand_CanExecute_Default()
			{
				return SelectedParametersItem != null;
			}

			private async void AddInPricesCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<SumOnDate, SumOnDateWrapper>(await _getEntitiesForAddInPricesCommand(), Item.Prices);
			}

			private void RemoveFromPricesCommand_Execute_Default()
			{
				Item.Prices.Remove(SelectedPricesItem);
			}

			private bool RemoveFromPricesCommand_CanExecute_Default()
			{
				return SelectedPricesItem != null;
			}

			private async void AddInFixedCostsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<SumOnDate, SumOnDateWrapper>(await _getEntitiesForAddInFixedCostsCommand(), Item.FixedCosts);
			}

			private void RemoveFromFixedCostsCommand_Execute_Default()
			{
				Item.FixedCosts.Remove(SelectedFixedCostsItem);
			}

			private bool RemoveFromFixedCostsCommand_CanExecute_Default()
			{
				return SelectedFixedCostsItem != null;
			}



    }


    public partial class ProductDependentDetailsViewModel : BaseDetailsViewModel<ProductDependentWrapper, ProductDependent, AfterSaveProductDependentEvent>
    {
		private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; private set; }
		public ICommand ClearProductCommand { get; private set; }


        public ProductDependentDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectProductCommand == null) _getEntitiesForSelectProductCommand = async () => { return await UnitOfWork.Repository<Product>().GetAllAsync(); };
			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute_Default);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute_Default);

		}

		private async void SelectProductCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(await _getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute_Default() 
		{
						Item.Product = null;
		    
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
			
			if (_getEntitiesForSelectFormCommand == null) _getEntitiesForSelectFormCommand = async () => { return await UnitOfWork.Repository<CompanyForm>().GetAllAsync(); };
			if (SelectFormCommand == null) SelectFormCommand = new DelegateCommand(SelectFormCommand_Execute_Default);
			if (ClearFormCommand == null) ClearFormCommand = new DelegateCommand(ClearFormCommand_Execute_Default);

			
			if (_getEntitiesForSelectParentCompanyCommand == null) _getEntitiesForSelectParentCompanyCommand = async () => { return await UnitOfWork.Repository<Company>().GetAllAsync(); };
			if (SelectParentCompanyCommand == null) SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_Execute_Default);
			if (ClearParentCompanyCommand == null) ClearParentCompanyCommand = new DelegateCommand(ClearParentCompanyCommand_Execute_Default);

			
			if (_getEntitiesForSelectAddressLegalCommand == null) _getEntitiesForSelectAddressLegalCommand = async () => { return await UnitOfWork.Repository<Address>().GetAllAsync(); };
			if (SelectAddressLegalCommand == null) SelectAddressLegalCommand = new DelegateCommand(SelectAddressLegalCommand_Execute_Default);
			if (ClearAddressLegalCommand == null) ClearAddressLegalCommand = new DelegateCommand(ClearAddressLegalCommand_Execute_Default);

			
			if (_getEntitiesForSelectAddressPostCommand == null) _getEntitiesForSelectAddressPostCommand = async () => { return await UnitOfWork.Repository<Address>().GetAllAsync(); };
			if (SelectAddressPostCommand == null) SelectAddressPostCommand = new DelegateCommand(SelectAddressPostCommand_Execute_Default);
			if (ClearAddressPostCommand == null) ClearAddressPostCommand = new DelegateCommand(ClearAddressPostCommand_Execute_Default);

			
			if (_getEntitiesForAddInBankDetailsListCommand == null) _getEntitiesForAddInBankDetailsListCommand = async () => { return await UnitOfWork.Repository<BankDetails>().GetAllAsync(); };;
			if (AddInBankDetailsListCommand == null) AddInBankDetailsListCommand = new DelegateCommand(AddInBankDetailsListCommand_Execute_Default);
			if (RemoveFromBankDetailsListCommand == null) RemoveFromBankDetailsListCommand = new DelegateCommand(RemoveFromBankDetailsListCommand_Execute_Default, RemoveFromBankDetailsListCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInActivityFildsCommand == null) _getEntitiesForAddInActivityFildsCommand = async () => { return await UnitOfWork.Repository<ActivityField>().GetAllAsync(); };;
			if (AddInActivityFildsCommand == null) AddInActivityFildsCommand = new DelegateCommand(AddInActivityFildsCommand_Execute_Default);
			if (RemoveFromActivityFildsCommand == null) RemoveFromActivityFildsCommand = new DelegateCommand(RemoveFromActivityFildsCommand_Execute_Default, RemoveFromActivityFildsCommand_CanExecute_Default);

		}

		private async void SelectFormCommand_Execute_Default() 
		{
            SelectAndSetWrapper<CompanyForm, CompanyFormWrapper>(await _getEntitiesForSelectFormCommand(), nameof(Item.Form), Item.Form?.Id);
		}

		private void ClearFormCommand_Execute_Default() 
		{
						Item.Form = null;
		    
		}

		private async void SelectParentCompanyCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectParentCompanyCommand(), nameof(Item.ParentCompany), Item.ParentCompany?.Id);
		}

		private void ClearParentCompanyCommand_Execute_Default() 
		{
						Item.ParentCompany = null;
		    
		}

		private async void SelectAddressLegalCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Address, AddressWrapper>(await _getEntitiesForSelectAddressLegalCommand(), nameof(Item.AddressLegal), Item.AddressLegal?.Id);
		}

		private void ClearAddressLegalCommand_Execute_Default() 
		{
						Item.AddressLegal = null;
		    
		}

		private async void SelectAddressPostCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Address, AddressWrapper>(await _getEntitiesForSelectAddressPostCommand(), nameof(Item.AddressPost), Item.AddressPost?.Id);
		}

		private void ClearAddressPostCommand_Execute_Default() 
		{
						Item.AddressPost = null;
		    
		}

			private async void AddInBankDetailsListCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<BankDetails, BankDetailsWrapper>(await _getEntitiesForAddInBankDetailsListCommand(), Item.BankDetailsList);
			}

			private void RemoveFromBankDetailsListCommand_Execute_Default()
			{
				Item.BankDetailsList.Remove(SelectedBankDetailsListItem);
			}

			private bool RemoveFromBankDetailsListCommand_CanExecute_Default()
			{
				return SelectedBankDetailsListItem != null;
			}

			private async void AddInActivityFildsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ActivityField, ActivityFieldWrapper>(await _getEntitiesForAddInActivityFildsCommand(), Item.ActivityFilds);
			}

			private void RemoveFromActivityFildsCommand_Execute_Default()
			{
				Item.ActivityFilds.Remove(SelectedActivityFildsItem);
			}

			private bool RemoveFromActivityFildsCommand_CanExecute_Default()
			{
				return SelectedActivityFildsItem != null;
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
		private Func<Task<List<Company>>> _getEntitiesForSelectContragentCommand;
		public ICommand SelectContragentCommand { get; private set; }
		public ICommand ClearContragentCommand { get; private set; }


        public ContractDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectContragentCommand == null) _getEntitiesForSelectContragentCommand = async () => { return await UnitOfWork.Repository<Company>().GetAllAsync(); };
			if (SelectContragentCommand == null) SelectContragentCommand = new DelegateCommand(SelectContragentCommand_Execute_Default);
			if (ClearContragentCommand == null) ClearContragentCommand = new DelegateCommand(ClearContragentCommand_Execute_Default);

		}

		private async void SelectContragentCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectContragentCommand(), nameof(Item.Contragent), Item.Contragent?.Id);
		}

		private void ClearContragentCommand_Execute_Default() 
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
			
			if (_getEntitiesForSelectParameterGroupCommand == null) _getEntitiesForSelectParameterGroupCommand = async () => { return await UnitOfWork.Repository<ParameterGroup>().GetAllAsync(); };
			if (SelectParameterGroupCommand == null) SelectParameterGroupCommand = new DelegateCommand(SelectParameterGroupCommand_Execute_Default);
			if (ClearParameterGroupCommand == null) ClearParameterGroupCommand = new DelegateCommand(ClearParameterGroupCommand_Execute_Default);

			
			if (_getEntitiesForAddInParameterRelationsCommand == null) _getEntitiesForAddInParameterRelationsCommand = async () => { return await UnitOfWork.Repository<ParameterRelation>().GetAllAsync(); };;
			if (AddInParameterRelationsCommand == null) AddInParameterRelationsCommand = new DelegateCommand(AddInParameterRelationsCommand_Execute_Default);
			if (RemoveFromParameterRelationsCommand == null) RemoveFromParameterRelationsCommand = new DelegateCommand(RemoveFromParameterRelationsCommand_Execute_Default, RemoveFromParameterRelationsCommand_CanExecute_Default);

		}

		private async void SelectParameterGroupCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(await _getEntitiesForSelectParameterGroupCommand(), nameof(Item.ParameterGroup), Item.ParameterGroup?.Id);
		}

		private void ClearParameterGroupCommand_Execute_Default() 
		{
						Item.ParameterGroup = null;
		    
		}

			private async void AddInParameterRelationsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ParameterRelation, ParameterRelationWrapper>(await _getEntitiesForAddInParameterRelationsCommand(), Item.ParameterRelations);
			}

			private void RemoveFromParameterRelationsCommand_Execute_Default()
			{
				Item.ParameterRelations.Remove(SelectedParameterRelationsItem);
			}

			private bool RemoveFromParameterRelationsCommand_CanExecute_Default()
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
			
			if (_getEntitiesForSelectMeasureCommand == null) _getEntitiesForSelectMeasureCommand = async () => { return await UnitOfWork.Repository<Measure>().GetAllAsync(); };
			if (SelectMeasureCommand == null) SelectMeasureCommand = new DelegateCommand(SelectMeasureCommand_Execute_Default);
			if (ClearMeasureCommand == null) ClearMeasureCommand = new DelegateCommand(ClearMeasureCommand_Execute_Default);

		}

		private async void SelectMeasureCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Measure, MeasureWrapper>(await _getEntitiesForSelectMeasureCommand(), nameof(Item.Measure), Item.Measure?.Id);
		}

		private void ClearMeasureCommand_Execute_Default() 
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
			
			if (_getEntitiesForAddInParentProductParametersCommand == null) _getEntitiesForAddInParentProductParametersCommand = async () => { return await UnitOfWork.Repository<Parameter>().GetAllAsync(); };;
			if (AddInParentProductParametersCommand == null) AddInParentProductParametersCommand = new DelegateCommand(AddInParentProductParametersCommand_Execute_Default);
			if (RemoveFromParentProductParametersCommand == null) RemoveFromParentProductParametersCommand = new DelegateCommand(RemoveFromParentProductParametersCommand_Execute_Default, RemoveFromParentProductParametersCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInChildProductParametersCommand == null) _getEntitiesForAddInChildProductParametersCommand = async () => { return await UnitOfWork.Repository<Parameter>().GetAllAsync(); };;
			if (AddInChildProductParametersCommand == null) AddInChildProductParametersCommand = new DelegateCommand(AddInChildProductParametersCommand_Execute_Default);
			if (RemoveFromChildProductParametersCommand == null) RemoveFromChildProductParametersCommand = new DelegateCommand(RemoveFromChildProductParametersCommand_Execute_Default, RemoveFromChildProductParametersCommand_CanExecute_Default);

		}

			private async void AddInParentProductParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(await _getEntitiesForAddInParentProductParametersCommand(), Item.ParentProductParameters);
			}

			private void RemoveFromParentProductParametersCommand_Execute_Default()
			{
				Item.ParentProductParameters.Remove(SelectedParentProductParametersItem);
			}

			private bool RemoveFromParentProductParametersCommand_CanExecute_Default()
			{
				return SelectedParentProductParametersItem != null;
			}

			private async void AddInChildProductParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(await _getEntitiesForAddInChildProductParametersCommand(), Item.ChildProductParameters);
			}

			private void RemoveFromChildProductParametersCommand_Execute_Default()
			{
				Item.ChildProductParameters.Remove(SelectedChildProductParametersItem);
			}

			private bool RemoveFromChildProductParametersCommand_CanExecute_Default()
			{
				return SelectedChildProductParametersItem != null;
			}



    }


    public partial class PersonDetailsViewModel : BaseDetailsViewModel<PersonWrapper, Person, AfterSavePersonEvent>
    {

        public PersonDetailsViewModel(IUnityContainer container) : base(container) 
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
			
			if (_getEntitiesForAddInRequiredParametersCommand == null) _getEntitiesForAddInRequiredParametersCommand = async () => { return await UnitOfWork.Repository<Parameter>().GetAllAsync(); };;
			if (AddInRequiredParametersCommand == null) AddInRequiredParametersCommand = new DelegateCommand(AddInRequiredParametersCommand_Execute_Default);
			if (RemoveFromRequiredParametersCommand == null) RemoveFromRequiredParametersCommand = new DelegateCommand(RemoveFromRequiredParametersCommand_Execute_Default, RemoveFromRequiredParametersCommand_CanExecute_Default);

		}

			private async void AddInRequiredParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(await _getEntitiesForAddInRequiredParametersCommand(), Item.RequiredParameters);
			}

			private void RemoveFromRequiredParametersCommand_Execute_Default()
			{
				Item.RequiredParameters.Remove(SelectedRequiredParametersItem);
			}

			private bool RemoveFromRequiredParametersCommand_CanExecute_Default()
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

		private Func<Task<List<ProductIncluded>>> _getEntitiesForAddInProductsIncludedCommand;
		public ICommand AddInProductsIncludedCommand { get; }
		public ICommand RemoveFromProductsIncludedCommand { get; }
		private ProductIncludedWrapper _selectedProductsIncludedItem;
		public ProductIncludedWrapper SelectedProductsIncludedItem 
		{ 
			get { return _selectedProductsIncludedItem; }
			set 
			{ 
				if (Equals(_selectedProductsIncludedItem, value)) return;
				_selectedProductsIncludedItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromProductsIncludedCommand).RaiseCanExecuteChanged();
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

		private Func<Task<List<PaymentPlanned>>> _getEntitiesForAddInPaymentsPlannedCommand;
		public ICommand AddInPaymentsPlannedCommand { get; }
		public ICommand RemoveFromPaymentsPlannedCommand { get; }
		private PaymentPlannedWrapper _selectedPaymentsPlannedItem;
		public PaymentPlannedWrapper SelectedPaymentsPlannedItem 
		{ 
			get { return _selectedPaymentsPlannedItem; }
			set 
			{ 
				if (Equals(_selectedPaymentsPlannedItem, value)) return;
				_selectedPaymentsPlannedItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromPaymentsPlannedCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<PaymentPlanned>>> _getEntitiesForAddInPaymentsPlannedActualCommand;
		public ICommand AddInPaymentsPlannedActualCommand { get; }
		public ICommand RemoveFromPaymentsPlannedActualCommand { get; }
		private PaymentPlannedWrapper _selectedPaymentsPlannedActualItem;
		public PaymentPlannedWrapper SelectedPaymentsPlannedActualItem 
		{ 
			get { return _selectedPaymentsPlannedActualItem; }
			set 
			{ 
				if (Equals(_selectedPaymentsPlannedActualItem, value)) return;
				_selectedPaymentsPlannedActualItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromPaymentsPlannedActualCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<PaymentPlanned>>> _getEntitiesForAddInPaymentsPlannedGeneratedCommand;
		public ICommand AddInPaymentsPlannedGeneratedCommand { get; }
		public ICommand RemoveFromPaymentsPlannedGeneratedCommand { get; }
		private PaymentPlannedWrapper _selectedPaymentsPlannedGeneratedItem;
		public PaymentPlannedWrapper SelectedPaymentsPlannedGeneratedItem 
		{ 
			get { return _selectedPaymentsPlannedGeneratedItem; }
			set 
			{ 
				if (Equals(_selectedPaymentsPlannedGeneratedItem, value)) return;
				_selectedPaymentsPlannedGeneratedItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromPaymentsPlannedGeneratedCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<Task<List<PaymentPlanned>>> _getEntitiesForAddInPaymentsPlannedCalculatedCommand;
		public ICommand AddInPaymentsPlannedCalculatedCommand { get; }
		public ICommand RemoveFromPaymentsPlannedCalculatedCommand { get; }
		private PaymentPlannedWrapper _selectedPaymentsPlannedCalculatedItem;
		public PaymentPlannedWrapper SelectedPaymentsPlannedCalculatedItem 
		{ 
			get { return _selectedPaymentsPlannedCalculatedItem; }
			set 
			{ 
				if (Equals(_selectedPaymentsPlannedCalculatedItem, value)) return;
				_selectedPaymentsPlannedCalculatedItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromPaymentsPlannedCalculatedCommand).RaiseCanExecuteChanged();
			}
		}


        public SalesUnitDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectProductCommand == null) _getEntitiesForSelectProductCommand = async () => { return await UnitOfWork.Repository<Product>().GetAllAsync(); };
			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute_Default);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute_Default);

			
			if (_getEntitiesForSelectFacilityCommand == null) _getEntitiesForSelectFacilityCommand = async () => { return await UnitOfWork.Repository<Facility>().GetAllAsync(); };
			if (SelectFacilityCommand == null) SelectFacilityCommand = new DelegateCommand(SelectFacilityCommand_Execute_Default);
			if (ClearFacilityCommand == null) ClearFacilityCommand = new DelegateCommand(ClearFacilityCommand_Execute_Default);

			
			if (_getEntitiesForSelectPaymentConditionSetCommand == null) _getEntitiesForSelectPaymentConditionSetCommand = async () => { return await UnitOfWork.Repository<PaymentConditionSet>().GetAllAsync(); };
			if (SelectPaymentConditionSetCommand == null) SelectPaymentConditionSetCommand = new DelegateCommand(SelectPaymentConditionSetCommand_Execute_Default);
			if (ClearPaymentConditionSetCommand == null) ClearPaymentConditionSetCommand = new DelegateCommand(ClearPaymentConditionSetCommand_Execute_Default);

			
			if (_getEntitiesForSelectProjectCommand == null) _getEntitiesForSelectProjectCommand = async () => { return await UnitOfWork.Repository<Project>().GetAllAsync(); };
			if (SelectProjectCommand == null) SelectProjectCommand = new DelegateCommand(SelectProjectCommand_Execute_Default);
			if (ClearProjectCommand == null) ClearProjectCommand = new DelegateCommand(ClearProjectCommand_Execute_Default);

			
			if (_getEntitiesForSelectProducerCommand == null) _getEntitiesForSelectProducerCommand = async () => { return await UnitOfWork.Repository<Company>().GetAllAsync(); };
			if (SelectProducerCommand == null) SelectProducerCommand = new DelegateCommand(SelectProducerCommand_Execute_Default);
			if (ClearProducerCommand == null) ClearProducerCommand = new DelegateCommand(ClearProducerCommand_Execute_Default);

			
			if (_getEntitiesForSelectOrderCommand == null) _getEntitiesForSelectOrderCommand = async () => { return await UnitOfWork.Repository<Order>().GetAllAsync(); };
			if (SelectOrderCommand == null) SelectOrderCommand = new DelegateCommand(SelectOrderCommand_Execute_Default);
			if (ClearOrderCommand == null) ClearOrderCommand = new DelegateCommand(ClearOrderCommand_Execute_Default);

			
			if (_getEntitiesForSelectSpecificationCommand == null) _getEntitiesForSelectSpecificationCommand = async () => { return await UnitOfWork.Repository<Specification>().GetAllAsync(); };
			if (SelectSpecificationCommand == null) SelectSpecificationCommand = new DelegateCommand(SelectSpecificationCommand_Execute_Default);
			if (ClearSpecificationCommand == null) ClearSpecificationCommand = new DelegateCommand(ClearSpecificationCommand_Execute_Default);

			
			if (_getEntitiesForSelectAddressCommand == null) _getEntitiesForSelectAddressCommand = async () => { return await UnitOfWork.Repository<Address>().GetAllAsync(); };
			if (SelectAddressCommand == null) SelectAddressCommand = new DelegateCommand(SelectAddressCommand_Execute_Default);
			if (ClearAddressCommand == null) ClearAddressCommand = new DelegateCommand(ClearAddressCommand_Execute_Default);

			
			if (_getEntitiesForAddInProductsIncludedCommand == null) _getEntitiesForAddInProductsIncludedCommand = async () => { return await UnitOfWork.Repository<ProductIncluded>().GetAllAsync(); };;
			if (AddInProductsIncludedCommand == null) AddInProductsIncludedCommand = new DelegateCommand(AddInProductsIncludedCommand_Execute_Default);
			if (RemoveFromProductsIncludedCommand == null) RemoveFromProductsIncludedCommand = new DelegateCommand(RemoveFromProductsIncludedCommand_Execute_Default, RemoveFromProductsIncludedCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInPaymentsActualCommand == null) _getEntitiesForAddInPaymentsActualCommand = async () => { return await UnitOfWork.Repository<PaymentActual>().GetAllAsync(); };;
			if (AddInPaymentsActualCommand == null) AddInPaymentsActualCommand = new DelegateCommand(AddInPaymentsActualCommand_Execute_Default);
			if (RemoveFromPaymentsActualCommand == null) RemoveFromPaymentsActualCommand = new DelegateCommand(RemoveFromPaymentsActualCommand_Execute_Default, RemoveFromPaymentsActualCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInPaymentsPlannedCommand == null) _getEntitiesForAddInPaymentsPlannedCommand = async () => { return await UnitOfWork.Repository<PaymentPlanned>().GetAllAsync(); };;
			if (AddInPaymentsPlannedCommand == null) AddInPaymentsPlannedCommand = new DelegateCommand(AddInPaymentsPlannedCommand_Execute_Default);
			if (RemoveFromPaymentsPlannedCommand == null) RemoveFromPaymentsPlannedCommand = new DelegateCommand(RemoveFromPaymentsPlannedCommand_Execute_Default, RemoveFromPaymentsPlannedCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInPaymentsPlannedActualCommand == null) _getEntitiesForAddInPaymentsPlannedActualCommand = async () => { return await UnitOfWork.Repository<PaymentPlanned>().GetAllAsync(); };;
			if (AddInPaymentsPlannedActualCommand == null) AddInPaymentsPlannedActualCommand = new DelegateCommand(AddInPaymentsPlannedActualCommand_Execute_Default);
			if (RemoveFromPaymentsPlannedActualCommand == null) RemoveFromPaymentsPlannedActualCommand = new DelegateCommand(RemoveFromPaymentsPlannedActualCommand_Execute_Default, RemoveFromPaymentsPlannedActualCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInPaymentsPlannedGeneratedCommand == null) _getEntitiesForAddInPaymentsPlannedGeneratedCommand = async () => { return await UnitOfWork.Repository<PaymentPlanned>().GetAllAsync(); };;
			if (AddInPaymentsPlannedGeneratedCommand == null) AddInPaymentsPlannedGeneratedCommand = new DelegateCommand(AddInPaymentsPlannedGeneratedCommand_Execute_Default);
			if (RemoveFromPaymentsPlannedGeneratedCommand == null) RemoveFromPaymentsPlannedGeneratedCommand = new DelegateCommand(RemoveFromPaymentsPlannedGeneratedCommand_Execute_Default, RemoveFromPaymentsPlannedGeneratedCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInPaymentsPlannedCalculatedCommand == null) _getEntitiesForAddInPaymentsPlannedCalculatedCommand = async () => { return await UnitOfWork.Repository<PaymentPlanned>().GetAllAsync(); };;
			if (AddInPaymentsPlannedCalculatedCommand == null) AddInPaymentsPlannedCalculatedCommand = new DelegateCommand(AddInPaymentsPlannedCalculatedCommand_Execute_Default);
			if (RemoveFromPaymentsPlannedCalculatedCommand == null) RemoveFromPaymentsPlannedCalculatedCommand = new DelegateCommand(RemoveFromPaymentsPlannedCalculatedCommand_Execute_Default, RemoveFromPaymentsPlannedCalculatedCommand_CanExecute_Default);

		}

		private async void SelectProductCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(await _getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute_Default() 
		{
						Item.Product = null;
		    
		}

		private async void SelectFacilityCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Facility, FacilityWrapper>(await _getEntitiesForSelectFacilityCommand(), nameof(Item.Facility), Item.Facility?.Id);
		}

		private void ClearFacilityCommand_Execute_Default() 
		{
						Item.Facility = null;
		    
		}

		private async void SelectPaymentConditionSetCommand_Execute_Default() 
		{
            SelectAndSetWrapper<PaymentConditionSet, PaymentConditionSetWrapper>(await _getEntitiesForSelectPaymentConditionSetCommand(), nameof(Item.PaymentConditionSet), Item.PaymentConditionSet?.Id);
		}

		private void ClearPaymentConditionSetCommand_Execute_Default() 
		{
						Item.PaymentConditionSet = null;
		    
		}

		private async void SelectProjectCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Project, ProjectWrapper>(await _getEntitiesForSelectProjectCommand(), nameof(Item.Project), Item.Project?.Id);
		}

		private void ClearProjectCommand_Execute_Default() 
		{
						Item.Project = null;
		    
		}

		private async void SelectProducerCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectProducerCommand(), nameof(Item.Producer), Item.Producer?.Id);
		}

		private void ClearProducerCommand_Execute_Default() 
		{
						Item.Producer = null;
		    
		}

		private async void SelectOrderCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Order, OrderWrapper>(await _getEntitiesForSelectOrderCommand(), nameof(Item.Order), Item.Order?.Id);
		}

		private void ClearOrderCommand_Execute_Default() 
		{
						Item.Order = null;
		    
		}

		private async void SelectSpecificationCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Specification, SpecificationWrapper>(await _getEntitiesForSelectSpecificationCommand(), nameof(Item.Specification), Item.Specification?.Id);
		}

		private void ClearSpecificationCommand_Execute_Default() 
		{
						Item.Specification = null;
		    
		}

		private async void SelectAddressCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Address, AddressWrapper>(await _getEntitiesForSelectAddressCommand(), nameof(Item.Address), Item.Address?.Id);
		}

		private void ClearAddressCommand_Execute_Default() 
		{
						Item.Address = null;
		    
		}

			private async void AddInProductsIncludedCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ProductIncluded, ProductIncludedWrapper>(await _getEntitiesForAddInProductsIncludedCommand(), Item.ProductsIncluded);
			}

			private void RemoveFromProductsIncludedCommand_Execute_Default()
			{
				Item.ProductsIncluded.Remove(SelectedProductsIncludedItem);
			}

			private bool RemoveFromProductsIncludedCommand_CanExecute_Default()
			{
				return SelectedProductsIncludedItem != null;
			}

			private async void AddInPaymentsActualCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentActual, PaymentActualWrapper>(await _getEntitiesForAddInPaymentsActualCommand(), Item.PaymentsActual);
			}

			private void RemoveFromPaymentsActualCommand_Execute_Default()
			{
				Item.PaymentsActual.Remove(SelectedPaymentsActualItem);
			}

			private bool RemoveFromPaymentsActualCommand_CanExecute_Default()
			{
				return SelectedPaymentsActualItem != null;
			}

			private async void AddInPaymentsPlannedCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentPlanned, PaymentPlannedWrapper>(await _getEntitiesForAddInPaymentsPlannedCommand(), Item.PaymentsPlanned);
			}

			private void RemoveFromPaymentsPlannedCommand_Execute_Default()
			{
				Item.PaymentsPlanned.Remove(SelectedPaymentsPlannedItem);
			}

			private bool RemoveFromPaymentsPlannedCommand_CanExecute_Default()
			{
				return SelectedPaymentsPlannedItem != null;
			}

			private async void AddInPaymentsPlannedActualCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentPlanned, PaymentPlannedWrapper>(await _getEntitiesForAddInPaymentsPlannedActualCommand(), Item.PaymentsPlannedActual);
			}

			private void RemoveFromPaymentsPlannedActualCommand_Execute_Default()
			{
				Item.PaymentsPlannedActual.Remove(SelectedPaymentsPlannedActualItem);
			}

			private bool RemoveFromPaymentsPlannedActualCommand_CanExecute_Default()
			{
				return SelectedPaymentsPlannedActualItem != null;
			}

			private async void AddInPaymentsPlannedGeneratedCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentPlanned, PaymentPlannedWrapper>(await _getEntitiesForAddInPaymentsPlannedGeneratedCommand(), Item.PaymentsPlannedGenerated);
			}

			private void RemoveFromPaymentsPlannedGeneratedCommand_Execute_Default()
			{
				Item.PaymentsPlannedGenerated.Remove(SelectedPaymentsPlannedGeneratedItem);
			}

			private bool RemoveFromPaymentsPlannedGeneratedCommand_CanExecute_Default()
			{
				return SelectedPaymentsPlannedGeneratedItem != null;
			}

			private async void AddInPaymentsPlannedCalculatedCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentPlanned, PaymentPlannedWrapper>(await _getEntitiesForAddInPaymentsPlannedCalculatedCommand(), Item.PaymentsPlannedCalculated);
			}

			private void RemoveFromPaymentsPlannedCalculatedCommand_Execute_Default()
			{
				Item.PaymentsPlannedCalculated.Remove(SelectedPaymentsPlannedCalculatedItem);
			}

			private bool RemoveFromPaymentsPlannedCalculatedCommand_CanExecute_Default()
			{
				return SelectedPaymentsPlannedCalculatedItem != null;
			}



    }


    public partial class DocumentDetailsViewModel : BaseDetailsViewModel<DocumentWrapper, Document, AfterSaveDocumentEvent>
    {
		private Func<Task<List<DocumentNumber>>> _getEntitiesForSelectNumberCommand;
		public ICommand SelectNumberCommand { get; private set; }
		public ICommand ClearNumberCommand { get; private set; }

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
			
			if (_getEntitiesForSelectNumberCommand == null) _getEntitiesForSelectNumberCommand = async () => { return await UnitOfWork.Repository<DocumentNumber>().GetAllAsync(); };
			if (SelectNumberCommand == null) SelectNumberCommand = new DelegateCommand(SelectNumberCommand_Execute_Default);
			if (ClearNumberCommand == null) ClearNumberCommand = new DelegateCommand(ClearNumberCommand_Execute_Default);

			
			if (_getEntitiesForSelectRequestDocumentCommand == null) _getEntitiesForSelectRequestDocumentCommand = async () => { return await UnitOfWork.Repository<Document>().GetAllAsync(); };
			if (SelectRequestDocumentCommand == null) SelectRequestDocumentCommand = new DelegateCommand(SelectRequestDocumentCommand_Execute_Default);
			if (ClearRequestDocumentCommand == null) ClearRequestDocumentCommand = new DelegateCommand(ClearRequestDocumentCommand_Execute_Default);

			
			if (_getEntitiesForSelectAuthorCommand == null) _getEntitiesForSelectAuthorCommand = async () => { return await UnitOfWork.Repository<Employee>().GetAllAsync(); };
			if (SelectAuthorCommand == null) SelectAuthorCommand = new DelegateCommand(SelectAuthorCommand_Execute_Default);
			if (ClearAuthorCommand == null) ClearAuthorCommand = new DelegateCommand(ClearAuthorCommand_Execute_Default);

			
			if (_getEntitiesForSelectSenderEmployeeCommand == null) _getEntitiesForSelectSenderEmployeeCommand = async () => { return await UnitOfWork.Repository<Employee>().GetAllAsync(); };
			if (SelectSenderEmployeeCommand == null) SelectSenderEmployeeCommand = new DelegateCommand(SelectSenderEmployeeCommand_Execute_Default);
			if (ClearSenderEmployeeCommand == null) ClearSenderEmployeeCommand = new DelegateCommand(ClearSenderEmployeeCommand_Execute_Default);

			
			if (_getEntitiesForSelectRecipientEmployeeCommand == null) _getEntitiesForSelectRecipientEmployeeCommand = async () => { return await UnitOfWork.Repository<Employee>().GetAllAsync(); };
			if (SelectRecipientEmployeeCommand == null) SelectRecipientEmployeeCommand = new DelegateCommand(SelectRecipientEmployeeCommand_Execute_Default);
			if (ClearRecipientEmployeeCommand == null) ClearRecipientEmployeeCommand = new DelegateCommand(ClearRecipientEmployeeCommand_Execute_Default);

			
			if (_getEntitiesForSelectRegistrationDetailsOfRecipientCommand == null) _getEntitiesForSelectRegistrationDetailsOfRecipientCommand = async () => { return await UnitOfWork.Repository<DocumentsRegistrationDetails>().GetAllAsync(); };
			if (SelectRegistrationDetailsOfRecipientCommand == null) SelectRegistrationDetailsOfRecipientCommand = new DelegateCommand(SelectRegistrationDetailsOfRecipientCommand_Execute_Default);
			if (ClearRegistrationDetailsOfRecipientCommand == null) ClearRegistrationDetailsOfRecipientCommand = new DelegateCommand(ClearRegistrationDetailsOfRecipientCommand_Execute_Default);

			
			if (_getEntitiesForAddInCopyToRecipientsCommand == null) _getEntitiesForAddInCopyToRecipientsCommand = async () => { return await UnitOfWork.Repository<Employee>().GetAllAsync(); };;
			if (AddInCopyToRecipientsCommand == null) AddInCopyToRecipientsCommand = new DelegateCommand(AddInCopyToRecipientsCommand_Execute_Default);
			if (RemoveFromCopyToRecipientsCommand == null) RemoveFromCopyToRecipientsCommand = new DelegateCommand(RemoveFromCopyToRecipientsCommand_Execute_Default, RemoveFromCopyToRecipientsCommand_CanExecute_Default);

		}

		private async void SelectNumberCommand_Execute_Default() 
		{
            SelectAndSetWrapper<DocumentNumber, DocumentNumberWrapper>(await _getEntitiesForSelectNumberCommand(), nameof(Item.Number), Item.Number?.Id);
		}

		private void ClearNumberCommand_Execute_Default() 
		{
						Item.Number = null;
		    
		}

		private async void SelectRequestDocumentCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Document, DocumentWrapper>(await _getEntitiesForSelectRequestDocumentCommand(), nameof(Item.RequestDocument), Item.RequestDocument?.Id);
		}

		private void ClearRequestDocumentCommand_Execute_Default() 
		{
						Item.RequestDocument = null;
		    
		}

		private async void SelectAuthorCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectAuthorCommand(), nameof(Item.Author), Item.Author?.Id);
		}

		private void ClearAuthorCommand_Execute_Default() 
		{
						Item.Author = null;
		    
		}

		private async void SelectSenderEmployeeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectSenderEmployeeCommand(), nameof(Item.SenderEmployee), Item.SenderEmployee?.Id);
		}

		private void ClearSenderEmployeeCommand_Execute_Default() 
		{
						Item.SenderEmployee = null;
		    
		}

		private async void SelectRecipientEmployeeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectRecipientEmployeeCommand(), nameof(Item.RecipientEmployee), Item.RecipientEmployee?.Id);
		}

		private void ClearRecipientEmployeeCommand_Execute_Default() 
		{
						Item.RecipientEmployee = null;
		    
		}

		private async void SelectRegistrationDetailsOfRecipientCommand_Execute_Default() 
		{
            SelectAndSetWrapper<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(await _getEntitiesForSelectRegistrationDetailsOfRecipientCommand(), nameof(Item.RegistrationDetailsOfRecipient), Item.RegistrationDetailsOfRecipient?.Id);
		}

		private void ClearRegistrationDetailsOfRecipientCommand_Execute_Default() 
		{
						Item.RegistrationDetailsOfRecipient = null;
		    
		}

			private async void AddInCopyToRecipientsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Employee, EmployeeWrapper>(await _getEntitiesForAddInCopyToRecipientsCommand(), Item.CopyToRecipients);
			}

			private void RemoveFromCopyToRecipientsCommand_Execute_Default()
			{
				Item.CopyToRecipients.Remove(SelectedCopyToRecipientsItem);
			}

			private bool RemoveFromCopyToRecipientsCommand_CanExecute_Default()
			{
				return SelectedCopyToRecipientsItem != null;
			}



    }


    public partial class SumOnDateDetailsViewModel : BaseDetailsViewModel<SumOnDateWrapper, SumOnDate, AfterSaveSumOnDateEvent>
    {

        public SumOnDateDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class ProductDetailsViewModel : BaseDetailsViewModel<ProductWrapper, Product, AfterSaveProductEvent>
    {
		private Func<Task<List<ProductType>>> _getEntitiesForSelectProductTypeCommand;
		public ICommand SelectProductTypeCommand { get; private set; }
		public ICommand ClearProductTypeCommand { get; private set; }

		private Func<Task<List<ProductBlock>>> _getEntitiesForSelectProductBlockCommand;
		public ICommand SelectProductBlockCommand { get; private set; }
		public ICommand ClearProductBlockCommand { get; private set; }

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


        public ProductDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectProductTypeCommand == null) _getEntitiesForSelectProductTypeCommand = async () => { return await UnitOfWork.Repository<ProductType>().GetAllAsync(); };
			if (SelectProductTypeCommand == null) SelectProductTypeCommand = new DelegateCommand(SelectProductTypeCommand_Execute_Default);
			if (ClearProductTypeCommand == null) ClearProductTypeCommand = new DelegateCommand(ClearProductTypeCommand_Execute_Default);

			
			if (_getEntitiesForSelectProductBlockCommand == null) _getEntitiesForSelectProductBlockCommand = async () => { return await UnitOfWork.Repository<ProductBlock>().GetAllAsync(); };
			if (SelectProductBlockCommand == null) SelectProductBlockCommand = new DelegateCommand(SelectProductBlockCommand_Execute_Default);
			if (ClearProductBlockCommand == null) ClearProductBlockCommand = new DelegateCommand(ClearProductBlockCommand_Execute_Default);

			
			if (_getEntitiesForAddInDependentProductsCommand == null) _getEntitiesForAddInDependentProductsCommand = async () => { return await UnitOfWork.Repository<ProductDependent>().GetAllAsync(); };;
			if (AddInDependentProductsCommand == null) AddInDependentProductsCommand = new DelegateCommand(AddInDependentProductsCommand_Execute_Default);
			if (RemoveFromDependentProductsCommand == null) RemoveFromDependentProductsCommand = new DelegateCommand(RemoveFromDependentProductsCommand_Execute_Default, RemoveFromDependentProductsCommand_CanExecute_Default);

		}

		private async void SelectProductTypeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ProductType, ProductTypeWrapper>(await _getEntitiesForSelectProductTypeCommand(), nameof(Item.ProductType), Item.ProductType?.Id);
		}

		private void ClearProductTypeCommand_Execute_Default() 
		{
		
		    
		}

		private async void SelectProductBlockCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ProductBlock, ProductBlockWrapper>(await _getEntitiesForSelectProductBlockCommand(), nameof(Item.ProductBlock), Item.ProductBlock?.Id);
		}

		private void ClearProductBlockCommand_Execute_Default() 
		{
						Item.ProductBlock = null;
		    
		}

			private async void AddInDependentProductsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ProductDependent, ProductDependentWrapper>(await _getEntitiesForAddInDependentProductsCommand(), Item.DependentProducts);
			}

			private void RemoveFromDependentProductsCommand_Execute_Default()
			{
				Item.DependentProducts.Remove(SelectedDependentProductsItem);
			}

			private bool RemoveFromDependentProductsCommand_CanExecute_Default()
			{
				return SelectedDependentProductsItem != null;
			}



    }


    public partial class OfferDetailsViewModel : BaseDetailsViewModel<OfferWrapper, Offer, AfterSaveOfferEvent>
    {
		private Func<Task<List<Project>>> _getEntitiesForSelectProjectCommand;
		public ICommand SelectProjectCommand { get; private set; }
		public ICommand ClearProjectCommand { get; private set; }

		private Func<Task<List<DocumentNumber>>> _getEntitiesForSelectNumberCommand;
		public ICommand SelectNumberCommand { get; private set; }
		public ICommand ClearNumberCommand { get; private set; }

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
			
			if (_getEntitiesForSelectProjectCommand == null) _getEntitiesForSelectProjectCommand = async () => { return await UnitOfWork.Repository<Project>().GetAllAsync(); };
			if (SelectProjectCommand == null) SelectProjectCommand = new DelegateCommand(SelectProjectCommand_Execute_Default);
			if (ClearProjectCommand == null) ClearProjectCommand = new DelegateCommand(ClearProjectCommand_Execute_Default);

			
			if (_getEntitiesForSelectNumberCommand == null) _getEntitiesForSelectNumberCommand = async () => { return await UnitOfWork.Repository<DocumentNumber>().GetAllAsync(); };
			if (SelectNumberCommand == null) SelectNumberCommand = new DelegateCommand(SelectNumberCommand_Execute_Default);
			if (ClearNumberCommand == null) ClearNumberCommand = new DelegateCommand(ClearNumberCommand_Execute_Default);

			
			if (_getEntitiesForSelectRequestDocumentCommand == null) _getEntitiesForSelectRequestDocumentCommand = async () => { return await UnitOfWork.Repository<Document>().GetAllAsync(); };
			if (SelectRequestDocumentCommand == null) SelectRequestDocumentCommand = new DelegateCommand(SelectRequestDocumentCommand_Execute_Default);
			if (ClearRequestDocumentCommand == null) ClearRequestDocumentCommand = new DelegateCommand(ClearRequestDocumentCommand_Execute_Default);

			
			if (_getEntitiesForSelectAuthorCommand == null) _getEntitiesForSelectAuthorCommand = async () => { return await UnitOfWork.Repository<Employee>().GetAllAsync(); };
			if (SelectAuthorCommand == null) SelectAuthorCommand = new DelegateCommand(SelectAuthorCommand_Execute_Default);
			if (ClearAuthorCommand == null) ClearAuthorCommand = new DelegateCommand(ClearAuthorCommand_Execute_Default);

			
			if (_getEntitiesForSelectSenderEmployeeCommand == null) _getEntitiesForSelectSenderEmployeeCommand = async () => { return await UnitOfWork.Repository<Employee>().GetAllAsync(); };
			if (SelectSenderEmployeeCommand == null) SelectSenderEmployeeCommand = new DelegateCommand(SelectSenderEmployeeCommand_Execute_Default);
			if (ClearSenderEmployeeCommand == null) ClearSenderEmployeeCommand = new DelegateCommand(ClearSenderEmployeeCommand_Execute_Default);

			
			if (_getEntitiesForSelectRecipientEmployeeCommand == null) _getEntitiesForSelectRecipientEmployeeCommand = async () => { return await UnitOfWork.Repository<Employee>().GetAllAsync(); };
			if (SelectRecipientEmployeeCommand == null) SelectRecipientEmployeeCommand = new DelegateCommand(SelectRecipientEmployeeCommand_Execute_Default);
			if (ClearRecipientEmployeeCommand == null) ClearRecipientEmployeeCommand = new DelegateCommand(ClearRecipientEmployeeCommand_Execute_Default);

			
			if (_getEntitiesForSelectRegistrationDetailsOfRecipientCommand == null) _getEntitiesForSelectRegistrationDetailsOfRecipientCommand = async () => { return await UnitOfWork.Repository<DocumentsRegistrationDetails>().GetAllAsync(); };
			if (SelectRegistrationDetailsOfRecipientCommand == null) SelectRegistrationDetailsOfRecipientCommand = new DelegateCommand(SelectRegistrationDetailsOfRecipientCommand_Execute_Default);
			if (ClearRegistrationDetailsOfRecipientCommand == null) ClearRegistrationDetailsOfRecipientCommand = new DelegateCommand(ClearRegistrationDetailsOfRecipientCommand_Execute_Default);

			
			if (_getEntitiesForAddInCopyToRecipientsCommand == null) _getEntitiesForAddInCopyToRecipientsCommand = async () => { return await UnitOfWork.Repository<Employee>().GetAllAsync(); };;
			if (AddInCopyToRecipientsCommand == null) AddInCopyToRecipientsCommand = new DelegateCommand(AddInCopyToRecipientsCommand_Execute_Default);
			if (RemoveFromCopyToRecipientsCommand == null) RemoveFromCopyToRecipientsCommand = new DelegateCommand(RemoveFromCopyToRecipientsCommand_Execute_Default, RemoveFromCopyToRecipientsCommand_CanExecute_Default);

		}

		private async void SelectProjectCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Project, ProjectWrapper>(await _getEntitiesForSelectProjectCommand(), nameof(Item.Project), Item.Project?.Id);
		}

		private void ClearProjectCommand_Execute_Default() 
		{
						Item.Project = null;
		    
		}

		private async void SelectNumberCommand_Execute_Default() 
		{
            SelectAndSetWrapper<DocumentNumber, DocumentNumberWrapper>(await _getEntitiesForSelectNumberCommand(), nameof(Item.Number), Item.Number?.Id);
		}

		private void ClearNumberCommand_Execute_Default() 
		{
						Item.Number = null;
		    
		}

		private async void SelectRequestDocumentCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Document, DocumentWrapper>(await _getEntitiesForSelectRequestDocumentCommand(), nameof(Item.RequestDocument), Item.RequestDocument?.Id);
		}

		private void ClearRequestDocumentCommand_Execute_Default() 
		{
						Item.RequestDocument = null;
		    
		}

		private async void SelectAuthorCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectAuthorCommand(), nameof(Item.Author), Item.Author?.Id);
		}

		private void ClearAuthorCommand_Execute_Default() 
		{
						Item.Author = null;
		    
		}

		private async void SelectSenderEmployeeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectSenderEmployeeCommand(), nameof(Item.SenderEmployee), Item.SenderEmployee?.Id);
		}

		private void ClearSenderEmployeeCommand_Execute_Default() 
		{
						Item.SenderEmployee = null;
		    
		}

		private async void SelectRecipientEmployeeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectRecipientEmployeeCommand(), nameof(Item.RecipientEmployee), Item.RecipientEmployee?.Id);
		}

		private void ClearRecipientEmployeeCommand_Execute_Default() 
		{
						Item.RecipientEmployee = null;
		    
		}

		private async void SelectRegistrationDetailsOfRecipientCommand_Execute_Default() 
		{
            SelectAndSetWrapper<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(await _getEntitiesForSelectRegistrationDetailsOfRecipientCommand(), nameof(Item.RegistrationDetailsOfRecipient), Item.RegistrationDetailsOfRecipient?.Id);
		}

		private void ClearRegistrationDetailsOfRecipientCommand_Execute_Default() 
		{
						Item.RegistrationDetailsOfRecipient = null;
		    
		}

			private async void AddInCopyToRecipientsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Employee, EmployeeWrapper>(await _getEntitiesForAddInCopyToRecipientsCommand(), Item.CopyToRecipients);
			}

			private void RemoveFromCopyToRecipientsCommand_Execute_Default()
			{
				Item.CopyToRecipients.Remove(SelectedCopyToRecipientsItem);
			}

			private bool RemoveFromCopyToRecipientsCommand_CanExecute_Default()
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
			
			if (_getEntitiesForSelectPersonCommand == null) _getEntitiesForSelectPersonCommand = async () => { return await UnitOfWork.Repository<Person>().GetAllAsync(); };
			if (SelectPersonCommand == null) SelectPersonCommand = new DelegateCommand(SelectPersonCommand_Execute_Default);
			if (ClearPersonCommand == null) ClearPersonCommand = new DelegateCommand(ClearPersonCommand_Execute_Default);

			
			if (_getEntitiesForSelectCompanyCommand == null) _getEntitiesForSelectCompanyCommand = async () => { return await UnitOfWork.Repository<Company>().GetAllAsync(); };
			if (SelectCompanyCommand == null) SelectCompanyCommand = new DelegateCommand(SelectCompanyCommand_Execute_Default);
			if (ClearCompanyCommand == null) ClearCompanyCommand = new DelegateCommand(ClearCompanyCommand_Execute_Default);

			
			if (_getEntitiesForSelectPositionCommand == null) _getEntitiesForSelectPositionCommand = async () => { return await UnitOfWork.Repository<EmployeesPosition>().GetAllAsync(); };
			if (SelectPositionCommand == null) SelectPositionCommand = new DelegateCommand(SelectPositionCommand_Execute_Default);
			if (ClearPositionCommand == null) ClearPositionCommand = new DelegateCommand(ClearPositionCommand_Execute_Default);

		}

		private async void SelectPersonCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Person, PersonWrapper>(await _getEntitiesForSelectPersonCommand(), nameof(Item.Person), Item.Person?.Id);
		}

		private void ClearPersonCommand_Execute_Default() 
		{
						Item.Person = null;
		    
		}

		private async void SelectCompanyCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectCompanyCommand(), nameof(Item.Company), Item.Company?.Id);
		}

		private void ClearCompanyCommand_Execute_Default() 
		{
						Item.Company = null;
		    
		}

		private async void SelectPositionCommand_Execute_Default() 
		{
            SelectAndSetWrapper<EmployeesPosition, EmployeesPositionWrapper>(await _getEntitiesForSelectPositionCommand(), nameof(Item.Position), Item.Position?.Id);
		}

		private void ClearPositionCommand_Execute_Default() 
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
			
			if (_getEntitiesForAddInPaymentsCommand == null) _getEntitiesForAddInPaymentsCommand = async () => { return await UnitOfWork.Repository<PaymentActual>().GetAllAsync(); };;
			if (AddInPaymentsCommand == null) AddInPaymentsCommand = new DelegateCommand(AddInPaymentsCommand_Execute_Default);
			if (RemoveFromPaymentsCommand == null) RemoveFromPaymentsCommand = new DelegateCommand(RemoveFromPaymentsCommand_Execute_Default, RemoveFromPaymentsCommand_CanExecute_Default);

		}

			private async void AddInPaymentsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentActual, PaymentActualWrapper>(await _getEntitiesForAddInPaymentsCommand(), Item.Payments);
			}

			private void RemoveFromPaymentsCommand_Execute_Default()
			{
				Item.Payments.Remove(SelectedPaymentsItem);
			}

			private bool RemoveFromPaymentsCommand_CanExecute_Default()
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
			
			if (_getEntitiesForSelectTypeCommand == null) _getEntitiesForSelectTypeCommand = async () => { return await UnitOfWork.Repository<FacilityType>().GetAllAsync(); };
			if (SelectTypeCommand == null) SelectTypeCommand = new DelegateCommand(SelectTypeCommand_Execute_Default);
			if (ClearTypeCommand == null) ClearTypeCommand = new DelegateCommand(ClearTypeCommand_Execute_Default);

			
			if (_getEntitiesForSelectOwnerCompanyCommand == null) _getEntitiesForSelectOwnerCompanyCommand = async () => { return await UnitOfWork.Repository<Company>().GetAllAsync(); };
			if (SelectOwnerCompanyCommand == null) SelectOwnerCompanyCommand = new DelegateCommand(SelectOwnerCompanyCommand_Execute_Default);
			if (ClearOwnerCompanyCommand == null) ClearOwnerCompanyCommand = new DelegateCommand(ClearOwnerCompanyCommand_Execute_Default);

			
			if (_getEntitiesForSelectAddressCommand == null) _getEntitiesForSelectAddressCommand = async () => { return await UnitOfWork.Repository<Address>().GetAllAsync(); };
			if (SelectAddressCommand == null) SelectAddressCommand = new DelegateCommand(SelectAddressCommand_Execute_Default);
			if (ClearAddressCommand == null) ClearAddressCommand = new DelegateCommand(ClearAddressCommand_Execute_Default);

		}

		private async void SelectTypeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<FacilityType, FacilityTypeWrapper>(await _getEntitiesForSelectTypeCommand(), nameof(Item.Type), Item.Type?.Id);
		}

		private void ClearTypeCommand_Execute_Default() 
		{
						Item.Type = null;
		    
		}

		private async void SelectOwnerCompanyCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectOwnerCompanyCommand(), nameof(Item.OwnerCompany), Item.OwnerCompany?.Id);
		}

		private void ClearOwnerCompanyCommand_Execute_Default() 
		{
						Item.OwnerCompany = null;
		    
		}

		private async void SelectAddressCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Address, AddressWrapper>(await _getEntitiesForSelectAddressCommand(), nameof(Item.Address), Item.Address?.Id);
		}

		private void ClearAddressCommand_Execute_Default() 
		{
						Item.Address = null;
		    
		}



    }


    public partial class ProjectDetailsViewModel : BaseDetailsViewModel<ProjectWrapper, Project, AfterSaveProjectEvent>
    {
		private Func<Task<List<ProjectType>>> _getEntitiesForSelectProjectTypeCommand;
		public ICommand SelectProjectTypeCommand { get; private set; }
		public ICommand ClearProjectTypeCommand { get; private set; }

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
			
			if (_getEntitiesForSelectProjectTypeCommand == null) _getEntitiesForSelectProjectTypeCommand = async () => { return await UnitOfWork.Repository<ProjectType>().GetAllAsync(); };
			if (SelectProjectTypeCommand == null) SelectProjectTypeCommand = new DelegateCommand(SelectProjectTypeCommand_Execute_Default);
			if (ClearProjectTypeCommand == null) ClearProjectTypeCommand = new DelegateCommand(ClearProjectTypeCommand_Execute_Default);

			
			if (_getEntitiesForSelectManagerCommand == null) _getEntitiesForSelectManagerCommand = async () => { return await UnitOfWork.Repository<User>().GetAllAsync(); };
			if (SelectManagerCommand == null) SelectManagerCommand = new DelegateCommand(SelectManagerCommand_Execute_Default);
			if (ClearManagerCommand == null) ClearManagerCommand = new DelegateCommand(ClearManagerCommand_Execute_Default);

			
			if (_getEntitiesForAddInNotesCommand == null) _getEntitiesForAddInNotesCommand = async () => { return await UnitOfWork.Repository<Note>().GetAllAsync(); };;
			if (AddInNotesCommand == null) AddInNotesCommand = new DelegateCommand(AddInNotesCommand_Execute_Default);
			if (RemoveFromNotesCommand == null) RemoveFromNotesCommand = new DelegateCommand(RemoveFromNotesCommand_Execute_Default, RemoveFromNotesCommand_CanExecute_Default);

		}

		private async void SelectProjectTypeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ProjectType, ProjectTypeWrapper>(await _getEntitiesForSelectProjectTypeCommand(), nameof(Item.ProjectType), Item.ProjectType?.Id);
		}

		private void ClearProjectTypeCommand_Execute_Default() 
		{
						Item.ProjectType = null;
		    
		}

		private async void SelectManagerCommand_Execute_Default() 
		{
            SelectAndSetWrapper<User, UserWrapper>(await _getEntitiesForSelectManagerCommand(), nameof(Item.Manager), Item.Manager?.Id);
		}

		private void ClearManagerCommand_Execute_Default() 
		{
						Item.Manager = null;
		    
		}

			private async void AddInNotesCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Note, NoteWrapper>(await _getEntitiesForAddInNotesCommand(), Item.Notes);
			}

			private void RemoveFromNotesCommand_Execute_Default()
			{
				Item.Notes.Remove(SelectedNotesItem);
			}

			private bool RemoveFromNotesCommand_CanExecute_Default()
			{
				return SelectedNotesItem != null;
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
		private Func<Task<List<Contract>>> _getEntitiesForSelectContractCommand;
		public ICommand SelectContractCommand { get; private set; }
		public ICommand ClearContractCommand { get; private set; }


        public SpecificationDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectContractCommand == null) _getEntitiesForSelectContractCommand = async () => { return await UnitOfWork.Repository<Contract>().GetAllAsync(); };
			if (SelectContractCommand == null) SelectContractCommand = new DelegateCommand(SelectContractCommand_Execute_Default);
			if (ClearContractCommand == null) ClearContractCommand = new DelegateCommand(ClearContractCommand_Execute_Default);

		}

		private async void SelectContractCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Contract, ContractWrapper>(await _getEntitiesForSelectContractCommand(), nameof(Item.Contract), Item.Contract?.Id);
		}

		private void ClearContractCommand_Execute_Default() 
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
			
			if (_getEntitiesForSelectProjectCommand == null) _getEntitiesForSelectProjectCommand = async () => { return await UnitOfWork.Repository<Project>().GetAllAsync(); };
			if (SelectProjectCommand == null) SelectProjectCommand = new DelegateCommand(SelectProjectCommand_Execute_Default);
			if (ClearProjectCommand == null) ClearProjectCommand = new DelegateCommand(ClearProjectCommand_Execute_Default);

			
			if (_getEntitiesForSelectWinnerCommand == null) _getEntitiesForSelectWinnerCommand = async () => { return await UnitOfWork.Repository<Company>().GetAllAsync(); };
			if (SelectWinnerCommand == null) SelectWinnerCommand = new DelegateCommand(SelectWinnerCommand_Execute_Default);
			if (ClearWinnerCommand == null) ClearWinnerCommand = new DelegateCommand(ClearWinnerCommand_Execute_Default);

			
			if (_getEntitiesForAddInTypesCommand == null) _getEntitiesForAddInTypesCommand = async () => { return await UnitOfWork.Repository<TenderType>().GetAllAsync(); };;
			if (AddInTypesCommand == null) AddInTypesCommand = new DelegateCommand(AddInTypesCommand_Execute_Default);
			if (RemoveFromTypesCommand == null) RemoveFromTypesCommand = new DelegateCommand(RemoveFromTypesCommand_Execute_Default, RemoveFromTypesCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInParticipantsCommand == null) _getEntitiesForAddInParticipantsCommand = async () => { return await UnitOfWork.Repository<Company>().GetAllAsync(); };;
			if (AddInParticipantsCommand == null) AddInParticipantsCommand = new DelegateCommand(AddInParticipantsCommand_Execute_Default);
			if (RemoveFromParticipantsCommand == null) RemoveFromParticipantsCommand = new DelegateCommand(RemoveFromParticipantsCommand_Execute_Default, RemoveFromParticipantsCommand_CanExecute_Default);

		}

		private async void SelectProjectCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Project, ProjectWrapper>(await _getEntitiesForSelectProjectCommand(), nameof(Item.Project), Item.Project?.Id);
		}

		private void ClearProjectCommand_Execute_Default() 
		{
						Item.Project = null;
		    
		}

		private async void SelectWinnerCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(await _getEntitiesForSelectWinnerCommand(), nameof(Item.Winner), Item.Winner?.Id);
		}

		private void ClearWinnerCommand_Execute_Default() 
		{
						Item.Winner = null;
		    
		}

			private async void AddInTypesCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<TenderType, TenderTypeWrapper>(await _getEntitiesForAddInTypesCommand(), Item.Types);
			}

			private void RemoveFromTypesCommand_Execute_Default()
			{
				Item.Types.Remove(SelectedTypesItem);
			}

			private bool RemoveFromTypesCommand_CanExecute_Default()
			{
				return SelectedTypesItem != null;
			}

			private async void AddInParticipantsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Company, CompanyWrapper>(await _getEntitiesForAddInParticipantsCommand(), Item.Participants);
			}

			private void RemoveFromParticipantsCommand_Execute_Default()
			{
				Item.Participants.Remove(SelectedParticipantsItem);
			}

			private bool RemoveFromParticipantsCommand_CanExecute_Default()
			{
				return SelectedParticipantsItem != null;
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
			
			if (_getEntitiesForSelectEmployeeCommand == null) _getEntitiesForSelectEmployeeCommand = async () => { return await UnitOfWork.Repository<Employee>().GetAllAsync(); };
			if (SelectEmployeeCommand == null) SelectEmployeeCommand = new DelegateCommand(SelectEmployeeCommand_Execute_Default);
			if (ClearEmployeeCommand == null) ClearEmployeeCommand = new DelegateCommand(ClearEmployeeCommand_Execute_Default);

			
			if (_getEntitiesForAddInRolesCommand == null) _getEntitiesForAddInRolesCommand = async () => { return await UnitOfWork.Repository<UserRole>().GetAllAsync(); };;
			if (AddInRolesCommand == null) AddInRolesCommand = new DelegateCommand(AddInRolesCommand_Execute_Default);
			if (RemoveFromRolesCommand == null) RemoveFromRolesCommand = new DelegateCommand(RemoveFromRolesCommand_Execute_Default, RemoveFromRolesCommand_CanExecute_Default);

		}

		private async void SelectEmployeeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(await _getEntitiesForSelectEmployeeCommand(), nameof(Item.Employee), Item.Employee?.Id);
		}

		private void ClearEmployeeCommand_Execute_Default() 
		{
						Item.Employee = null;
		    
		}

			private async void AddInRolesCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<UserRole, UserRoleWrapper>(await _getEntitiesForAddInRolesCommand(), Item.Roles);
			}

			private void RemoveFromRolesCommand_Execute_Default()
			{
				Item.Roles.Remove(SelectedRolesItem);
			}

			private bool RemoveFromRolesCommand_CanExecute_Default()
			{
				return SelectedRolesItem != null;
			}



    }


}
