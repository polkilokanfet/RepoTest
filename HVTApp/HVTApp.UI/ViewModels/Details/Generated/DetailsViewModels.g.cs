














using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
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
		private Func<List<Country>> _getEntitiesForAddInCountriesCommand;
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
			
			if (_getEntitiesForAddInCountriesCommand == null) _getEntitiesForAddInCountriesCommand = () => { return UnitOfWork.Repository<Country>().GetAll(); };;
			if (AddInCountriesCommand == null) AddInCountriesCommand = new DelegateCommand(AddInCountriesCommand_Execute_Default);
			if (RemoveFromCountriesCommand == null) RemoveFromCountriesCommand = new DelegateCommand(RemoveFromCountriesCommand_Execute_Default, RemoveFromCountriesCommand_CanExecute_Default);

		}

			private void AddInCountriesCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Country, CountryWrapper>(_getEntitiesForAddInCountriesCommand(), Item.Countries);
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


    public partial class BankGuaranteeDetailsViewModel : BaseDetailsViewModel<BankGuaranteeWrapper, BankGuarantee, AfterSaveBankGuaranteeEvent>
    {
		//private Func<Task<List<BankGuaranteeType>>> _getEntitiesForSelectBankGuaranteeTypeCommand;
		private Func<List<BankGuaranteeType>> _getEntitiesForSelectBankGuaranteeTypeCommand;
		public ICommand SelectBankGuaranteeTypeCommand { get; private set; }
		public ICommand ClearBankGuaranteeTypeCommand { get; private set; }


        public BankGuaranteeDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectBankGuaranteeTypeCommand == null) _getEntitiesForSelectBankGuaranteeTypeCommand = () => { return UnitOfWork.Repository<BankGuaranteeType>().GetAll(); };
			if (SelectBankGuaranteeTypeCommand == null) SelectBankGuaranteeTypeCommand = new DelegateCommand(SelectBankGuaranteeTypeCommand_Execute_Default);
			if (ClearBankGuaranteeTypeCommand == null) ClearBankGuaranteeTypeCommand = new DelegateCommand(ClearBankGuaranteeTypeCommand_Execute_Default);

		}

		private void SelectBankGuaranteeTypeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<BankGuaranteeType, BankGuaranteeTypeWrapper>(_getEntitiesForSelectBankGuaranteeTypeCommand(), nameof(Item.BankGuaranteeType), Item.BankGuaranteeType?.Id);
		}

		private void ClearBankGuaranteeTypeCommand_Execute_Default() 
		{
						Item.BankGuaranteeType = null;
		    
		}



    }


    public partial class BankGuaranteeTypeDetailsViewModel : BaseDetailsViewModel<BankGuaranteeTypeWrapper, BankGuaranteeType, AfterSaveBankGuaranteeTypeEvent>
    {

        public BankGuaranteeTypeDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class ConstructorParametersListDetailsViewModel : BaseDetailsViewModel<ConstructorParametersListWrapper, ConstructorParametersList, AfterSaveConstructorParametersListEvent>
    {
		private Func<List<Parameter>> _getEntitiesForAddInParametersCommand;
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


        public ConstructorParametersListDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForAddInParametersCommand == null) _getEntitiesForAddInParametersCommand = () => { return UnitOfWork.Repository<Parameter>().GetAll(); };;
			if (AddInParametersCommand == null) AddInParametersCommand = new DelegateCommand(AddInParametersCommand_Execute_Default);
			if (RemoveFromParametersCommand == null) RemoveFromParametersCommand = new DelegateCommand(RemoveFromParametersCommand_Execute_Default, RemoveFromParametersCommand_CanExecute_Default);

		}

			private void AddInParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(_getEntitiesForAddInParametersCommand(), Item.Parameters);
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


    public partial class ConstructorsParametersDetailsViewModel : BaseDetailsViewModel<ConstructorsParametersWrapper, ConstructorsParameters, AfterSaveConstructorsParametersEvent>
    {
		private Func<List<User>> _getEntitiesForAddInConstructorsCommand;
		public ICommand AddInConstructorsCommand { get; }
		public ICommand RemoveFromConstructorsCommand { get; }
		private UserWrapper _selectedConstructorsItem;
		public UserWrapper SelectedConstructorsItem 
		{ 
			get { return _selectedConstructorsItem; }
			set 
			{ 
				if (Equals(_selectedConstructorsItem, value)) return;
				_selectedConstructorsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromConstructorsCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<List<ConstructorParametersList>> _getEntitiesForAddInPatametersListsCommand;
		public ICommand AddInPatametersListsCommand { get; }
		public ICommand RemoveFromPatametersListsCommand { get; }
		private ConstructorParametersListWrapper _selectedPatametersListsItem;
		public ConstructorParametersListWrapper SelectedPatametersListsItem 
		{ 
			get { return _selectedPatametersListsItem; }
			set 
			{ 
				if (Equals(_selectedPatametersListsItem, value)) return;
				_selectedPatametersListsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromPatametersListsCommand).RaiseCanExecuteChanged();
			}
		}


        public ConstructorsParametersDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForAddInConstructorsCommand == null) _getEntitiesForAddInConstructorsCommand = () => { return UnitOfWork.Repository<User>().GetAll(); };;
			if (AddInConstructorsCommand == null) AddInConstructorsCommand = new DelegateCommand(AddInConstructorsCommand_Execute_Default);
			if (RemoveFromConstructorsCommand == null) RemoveFromConstructorsCommand = new DelegateCommand(RemoveFromConstructorsCommand_Execute_Default, RemoveFromConstructorsCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInPatametersListsCommand == null) _getEntitiesForAddInPatametersListsCommand = () => { return UnitOfWork.Repository<ConstructorParametersList>().GetAll(); };;
			if (AddInPatametersListsCommand == null) AddInPatametersListsCommand = new DelegateCommand(AddInPatametersListsCommand_Execute_Default);
			if (RemoveFromPatametersListsCommand == null) RemoveFromPatametersListsCommand = new DelegateCommand(RemoveFromPatametersListsCommand_Execute_Default, RemoveFromPatametersListsCommand_CanExecute_Default);

		}

			private void AddInConstructorsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<User, UserWrapper>(_getEntitiesForAddInConstructorsCommand(), Item.Constructors);
			}

			private void RemoveFromConstructorsCommand_Execute_Default()
			{
				Item.Constructors.Remove(SelectedConstructorsItem);
			}

			private bool RemoveFromConstructorsCommand_CanExecute_Default()
			{
				return SelectedConstructorsItem != null;
			}

			private void AddInPatametersListsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ConstructorParametersList, ConstructorParametersListWrapper>(_getEntitiesForAddInPatametersListsCommand(), Item.PatametersLists);
			}

			private void RemoveFromPatametersListsCommand_Execute_Default()
			{
				Item.PatametersLists.Remove(SelectedPatametersListsItem);
			}

			private bool RemoveFromPatametersListsCommand_CanExecute_Default()
			{
				return SelectedPatametersListsItem != null;
			}



    }


    public partial class CreateNewProductTaskDetailsViewModel : BaseDetailsViewModel<CreateNewProductTaskWrapper, CreateNewProductTask, AfterSaveCreateNewProductTaskEvent>
    {
		//private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		private Func<List<Product>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; private set; }
		public ICommand ClearProductCommand { get; private set; }


        public CreateNewProductTaskDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectProductCommand == null) _getEntitiesForSelectProductCommand = () => { return UnitOfWork.Repository<Product>().GetAll(); };
			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute_Default);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute_Default);

		}

		private void SelectProductCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(_getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute_Default() 
		{
						Item.Product = null;
		    
		}



    }


    public partial class DirectumTaskDetailsViewModel : BaseDetailsViewModel<DirectumTaskWrapper, DirectumTask, AfterSaveDirectumTaskEvent>
    {
		//private Func<Task<List<DirectumTaskGroup>>> _getEntitiesForSelectGroupCommand;
		private Func<List<DirectumTaskGroup>> _getEntitiesForSelectGroupCommand;
		public ICommand SelectGroupCommand { get; private set; }
		public ICommand ClearGroupCommand { get; private set; }

		//private Func<Task<List<User>>> _getEntitiesForSelectPerformerCommand;
		private Func<List<User>> _getEntitiesForSelectPerformerCommand;
		public ICommand SelectPerformerCommand { get; private set; }
		public ICommand ClearPerformerCommand { get; private set; }

		//private Func<Task<List<DirectumTask>>> _getEntitiesForSelectParentTaskCommand;
		private Func<List<DirectumTask>> _getEntitiesForSelectParentTaskCommand;
		public ICommand SelectParentTaskCommand { get; private set; }
		public ICommand ClearParentTaskCommand { get; private set; }

		//private Func<Task<List<DirectumTask>>> _getEntitiesForSelectPreviousTaskCommand;
		private Func<List<DirectumTask>> _getEntitiesForSelectPreviousTaskCommand;
		public ICommand SelectPreviousTaskCommand { get; private set; }
		public ICommand ClearPreviousTaskCommand { get; private set; }

		private Func<List<DirectumTaskMessage>> _getEntitiesForAddInMessagesCommand;
		public ICommand AddInMessagesCommand { get; }
		public ICommand RemoveFromMessagesCommand { get; }
		private DirectumTaskMessageWrapper _selectedMessagesItem;
		public DirectumTaskMessageWrapper SelectedMessagesItem 
		{ 
			get { return _selectedMessagesItem; }
			set 
			{ 
				if (Equals(_selectedMessagesItem, value)) return;
				_selectedMessagesItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromMessagesCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<List<DirectumTask>> _getEntitiesForAddInChildsCommand;
		public ICommand AddInChildsCommand { get; }
		public ICommand RemoveFromChildsCommand { get; }
		private DirectumTaskWrapper _selectedChildsItem;
		public DirectumTaskWrapper SelectedChildsItem 
		{ 
			get { return _selectedChildsItem; }
			set 
			{ 
				if (Equals(_selectedChildsItem, value)) return;
				_selectedChildsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromChildsCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<List<DirectumTask>> _getEntitiesForAddInParallelCommand;
		public ICommand AddInParallelCommand { get; }
		public ICommand RemoveFromParallelCommand { get; }
		private DirectumTaskWrapper _selectedParallelItem;
		public DirectumTaskWrapper SelectedParallelItem 
		{ 
			get { return _selectedParallelItem; }
			set 
			{ 
				if (Equals(_selectedParallelItem, value)) return;
				_selectedParallelItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromParallelCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<List<DirectumTask>> _getEntitiesForAddInNextCommand;
		public ICommand AddInNextCommand { get; }
		public ICommand RemoveFromNextCommand { get; }
		private DirectumTaskWrapper _selectedNextItem;
		public DirectumTaskWrapper SelectedNextItem 
		{ 
			get { return _selectedNextItem; }
			set 
			{ 
				if (Equals(_selectedNextItem, value)) return;
				_selectedNextItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromNextCommand).RaiseCanExecuteChanged();
			}
		}


        public DirectumTaskDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectGroupCommand == null) _getEntitiesForSelectGroupCommand = () => { return UnitOfWork.Repository<DirectumTaskGroup>().GetAll(); };
			if (SelectGroupCommand == null) SelectGroupCommand = new DelegateCommand(SelectGroupCommand_Execute_Default);
			if (ClearGroupCommand == null) ClearGroupCommand = new DelegateCommand(ClearGroupCommand_Execute_Default);

			
			if (_getEntitiesForSelectPerformerCommand == null) _getEntitiesForSelectPerformerCommand = () => { return UnitOfWork.Repository<User>().GetAll(); };
			if (SelectPerformerCommand == null) SelectPerformerCommand = new DelegateCommand(SelectPerformerCommand_Execute_Default);
			if (ClearPerformerCommand == null) ClearPerformerCommand = new DelegateCommand(ClearPerformerCommand_Execute_Default);

			
			if (_getEntitiesForSelectParentTaskCommand == null) _getEntitiesForSelectParentTaskCommand = () => { return UnitOfWork.Repository<DirectumTask>().GetAll(); };
			if (SelectParentTaskCommand == null) SelectParentTaskCommand = new DelegateCommand(SelectParentTaskCommand_Execute_Default);
			if (ClearParentTaskCommand == null) ClearParentTaskCommand = new DelegateCommand(ClearParentTaskCommand_Execute_Default);

			
			if (_getEntitiesForSelectPreviousTaskCommand == null) _getEntitiesForSelectPreviousTaskCommand = () => { return UnitOfWork.Repository<DirectumTask>().GetAll(); };
			if (SelectPreviousTaskCommand == null) SelectPreviousTaskCommand = new DelegateCommand(SelectPreviousTaskCommand_Execute_Default);
			if (ClearPreviousTaskCommand == null) ClearPreviousTaskCommand = new DelegateCommand(ClearPreviousTaskCommand_Execute_Default);

			
			if (_getEntitiesForAddInMessagesCommand == null) _getEntitiesForAddInMessagesCommand = () => { return UnitOfWork.Repository<DirectumTaskMessage>().GetAll(); };;
			if (AddInMessagesCommand == null) AddInMessagesCommand = new DelegateCommand(AddInMessagesCommand_Execute_Default);
			if (RemoveFromMessagesCommand == null) RemoveFromMessagesCommand = new DelegateCommand(RemoveFromMessagesCommand_Execute_Default, RemoveFromMessagesCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInChildsCommand == null) _getEntitiesForAddInChildsCommand = () => { return UnitOfWork.Repository<DirectumTask>().GetAll(); };;
			if (AddInChildsCommand == null) AddInChildsCommand = new DelegateCommand(AddInChildsCommand_Execute_Default);
			if (RemoveFromChildsCommand == null) RemoveFromChildsCommand = new DelegateCommand(RemoveFromChildsCommand_Execute_Default, RemoveFromChildsCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInParallelCommand == null) _getEntitiesForAddInParallelCommand = () => { return UnitOfWork.Repository<DirectumTask>().GetAll(); };;
			if (AddInParallelCommand == null) AddInParallelCommand = new DelegateCommand(AddInParallelCommand_Execute_Default);
			if (RemoveFromParallelCommand == null) RemoveFromParallelCommand = new DelegateCommand(RemoveFromParallelCommand_Execute_Default, RemoveFromParallelCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInNextCommand == null) _getEntitiesForAddInNextCommand = () => { return UnitOfWork.Repository<DirectumTask>().GetAll(); };;
			if (AddInNextCommand == null) AddInNextCommand = new DelegateCommand(AddInNextCommand_Execute_Default);
			if (RemoveFromNextCommand == null) RemoveFromNextCommand = new DelegateCommand(RemoveFromNextCommand_Execute_Default, RemoveFromNextCommand_CanExecute_Default);

		}

		private void SelectGroupCommand_Execute_Default() 
		{
            SelectAndSetWrapper<DirectumTaskGroup, DirectumTaskGroupWrapper>(_getEntitiesForSelectGroupCommand(), nameof(Item.Group), Item.Group?.Id);
		}

		private void ClearGroupCommand_Execute_Default() 
		{
						Item.Group = null;
		    
		}

		private void SelectPerformerCommand_Execute_Default() 
		{
            SelectAndSetWrapper<User, UserWrapper>(_getEntitiesForSelectPerformerCommand(), nameof(Item.Performer), Item.Performer?.Id);
		}

		private void ClearPerformerCommand_Execute_Default() 
		{
						Item.Performer = null;
		    
		}

		private void SelectParentTaskCommand_Execute_Default() 
		{
            SelectAndSetWrapper<DirectumTask, DirectumTaskWrapper>(_getEntitiesForSelectParentTaskCommand(), nameof(Item.ParentTask), Item.ParentTask?.Id);
		}

		private void ClearParentTaskCommand_Execute_Default() 
		{
						Item.ParentTask = null;
		    
		}

		private void SelectPreviousTaskCommand_Execute_Default() 
		{
            SelectAndSetWrapper<DirectumTask, DirectumTaskWrapper>(_getEntitiesForSelectPreviousTaskCommand(), nameof(Item.PreviousTask), Item.PreviousTask?.Id);
		}

		private void ClearPreviousTaskCommand_Execute_Default() 
		{
						Item.PreviousTask = null;
		    
		}

			private void AddInMessagesCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<DirectumTaskMessage, DirectumTaskMessageWrapper>(_getEntitiesForAddInMessagesCommand(), Item.Messages);
			}

			private void RemoveFromMessagesCommand_Execute_Default()
			{
				Item.Messages.Remove(SelectedMessagesItem);
			}

			private bool RemoveFromMessagesCommand_CanExecute_Default()
			{
				return SelectedMessagesItem != null;
			}

			private void AddInChildsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<DirectumTask, DirectumTaskWrapper>(_getEntitiesForAddInChildsCommand(), Item.Childs);
			}

			private void RemoveFromChildsCommand_Execute_Default()
			{
				Item.Childs.Remove(SelectedChildsItem);
			}

			private bool RemoveFromChildsCommand_CanExecute_Default()
			{
				return SelectedChildsItem != null;
			}

			private void AddInParallelCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<DirectumTask, DirectumTaskWrapper>(_getEntitiesForAddInParallelCommand(), Item.Parallel);
			}

			private void RemoveFromParallelCommand_Execute_Default()
			{
				Item.Parallel.Remove(SelectedParallelItem);
			}

			private bool RemoveFromParallelCommand_CanExecute_Default()
			{
				return SelectedParallelItem != null;
			}

			private void AddInNextCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<DirectumTask, DirectumTaskWrapper>(_getEntitiesForAddInNextCommand(), Item.Next);
			}

			private void RemoveFromNextCommand_Execute_Default()
			{
				Item.Next.Remove(SelectedNextItem);
			}

			private bool RemoveFromNextCommand_CanExecute_Default()
			{
				return SelectedNextItem != null;
			}



    }


    public partial class DirectumTaskGroupDetailsViewModel : BaseDetailsViewModel<DirectumTaskGroupWrapper, DirectumTaskGroup, AfterSaveDirectumTaskGroupEvent>
    {
		//private Func<Task<List<User>>> _getEntitiesForSelectAuthorCommand;
		private Func<List<User>> _getEntitiesForSelectAuthorCommand;
		public ICommand SelectAuthorCommand { get; private set; }
		public ICommand ClearAuthorCommand { get; private set; }

		private Func<List<User>> _getEntitiesForAddInObserversCommand;
		public ICommand AddInObserversCommand { get; }
		public ICommand RemoveFromObserversCommand { get; }
		private UserWrapper _selectedObserversItem;
		public UserWrapper SelectedObserversItem 
		{ 
			get { return _selectedObserversItem; }
			set 
			{ 
				if (Equals(_selectedObserversItem, value)) return;
				_selectedObserversItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromObserversCommand).RaiseCanExecuteChanged();
			}
		}


        public DirectumTaskGroupDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectAuthorCommand == null) _getEntitiesForSelectAuthorCommand = () => { return UnitOfWork.Repository<User>().GetAll(); };
			if (SelectAuthorCommand == null) SelectAuthorCommand = new DelegateCommand(SelectAuthorCommand_Execute_Default);
			if (ClearAuthorCommand == null) ClearAuthorCommand = new DelegateCommand(ClearAuthorCommand_Execute_Default);

			
			if (_getEntitiesForAddInObserversCommand == null) _getEntitiesForAddInObserversCommand = () => { return UnitOfWork.Repository<User>().GetAll(); };;
			if (AddInObserversCommand == null) AddInObserversCommand = new DelegateCommand(AddInObserversCommand_Execute_Default);
			if (RemoveFromObserversCommand == null) RemoveFromObserversCommand = new DelegateCommand(RemoveFromObserversCommand_Execute_Default, RemoveFromObserversCommand_CanExecute_Default);

		}

		private void SelectAuthorCommand_Execute_Default() 
		{
            SelectAndSetWrapper<User, UserWrapper>(_getEntitiesForSelectAuthorCommand(), nameof(Item.Author), Item.Author?.Id);
		}

		private void ClearAuthorCommand_Execute_Default() 
		{
						Item.Author = null;
		    
		}

			private void AddInObserversCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<User, UserWrapper>(_getEntitiesForAddInObserversCommand(), Item.Observers);
			}

			private void RemoveFromObserversCommand_Execute_Default()
			{
				Item.Observers.Remove(SelectedObserversItem);
			}

			private bool RemoveFromObserversCommand_CanExecute_Default()
			{
				return SelectedObserversItem != null;
			}



    }


    public partial class DirectumTaskMessageDetailsViewModel : BaseDetailsViewModel<DirectumTaskMessageWrapper, DirectumTaskMessage, AfterSaveDirectumTaskMessageEvent>
    {
		//private Func<Task<List<User>>> _getEntitiesForSelectAuthorCommand;
		private Func<List<User>> _getEntitiesForSelectAuthorCommand;
		public ICommand SelectAuthorCommand { get; private set; }
		public ICommand ClearAuthorCommand { get; private set; }


        public DirectumTaskMessageDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectAuthorCommand == null) _getEntitiesForSelectAuthorCommand = () => { return UnitOfWork.Repository<User>().GetAll(); };
			if (SelectAuthorCommand == null) SelectAuthorCommand = new DelegateCommand(SelectAuthorCommand_Execute_Default);
			if (ClearAuthorCommand == null) ClearAuthorCommand = new DelegateCommand(ClearAuthorCommand_Execute_Default);

		}

		private void SelectAuthorCommand_Execute_Default() 
		{
            SelectAndSetWrapper<User, UserWrapper>(_getEntitiesForSelectAuthorCommand(), nameof(Item.Author), Item.Author?.Id);
		}

		private void ClearAuthorCommand_Execute_Default() 
		{
						Item.Author = null;
		    
		}



    }


    public partial class DocumentNumberDetailsViewModel : BaseDetailsViewModel<DocumentNumberWrapper, DocumentNumber, AfterSaveDocumentNumberEvent>
    {

        public DocumentNumberDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class FakeDataDetailsViewModel : BaseDetailsViewModel<FakeDataWrapper, FakeData, AfterSaveFakeDataEvent>
    {
		//private Func<Task<List<PaymentConditionSet>>> _getEntitiesForSelectPaymentConditionSetCommand;
		private Func<List<PaymentConditionSet>> _getEntitiesForSelectPaymentConditionSetCommand;
		public ICommand SelectPaymentConditionSetCommand { get; private set; }
		public ICommand ClearPaymentConditionSetCommand { get; private set; }


        public FakeDataDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectPaymentConditionSetCommand == null) _getEntitiesForSelectPaymentConditionSetCommand = () => { return UnitOfWork.Repository<PaymentConditionSet>().GetAll(); };
			if (SelectPaymentConditionSetCommand == null) SelectPaymentConditionSetCommand = new DelegateCommand(SelectPaymentConditionSetCommand_Execute_Default);
			if (ClearPaymentConditionSetCommand == null) ClearPaymentConditionSetCommand = new DelegateCommand(ClearPaymentConditionSetCommand_Execute_Default);

		}

		private void SelectPaymentConditionSetCommand_Execute_Default() 
		{
            SelectAndSetWrapper<PaymentConditionSet, PaymentConditionSetWrapper>(_getEntitiesForSelectPaymentConditionSetCommand(), nameof(Item.PaymentConditionSet), Item.PaymentConditionSet?.Id);
		}

		private void ClearPaymentConditionSetCommand_Execute_Default() 
		{
						Item.PaymentConditionSet = null;
		    
		}



    }


    public partial class IncomingRequestDetailsViewModel : BaseDetailsViewModel<IncomingRequestWrapper, IncomingRequest, AfterSaveIncomingRequestEvent>
    {
		//private Func<Task<List<Document>>> _getEntitiesForSelectDocumentCommand;
		private Func<List<Document>> _getEntitiesForSelectDocumentCommand;
		public ICommand SelectDocumentCommand { get; private set; }
		public ICommand ClearDocumentCommand { get; private set; }

		private Func<List<Employee>> _getEntitiesForAddInPerformersCommand;
		public ICommand AddInPerformersCommand { get; }
		public ICommand RemoveFromPerformersCommand { get; }
		private EmployeeWrapper _selectedPerformersItem;
		public EmployeeWrapper SelectedPerformersItem 
		{ 
			get { return _selectedPerformersItem; }
			set 
			{ 
				if (Equals(_selectedPerformersItem, value)) return;
				_selectedPerformersItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromPerformersCommand).RaiseCanExecuteChanged();
			}
		}


        public IncomingRequestDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectDocumentCommand == null) _getEntitiesForSelectDocumentCommand = () => { return UnitOfWork.Repository<Document>().GetAll(); };
			if (SelectDocumentCommand == null) SelectDocumentCommand = new DelegateCommand(SelectDocumentCommand_Execute_Default);
			if (ClearDocumentCommand == null) ClearDocumentCommand = new DelegateCommand(ClearDocumentCommand_Execute_Default);

			
			if (_getEntitiesForAddInPerformersCommand == null) _getEntitiesForAddInPerformersCommand = () => { return UnitOfWork.Repository<Employee>().GetAll(); };;
			if (AddInPerformersCommand == null) AddInPerformersCommand = new DelegateCommand(AddInPerformersCommand_Execute_Default);
			if (RemoveFromPerformersCommand == null) RemoveFromPerformersCommand = new DelegateCommand(RemoveFromPerformersCommand_Execute_Default, RemoveFromPerformersCommand_CanExecute_Default);

		}

		private void SelectDocumentCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Document, DocumentWrapper>(_getEntitiesForSelectDocumentCommand(), nameof(Item.Document), Item.Document?.Id);
		}

		private void ClearDocumentCommand_Execute_Default() 
		{
						Item.Document = null;
		    
		}

			private void AddInPerformersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Employee, EmployeeWrapper>(_getEntitiesForAddInPerformersCommand(), Item.Performers);
			}

			private void RemoveFromPerformersCommand_Execute_Default()
			{
				Item.Performers.Remove(SelectedPerformersItem);
			}

			private bool RemoveFromPerformersCommand_CanExecute_Default()
			{
				return SelectedPerformersItem != null;
			}



    }


    public partial class LosingReasonDetailsViewModel : BaseDetailsViewModel<LosingReasonWrapper, LosingReason, AfterSaveLosingReasonEvent>
    {

        public LosingReasonDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class MarketFieldDetailsViewModel : BaseDetailsViewModel<MarketFieldWrapper, MarketField, AfterSaveMarketFieldEvent>
    {
		private Func<List<ActivityField>> _getEntitiesForAddInActivityFieldsCommand;
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
			
			if (_getEntitiesForAddInActivityFieldsCommand == null) _getEntitiesForAddInActivityFieldsCommand = () => { return UnitOfWork.Repository<ActivityField>().GetAll(); };;
			if (AddInActivityFieldsCommand == null) AddInActivityFieldsCommand = new DelegateCommand(AddInActivityFieldsCommand_Execute_Default);
			if (RemoveFromActivityFieldsCommand == null) RemoveFromActivityFieldsCommand = new DelegateCommand(RemoveFromActivityFieldsCommand_Execute_Default, RemoveFromActivityFieldsCommand_CanExecute_Default);

		}

			private void AddInActivityFieldsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ActivityField, ActivityFieldWrapper>(_getEntitiesForAddInActivityFieldsCommand(), Item.ActivityFields);
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


    public partial class PaymentConditionPointDetailsViewModel : BaseDetailsViewModel<PaymentConditionPointWrapper, PaymentConditionPoint, AfterSavePaymentConditionPointEvent>
    {

        public PaymentConditionPointDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class PaymentPlannedDetailsViewModel : BaseDetailsViewModel<PaymentPlannedWrapper, PaymentPlanned, AfterSavePaymentPlannedEvent>
    {
		//private Func<Task<List<PaymentCondition>>> _getEntitiesForSelectConditionCommand;
		private Func<List<PaymentCondition>> _getEntitiesForSelectConditionCommand;
		public ICommand SelectConditionCommand { get; private set; }
		public ICommand ClearConditionCommand { get; private set; }


        public PaymentPlannedDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectConditionCommand == null) _getEntitiesForSelectConditionCommand = () => { return UnitOfWork.Repository<PaymentCondition>().GetAll(); };
			if (SelectConditionCommand == null) SelectConditionCommand = new DelegateCommand(SelectConditionCommand_Execute_Default);
			if (ClearConditionCommand == null) ClearConditionCommand = new DelegateCommand(ClearConditionCommand_Execute_Default);

		}

		private void SelectConditionCommand_Execute_Default() 
		{
            SelectAndSetWrapper<PaymentCondition, PaymentConditionWrapper>(_getEntitiesForSelectConditionCommand(), nameof(Item.Condition), Item.Condition?.Id);
		}

		private void ClearConditionCommand_Execute_Default() 
		{
						Item.Condition = null;
		    
		}



    }


    public partial class PenaltyDetailsViewModel : BaseDetailsViewModel<PenaltyWrapper, Penalty, AfterSavePenaltyEvent>
    {

        public PenaltyDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class PriceCalculationDetailsViewModel : BaseDetailsViewModel<PriceCalculationWrapper, PriceCalculation, AfterSavePriceCalculationEvent>
    {
		private Func<List<PriceCalculationItem>> _getEntitiesForAddInPriceCalculationItemsCommand;
		public ICommand AddInPriceCalculationItemsCommand { get; }
		public ICommand RemoveFromPriceCalculationItemsCommand { get; }
		private PriceCalculationItemWrapper _selectedPriceCalculationItemsItem;
		public PriceCalculationItemWrapper SelectedPriceCalculationItemsItem 
		{ 
			get { return _selectedPriceCalculationItemsItem; }
			set 
			{ 
				if (Equals(_selectedPriceCalculationItemsItem, value)) return;
				_selectedPriceCalculationItemsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromPriceCalculationItemsCommand).RaiseCanExecuteChanged();
			}
		}


        public PriceCalculationDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForAddInPriceCalculationItemsCommand == null) _getEntitiesForAddInPriceCalculationItemsCommand = () => { return UnitOfWork.Repository<PriceCalculationItem>().GetAll(); };;
			if (AddInPriceCalculationItemsCommand == null) AddInPriceCalculationItemsCommand = new DelegateCommand(AddInPriceCalculationItemsCommand_Execute_Default);
			if (RemoveFromPriceCalculationItemsCommand == null) RemoveFromPriceCalculationItemsCommand = new DelegateCommand(RemoveFromPriceCalculationItemsCommand_Execute_Default, RemoveFromPriceCalculationItemsCommand_CanExecute_Default);

		}

			private void AddInPriceCalculationItemsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PriceCalculationItem, PriceCalculationItemWrapper>(_getEntitiesForAddInPriceCalculationItemsCommand(), Item.PriceCalculationItems);
			}

			private void RemoveFromPriceCalculationItemsCommand_Execute_Default()
			{
				Item.PriceCalculationItems.Remove(SelectedPriceCalculationItemsItem);
			}

			private bool RemoveFromPriceCalculationItemsCommand_CanExecute_Default()
			{
				return SelectedPriceCalculationItemsItem != null;
			}



    }


    public partial class PriceCalculationItemDetailsViewModel : BaseDetailsViewModel<PriceCalculationItemWrapper, PriceCalculationItem, AfterSavePriceCalculationItemEvent>
    {
		//private Func<Task<List<PaymentConditionSet>>> _getEntitiesForSelectPaymentConditionSetCommand;
		private Func<List<PaymentConditionSet>> _getEntitiesForSelectPaymentConditionSetCommand;
		public ICommand SelectPaymentConditionSetCommand { get; private set; }
		public ICommand ClearPaymentConditionSetCommand { get; private set; }

		private Func<List<SalesUnit>> _getEntitiesForAddInSalesUnitsCommand;
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

		private Func<List<StructureCost>> _getEntitiesForAddInStructureCostsCommand;
		public ICommand AddInStructureCostsCommand { get; }
		public ICommand RemoveFromStructureCostsCommand { get; }
		private StructureCostWrapper _selectedStructureCostsItem;
		public StructureCostWrapper SelectedStructureCostsItem 
		{ 
			get { return _selectedStructureCostsItem; }
			set 
			{ 
				if (Equals(_selectedStructureCostsItem, value)) return;
				_selectedStructureCostsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromStructureCostsCommand).RaiseCanExecuteChanged();
			}
		}


        public PriceCalculationItemDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectPaymentConditionSetCommand == null) _getEntitiesForSelectPaymentConditionSetCommand = () => { return UnitOfWork.Repository<PaymentConditionSet>().GetAll(); };
			if (SelectPaymentConditionSetCommand == null) SelectPaymentConditionSetCommand = new DelegateCommand(SelectPaymentConditionSetCommand_Execute_Default);
			if (ClearPaymentConditionSetCommand == null) ClearPaymentConditionSetCommand = new DelegateCommand(ClearPaymentConditionSetCommand_Execute_Default);

			
			if (_getEntitiesForAddInSalesUnitsCommand == null) _getEntitiesForAddInSalesUnitsCommand = () => { return UnitOfWork.Repository<SalesUnit>().GetAll(); };;
			if (AddInSalesUnitsCommand == null) AddInSalesUnitsCommand = new DelegateCommand(AddInSalesUnitsCommand_Execute_Default);
			if (RemoveFromSalesUnitsCommand == null) RemoveFromSalesUnitsCommand = new DelegateCommand(RemoveFromSalesUnitsCommand_Execute_Default, RemoveFromSalesUnitsCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInStructureCostsCommand == null) _getEntitiesForAddInStructureCostsCommand = () => { return UnitOfWork.Repository<StructureCost>().GetAll(); };;
			if (AddInStructureCostsCommand == null) AddInStructureCostsCommand = new DelegateCommand(AddInStructureCostsCommand_Execute_Default);
			if (RemoveFromStructureCostsCommand == null) RemoveFromStructureCostsCommand = new DelegateCommand(RemoveFromStructureCostsCommand_Execute_Default, RemoveFromStructureCostsCommand_CanExecute_Default);

		}

		private void SelectPaymentConditionSetCommand_Execute_Default() 
		{
            SelectAndSetWrapper<PaymentConditionSet, PaymentConditionSetWrapper>(_getEntitiesForSelectPaymentConditionSetCommand(), nameof(Item.PaymentConditionSet), Item.PaymentConditionSet?.Id);
		}

		private void ClearPaymentConditionSetCommand_Execute_Default() 
		{
						Item.PaymentConditionSet = null;
		    
		}

			private void AddInSalesUnitsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<SalesUnit, SalesUnitWrapper>(_getEntitiesForAddInSalesUnitsCommand(), Item.SalesUnits);
			}

			private void RemoveFromSalesUnitsCommand_Execute_Default()
			{
				Item.SalesUnits.Remove(SelectedSalesUnitsItem);
			}

			private bool RemoveFromSalesUnitsCommand_CanExecute_Default()
			{
				return SelectedSalesUnitsItem != null;
			}

			private void AddInStructureCostsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<StructureCost, StructureCostWrapper>(_getEntitiesForAddInStructureCostsCommand(), Item.StructureCosts);
			}

			private void RemoveFromStructureCostsCommand_Execute_Default()
			{
				Item.StructureCosts.Remove(SelectedStructureCostsItem);
			}

			private bool RemoveFromStructureCostsCommand_CanExecute_Default()
			{
				return SelectedStructureCostsItem != null;
			}



    }


    public partial class ProductIncludedDetailsViewModel : BaseDetailsViewModel<ProductIncludedWrapper, ProductIncluded, AfterSaveProductIncludedEvent>
    {
		//private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		private Func<List<Product>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; private set; }
		public ICommand ClearProductCommand { get; private set; }


        public ProductIncludedDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectProductCommand == null) _getEntitiesForSelectProductCommand = () => { return UnitOfWork.Repository<Product>().GetAll(); };
			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute_Default);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute_Default);

		}

		private void SelectProductCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(_getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute_Default() 
		{
						Item.Product = null;
		    
		}



    }


    public partial class ProductDesignationDetailsViewModel : BaseDetailsViewModel<ProductDesignationWrapper, ProductDesignation, AfterSaveProductDesignationEvent>
    {
		private Func<List<Parameter>> _getEntitiesForAddInParametersCommand;
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

		private Func<List<ProductDesignation>> _getEntitiesForAddInParentsCommand;
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
			
			if (_getEntitiesForAddInParametersCommand == null) _getEntitiesForAddInParametersCommand = () => { return UnitOfWork.Repository<Parameter>().GetAll(); };;
			if (AddInParametersCommand == null) AddInParametersCommand = new DelegateCommand(AddInParametersCommand_Execute_Default);
			if (RemoveFromParametersCommand == null) RemoveFromParametersCommand = new DelegateCommand(RemoveFromParametersCommand_Execute_Default, RemoveFromParametersCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInParentsCommand == null) _getEntitiesForAddInParentsCommand = () => { return UnitOfWork.Repository<ProductDesignation>().GetAll(); };;
			if (AddInParentsCommand == null) AddInParentsCommand = new DelegateCommand(AddInParentsCommand_Execute_Default);
			if (RemoveFromParentsCommand == null) RemoveFromParentsCommand = new DelegateCommand(RemoveFromParentsCommand_Execute_Default, RemoveFromParentsCommand_CanExecute_Default);

		}

			private void AddInParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(_getEntitiesForAddInParametersCommand(), Item.Parameters);
			}

			private void RemoveFromParametersCommand_Execute_Default()
			{
				Item.Parameters.Remove(SelectedParametersItem);
			}

			private bool RemoveFromParametersCommand_CanExecute_Default()
			{
				return SelectedParametersItem != null;
			}

			private void AddInParentsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ProductDesignation, ProductDesignationWrapper>(_getEntitiesForAddInParentsCommand(), Item.Parents);
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
		//private Func<Task<List<ProductType>>> _getEntitiesForSelectProductTypeCommand;
		private Func<List<ProductType>> _getEntitiesForSelectProductTypeCommand;
		public ICommand SelectProductTypeCommand { get; private set; }
		public ICommand ClearProductTypeCommand { get; private set; }

		private Func<List<Parameter>> _getEntitiesForAddInParametersCommand;
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
			
			if (_getEntitiesForSelectProductTypeCommand == null) _getEntitiesForSelectProductTypeCommand = () => { return UnitOfWork.Repository<ProductType>().GetAll(); };
			if (SelectProductTypeCommand == null) SelectProductTypeCommand = new DelegateCommand(SelectProductTypeCommand_Execute_Default);
			if (ClearProductTypeCommand == null) ClearProductTypeCommand = new DelegateCommand(ClearProductTypeCommand_Execute_Default);

			
			if (_getEntitiesForAddInParametersCommand == null) _getEntitiesForAddInParametersCommand = () => { return UnitOfWork.Repository<Parameter>().GetAll(); };;
			if (AddInParametersCommand == null) AddInParametersCommand = new DelegateCommand(AddInParametersCommand_Execute_Default);
			if (RemoveFromParametersCommand == null) RemoveFromParametersCommand = new DelegateCommand(RemoveFromParametersCommand_Execute_Default, RemoveFromParametersCommand_CanExecute_Default);

		}

		private void SelectProductTypeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ProductType, ProductTypeWrapper>(_getEntitiesForSelectProductTypeCommand(), nameof(Item.ProductType), Item.ProductType?.Id);
		}

		private void ClearProductTypeCommand_Execute_Default() 
		{
						Item.ProductType = null;
		    
		}

			private void AddInParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(_getEntitiesForAddInParametersCommand(), Item.Parameters);
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
		private Func<List<Parameter>> _getEntitiesForAddInParametersCommand;
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
			
			if (_getEntitiesForAddInParametersCommand == null) _getEntitiesForAddInParametersCommand = () => { return UnitOfWork.Repository<Parameter>().GetAll(); };;
			if (AddInParametersCommand == null) AddInParametersCommand = new DelegateCommand(AddInParametersCommand_Execute_Default);
			if (RemoveFromParametersCommand == null) RemoveFromParametersCommand = new DelegateCommand(RemoveFromParametersCommand_Execute_Default, RemoveFromParametersCommand_CanExecute_Default);

		}

			private void AddInParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(_getEntitiesForAddInParametersCommand(), Item.Parameters);
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
		private Func<List<Parameter>> _getEntitiesForAddInParametersCommand;
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
			
			if (_getEntitiesForAddInParametersCommand == null) _getEntitiesForAddInParametersCommand = () => { return UnitOfWork.Repository<Parameter>().GetAll(); };;
			if (AddInParametersCommand == null) AddInParametersCommand = new DelegateCommand(AddInParametersCommand_Execute_Default);
			if (RemoveFromParametersCommand == null) RemoveFromParametersCommand = new DelegateCommand(RemoveFromParametersCommand_Execute_Default, RemoveFromParametersCommand_CanExecute_Default);

		}

			private void AddInParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(_getEntitiesForAddInParametersCommand(), Item.Parameters);
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


    public partial class StructureCostDetailsViewModel : BaseDetailsViewModel<StructureCostWrapper, StructureCost, AfterSaveStructureCostEvent>
    {

        public StructureCostDetailsViewModel(IUnityContainer container) : base(container) 
		{
		}



    }


    public partial class GlobalPropertiesDetailsViewModel : BaseDetailsViewModel<GlobalPropertiesWrapper, GlobalProperties, AfterSaveGlobalPropertiesEvent>
    {
		//private Func<Task<List<Company>>> _getEntitiesForSelectOurCompanyCommand;
		private Func<List<Company>> _getEntitiesForSelectOurCompanyCommand;
		public ICommand SelectOurCompanyCommand { get; private set; }
		public ICommand ClearOurCompanyCommand { get; private set; }

		//private Func<Task<List<PaymentConditionSet>>> _getEntitiesForSelectStandartPaymentsConditionSetCommand;
		private Func<List<PaymentConditionSet>> _getEntitiesForSelectStandartPaymentsConditionSetCommand;
		public ICommand SelectStandartPaymentsConditionSetCommand { get; private set; }
		public ICommand ClearStandartPaymentsConditionSetCommand { get; private set; }

		//private Func<Task<List<Parameter>>> _getEntitiesForSelectNewProductParameterCommand;
		private Func<List<Parameter>> _getEntitiesForSelectNewProductParameterCommand;
		public ICommand SelectNewProductParameterCommand { get; private set; }
		public ICommand ClearNewProductParameterCommand { get; private set; }

		//private Func<Task<List<ParameterGroup>>> _getEntitiesForSelectNewProductParameterGroupCommand;
		private Func<List<ParameterGroup>> _getEntitiesForSelectNewProductParameterGroupCommand;
		public ICommand SelectNewProductParameterGroupCommand { get; private set; }
		public ICommand ClearNewProductParameterGroupCommand { get; private set; }

		//private Func<Task<List<Parameter>>> _getEntitiesForSelectServiceParameterCommand;
		private Func<List<Parameter>> _getEntitiesForSelectServiceParameterCommand;
		public ICommand SelectServiceParameterCommand { get; private set; }
		public ICommand ClearServiceParameterCommand { get; private set; }

		//private Func<Task<List<Parameter>>> _getEntitiesForSelectSupervisionParameterCommand;
		private Func<List<Parameter>> _getEntitiesForSelectSupervisionParameterCommand;
		public ICommand SelectSupervisionParameterCommand { get; private set; }
		public ICommand ClearSupervisionParameterCommand { get; private set; }

		//private Func<Task<List<ParameterGroup>>> _getEntitiesForSelectVoltageGroupCommand;
		private Func<List<ParameterGroup>> _getEntitiesForSelectVoltageGroupCommand;
		public ICommand SelectVoltageGroupCommand { get; private set; }
		public ICommand ClearVoltageGroupCommand { get; private set; }

		//private Func<Task<List<ParameterGroup>>> _getEntitiesForSelectIsolationMaterialGroupCommand;
		private Func<List<ParameterGroup>> _getEntitiesForSelectIsolationMaterialGroupCommand;
		public ICommand SelectIsolationMaterialGroupCommand { get; private set; }
		public ICommand ClearIsolationMaterialGroupCommand { get; private set; }

		//private Func<Task<List<ParameterGroup>>> _getEntitiesForSelectIsolationColorGroupCommand;
		private Func<List<ParameterGroup>> _getEntitiesForSelectIsolationColorGroupCommand;
		public ICommand SelectIsolationColorGroupCommand { get; private set; }
		public ICommand ClearIsolationColorGroupCommand { get; private set; }

		//private Func<Task<List<ParameterGroup>>> _getEntitiesForSelectIsolationDpuGroupCommand;
		private Func<List<ParameterGroup>> _getEntitiesForSelectIsolationDpuGroupCommand;
		public ICommand SelectIsolationDpuGroupCommand { get; private set; }
		public ICommand ClearIsolationDpuGroupCommand { get; private set; }

		//private Func<Task<List<ParameterGroup>>> _getEntitiesForSelectComplectDesignationGroupCommand;
		private Func<List<ParameterGroup>> _getEntitiesForSelectComplectDesignationGroupCommand;
		public ICommand SelectComplectDesignationGroupCommand { get; private set; }
		public ICommand ClearComplectDesignationGroupCommand { get; private set; }

		//private Func<Task<List<Parameter>>> _getEntitiesForSelectComplectsParameterCommand;
		private Func<List<Parameter>> _getEntitiesForSelectComplectsParameterCommand;
		public ICommand SelectComplectsParameterCommand { get; private set; }
		public ICommand ClearComplectsParameterCommand { get; private set; }

		//private Func<Task<List<ParameterGroup>>> _getEntitiesForSelectComplectsGroupCommand;
		private Func<List<ParameterGroup>> _getEntitiesForSelectComplectsGroupCommand;
		public ICommand SelectComplectsGroupCommand { get; private set; }
		public ICommand ClearComplectsGroupCommand { get; private set; }

		//private Func<Task<List<Employee>>> _getEntitiesForSelectSenderOfferEmployeeCommand;
		private Func<List<Employee>> _getEntitiesForSelectSenderOfferEmployeeCommand;
		public ICommand SelectSenderOfferEmployeeCommand { get; private set; }
		public ICommand ClearSenderOfferEmployeeCommand { get; private set; }

		//private Func<Task<List<ActivityField>>> _getEntitiesForSelectHvtProducersActivityFieldCommand;
		private Func<List<ActivityField>> _getEntitiesForSelectHvtProducersActivityFieldCommand;
		public ICommand SelectHvtProducersActivityFieldCommand { get; private set; }
		public ICommand ClearHvtProducersActivityFieldCommand { get; private set; }

		//private Func<Task<List<PaymentConditionSet>>> _getEntitiesForSelectPaymentConditionSetCommand;
		private Func<List<PaymentConditionSet>> _getEntitiesForSelectPaymentConditionSetCommand;
		public ICommand SelectPaymentConditionSetCommand { get; private set; }
		public ICommand ClearPaymentConditionSetCommand { get; private set; }

		//private Func<Task<List<User>>> _getEntitiesForSelectDeveloperCommand;
		private Func<List<User>> _getEntitiesForSelectDeveloperCommand;
		public ICommand SelectDeveloperCommand { get; private set; }
		public ICommand ClearDeveloperCommand { get; private set; }

		//private Func<Task<List<Product>>> _getEntitiesForSelectProductIncludedDefaultCommand;
		private Func<List<Product>> _getEntitiesForSelectProductIncludedDefaultCommand;
		public ICommand SelectProductIncludedDefaultCommand { get; private set; }
		public ICommand ClearProductIncludedDefaultCommand { get; private set; }


        public GlobalPropertiesDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectOurCompanyCommand == null) _getEntitiesForSelectOurCompanyCommand = () => { return UnitOfWork.Repository<Company>().GetAll(); };
			if (SelectOurCompanyCommand == null) SelectOurCompanyCommand = new DelegateCommand(SelectOurCompanyCommand_Execute_Default);
			if (ClearOurCompanyCommand == null) ClearOurCompanyCommand = new DelegateCommand(ClearOurCompanyCommand_Execute_Default);

			
			if (_getEntitiesForSelectStandartPaymentsConditionSetCommand == null) _getEntitiesForSelectStandartPaymentsConditionSetCommand = () => { return UnitOfWork.Repository<PaymentConditionSet>().GetAll(); };
			if (SelectStandartPaymentsConditionSetCommand == null) SelectStandartPaymentsConditionSetCommand = new DelegateCommand(SelectStandartPaymentsConditionSetCommand_Execute_Default);
			if (ClearStandartPaymentsConditionSetCommand == null) ClearStandartPaymentsConditionSetCommand = new DelegateCommand(ClearStandartPaymentsConditionSetCommand_Execute_Default);

			
			if (_getEntitiesForSelectNewProductParameterCommand == null) _getEntitiesForSelectNewProductParameterCommand = () => { return UnitOfWork.Repository<Parameter>().GetAll(); };
			if (SelectNewProductParameterCommand == null) SelectNewProductParameterCommand = new DelegateCommand(SelectNewProductParameterCommand_Execute_Default);
			if (ClearNewProductParameterCommand == null) ClearNewProductParameterCommand = new DelegateCommand(ClearNewProductParameterCommand_Execute_Default);

			
			if (_getEntitiesForSelectNewProductParameterGroupCommand == null) _getEntitiesForSelectNewProductParameterGroupCommand = () => { return UnitOfWork.Repository<ParameterGroup>().GetAll(); };
			if (SelectNewProductParameterGroupCommand == null) SelectNewProductParameterGroupCommand = new DelegateCommand(SelectNewProductParameterGroupCommand_Execute_Default);
			if (ClearNewProductParameterGroupCommand == null) ClearNewProductParameterGroupCommand = new DelegateCommand(ClearNewProductParameterGroupCommand_Execute_Default);

			
			if (_getEntitiesForSelectServiceParameterCommand == null) _getEntitiesForSelectServiceParameterCommand = () => { return UnitOfWork.Repository<Parameter>().GetAll(); };
			if (SelectServiceParameterCommand == null) SelectServiceParameterCommand = new DelegateCommand(SelectServiceParameterCommand_Execute_Default);
			if (ClearServiceParameterCommand == null) ClearServiceParameterCommand = new DelegateCommand(ClearServiceParameterCommand_Execute_Default);

			
			if (_getEntitiesForSelectSupervisionParameterCommand == null) _getEntitiesForSelectSupervisionParameterCommand = () => { return UnitOfWork.Repository<Parameter>().GetAll(); };
			if (SelectSupervisionParameterCommand == null) SelectSupervisionParameterCommand = new DelegateCommand(SelectSupervisionParameterCommand_Execute_Default);
			if (ClearSupervisionParameterCommand == null) ClearSupervisionParameterCommand = new DelegateCommand(ClearSupervisionParameterCommand_Execute_Default);

			
			if (_getEntitiesForSelectVoltageGroupCommand == null) _getEntitiesForSelectVoltageGroupCommand = () => { return UnitOfWork.Repository<ParameterGroup>().GetAll(); };
			if (SelectVoltageGroupCommand == null) SelectVoltageGroupCommand = new DelegateCommand(SelectVoltageGroupCommand_Execute_Default);
			if (ClearVoltageGroupCommand == null) ClearVoltageGroupCommand = new DelegateCommand(ClearVoltageGroupCommand_Execute_Default);

			
			if (_getEntitiesForSelectIsolationMaterialGroupCommand == null) _getEntitiesForSelectIsolationMaterialGroupCommand = () => { return UnitOfWork.Repository<ParameterGroup>().GetAll(); };
			if (SelectIsolationMaterialGroupCommand == null) SelectIsolationMaterialGroupCommand = new DelegateCommand(SelectIsolationMaterialGroupCommand_Execute_Default);
			if (ClearIsolationMaterialGroupCommand == null) ClearIsolationMaterialGroupCommand = new DelegateCommand(ClearIsolationMaterialGroupCommand_Execute_Default);

			
			if (_getEntitiesForSelectIsolationColorGroupCommand == null) _getEntitiesForSelectIsolationColorGroupCommand = () => { return UnitOfWork.Repository<ParameterGroup>().GetAll(); };
			if (SelectIsolationColorGroupCommand == null) SelectIsolationColorGroupCommand = new DelegateCommand(SelectIsolationColorGroupCommand_Execute_Default);
			if (ClearIsolationColorGroupCommand == null) ClearIsolationColorGroupCommand = new DelegateCommand(ClearIsolationColorGroupCommand_Execute_Default);

			
			if (_getEntitiesForSelectIsolationDpuGroupCommand == null) _getEntitiesForSelectIsolationDpuGroupCommand = () => { return UnitOfWork.Repository<ParameterGroup>().GetAll(); };
			if (SelectIsolationDpuGroupCommand == null) SelectIsolationDpuGroupCommand = new DelegateCommand(SelectIsolationDpuGroupCommand_Execute_Default);
			if (ClearIsolationDpuGroupCommand == null) ClearIsolationDpuGroupCommand = new DelegateCommand(ClearIsolationDpuGroupCommand_Execute_Default);

			
			if (_getEntitiesForSelectComplectDesignationGroupCommand == null) _getEntitiesForSelectComplectDesignationGroupCommand = () => { return UnitOfWork.Repository<ParameterGroup>().GetAll(); };
			if (SelectComplectDesignationGroupCommand == null) SelectComplectDesignationGroupCommand = new DelegateCommand(SelectComplectDesignationGroupCommand_Execute_Default);
			if (ClearComplectDesignationGroupCommand == null) ClearComplectDesignationGroupCommand = new DelegateCommand(ClearComplectDesignationGroupCommand_Execute_Default);

			
			if (_getEntitiesForSelectComplectsParameterCommand == null) _getEntitiesForSelectComplectsParameterCommand = () => { return UnitOfWork.Repository<Parameter>().GetAll(); };
			if (SelectComplectsParameterCommand == null) SelectComplectsParameterCommand = new DelegateCommand(SelectComplectsParameterCommand_Execute_Default);
			if (ClearComplectsParameterCommand == null) ClearComplectsParameterCommand = new DelegateCommand(ClearComplectsParameterCommand_Execute_Default);

			
			if (_getEntitiesForSelectComplectsGroupCommand == null) _getEntitiesForSelectComplectsGroupCommand = () => { return UnitOfWork.Repository<ParameterGroup>().GetAll(); };
			if (SelectComplectsGroupCommand == null) SelectComplectsGroupCommand = new DelegateCommand(SelectComplectsGroupCommand_Execute_Default);
			if (ClearComplectsGroupCommand == null) ClearComplectsGroupCommand = new DelegateCommand(ClearComplectsGroupCommand_Execute_Default);

			
			if (_getEntitiesForSelectSenderOfferEmployeeCommand == null) _getEntitiesForSelectSenderOfferEmployeeCommand = () => { return UnitOfWork.Repository<Employee>().GetAll(); };
			if (SelectSenderOfferEmployeeCommand == null) SelectSenderOfferEmployeeCommand = new DelegateCommand(SelectSenderOfferEmployeeCommand_Execute_Default);
			if (ClearSenderOfferEmployeeCommand == null) ClearSenderOfferEmployeeCommand = new DelegateCommand(ClearSenderOfferEmployeeCommand_Execute_Default);

			
			if (_getEntitiesForSelectHvtProducersActivityFieldCommand == null) _getEntitiesForSelectHvtProducersActivityFieldCommand = () => { return UnitOfWork.Repository<ActivityField>().GetAll(); };
			if (SelectHvtProducersActivityFieldCommand == null) SelectHvtProducersActivityFieldCommand = new DelegateCommand(SelectHvtProducersActivityFieldCommand_Execute_Default);
			if (ClearHvtProducersActivityFieldCommand == null) ClearHvtProducersActivityFieldCommand = new DelegateCommand(ClearHvtProducersActivityFieldCommand_Execute_Default);

			
			if (_getEntitiesForSelectPaymentConditionSetCommand == null) _getEntitiesForSelectPaymentConditionSetCommand = () => { return UnitOfWork.Repository<PaymentConditionSet>().GetAll(); };
			if (SelectPaymentConditionSetCommand == null) SelectPaymentConditionSetCommand = new DelegateCommand(SelectPaymentConditionSetCommand_Execute_Default);
			if (ClearPaymentConditionSetCommand == null) ClearPaymentConditionSetCommand = new DelegateCommand(ClearPaymentConditionSetCommand_Execute_Default);

			
			if (_getEntitiesForSelectDeveloperCommand == null) _getEntitiesForSelectDeveloperCommand = () => { return UnitOfWork.Repository<User>().GetAll(); };
			if (SelectDeveloperCommand == null) SelectDeveloperCommand = new DelegateCommand(SelectDeveloperCommand_Execute_Default);
			if (ClearDeveloperCommand == null) ClearDeveloperCommand = new DelegateCommand(ClearDeveloperCommand_Execute_Default);

			
			if (_getEntitiesForSelectProductIncludedDefaultCommand == null) _getEntitiesForSelectProductIncludedDefaultCommand = () => { return UnitOfWork.Repository<Product>().GetAll(); };
			if (SelectProductIncludedDefaultCommand == null) SelectProductIncludedDefaultCommand = new DelegateCommand(SelectProductIncludedDefaultCommand_Execute_Default);
			if (ClearProductIncludedDefaultCommand == null) ClearProductIncludedDefaultCommand = new DelegateCommand(ClearProductIncludedDefaultCommand_Execute_Default);

		}

		private void SelectOurCompanyCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(_getEntitiesForSelectOurCompanyCommand(), nameof(Item.OurCompany), Item.OurCompany?.Id);
		}

		private void ClearOurCompanyCommand_Execute_Default() 
		{
						Item.OurCompany = null;
		    
		}

		private void SelectStandartPaymentsConditionSetCommand_Execute_Default() 
		{
            SelectAndSetWrapper<PaymentConditionSet, PaymentConditionSetWrapper>(_getEntitiesForSelectStandartPaymentsConditionSetCommand(), nameof(Item.StandartPaymentsConditionSet), Item.StandartPaymentsConditionSet?.Id);
		}

		private void ClearStandartPaymentsConditionSetCommand_Execute_Default() 
		{
						Item.StandartPaymentsConditionSet = null;
		    
		}

		private void SelectNewProductParameterCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Parameter, ParameterWrapper>(_getEntitiesForSelectNewProductParameterCommand(), nameof(Item.NewProductParameter), Item.NewProductParameter?.Id);
		}

		private void ClearNewProductParameterCommand_Execute_Default() 
		{
						Item.NewProductParameter = null;
		    
		}

		private void SelectNewProductParameterGroupCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(_getEntitiesForSelectNewProductParameterGroupCommand(), nameof(Item.NewProductParameterGroup), Item.NewProductParameterGroup?.Id);
		}

		private void ClearNewProductParameterGroupCommand_Execute_Default() 
		{
						Item.NewProductParameterGroup = null;
		    
		}

		private void SelectServiceParameterCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Parameter, ParameterWrapper>(_getEntitiesForSelectServiceParameterCommand(), nameof(Item.ServiceParameter), Item.ServiceParameter?.Id);
		}

		private void ClearServiceParameterCommand_Execute_Default() 
		{
						Item.ServiceParameter = null;
		    
		}

		private void SelectSupervisionParameterCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Parameter, ParameterWrapper>(_getEntitiesForSelectSupervisionParameterCommand(), nameof(Item.SupervisionParameter), Item.SupervisionParameter?.Id);
		}

		private void ClearSupervisionParameterCommand_Execute_Default() 
		{
						Item.SupervisionParameter = null;
		    
		}

		private void SelectVoltageGroupCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(_getEntitiesForSelectVoltageGroupCommand(), nameof(Item.VoltageGroup), Item.VoltageGroup?.Id);
		}

		private void ClearVoltageGroupCommand_Execute_Default() 
		{
						Item.VoltageGroup = null;
		    
		}

		private void SelectIsolationMaterialGroupCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(_getEntitiesForSelectIsolationMaterialGroupCommand(), nameof(Item.IsolationMaterialGroup), Item.IsolationMaterialGroup?.Id);
		}

		private void ClearIsolationMaterialGroupCommand_Execute_Default() 
		{
						Item.IsolationMaterialGroup = null;
		    
		}

		private void SelectIsolationColorGroupCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(_getEntitiesForSelectIsolationColorGroupCommand(), nameof(Item.IsolationColorGroup), Item.IsolationColorGroup?.Id);
		}

		private void ClearIsolationColorGroupCommand_Execute_Default() 
		{
						Item.IsolationColorGroup = null;
		    
		}

		private void SelectIsolationDpuGroupCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(_getEntitiesForSelectIsolationDpuGroupCommand(), nameof(Item.IsolationDpuGroup), Item.IsolationDpuGroup?.Id);
		}

		private void ClearIsolationDpuGroupCommand_Execute_Default() 
		{
						Item.IsolationDpuGroup = null;
		    
		}

		private void SelectComplectDesignationGroupCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(_getEntitiesForSelectComplectDesignationGroupCommand(), nameof(Item.ComplectDesignationGroup), Item.ComplectDesignationGroup?.Id);
		}

		private void ClearComplectDesignationGroupCommand_Execute_Default() 
		{
						Item.ComplectDesignationGroup = null;
		    
		}

		private void SelectComplectsParameterCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Parameter, ParameterWrapper>(_getEntitiesForSelectComplectsParameterCommand(), nameof(Item.ComplectsParameter), Item.ComplectsParameter?.Id);
		}

		private void ClearComplectsParameterCommand_Execute_Default() 
		{
						Item.ComplectsParameter = null;
		    
		}

		private void SelectComplectsGroupCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(_getEntitiesForSelectComplectsGroupCommand(), nameof(Item.ComplectsGroup), Item.ComplectsGroup?.Id);
		}

		private void ClearComplectsGroupCommand_Execute_Default() 
		{
						Item.ComplectsGroup = null;
		    
		}

		private void SelectSenderOfferEmployeeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(_getEntitiesForSelectSenderOfferEmployeeCommand(), nameof(Item.SenderOfferEmployee), Item.SenderOfferEmployee?.Id);
		}

		private void ClearSenderOfferEmployeeCommand_Execute_Default() 
		{
						Item.SenderOfferEmployee = null;
		    
		}

		private void SelectHvtProducersActivityFieldCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ActivityField, ActivityFieldWrapper>(_getEntitiesForSelectHvtProducersActivityFieldCommand(), nameof(Item.HvtProducersActivityField), Item.HvtProducersActivityField?.Id);
		}

		private void ClearHvtProducersActivityFieldCommand_Execute_Default() 
		{
						Item.HvtProducersActivityField = null;
		    
		}

		private void SelectPaymentConditionSetCommand_Execute_Default() 
		{
            SelectAndSetWrapper<PaymentConditionSet, PaymentConditionSetWrapper>(_getEntitiesForSelectPaymentConditionSetCommand(), nameof(Item.PaymentConditionSet), Item.PaymentConditionSet?.Id);
		}

		private void ClearPaymentConditionSetCommand_Execute_Default() 
		{
						Item.PaymentConditionSet = null;
		    
		}

		private void SelectDeveloperCommand_Execute_Default() 
		{
            SelectAndSetWrapper<User, UserWrapper>(_getEntitiesForSelectDeveloperCommand(), nameof(Item.Developer), Item.Developer?.Id);
		}

		private void ClearDeveloperCommand_Execute_Default() 
		{
						Item.Developer = null;
		    
		}

		private void SelectProductIncludedDefaultCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(_getEntitiesForSelectProductIncludedDefaultCommand(), nameof(Item.ProductIncludedDefault), Item.ProductIncludedDefault?.Id);
		}

		private void ClearProductIncludedDefaultCommand_Execute_Default() 
		{
						Item.ProductIncludedDefault = null;
		    
		}



    }


    public partial class AddressDetailsViewModel : BaseDetailsViewModel<AddressWrapper, Address, AfterSaveAddressEvent>
    {
		//private Func<Task<List<Locality>>> _getEntitiesForSelectLocalityCommand;
		private Func<List<Locality>> _getEntitiesForSelectLocalityCommand;
		public ICommand SelectLocalityCommand { get; private set; }
		public ICommand ClearLocalityCommand { get; private set; }


        public AddressDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectLocalityCommand == null) _getEntitiesForSelectLocalityCommand = () => { return UnitOfWork.Repository<Locality>().GetAll(); };
			if (SelectLocalityCommand == null) SelectLocalityCommand = new DelegateCommand(SelectLocalityCommand_Execute_Default);
			if (ClearLocalityCommand == null) ClearLocalityCommand = new DelegateCommand(ClearLocalityCommand_Execute_Default);

		}

		private void SelectLocalityCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Locality, LocalityWrapper>(_getEntitiesForSelectLocalityCommand(), nameof(Item.Locality), Item.Locality?.Id);
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
		//private Func<Task<List<Country>>> _getEntitiesForSelectCountryCommand;
		private Func<List<Country>> _getEntitiesForSelectCountryCommand;
		public ICommand SelectCountryCommand { get; private set; }
		public ICommand ClearCountryCommand { get; private set; }


        public DistrictDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectCountryCommand == null) _getEntitiesForSelectCountryCommand = () => { return UnitOfWork.Repository<Country>().GetAll(); };
			if (SelectCountryCommand == null) SelectCountryCommand = new DelegateCommand(SelectCountryCommand_Execute_Default);
			if (ClearCountryCommand == null) ClearCountryCommand = new DelegateCommand(ClearCountryCommand_Execute_Default);

		}

		private void SelectCountryCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Country, CountryWrapper>(_getEntitiesForSelectCountryCommand(), nameof(Item.Country), Item.Country?.Id);
		}

		private void ClearCountryCommand_Execute_Default() 
		{
						Item.Country = null;
		    
		}



    }


    public partial class LocalityDetailsViewModel : BaseDetailsViewModel<LocalityWrapper, Locality, AfterSaveLocalityEvent>
    {
		//private Func<Task<List<LocalityType>>> _getEntitiesForSelectLocalityTypeCommand;
		private Func<List<LocalityType>> _getEntitiesForSelectLocalityTypeCommand;
		public ICommand SelectLocalityTypeCommand { get; private set; }
		public ICommand ClearLocalityTypeCommand { get; private set; }

		//private Func<Task<List<Region>>> _getEntitiesForSelectRegionCommand;
		private Func<List<Region>> _getEntitiesForSelectRegionCommand;
		public ICommand SelectRegionCommand { get; private set; }
		public ICommand ClearRegionCommand { get; private set; }


        public LocalityDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectLocalityTypeCommand == null) _getEntitiesForSelectLocalityTypeCommand = () => { return UnitOfWork.Repository<LocalityType>().GetAll(); };
			if (SelectLocalityTypeCommand == null) SelectLocalityTypeCommand = new DelegateCommand(SelectLocalityTypeCommand_Execute_Default);
			if (ClearLocalityTypeCommand == null) ClearLocalityTypeCommand = new DelegateCommand(ClearLocalityTypeCommand_Execute_Default);

			
			if (_getEntitiesForSelectRegionCommand == null) _getEntitiesForSelectRegionCommand = () => { return UnitOfWork.Repository<Region>().GetAll(); };
			if (SelectRegionCommand == null) SelectRegionCommand = new DelegateCommand(SelectRegionCommand_Execute_Default);
			if (ClearRegionCommand == null) ClearRegionCommand = new DelegateCommand(ClearRegionCommand_Execute_Default);

		}

		private void SelectLocalityTypeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<LocalityType, LocalityTypeWrapper>(_getEntitiesForSelectLocalityTypeCommand(), nameof(Item.LocalityType), Item.LocalityType?.Id);
		}

		private void ClearLocalityTypeCommand_Execute_Default() 
		{
						Item.LocalityType = null;
		    
		}

		private void SelectRegionCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Region, RegionWrapper>(_getEntitiesForSelectRegionCommand(), nameof(Item.Region), Item.Region?.Id);
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
		//private Func<Task<List<District>>> _getEntitiesForSelectDistrictCommand;
		private Func<List<District>> _getEntitiesForSelectDistrictCommand;
		public ICommand SelectDistrictCommand { get; private set; }
		public ICommand ClearDistrictCommand { get; private set; }


        public RegionDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectDistrictCommand == null) _getEntitiesForSelectDistrictCommand = () => { return UnitOfWork.Repository<District>().GetAll(); };
			if (SelectDistrictCommand == null) SelectDistrictCommand = new DelegateCommand(SelectDistrictCommand_Execute_Default);
			if (ClearDistrictCommand == null) ClearDistrictCommand = new DelegateCommand(ClearDistrictCommand_Execute_Default);

		}

		private void SelectDistrictCommand_Execute_Default() 
		{
            SelectAndSetWrapper<District, DistrictWrapper>(_getEntitiesForSelectDistrictCommand(), nameof(Item.District), Item.District?.Id);
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
		//private Func<Task<List<Offer>>> _getEntitiesForSelectOfferCommand;
		private Func<List<Offer>> _getEntitiesForSelectOfferCommand;
		public ICommand SelectOfferCommand { get; private set; }
		public ICommand ClearOfferCommand { get; private set; }

		//private Func<Task<List<Facility>>> _getEntitiesForSelectFacilityCommand;
		private Func<List<Facility>> _getEntitiesForSelectFacilityCommand;
		public ICommand SelectFacilityCommand { get; private set; }
		public ICommand ClearFacilityCommand { get; private set; }

		//private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		private Func<List<Product>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; private set; }
		public ICommand ClearProductCommand { get; private set; }

		//private Func<Task<List<PaymentConditionSet>>> _getEntitiesForSelectPaymentConditionSetCommand;
		private Func<List<PaymentConditionSet>> _getEntitiesForSelectPaymentConditionSetCommand;
		public ICommand SelectPaymentConditionSetCommand { get; private set; }
		public ICommand ClearPaymentConditionSetCommand { get; private set; }

		private Func<List<ProductIncluded>> _getEntitiesForAddInProductsIncludedCommand;
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
			
			if (_getEntitiesForSelectOfferCommand == null) _getEntitiesForSelectOfferCommand = () => { return UnitOfWork.Repository<Offer>().GetAll(); };
			if (SelectOfferCommand == null) SelectOfferCommand = new DelegateCommand(SelectOfferCommand_Execute_Default);
			if (ClearOfferCommand == null) ClearOfferCommand = new DelegateCommand(ClearOfferCommand_Execute_Default);

			
			if (_getEntitiesForSelectFacilityCommand == null) _getEntitiesForSelectFacilityCommand = () => { return UnitOfWork.Repository<Facility>().GetAll(); };
			if (SelectFacilityCommand == null) SelectFacilityCommand = new DelegateCommand(SelectFacilityCommand_Execute_Default);
			if (ClearFacilityCommand == null) ClearFacilityCommand = new DelegateCommand(ClearFacilityCommand_Execute_Default);

			
			if (_getEntitiesForSelectProductCommand == null) _getEntitiesForSelectProductCommand = () => { return UnitOfWork.Repository<Product>().GetAll(); };
			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute_Default);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute_Default);

			
			if (_getEntitiesForSelectPaymentConditionSetCommand == null) _getEntitiesForSelectPaymentConditionSetCommand = () => { return UnitOfWork.Repository<PaymentConditionSet>().GetAll(); };
			if (SelectPaymentConditionSetCommand == null) SelectPaymentConditionSetCommand = new DelegateCommand(SelectPaymentConditionSetCommand_Execute_Default);
			if (ClearPaymentConditionSetCommand == null) ClearPaymentConditionSetCommand = new DelegateCommand(ClearPaymentConditionSetCommand_Execute_Default);

			
			if (_getEntitiesForAddInProductsIncludedCommand == null) _getEntitiesForAddInProductsIncludedCommand = () => { return UnitOfWork.Repository<ProductIncluded>().GetAll(); };;
			if (AddInProductsIncludedCommand == null) AddInProductsIncludedCommand = new DelegateCommand(AddInProductsIncludedCommand_Execute_Default);
			if (RemoveFromProductsIncludedCommand == null) RemoveFromProductsIncludedCommand = new DelegateCommand(RemoveFromProductsIncludedCommand_Execute_Default, RemoveFromProductsIncludedCommand_CanExecute_Default);

		}

		private void SelectOfferCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Offer, OfferWrapper>(_getEntitiesForSelectOfferCommand(), nameof(Item.Offer), Item.Offer?.Id);
		}

		private void ClearOfferCommand_Execute_Default() 
		{
						Item.Offer = null;
		    
		}

		private void SelectFacilityCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Facility, FacilityWrapper>(_getEntitiesForSelectFacilityCommand(), nameof(Item.Facility), Item.Facility?.Id);
		}

		private void ClearFacilityCommand_Execute_Default() 
		{
						Item.Facility = null;
		    
		}

		private void SelectProductCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(_getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute_Default() 
		{
						Item.Product = null;
		    
		}

		private void SelectPaymentConditionSetCommand_Execute_Default() 
		{
            SelectAndSetWrapper<PaymentConditionSet, PaymentConditionSetWrapper>(_getEntitiesForSelectPaymentConditionSetCommand(), nameof(Item.PaymentConditionSet), Item.PaymentConditionSet?.Id);
		}

		private void ClearPaymentConditionSetCommand_Execute_Default() 
		{
						Item.PaymentConditionSet = null;
		    
		}

			private void AddInProductsIncludedCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ProductIncluded, ProductIncludedWrapper>(_getEntitiesForAddInProductsIncludedCommand(), Item.ProductsIncluded);
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
		private Func<List<PaymentCondition>> _getEntitiesForAddInPaymentConditionsCommand;
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
			
			if (_getEntitiesForAddInPaymentConditionsCommand == null) _getEntitiesForAddInPaymentConditionsCommand = () => { return UnitOfWork.Repository<PaymentCondition>().GetAll(); };;
			if (AddInPaymentConditionsCommand == null) AddInPaymentConditionsCommand = new DelegateCommand(AddInPaymentConditionsCommand_Execute_Default);
			if (RemoveFromPaymentConditionsCommand == null) RemoveFromPaymentConditionsCommand = new DelegateCommand(RemoveFromPaymentConditionsCommand_Execute_Default, RemoveFromPaymentConditionsCommand_CanExecute_Default);

		}

			private void AddInPaymentConditionsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentCondition, PaymentConditionWrapper>(_getEntitiesForAddInPaymentConditionsCommand(), Item.PaymentConditions);
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
		//private Func<Task<List<ProductType>>> _getEntitiesForSelectProductTypeCommand;
		private Func<List<ProductType>> _getEntitiesForSelectProductTypeCommand;
		public ICommand SelectProductTypeCommand { get; private set; }
		public ICommand ClearProductTypeCommand { get; private set; }

		private Func<List<Parameter>> _getEntitiesForAddInParametersCommand;
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

		private Func<List<SumOnDate>> _getEntitiesForAddInPricesCommand;
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

		private Func<List<SumOnDate>> _getEntitiesForAddInFixedCostsCommand;
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
			
			if (_getEntitiesForSelectProductTypeCommand == null) _getEntitiesForSelectProductTypeCommand = () => { return UnitOfWork.Repository<ProductType>().GetAll(); };
			if (SelectProductTypeCommand == null) SelectProductTypeCommand = new DelegateCommand(SelectProductTypeCommand_Execute_Default);
			if (ClearProductTypeCommand == null) ClearProductTypeCommand = new DelegateCommand(ClearProductTypeCommand_Execute_Default);

			
			if (_getEntitiesForAddInParametersCommand == null) _getEntitiesForAddInParametersCommand = () => { return UnitOfWork.Repository<Parameter>().GetAll(); };;
			if (AddInParametersCommand == null) AddInParametersCommand = new DelegateCommand(AddInParametersCommand_Execute_Default);
			if (RemoveFromParametersCommand == null) RemoveFromParametersCommand = new DelegateCommand(RemoveFromParametersCommand_Execute_Default, RemoveFromParametersCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInPricesCommand == null) _getEntitiesForAddInPricesCommand = () => { return UnitOfWork.Repository<SumOnDate>().GetAll(); };;
			if (AddInPricesCommand == null) AddInPricesCommand = new DelegateCommand(AddInPricesCommand_Execute_Default);
			if (RemoveFromPricesCommand == null) RemoveFromPricesCommand = new DelegateCommand(RemoveFromPricesCommand_Execute_Default, RemoveFromPricesCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInFixedCostsCommand == null) _getEntitiesForAddInFixedCostsCommand = () => { return UnitOfWork.Repository<SumOnDate>().GetAll(); };;
			if (AddInFixedCostsCommand == null) AddInFixedCostsCommand = new DelegateCommand(AddInFixedCostsCommand_Execute_Default);
			if (RemoveFromFixedCostsCommand == null) RemoveFromFixedCostsCommand = new DelegateCommand(RemoveFromFixedCostsCommand_Execute_Default, RemoveFromFixedCostsCommand_CanExecute_Default);

		}

		private void SelectProductTypeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ProductType, ProductTypeWrapper>(_getEntitiesForSelectProductTypeCommand(), nameof(Item.ProductType), Item.ProductType?.Id);
		}

		private void ClearProductTypeCommand_Execute_Default() 
		{
		
		    
		}

			private void AddInParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(_getEntitiesForAddInParametersCommand(), Item.Parameters);
			}

			private void RemoveFromParametersCommand_Execute_Default()
			{
				Item.Parameters.Remove(SelectedParametersItem);
			}

			private bool RemoveFromParametersCommand_CanExecute_Default()
			{
				return SelectedParametersItem != null;
			}

			private void AddInPricesCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<SumOnDate, SumOnDateWrapper>(_getEntitiesForAddInPricesCommand(), Item.Prices);
			}

			private void RemoveFromPricesCommand_Execute_Default()
			{
				Item.Prices.Remove(SelectedPricesItem);
			}

			private bool RemoveFromPricesCommand_CanExecute_Default()
			{
				return SelectedPricesItem != null;
			}

			private void AddInFixedCostsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<SumOnDate, SumOnDateWrapper>(_getEntitiesForAddInFixedCostsCommand(), Item.FixedCosts);
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
		//private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		private Func<List<Product>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; private set; }
		public ICommand ClearProductCommand { get; private set; }


        public ProductDependentDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectProductCommand == null) _getEntitiesForSelectProductCommand = () => { return UnitOfWork.Repository<Product>().GetAll(); };
			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute_Default);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute_Default);

		}

		private void SelectProductCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(_getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
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
		//private Func<Task<List<CompanyForm>>> _getEntitiesForSelectFormCommand;
		private Func<List<CompanyForm>> _getEntitiesForSelectFormCommand;
		public ICommand SelectFormCommand { get; private set; }
		public ICommand ClearFormCommand { get; private set; }

		//private Func<Task<List<Company>>> _getEntitiesForSelectParentCompanyCommand;
		private Func<List<Company>> _getEntitiesForSelectParentCompanyCommand;
		public ICommand SelectParentCompanyCommand { get; private set; }
		public ICommand ClearParentCompanyCommand { get; private set; }

		//private Func<Task<List<Address>>> _getEntitiesForSelectAddressLegalCommand;
		private Func<List<Address>> _getEntitiesForSelectAddressLegalCommand;
		public ICommand SelectAddressLegalCommand { get; private set; }
		public ICommand ClearAddressLegalCommand { get; private set; }

		//private Func<Task<List<Address>>> _getEntitiesForSelectAddressPostCommand;
		private Func<List<Address>> _getEntitiesForSelectAddressPostCommand;
		public ICommand SelectAddressPostCommand { get; private set; }
		public ICommand ClearAddressPostCommand { get; private set; }

		private Func<List<BankDetails>> _getEntitiesForAddInBankDetailsListCommand;
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

		private Func<List<ActivityField>> _getEntitiesForAddInActivityFildsCommand;
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
			
			if (_getEntitiesForSelectFormCommand == null) _getEntitiesForSelectFormCommand = () => { return UnitOfWork.Repository<CompanyForm>().GetAll(); };
			if (SelectFormCommand == null) SelectFormCommand = new DelegateCommand(SelectFormCommand_Execute_Default);
			if (ClearFormCommand == null) ClearFormCommand = new DelegateCommand(ClearFormCommand_Execute_Default);

			
			if (_getEntitiesForSelectParentCompanyCommand == null) _getEntitiesForSelectParentCompanyCommand = () => { return UnitOfWork.Repository<Company>().GetAll(); };
			if (SelectParentCompanyCommand == null) SelectParentCompanyCommand = new DelegateCommand(SelectParentCompanyCommand_Execute_Default);
			if (ClearParentCompanyCommand == null) ClearParentCompanyCommand = new DelegateCommand(ClearParentCompanyCommand_Execute_Default);

			
			if (_getEntitiesForSelectAddressLegalCommand == null) _getEntitiesForSelectAddressLegalCommand = () => { return UnitOfWork.Repository<Address>().GetAll(); };
			if (SelectAddressLegalCommand == null) SelectAddressLegalCommand = new DelegateCommand(SelectAddressLegalCommand_Execute_Default);
			if (ClearAddressLegalCommand == null) ClearAddressLegalCommand = new DelegateCommand(ClearAddressLegalCommand_Execute_Default);

			
			if (_getEntitiesForSelectAddressPostCommand == null) _getEntitiesForSelectAddressPostCommand = () => { return UnitOfWork.Repository<Address>().GetAll(); };
			if (SelectAddressPostCommand == null) SelectAddressPostCommand = new DelegateCommand(SelectAddressPostCommand_Execute_Default);
			if (ClearAddressPostCommand == null) ClearAddressPostCommand = new DelegateCommand(ClearAddressPostCommand_Execute_Default);

			
			if (_getEntitiesForAddInBankDetailsListCommand == null) _getEntitiesForAddInBankDetailsListCommand = () => { return UnitOfWork.Repository<BankDetails>().GetAll(); };;
			if (AddInBankDetailsListCommand == null) AddInBankDetailsListCommand = new DelegateCommand(AddInBankDetailsListCommand_Execute_Default);
			if (RemoveFromBankDetailsListCommand == null) RemoveFromBankDetailsListCommand = new DelegateCommand(RemoveFromBankDetailsListCommand_Execute_Default, RemoveFromBankDetailsListCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInActivityFildsCommand == null) _getEntitiesForAddInActivityFildsCommand = () => { return UnitOfWork.Repository<ActivityField>().GetAll(); };;
			if (AddInActivityFildsCommand == null) AddInActivityFildsCommand = new DelegateCommand(AddInActivityFildsCommand_Execute_Default);
			if (RemoveFromActivityFildsCommand == null) RemoveFromActivityFildsCommand = new DelegateCommand(RemoveFromActivityFildsCommand_Execute_Default, RemoveFromActivityFildsCommand_CanExecute_Default);

		}

		private void SelectFormCommand_Execute_Default() 
		{
            SelectAndSetWrapper<CompanyForm, CompanyFormWrapper>(_getEntitiesForSelectFormCommand(), nameof(Item.Form), Item.Form?.Id);
		}

		private void ClearFormCommand_Execute_Default() 
		{
						Item.Form = null;
		    
		}

		private void SelectParentCompanyCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(_getEntitiesForSelectParentCompanyCommand(), nameof(Item.ParentCompany), Item.ParentCompany?.Id);
		}

		private void ClearParentCompanyCommand_Execute_Default() 
		{
						Item.ParentCompany = null;
		    
		}

		private void SelectAddressLegalCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Address, AddressWrapper>(_getEntitiesForSelectAddressLegalCommand(), nameof(Item.AddressLegal), Item.AddressLegal?.Id);
		}

		private void ClearAddressLegalCommand_Execute_Default() 
		{
						Item.AddressLegal = null;
		    
		}

		private void SelectAddressPostCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Address, AddressWrapper>(_getEntitiesForSelectAddressPostCommand(), nameof(Item.AddressPost), Item.AddressPost?.Id);
		}

		private void ClearAddressPostCommand_Execute_Default() 
		{
						Item.AddressPost = null;
		    
		}

			private void AddInBankDetailsListCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<BankDetails, BankDetailsWrapper>(_getEntitiesForAddInBankDetailsListCommand(), Item.BankDetailsList);
			}

			private void RemoveFromBankDetailsListCommand_Execute_Default()
			{
				Item.BankDetailsList.Remove(SelectedBankDetailsListItem);
			}

			private bool RemoveFromBankDetailsListCommand_CanExecute_Default()
			{
				return SelectedBankDetailsListItem != null;
			}

			private void AddInActivityFildsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ActivityField, ActivityFieldWrapper>(_getEntitiesForAddInActivityFildsCommand(), Item.ActivityFilds);
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
		//private Func<Task<List<Company>>> _getEntitiesForSelectContragentCommand;
		private Func<List<Company>> _getEntitiesForSelectContragentCommand;
		public ICommand SelectContragentCommand { get; private set; }
		public ICommand ClearContragentCommand { get; private set; }


        public ContractDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectContragentCommand == null) _getEntitiesForSelectContragentCommand = () => { return UnitOfWork.Repository<Company>().GetAll(); };
			if (SelectContragentCommand == null) SelectContragentCommand = new DelegateCommand(SelectContragentCommand_Execute_Default);
			if (ClearContragentCommand == null) ClearContragentCommand = new DelegateCommand(ClearContragentCommand_Execute_Default);

		}

		private void SelectContragentCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(_getEntitiesForSelectContragentCommand(), nameof(Item.Contragent), Item.Contragent?.Id);
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
		//private Func<Task<List<ParameterGroup>>> _getEntitiesForSelectParameterGroupCommand;
		private Func<List<ParameterGroup>> _getEntitiesForSelectParameterGroupCommand;
		public ICommand SelectParameterGroupCommand { get; private set; }
		public ICommand ClearParameterGroupCommand { get; private set; }

		private Func<List<ParameterRelation>> _getEntitiesForAddInParameterRelationsCommand;
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
			
			if (_getEntitiesForSelectParameterGroupCommand == null) _getEntitiesForSelectParameterGroupCommand = () => { return UnitOfWork.Repository<ParameterGroup>().GetAll(); };
			if (SelectParameterGroupCommand == null) SelectParameterGroupCommand = new DelegateCommand(SelectParameterGroupCommand_Execute_Default);
			if (ClearParameterGroupCommand == null) ClearParameterGroupCommand = new DelegateCommand(ClearParameterGroupCommand_Execute_Default);

			
			if (_getEntitiesForAddInParameterRelationsCommand == null) _getEntitiesForAddInParameterRelationsCommand = () => { return UnitOfWork.Repository<ParameterRelation>().GetAll(); };;
			if (AddInParameterRelationsCommand == null) AddInParameterRelationsCommand = new DelegateCommand(AddInParameterRelationsCommand_Execute_Default);
			if (RemoveFromParameterRelationsCommand == null) RemoveFromParameterRelationsCommand = new DelegateCommand(RemoveFromParameterRelationsCommand_Execute_Default, RemoveFromParameterRelationsCommand_CanExecute_Default);

		}

		private void SelectParameterGroupCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ParameterGroup, ParameterGroupWrapper>(_getEntitiesForSelectParameterGroupCommand(), nameof(Item.ParameterGroup), Item.ParameterGroup?.Id);
		}

		private void ClearParameterGroupCommand_Execute_Default() 
		{
						Item.ParameterGroup = null;
		    
		}

			private void AddInParameterRelationsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ParameterRelation, ParameterRelationWrapper>(_getEntitiesForAddInParameterRelationsCommand(), Item.ParameterRelations);
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
		//private Func<Task<List<Measure>>> _getEntitiesForSelectMeasureCommand;
		private Func<List<Measure>> _getEntitiesForSelectMeasureCommand;
		public ICommand SelectMeasureCommand { get; private set; }
		public ICommand ClearMeasureCommand { get; private set; }


        public ParameterGroupDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectMeasureCommand == null) _getEntitiesForSelectMeasureCommand = () => { return UnitOfWork.Repository<Measure>().GetAll(); };
			if (SelectMeasureCommand == null) SelectMeasureCommand = new DelegateCommand(SelectMeasureCommand_Execute_Default);
			if (ClearMeasureCommand == null) ClearMeasureCommand = new DelegateCommand(ClearMeasureCommand_Execute_Default);

		}

		private void SelectMeasureCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Measure, MeasureWrapper>(_getEntitiesForSelectMeasureCommand(), nameof(Item.Measure), Item.Measure?.Id);
		}

		private void ClearMeasureCommand_Execute_Default() 
		{
						Item.Measure = null;
		    
		}



    }


    public partial class ProductRelationDetailsViewModel : BaseDetailsViewModel<ProductRelationWrapper, ProductRelation, AfterSaveProductRelationEvent>
    {
		private Func<List<Parameter>> _getEntitiesForAddInParentProductParametersCommand;
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

		private Func<List<Parameter>> _getEntitiesForAddInChildProductParametersCommand;
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
			
			if (_getEntitiesForAddInParentProductParametersCommand == null) _getEntitiesForAddInParentProductParametersCommand = () => { return UnitOfWork.Repository<Parameter>().GetAll(); };;
			if (AddInParentProductParametersCommand == null) AddInParentProductParametersCommand = new DelegateCommand(AddInParentProductParametersCommand_Execute_Default);
			if (RemoveFromParentProductParametersCommand == null) RemoveFromParentProductParametersCommand = new DelegateCommand(RemoveFromParentProductParametersCommand_Execute_Default, RemoveFromParentProductParametersCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInChildProductParametersCommand == null) _getEntitiesForAddInChildProductParametersCommand = () => { return UnitOfWork.Repository<Parameter>().GetAll(); };;
			if (AddInChildProductParametersCommand == null) AddInChildProductParametersCommand = new DelegateCommand(AddInChildProductParametersCommand_Execute_Default);
			if (RemoveFromChildProductParametersCommand == null) RemoveFromChildProductParametersCommand = new DelegateCommand(RemoveFromChildProductParametersCommand_Execute_Default, RemoveFromChildProductParametersCommand_CanExecute_Default);

		}

			private void AddInParentProductParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(_getEntitiesForAddInParentProductParametersCommand(), Item.ParentProductParameters);
			}

			private void RemoveFromParentProductParametersCommand_Execute_Default()
			{
				Item.ParentProductParameters.Remove(SelectedParentProductParametersItem);
			}

			private bool RemoveFromParentProductParametersCommand_CanExecute_Default()
			{
				return SelectedParentProductParametersItem != null;
			}

			private void AddInChildProductParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(_getEntitiesForAddInChildProductParametersCommand(), Item.ChildProductParameters);
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
		private Func<List<Parameter>> _getEntitiesForAddInRequiredParametersCommand;
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
			
			if (_getEntitiesForAddInRequiredParametersCommand == null) _getEntitiesForAddInRequiredParametersCommand = () => { return UnitOfWork.Repository<Parameter>().GetAll(); };;
			if (AddInRequiredParametersCommand == null) AddInRequiredParametersCommand = new DelegateCommand(AddInRequiredParametersCommand_Execute_Default);
			if (RemoveFromRequiredParametersCommand == null) RemoveFromRequiredParametersCommand = new DelegateCommand(RemoveFromRequiredParametersCommand_Execute_Default, RemoveFromRequiredParametersCommand_CanExecute_Default);

		}

			private void AddInRequiredParametersCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Parameter, ParameterWrapper>(_getEntitiesForAddInRequiredParametersCommand(), Item.RequiredParameters);
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
		//private Func<Task<List<Facility>>> _getEntitiesForSelectFacilityCommand;
		private Func<List<Facility>> _getEntitiesForSelectFacilityCommand;
		public ICommand SelectFacilityCommand { get; private set; }
		public ICommand ClearFacilityCommand { get; private set; }

		//private Func<Task<List<Product>>> _getEntitiesForSelectProductCommand;
		private Func<List<Product>> _getEntitiesForSelectProductCommand;
		public ICommand SelectProductCommand { get; private set; }
		public ICommand ClearProductCommand { get; private set; }

		//private Func<Task<List<PaymentConditionSet>>> _getEntitiesForSelectPaymentConditionSetCommand;
		private Func<List<PaymentConditionSet>> _getEntitiesForSelectPaymentConditionSetCommand;
		public ICommand SelectPaymentConditionSetCommand { get; private set; }
		public ICommand ClearPaymentConditionSetCommand { get; private set; }

		//private Func<Task<List<Project>>> _getEntitiesForSelectProjectCommand;
		private Func<List<Project>> _getEntitiesForSelectProjectCommand;
		public ICommand SelectProjectCommand { get; private set; }
		public ICommand ClearProjectCommand { get; private set; }

		//private Func<Task<List<Company>>> _getEntitiesForSelectProducerCommand;
		private Func<List<Company>> _getEntitiesForSelectProducerCommand;
		public ICommand SelectProducerCommand { get; private set; }
		public ICommand ClearProducerCommand { get; private set; }

		//private Func<Task<List<Order>>> _getEntitiesForSelectOrderCommand;
		private Func<List<Order>> _getEntitiesForSelectOrderCommand;
		public ICommand SelectOrderCommand { get; private set; }
		public ICommand ClearOrderCommand { get; private set; }

		//private Func<Task<List<Specification>>> _getEntitiesForSelectSpecificationCommand;
		private Func<List<Specification>> _getEntitiesForSelectSpecificationCommand;
		public ICommand SelectSpecificationCommand { get; private set; }
		public ICommand ClearSpecificationCommand { get; private set; }

		//private Func<Task<List<Penalty>>> _getEntitiesForSelectPenaltyCommand;
		private Func<List<Penalty>> _getEntitiesForSelectPenaltyCommand;
		public ICommand SelectPenaltyCommand { get; private set; }
		public ICommand ClearPenaltyCommand { get; private set; }

		//private Func<Task<List<Address>>> _getEntitiesForSelectAddressDeliveryCommand;
		private Func<List<Address>> _getEntitiesForSelectAddressDeliveryCommand;
		public ICommand SelectAddressDeliveryCommand { get; private set; }
		public ICommand ClearAddressDeliveryCommand { get; private set; }

		//private Func<Task<List<FakeData>>> _getEntitiesForSelectFakeDataCommand;
		private Func<List<FakeData>> _getEntitiesForSelectFakeDataCommand;
		public ICommand SelectFakeDataCommand { get; private set; }
		public ICommand ClearFakeDataCommand { get; private set; }

		private Func<List<ProductIncluded>> _getEntitiesForAddInProductsIncludedCommand;
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

		private Func<List<LosingReason>> _getEntitiesForAddInLosingReasonsCommand;
		public ICommand AddInLosingReasonsCommand { get; }
		public ICommand RemoveFromLosingReasonsCommand { get; }
		private LosingReasonWrapper _selectedLosingReasonsItem;
		public LosingReasonWrapper SelectedLosingReasonsItem 
		{ 
			get { return _selectedLosingReasonsItem; }
			set 
			{ 
				if (Equals(_selectedLosingReasonsItem, value)) return;
				_selectedLosingReasonsItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromLosingReasonsCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<List<PaymentActual>> _getEntitiesForAddInPaymentsActualCommand;
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

		private Func<List<PaymentPlanned>> _getEntitiesForAddInPaymentsPlannedCommand;
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

		private Func<List<BankGuarantee>> _getEntitiesForAddInBankGuaranteesCommand;
		public ICommand AddInBankGuaranteesCommand { get; }
		public ICommand RemoveFromBankGuaranteesCommand { get; }
		private BankGuaranteeWrapper _selectedBankGuaranteesItem;
		public BankGuaranteeWrapper SelectedBankGuaranteesItem 
		{ 
			get { return _selectedBankGuaranteesItem; }
			set 
			{ 
				if (Equals(_selectedBankGuaranteesItem, value)) return;
				_selectedBankGuaranteesItem = value;
				OnPropertyChanged();
				((DelegateCommand)RemoveFromBankGuaranteesCommand).RaiseCanExecuteChanged();
			}
		}

		private Func<List<PaymentPlanned>> _getEntitiesForAddInPaymentsPlannedActualCommand;
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

		private Func<List<PaymentPlanned>> _getEntitiesForAddInPaymentsPlannedGeneratedCommand;
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

		private Func<List<PaymentPlanned>> _getEntitiesForAddInPaymentsPlannedCalculatedCommand;
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
			
			if (_getEntitiesForSelectFacilityCommand == null) _getEntitiesForSelectFacilityCommand = () => { return UnitOfWork.Repository<Facility>().GetAll(); };
			if (SelectFacilityCommand == null) SelectFacilityCommand = new DelegateCommand(SelectFacilityCommand_Execute_Default);
			if (ClearFacilityCommand == null) ClearFacilityCommand = new DelegateCommand(ClearFacilityCommand_Execute_Default);

			
			if (_getEntitiesForSelectProductCommand == null) _getEntitiesForSelectProductCommand = () => { return UnitOfWork.Repository<Product>().GetAll(); };
			if (SelectProductCommand == null) SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute_Default);
			if (ClearProductCommand == null) ClearProductCommand = new DelegateCommand(ClearProductCommand_Execute_Default);

			
			if (_getEntitiesForSelectPaymentConditionSetCommand == null) _getEntitiesForSelectPaymentConditionSetCommand = () => { return UnitOfWork.Repository<PaymentConditionSet>().GetAll(); };
			if (SelectPaymentConditionSetCommand == null) SelectPaymentConditionSetCommand = new DelegateCommand(SelectPaymentConditionSetCommand_Execute_Default);
			if (ClearPaymentConditionSetCommand == null) ClearPaymentConditionSetCommand = new DelegateCommand(ClearPaymentConditionSetCommand_Execute_Default);

			
			if (_getEntitiesForSelectProjectCommand == null) _getEntitiesForSelectProjectCommand = () => { return UnitOfWork.Repository<Project>().GetAll(); };
			if (SelectProjectCommand == null) SelectProjectCommand = new DelegateCommand(SelectProjectCommand_Execute_Default);
			if (ClearProjectCommand == null) ClearProjectCommand = new DelegateCommand(ClearProjectCommand_Execute_Default);

			
			if (_getEntitiesForSelectProducerCommand == null) _getEntitiesForSelectProducerCommand = () => { return UnitOfWork.Repository<Company>().GetAll(); };
			if (SelectProducerCommand == null) SelectProducerCommand = new DelegateCommand(SelectProducerCommand_Execute_Default);
			if (ClearProducerCommand == null) ClearProducerCommand = new DelegateCommand(ClearProducerCommand_Execute_Default);

			
			if (_getEntitiesForSelectOrderCommand == null) _getEntitiesForSelectOrderCommand = () => { return UnitOfWork.Repository<Order>().GetAll(); };
			if (SelectOrderCommand == null) SelectOrderCommand = new DelegateCommand(SelectOrderCommand_Execute_Default);
			if (ClearOrderCommand == null) ClearOrderCommand = new DelegateCommand(ClearOrderCommand_Execute_Default);

			
			if (_getEntitiesForSelectSpecificationCommand == null) _getEntitiesForSelectSpecificationCommand = () => { return UnitOfWork.Repository<Specification>().GetAll(); };
			if (SelectSpecificationCommand == null) SelectSpecificationCommand = new DelegateCommand(SelectSpecificationCommand_Execute_Default);
			if (ClearSpecificationCommand == null) ClearSpecificationCommand = new DelegateCommand(ClearSpecificationCommand_Execute_Default);

			
			if (_getEntitiesForSelectPenaltyCommand == null) _getEntitiesForSelectPenaltyCommand = () => { return UnitOfWork.Repository<Penalty>().GetAll(); };
			if (SelectPenaltyCommand == null) SelectPenaltyCommand = new DelegateCommand(SelectPenaltyCommand_Execute_Default);
			if (ClearPenaltyCommand == null) ClearPenaltyCommand = new DelegateCommand(ClearPenaltyCommand_Execute_Default);

			
			if (_getEntitiesForSelectAddressDeliveryCommand == null) _getEntitiesForSelectAddressDeliveryCommand = () => { return UnitOfWork.Repository<Address>().GetAll(); };
			if (SelectAddressDeliveryCommand == null) SelectAddressDeliveryCommand = new DelegateCommand(SelectAddressDeliveryCommand_Execute_Default);
			if (ClearAddressDeliveryCommand == null) ClearAddressDeliveryCommand = new DelegateCommand(ClearAddressDeliveryCommand_Execute_Default);

			
			if (_getEntitiesForSelectFakeDataCommand == null) _getEntitiesForSelectFakeDataCommand = () => { return UnitOfWork.Repository<FakeData>().GetAll(); };
			if (SelectFakeDataCommand == null) SelectFakeDataCommand = new DelegateCommand(SelectFakeDataCommand_Execute_Default);
			if (ClearFakeDataCommand == null) ClearFakeDataCommand = new DelegateCommand(ClearFakeDataCommand_Execute_Default);

			
			if (_getEntitiesForAddInProductsIncludedCommand == null) _getEntitiesForAddInProductsIncludedCommand = () => { return UnitOfWork.Repository<ProductIncluded>().GetAll(); };;
			if (AddInProductsIncludedCommand == null) AddInProductsIncludedCommand = new DelegateCommand(AddInProductsIncludedCommand_Execute_Default);
			if (RemoveFromProductsIncludedCommand == null) RemoveFromProductsIncludedCommand = new DelegateCommand(RemoveFromProductsIncludedCommand_Execute_Default, RemoveFromProductsIncludedCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInLosingReasonsCommand == null) _getEntitiesForAddInLosingReasonsCommand = () => { return UnitOfWork.Repository<LosingReason>().GetAll(); };;
			if (AddInLosingReasonsCommand == null) AddInLosingReasonsCommand = new DelegateCommand(AddInLosingReasonsCommand_Execute_Default);
			if (RemoveFromLosingReasonsCommand == null) RemoveFromLosingReasonsCommand = new DelegateCommand(RemoveFromLosingReasonsCommand_Execute_Default, RemoveFromLosingReasonsCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInPaymentsActualCommand == null) _getEntitiesForAddInPaymentsActualCommand = () => { return UnitOfWork.Repository<PaymentActual>().GetAll(); };;
			if (AddInPaymentsActualCommand == null) AddInPaymentsActualCommand = new DelegateCommand(AddInPaymentsActualCommand_Execute_Default);
			if (RemoveFromPaymentsActualCommand == null) RemoveFromPaymentsActualCommand = new DelegateCommand(RemoveFromPaymentsActualCommand_Execute_Default, RemoveFromPaymentsActualCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInPaymentsPlannedCommand == null) _getEntitiesForAddInPaymentsPlannedCommand = () => { return UnitOfWork.Repository<PaymentPlanned>().GetAll(); };;
			if (AddInPaymentsPlannedCommand == null) AddInPaymentsPlannedCommand = new DelegateCommand(AddInPaymentsPlannedCommand_Execute_Default);
			if (RemoveFromPaymentsPlannedCommand == null) RemoveFromPaymentsPlannedCommand = new DelegateCommand(RemoveFromPaymentsPlannedCommand_Execute_Default, RemoveFromPaymentsPlannedCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInBankGuaranteesCommand == null) _getEntitiesForAddInBankGuaranteesCommand = () => { return UnitOfWork.Repository<BankGuarantee>().GetAll(); };;
			if (AddInBankGuaranteesCommand == null) AddInBankGuaranteesCommand = new DelegateCommand(AddInBankGuaranteesCommand_Execute_Default);
			if (RemoveFromBankGuaranteesCommand == null) RemoveFromBankGuaranteesCommand = new DelegateCommand(RemoveFromBankGuaranteesCommand_Execute_Default, RemoveFromBankGuaranteesCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInPaymentsPlannedActualCommand == null) _getEntitiesForAddInPaymentsPlannedActualCommand = () => { return UnitOfWork.Repository<PaymentPlanned>().GetAll(); };;
			if (AddInPaymentsPlannedActualCommand == null) AddInPaymentsPlannedActualCommand = new DelegateCommand(AddInPaymentsPlannedActualCommand_Execute_Default);
			if (RemoveFromPaymentsPlannedActualCommand == null) RemoveFromPaymentsPlannedActualCommand = new DelegateCommand(RemoveFromPaymentsPlannedActualCommand_Execute_Default, RemoveFromPaymentsPlannedActualCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInPaymentsPlannedGeneratedCommand == null) _getEntitiesForAddInPaymentsPlannedGeneratedCommand = () => { return UnitOfWork.Repository<PaymentPlanned>().GetAll(); };;
			if (AddInPaymentsPlannedGeneratedCommand == null) AddInPaymentsPlannedGeneratedCommand = new DelegateCommand(AddInPaymentsPlannedGeneratedCommand_Execute_Default);
			if (RemoveFromPaymentsPlannedGeneratedCommand == null) RemoveFromPaymentsPlannedGeneratedCommand = new DelegateCommand(RemoveFromPaymentsPlannedGeneratedCommand_Execute_Default, RemoveFromPaymentsPlannedGeneratedCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInPaymentsPlannedCalculatedCommand == null) _getEntitiesForAddInPaymentsPlannedCalculatedCommand = () => { return UnitOfWork.Repository<PaymentPlanned>().GetAll(); };;
			if (AddInPaymentsPlannedCalculatedCommand == null) AddInPaymentsPlannedCalculatedCommand = new DelegateCommand(AddInPaymentsPlannedCalculatedCommand_Execute_Default);
			if (RemoveFromPaymentsPlannedCalculatedCommand == null) RemoveFromPaymentsPlannedCalculatedCommand = new DelegateCommand(RemoveFromPaymentsPlannedCalculatedCommand_Execute_Default, RemoveFromPaymentsPlannedCalculatedCommand_CanExecute_Default);

		}

		private void SelectFacilityCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Facility, FacilityWrapper>(_getEntitiesForSelectFacilityCommand(), nameof(Item.Facility), Item.Facility?.Id);
		}

		private void ClearFacilityCommand_Execute_Default() 
		{
						Item.Facility = null;
		    
		}

		private void SelectProductCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Product, ProductWrapper>(_getEntitiesForSelectProductCommand(), nameof(Item.Product), Item.Product?.Id);
		}

		private void ClearProductCommand_Execute_Default() 
		{
						Item.Product = null;
		    
		}

		private void SelectPaymentConditionSetCommand_Execute_Default() 
		{
            SelectAndSetWrapper<PaymentConditionSet, PaymentConditionSetWrapper>(_getEntitiesForSelectPaymentConditionSetCommand(), nameof(Item.PaymentConditionSet), Item.PaymentConditionSet?.Id);
		}

		private void ClearPaymentConditionSetCommand_Execute_Default() 
		{
						Item.PaymentConditionSet = null;
		    
		}

		private void SelectProjectCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Project, ProjectWrapper>(_getEntitiesForSelectProjectCommand(), nameof(Item.Project), Item.Project?.Id);
		}

		private void ClearProjectCommand_Execute_Default() 
		{
						Item.Project = null;
		    
		}

		private void SelectProducerCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(_getEntitiesForSelectProducerCommand(), nameof(Item.Producer), Item.Producer?.Id);
		}

		private void ClearProducerCommand_Execute_Default() 
		{
						Item.Producer = null;
		    
		}

		private void SelectOrderCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Order, OrderWrapper>(_getEntitiesForSelectOrderCommand(), nameof(Item.Order), Item.Order?.Id);
		}

		private void ClearOrderCommand_Execute_Default() 
		{
						Item.Order = null;
		    
		}

		private void SelectSpecificationCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Specification, SpecificationWrapper>(_getEntitiesForSelectSpecificationCommand(), nameof(Item.Specification), Item.Specification?.Id);
		}

		private void ClearSpecificationCommand_Execute_Default() 
		{
						Item.Specification = null;
		    
		}

		private void SelectPenaltyCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Penalty, PenaltyWrapper>(_getEntitiesForSelectPenaltyCommand(), nameof(Item.Penalty), Item.Penalty?.Id);
		}

		private void ClearPenaltyCommand_Execute_Default() 
		{
						Item.Penalty = null;
		    
		}

		private void SelectAddressDeliveryCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Address, AddressWrapper>(_getEntitiesForSelectAddressDeliveryCommand(), nameof(Item.AddressDelivery), Item.AddressDelivery?.Id);
		}

		private void ClearAddressDeliveryCommand_Execute_Default() 
		{
						Item.AddressDelivery = null;
		    
		}

		private void SelectFakeDataCommand_Execute_Default() 
		{
            SelectAndSetWrapper<FakeData, FakeDataWrapper>(_getEntitiesForSelectFakeDataCommand(), nameof(Item.FakeData), Item.FakeData?.Id);
		}

		private void ClearFakeDataCommand_Execute_Default() 
		{
						Item.FakeData = null;
		    
		}

			private void AddInProductsIncludedCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ProductIncluded, ProductIncludedWrapper>(_getEntitiesForAddInProductsIncludedCommand(), Item.ProductsIncluded);
			}

			private void RemoveFromProductsIncludedCommand_Execute_Default()
			{
				Item.ProductsIncluded.Remove(SelectedProductsIncludedItem);
			}

			private bool RemoveFromProductsIncludedCommand_CanExecute_Default()
			{
				return SelectedProductsIncludedItem != null;
			}

			private void AddInLosingReasonsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<LosingReason, LosingReasonWrapper>(_getEntitiesForAddInLosingReasonsCommand(), Item.LosingReasons);
			}

			private void RemoveFromLosingReasonsCommand_Execute_Default()
			{
				Item.LosingReasons.Remove(SelectedLosingReasonsItem);
			}

			private bool RemoveFromLosingReasonsCommand_CanExecute_Default()
			{
				return SelectedLosingReasonsItem != null;
			}

			private void AddInPaymentsActualCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentActual, PaymentActualWrapper>(_getEntitiesForAddInPaymentsActualCommand(), Item.PaymentsActual);
			}

			private void RemoveFromPaymentsActualCommand_Execute_Default()
			{
				Item.PaymentsActual.Remove(SelectedPaymentsActualItem);
			}

			private bool RemoveFromPaymentsActualCommand_CanExecute_Default()
			{
				return SelectedPaymentsActualItem != null;
			}

			private void AddInPaymentsPlannedCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentPlanned, PaymentPlannedWrapper>(_getEntitiesForAddInPaymentsPlannedCommand(), Item.PaymentsPlanned);
			}

			private void RemoveFromPaymentsPlannedCommand_Execute_Default()
			{
				Item.PaymentsPlanned.Remove(SelectedPaymentsPlannedItem);
			}

			private bool RemoveFromPaymentsPlannedCommand_CanExecute_Default()
			{
				return SelectedPaymentsPlannedItem != null;
			}

			private void AddInBankGuaranteesCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<BankGuarantee, BankGuaranteeWrapper>(_getEntitiesForAddInBankGuaranteesCommand(), Item.BankGuarantees);
			}

			private void RemoveFromBankGuaranteesCommand_Execute_Default()
			{
				Item.BankGuarantees.Remove(SelectedBankGuaranteesItem);
			}

			private bool RemoveFromBankGuaranteesCommand_CanExecute_Default()
			{
				return SelectedBankGuaranteesItem != null;
			}

			private void AddInPaymentsPlannedActualCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentPlanned, PaymentPlannedWrapper>(_getEntitiesForAddInPaymentsPlannedActualCommand(), Item.PaymentsPlannedActual);
			}

			private void RemoveFromPaymentsPlannedActualCommand_Execute_Default()
			{
				Item.PaymentsPlannedActual.Remove(SelectedPaymentsPlannedActualItem);
			}

			private bool RemoveFromPaymentsPlannedActualCommand_CanExecute_Default()
			{
				return SelectedPaymentsPlannedActualItem != null;
			}

			private void AddInPaymentsPlannedGeneratedCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentPlanned, PaymentPlannedWrapper>(_getEntitiesForAddInPaymentsPlannedGeneratedCommand(), Item.PaymentsPlannedGenerated);
			}

			private void RemoveFromPaymentsPlannedGeneratedCommand_Execute_Default()
			{
				Item.PaymentsPlannedGenerated.Remove(SelectedPaymentsPlannedGeneratedItem);
			}

			private bool RemoveFromPaymentsPlannedGeneratedCommand_CanExecute_Default()
			{
				return SelectedPaymentsPlannedGeneratedItem != null;
			}

			private void AddInPaymentsPlannedCalculatedCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentPlanned, PaymentPlannedWrapper>(_getEntitiesForAddInPaymentsPlannedCalculatedCommand(), Item.PaymentsPlannedCalculated);
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
		//private Func<Task<List<DocumentNumber>>> _getEntitiesForSelectNumberCommand;
		private Func<List<DocumentNumber>> _getEntitiesForSelectNumberCommand;
		public ICommand SelectNumberCommand { get; private set; }
		public ICommand ClearNumberCommand { get; private set; }

		//private Func<Task<List<Document>>> _getEntitiesForSelectRequestDocumentCommand;
		private Func<List<Document>> _getEntitiesForSelectRequestDocumentCommand;
		public ICommand SelectRequestDocumentCommand { get; private set; }
		public ICommand ClearRequestDocumentCommand { get; private set; }

		//private Func<Task<List<Employee>>> _getEntitiesForSelectAuthorCommand;
		private Func<List<Employee>> _getEntitiesForSelectAuthorCommand;
		public ICommand SelectAuthorCommand { get; private set; }
		public ICommand ClearAuthorCommand { get; private set; }

		//private Func<Task<List<Employee>>> _getEntitiesForSelectSenderEmployeeCommand;
		private Func<List<Employee>> _getEntitiesForSelectSenderEmployeeCommand;
		public ICommand SelectSenderEmployeeCommand { get; private set; }
		public ICommand ClearSenderEmployeeCommand { get; private set; }

		//private Func<Task<List<Employee>>> _getEntitiesForSelectRecipientEmployeeCommand;
		private Func<List<Employee>> _getEntitiesForSelectRecipientEmployeeCommand;
		public ICommand SelectRecipientEmployeeCommand { get; private set; }
		public ICommand ClearRecipientEmployeeCommand { get; private set; }

		//private Func<Task<List<DocumentsRegistrationDetails>>> _getEntitiesForSelectRegistrationDetailsOfRecipientCommand;
		private Func<List<DocumentsRegistrationDetails>> _getEntitiesForSelectRegistrationDetailsOfRecipientCommand;
		public ICommand SelectRegistrationDetailsOfRecipientCommand { get; private set; }
		public ICommand ClearRegistrationDetailsOfRecipientCommand { get; private set; }

		private Func<List<Employee>> _getEntitiesForAddInCopyToRecipientsCommand;
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
			
			if (_getEntitiesForSelectNumberCommand == null) _getEntitiesForSelectNumberCommand = () => { return UnitOfWork.Repository<DocumentNumber>().GetAll(); };
			if (SelectNumberCommand == null) SelectNumberCommand = new DelegateCommand(SelectNumberCommand_Execute_Default);
			if (ClearNumberCommand == null) ClearNumberCommand = new DelegateCommand(ClearNumberCommand_Execute_Default);

			
			if (_getEntitiesForSelectRequestDocumentCommand == null) _getEntitiesForSelectRequestDocumentCommand = () => { return UnitOfWork.Repository<Document>().GetAll(); };
			if (SelectRequestDocumentCommand == null) SelectRequestDocumentCommand = new DelegateCommand(SelectRequestDocumentCommand_Execute_Default);
			if (ClearRequestDocumentCommand == null) ClearRequestDocumentCommand = new DelegateCommand(ClearRequestDocumentCommand_Execute_Default);

			
			if (_getEntitiesForSelectAuthorCommand == null) _getEntitiesForSelectAuthorCommand = () => { return UnitOfWork.Repository<Employee>().GetAll(); };
			if (SelectAuthorCommand == null) SelectAuthorCommand = new DelegateCommand(SelectAuthorCommand_Execute_Default);
			if (ClearAuthorCommand == null) ClearAuthorCommand = new DelegateCommand(ClearAuthorCommand_Execute_Default);

			
			if (_getEntitiesForSelectSenderEmployeeCommand == null) _getEntitiesForSelectSenderEmployeeCommand = () => { return UnitOfWork.Repository<Employee>().GetAll(); };
			if (SelectSenderEmployeeCommand == null) SelectSenderEmployeeCommand = new DelegateCommand(SelectSenderEmployeeCommand_Execute_Default);
			if (ClearSenderEmployeeCommand == null) ClearSenderEmployeeCommand = new DelegateCommand(ClearSenderEmployeeCommand_Execute_Default);

			
			if (_getEntitiesForSelectRecipientEmployeeCommand == null) _getEntitiesForSelectRecipientEmployeeCommand = () => { return UnitOfWork.Repository<Employee>().GetAll(); };
			if (SelectRecipientEmployeeCommand == null) SelectRecipientEmployeeCommand = new DelegateCommand(SelectRecipientEmployeeCommand_Execute_Default);
			if (ClearRecipientEmployeeCommand == null) ClearRecipientEmployeeCommand = new DelegateCommand(ClearRecipientEmployeeCommand_Execute_Default);

			
			if (_getEntitiesForSelectRegistrationDetailsOfRecipientCommand == null) _getEntitiesForSelectRegistrationDetailsOfRecipientCommand = () => { return UnitOfWork.Repository<DocumentsRegistrationDetails>().GetAll(); };
			if (SelectRegistrationDetailsOfRecipientCommand == null) SelectRegistrationDetailsOfRecipientCommand = new DelegateCommand(SelectRegistrationDetailsOfRecipientCommand_Execute_Default);
			if (ClearRegistrationDetailsOfRecipientCommand == null) ClearRegistrationDetailsOfRecipientCommand = new DelegateCommand(ClearRegistrationDetailsOfRecipientCommand_Execute_Default);

			
			if (_getEntitiesForAddInCopyToRecipientsCommand == null) _getEntitiesForAddInCopyToRecipientsCommand = () => { return UnitOfWork.Repository<Employee>().GetAll(); };;
			if (AddInCopyToRecipientsCommand == null) AddInCopyToRecipientsCommand = new DelegateCommand(AddInCopyToRecipientsCommand_Execute_Default);
			if (RemoveFromCopyToRecipientsCommand == null) RemoveFromCopyToRecipientsCommand = new DelegateCommand(RemoveFromCopyToRecipientsCommand_Execute_Default, RemoveFromCopyToRecipientsCommand_CanExecute_Default);

		}

		private void SelectNumberCommand_Execute_Default() 
		{
            SelectAndSetWrapper<DocumentNumber, DocumentNumberWrapper>(_getEntitiesForSelectNumberCommand(), nameof(Item.Number), Item.Number?.Id);
		}

		private void ClearNumberCommand_Execute_Default() 
		{
						Item.Number = null;
		    
		}

		private void SelectRequestDocumentCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Document, DocumentWrapper>(_getEntitiesForSelectRequestDocumentCommand(), nameof(Item.RequestDocument), Item.RequestDocument?.Id);
		}

		private void ClearRequestDocumentCommand_Execute_Default() 
		{
						Item.RequestDocument = null;
		    
		}

		private void SelectAuthorCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(_getEntitiesForSelectAuthorCommand(), nameof(Item.Author), Item.Author?.Id);
		}

		private void ClearAuthorCommand_Execute_Default() 
		{
						Item.Author = null;
		    
		}

		private void SelectSenderEmployeeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(_getEntitiesForSelectSenderEmployeeCommand(), nameof(Item.SenderEmployee), Item.SenderEmployee?.Id);
		}

		private void ClearSenderEmployeeCommand_Execute_Default() 
		{
						Item.SenderEmployee = null;
		    
		}

		private void SelectRecipientEmployeeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(_getEntitiesForSelectRecipientEmployeeCommand(), nameof(Item.RecipientEmployee), Item.RecipientEmployee?.Id);
		}

		private void ClearRecipientEmployeeCommand_Execute_Default() 
		{
						Item.RecipientEmployee = null;
		    
		}

		private void SelectRegistrationDetailsOfRecipientCommand_Execute_Default() 
		{
            SelectAndSetWrapper<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(_getEntitiesForSelectRegistrationDetailsOfRecipientCommand(), nameof(Item.RegistrationDetailsOfRecipient), Item.RegistrationDetailsOfRecipient?.Id);
		}

		private void ClearRegistrationDetailsOfRecipientCommand_Execute_Default() 
		{
						Item.RegistrationDetailsOfRecipient = null;
		    
		}

			private void AddInCopyToRecipientsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Employee, EmployeeWrapper>(_getEntitiesForAddInCopyToRecipientsCommand(), Item.CopyToRecipients);
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
		//private Func<Task<List<ProductType>>> _getEntitiesForSelectProductTypeCommand;
		private Func<List<ProductType>> _getEntitiesForSelectProductTypeCommand;
		public ICommand SelectProductTypeCommand { get; private set; }
		public ICommand ClearProductTypeCommand { get; private set; }

		//private Func<Task<List<ProductBlock>>> _getEntitiesForSelectProductBlockCommand;
		private Func<List<ProductBlock>> _getEntitiesForSelectProductBlockCommand;
		public ICommand SelectProductBlockCommand { get; private set; }
		public ICommand ClearProductBlockCommand { get; private set; }

		private Func<List<ProductDependent>> _getEntitiesForAddInDependentProductsCommand;
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
			
			if (_getEntitiesForSelectProductTypeCommand == null) _getEntitiesForSelectProductTypeCommand = () => { return UnitOfWork.Repository<ProductType>().GetAll(); };
			if (SelectProductTypeCommand == null) SelectProductTypeCommand = new DelegateCommand(SelectProductTypeCommand_Execute_Default);
			if (ClearProductTypeCommand == null) ClearProductTypeCommand = new DelegateCommand(ClearProductTypeCommand_Execute_Default);

			
			if (_getEntitiesForSelectProductBlockCommand == null) _getEntitiesForSelectProductBlockCommand = () => { return UnitOfWork.Repository<ProductBlock>().GetAll(); };
			if (SelectProductBlockCommand == null) SelectProductBlockCommand = new DelegateCommand(SelectProductBlockCommand_Execute_Default);
			if (ClearProductBlockCommand == null) ClearProductBlockCommand = new DelegateCommand(ClearProductBlockCommand_Execute_Default);

			
			if (_getEntitiesForAddInDependentProductsCommand == null) _getEntitiesForAddInDependentProductsCommand = () => { return UnitOfWork.Repository<ProductDependent>().GetAll(); };;
			if (AddInDependentProductsCommand == null) AddInDependentProductsCommand = new DelegateCommand(AddInDependentProductsCommand_Execute_Default);
			if (RemoveFromDependentProductsCommand == null) RemoveFromDependentProductsCommand = new DelegateCommand(RemoveFromDependentProductsCommand_Execute_Default, RemoveFromDependentProductsCommand_CanExecute_Default);

		}

		private void SelectProductTypeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ProductType, ProductTypeWrapper>(_getEntitiesForSelectProductTypeCommand(), nameof(Item.ProductType), Item.ProductType?.Id);
		}

		private void ClearProductTypeCommand_Execute_Default() 
		{
		
		    
		}

		private void SelectProductBlockCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ProductBlock, ProductBlockWrapper>(_getEntitiesForSelectProductBlockCommand(), nameof(Item.ProductBlock), Item.ProductBlock?.Id);
		}

		private void ClearProductBlockCommand_Execute_Default() 
		{
						Item.ProductBlock = null;
		    
		}

			private void AddInDependentProductsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<ProductDependent, ProductDependentWrapper>(_getEntitiesForAddInDependentProductsCommand(), Item.DependentProducts);
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
		//private Func<Task<List<Project>>> _getEntitiesForSelectProjectCommand;
		private Func<List<Project>> _getEntitiesForSelectProjectCommand;
		public ICommand SelectProjectCommand { get; private set; }
		public ICommand ClearProjectCommand { get; private set; }

		//private Func<Task<List<DocumentNumber>>> _getEntitiesForSelectNumberCommand;
		private Func<List<DocumentNumber>> _getEntitiesForSelectNumberCommand;
		public ICommand SelectNumberCommand { get; private set; }
		public ICommand ClearNumberCommand { get; private set; }

		//private Func<Task<List<Document>>> _getEntitiesForSelectRequestDocumentCommand;
		private Func<List<Document>> _getEntitiesForSelectRequestDocumentCommand;
		public ICommand SelectRequestDocumentCommand { get; private set; }
		public ICommand ClearRequestDocumentCommand { get; private set; }

		//private Func<Task<List<Employee>>> _getEntitiesForSelectAuthorCommand;
		private Func<List<Employee>> _getEntitiesForSelectAuthorCommand;
		public ICommand SelectAuthorCommand { get; private set; }
		public ICommand ClearAuthorCommand { get; private set; }

		//private Func<Task<List<Employee>>> _getEntitiesForSelectSenderEmployeeCommand;
		private Func<List<Employee>> _getEntitiesForSelectSenderEmployeeCommand;
		public ICommand SelectSenderEmployeeCommand { get; private set; }
		public ICommand ClearSenderEmployeeCommand { get; private set; }

		//private Func<Task<List<Employee>>> _getEntitiesForSelectRecipientEmployeeCommand;
		private Func<List<Employee>> _getEntitiesForSelectRecipientEmployeeCommand;
		public ICommand SelectRecipientEmployeeCommand { get; private set; }
		public ICommand ClearRecipientEmployeeCommand { get; private set; }

		//private Func<Task<List<DocumentsRegistrationDetails>>> _getEntitiesForSelectRegistrationDetailsOfRecipientCommand;
		private Func<List<DocumentsRegistrationDetails>> _getEntitiesForSelectRegistrationDetailsOfRecipientCommand;
		public ICommand SelectRegistrationDetailsOfRecipientCommand { get; private set; }
		public ICommand ClearRegistrationDetailsOfRecipientCommand { get; private set; }

		private Func<List<Employee>> _getEntitiesForAddInCopyToRecipientsCommand;
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
			
			if (_getEntitiesForSelectProjectCommand == null) _getEntitiesForSelectProjectCommand = () => { return UnitOfWork.Repository<Project>().GetAll(); };
			if (SelectProjectCommand == null) SelectProjectCommand = new DelegateCommand(SelectProjectCommand_Execute_Default);
			if (ClearProjectCommand == null) ClearProjectCommand = new DelegateCommand(ClearProjectCommand_Execute_Default);

			
			if (_getEntitiesForSelectNumberCommand == null) _getEntitiesForSelectNumberCommand = () => { return UnitOfWork.Repository<DocumentNumber>().GetAll(); };
			if (SelectNumberCommand == null) SelectNumberCommand = new DelegateCommand(SelectNumberCommand_Execute_Default);
			if (ClearNumberCommand == null) ClearNumberCommand = new DelegateCommand(ClearNumberCommand_Execute_Default);

			
			if (_getEntitiesForSelectRequestDocumentCommand == null) _getEntitiesForSelectRequestDocumentCommand = () => { return UnitOfWork.Repository<Document>().GetAll(); };
			if (SelectRequestDocumentCommand == null) SelectRequestDocumentCommand = new DelegateCommand(SelectRequestDocumentCommand_Execute_Default);
			if (ClearRequestDocumentCommand == null) ClearRequestDocumentCommand = new DelegateCommand(ClearRequestDocumentCommand_Execute_Default);

			
			if (_getEntitiesForSelectAuthorCommand == null) _getEntitiesForSelectAuthorCommand = () => { return UnitOfWork.Repository<Employee>().GetAll(); };
			if (SelectAuthorCommand == null) SelectAuthorCommand = new DelegateCommand(SelectAuthorCommand_Execute_Default);
			if (ClearAuthorCommand == null) ClearAuthorCommand = new DelegateCommand(ClearAuthorCommand_Execute_Default);

			
			if (_getEntitiesForSelectSenderEmployeeCommand == null) _getEntitiesForSelectSenderEmployeeCommand = () => { return UnitOfWork.Repository<Employee>().GetAll(); };
			if (SelectSenderEmployeeCommand == null) SelectSenderEmployeeCommand = new DelegateCommand(SelectSenderEmployeeCommand_Execute_Default);
			if (ClearSenderEmployeeCommand == null) ClearSenderEmployeeCommand = new DelegateCommand(ClearSenderEmployeeCommand_Execute_Default);

			
			if (_getEntitiesForSelectRecipientEmployeeCommand == null) _getEntitiesForSelectRecipientEmployeeCommand = () => { return UnitOfWork.Repository<Employee>().GetAll(); };
			if (SelectRecipientEmployeeCommand == null) SelectRecipientEmployeeCommand = new DelegateCommand(SelectRecipientEmployeeCommand_Execute_Default);
			if (ClearRecipientEmployeeCommand == null) ClearRecipientEmployeeCommand = new DelegateCommand(ClearRecipientEmployeeCommand_Execute_Default);

			
			if (_getEntitiesForSelectRegistrationDetailsOfRecipientCommand == null) _getEntitiesForSelectRegistrationDetailsOfRecipientCommand = () => { return UnitOfWork.Repository<DocumentsRegistrationDetails>().GetAll(); };
			if (SelectRegistrationDetailsOfRecipientCommand == null) SelectRegistrationDetailsOfRecipientCommand = new DelegateCommand(SelectRegistrationDetailsOfRecipientCommand_Execute_Default);
			if (ClearRegistrationDetailsOfRecipientCommand == null) ClearRegistrationDetailsOfRecipientCommand = new DelegateCommand(ClearRegistrationDetailsOfRecipientCommand_Execute_Default);

			
			if (_getEntitiesForAddInCopyToRecipientsCommand == null) _getEntitiesForAddInCopyToRecipientsCommand = () => { return UnitOfWork.Repository<Employee>().GetAll(); };;
			if (AddInCopyToRecipientsCommand == null) AddInCopyToRecipientsCommand = new DelegateCommand(AddInCopyToRecipientsCommand_Execute_Default);
			if (RemoveFromCopyToRecipientsCommand == null) RemoveFromCopyToRecipientsCommand = new DelegateCommand(RemoveFromCopyToRecipientsCommand_Execute_Default, RemoveFromCopyToRecipientsCommand_CanExecute_Default);

		}

		private void SelectProjectCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Project, ProjectWrapper>(_getEntitiesForSelectProjectCommand(), nameof(Item.Project), Item.Project?.Id);
		}

		private void ClearProjectCommand_Execute_Default() 
		{
						Item.Project = null;
		    
		}

		private void SelectNumberCommand_Execute_Default() 
		{
            SelectAndSetWrapper<DocumentNumber, DocumentNumberWrapper>(_getEntitiesForSelectNumberCommand(), nameof(Item.Number), Item.Number?.Id);
		}

		private void ClearNumberCommand_Execute_Default() 
		{
						Item.Number = null;
		    
		}

		private void SelectRequestDocumentCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Document, DocumentWrapper>(_getEntitiesForSelectRequestDocumentCommand(), nameof(Item.RequestDocument), Item.RequestDocument?.Id);
		}

		private void ClearRequestDocumentCommand_Execute_Default() 
		{
						Item.RequestDocument = null;
		    
		}

		private void SelectAuthorCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(_getEntitiesForSelectAuthorCommand(), nameof(Item.Author), Item.Author?.Id);
		}

		private void ClearAuthorCommand_Execute_Default() 
		{
						Item.Author = null;
		    
		}

		private void SelectSenderEmployeeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(_getEntitiesForSelectSenderEmployeeCommand(), nameof(Item.SenderEmployee), Item.SenderEmployee?.Id);
		}

		private void ClearSenderEmployeeCommand_Execute_Default() 
		{
						Item.SenderEmployee = null;
		    
		}

		private void SelectRecipientEmployeeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(_getEntitiesForSelectRecipientEmployeeCommand(), nameof(Item.RecipientEmployee), Item.RecipientEmployee?.Id);
		}

		private void ClearRecipientEmployeeCommand_Execute_Default() 
		{
						Item.RecipientEmployee = null;
		    
		}

		private void SelectRegistrationDetailsOfRecipientCommand_Execute_Default() 
		{
            SelectAndSetWrapper<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(_getEntitiesForSelectRegistrationDetailsOfRecipientCommand(), nameof(Item.RegistrationDetailsOfRecipient), Item.RegistrationDetailsOfRecipient?.Id);
		}

		private void ClearRegistrationDetailsOfRecipientCommand_Execute_Default() 
		{
						Item.RegistrationDetailsOfRecipient = null;
		    
		}

			private void AddInCopyToRecipientsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Employee, EmployeeWrapper>(_getEntitiesForAddInCopyToRecipientsCommand(), Item.CopyToRecipients);
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
		//private Func<Task<List<Person>>> _getEntitiesForSelectPersonCommand;
		private Func<List<Person>> _getEntitiesForSelectPersonCommand;
		public ICommand SelectPersonCommand { get; private set; }
		public ICommand ClearPersonCommand { get; private set; }

		//private Func<Task<List<Company>>> _getEntitiesForSelectCompanyCommand;
		private Func<List<Company>> _getEntitiesForSelectCompanyCommand;
		public ICommand SelectCompanyCommand { get; private set; }
		public ICommand ClearCompanyCommand { get; private set; }

		//private Func<Task<List<EmployeesPosition>>> _getEntitiesForSelectPositionCommand;
		private Func<List<EmployeesPosition>> _getEntitiesForSelectPositionCommand;
		public ICommand SelectPositionCommand { get; private set; }
		public ICommand ClearPositionCommand { get; private set; }


        public EmployeeDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectPersonCommand == null) _getEntitiesForSelectPersonCommand = () => { return UnitOfWork.Repository<Person>().GetAll(); };
			if (SelectPersonCommand == null) SelectPersonCommand = new DelegateCommand(SelectPersonCommand_Execute_Default);
			if (ClearPersonCommand == null) ClearPersonCommand = new DelegateCommand(ClearPersonCommand_Execute_Default);

			
			if (_getEntitiesForSelectCompanyCommand == null) _getEntitiesForSelectCompanyCommand = () => { return UnitOfWork.Repository<Company>().GetAll(); };
			if (SelectCompanyCommand == null) SelectCompanyCommand = new DelegateCommand(SelectCompanyCommand_Execute_Default);
			if (ClearCompanyCommand == null) ClearCompanyCommand = new DelegateCommand(ClearCompanyCommand_Execute_Default);

			
			if (_getEntitiesForSelectPositionCommand == null) _getEntitiesForSelectPositionCommand = () => { return UnitOfWork.Repository<EmployeesPosition>().GetAll(); };
			if (SelectPositionCommand == null) SelectPositionCommand = new DelegateCommand(SelectPositionCommand_Execute_Default);
			if (ClearPositionCommand == null) ClearPositionCommand = new DelegateCommand(ClearPositionCommand_Execute_Default);

		}

		private void SelectPersonCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Person, PersonWrapper>(_getEntitiesForSelectPersonCommand(), nameof(Item.Person), Item.Person?.Id);
		}

		private void ClearPersonCommand_Execute_Default() 
		{
						Item.Person = null;
		    
		}

		private void SelectCompanyCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(_getEntitiesForSelectCompanyCommand(), nameof(Item.Company), Item.Company?.Id);
		}

		private void ClearCompanyCommand_Execute_Default() 
		{
						Item.Company = null;
		    
		}

		private void SelectPositionCommand_Execute_Default() 
		{
            SelectAndSetWrapper<EmployeesPosition, EmployeesPositionWrapper>(_getEntitiesForSelectPositionCommand(), nameof(Item.Position), Item.Position?.Id);
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
		//private Func<Task<List<PaymentConditionPoint>>> _getEntitiesForSelectPaymentConditionPointCommand;
		private Func<List<PaymentConditionPoint>> _getEntitiesForSelectPaymentConditionPointCommand;
		public ICommand SelectPaymentConditionPointCommand { get; private set; }
		public ICommand ClearPaymentConditionPointCommand { get; private set; }


        public PaymentConditionDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectPaymentConditionPointCommand == null) _getEntitiesForSelectPaymentConditionPointCommand = () => { return UnitOfWork.Repository<PaymentConditionPoint>().GetAll(); };
			if (SelectPaymentConditionPointCommand == null) SelectPaymentConditionPointCommand = new DelegateCommand(SelectPaymentConditionPointCommand_Execute_Default);
			if (ClearPaymentConditionPointCommand == null) ClearPaymentConditionPointCommand = new DelegateCommand(ClearPaymentConditionPointCommand_Execute_Default);

		}

		private void SelectPaymentConditionPointCommand_Execute_Default() 
		{
            SelectAndSetWrapper<PaymentConditionPoint, PaymentConditionPointWrapper>(_getEntitiesForSelectPaymentConditionPointCommand(), nameof(Item.PaymentConditionPoint), Item.PaymentConditionPoint?.Id);
		}

		private void ClearPaymentConditionPointCommand_Execute_Default() 
		{
						Item.PaymentConditionPoint = null;
		    
		}



    }


    public partial class PaymentDocumentDetailsViewModel : BaseDetailsViewModel<PaymentDocumentWrapper, PaymentDocument, AfterSavePaymentDocumentEvent>
    {
		private Func<List<PaymentActual>> _getEntitiesForAddInPaymentsCommand;
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
			
			if (_getEntitiesForAddInPaymentsCommand == null) _getEntitiesForAddInPaymentsCommand = () => { return UnitOfWork.Repository<PaymentActual>().GetAll(); };;
			if (AddInPaymentsCommand == null) AddInPaymentsCommand = new DelegateCommand(AddInPaymentsCommand_Execute_Default);
			if (RemoveFromPaymentsCommand == null) RemoveFromPaymentsCommand = new DelegateCommand(RemoveFromPaymentsCommand_Execute_Default, RemoveFromPaymentsCommand_CanExecute_Default);

		}

			private void AddInPaymentsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<PaymentActual, PaymentActualWrapper>(_getEntitiesForAddInPaymentsCommand(), Item.Payments);
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
		//private Func<Task<List<FacilityType>>> _getEntitiesForSelectTypeCommand;
		private Func<List<FacilityType>> _getEntitiesForSelectTypeCommand;
		public ICommand SelectTypeCommand { get; private set; }
		public ICommand ClearTypeCommand { get; private set; }

		//private Func<Task<List<Company>>> _getEntitiesForSelectOwnerCompanyCommand;
		private Func<List<Company>> _getEntitiesForSelectOwnerCompanyCommand;
		public ICommand SelectOwnerCompanyCommand { get; private set; }
		public ICommand ClearOwnerCompanyCommand { get; private set; }

		//private Func<Task<List<Address>>> _getEntitiesForSelectAddressCommand;
		private Func<List<Address>> _getEntitiesForSelectAddressCommand;
		public ICommand SelectAddressCommand { get; private set; }
		public ICommand ClearAddressCommand { get; private set; }


        public FacilityDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectTypeCommand == null) _getEntitiesForSelectTypeCommand = () => { return UnitOfWork.Repository<FacilityType>().GetAll(); };
			if (SelectTypeCommand == null) SelectTypeCommand = new DelegateCommand(SelectTypeCommand_Execute_Default);
			if (ClearTypeCommand == null) ClearTypeCommand = new DelegateCommand(ClearTypeCommand_Execute_Default);

			
			if (_getEntitiesForSelectOwnerCompanyCommand == null) _getEntitiesForSelectOwnerCompanyCommand = () => { return UnitOfWork.Repository<Company>().GetAll(); };
			if (SelectOwnerCompanyCommand == null) SelectOwnerCompanyCommand = new DelegateCommand(SelectOwnerCompanyCommand_Execute_Default);
			if (ClearOwnerCompanyCommand == null) ClearOwnerCompanyCommand = new DelegateCommand(ClearOwnerCompanyCommand_Execute_Default);

			
			if (_getEntitiesForSelectAddressCommand == null) _getEntitiesForSelectAddressCommand = () => { return UnitOfWork.Repository<Address>().GetAll(); };
			if (SelectAddressCommand == null) SelectAddressCommand = new DelegateCommand(SelectAddressCommand_Execute_Default);
			if (ClearAddressCommand == null) ClearAddressCommand = new DelegateCommand(ClearAddressCommand_Execute_Default);

		}

		private void SelectTypeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<FacilityType, FacilityTypeWrapper>(_getEntitiesForSelectTypeCommand(), nameof(Item.Type), Item.Type?.Id);
		}

		private void ClearTypeCommand_Execute_Default() 
		{
						Item.Type = null;
		    
		}

		private void SelectOwnerCompanyCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(_getEntitiesForSelectOwnerCompanyCommand(), nameof(Item.OwnerCompany), Item.OwnerCompany?.Id);
		}

		private void ClearOwnerCompanyCommand_Execute_Default() 
		{
						Item.OwnerCompany = null;
		    
		}

		private void SelectAddressCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Address, AddressWrapper>(_getEntitiesForSelectAddressCommand(), nameof(Item.Address), Item.Address?.Id);
		}

		private void ClearAddressCommand_Execute_Default() 
		{
						Item.Address = null;
		    
		}



    }


    public partial class ProjectDetailsViewModel : BaseDetailsViewModel<ProjectWrapper, Project, AfterSaveProjectEvent>
    {
		//private Func<Task<List<ProjectType>>> _getEntitiesForSelectProjectTypeCommand;
		private Func<List<ProjectType>> _getEntitiesForSelectProjectTypeCommand;
		public ICommand SelectProjectTypeCommand { get; private set; }
		public ICommand ClearProjectTypeCommand { get; private set; }

		//private Func<Task<List<User>>> _getEntitiesForSelectManagerCommand;
		private Func<List<User>> _getEntitiesForSelectManagerCommand;
		public ICommand SelectManagerCommand { get; private set; }
		public ICommand ClearManagerCommand { get; private set; }

		private Func<List<Note>> _getEntitiesForAddInNotesCommand;
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
			
			if (_getEntitiesForSelectProjectTypeCommand == null) _getEntitiesForSelectProjectTypeCommand = () => { return UnitOfWork.Repository<ProjectType>().GetAll(); };
			if (SelectProjectTypeCommand == null) SelectProjectTypeCommand = new DelegateCommand(SelectProjectTypeCommand_Execute_Default);
			if (ClearProjectTypeCommand == null) ClearProjectTypeCommand = new DelegateCommand(ClearProjectTypeCommand_Execute_Default);

			
			if (_getEntitiesForSelectManagerCommand == null) _getEntitiesForSelectManagerCommand = () => { return UnitOfWork.Repository<User>().GetAll(); };
			if (SelectManagerCommand == null) SelectManagerCommand = new DelegateCommand(SelectManagerCommand_Execute_Default);
			if (ClearManagerCommand == null) ClearManagerCommand = new DelegateCommand(ClearManagerCommand_Execute_Default);

			
			if (_getEntitiesForAddInNotesCommand == null) _getEntitiesForAddInNotesCommand = () => { return UnitOfWork.Repository<Note>().GetAll(); };;
			if (AddInNotesCommand == null) AddInNotesCommand = new DelegateCommand(AddInNotesCommand_Execute_Default);
			if (RemoveFromNotesCommand == null) RemoveFromNotesCommand = new DelegateCommand(RemoveFromNotesCommand_Execute_Default, RemoveFromNotesCommand_CanExecute_Default);

		}

		private void SelectProjectTypeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<ProjectType, ProjectTypeWrapper>(_getEntitiesForSelectProjectTypeCommand(), nameof(Item.ProjectType), Item.ProjectType?.Id);
		}

		private void ClearProjectTypeCommand_Execute_Default() 
		{
						Item.ProjectType = null;
		    
		}

		private void SelectManagerCommand_Execute_Default() 
		{
            SelectAndSetWrapper<User, UserWrapper>(_getEntitiesForSelectManagerCommand(), nameof(Item.Manager), Item.Manager?.Id);
		}

		private void ClearManagerCommand_Execute_Default() 
		{
						Item.Manager = null;
		    
		}

			private void AddInNotesCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Note, NoteWrapper>(_getEntitiesForAddInNotesCommand(), Item.Notes);
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
		//private Func<Task<List<Contract>>> _getEntitiesForSelectContractCommand;
		private Func<List<Contract>> _getEntitiesForSelectContractCommand;
		public ICommand SelectContractCommand { get; private set; }
		public ICommand ClearContractCommand { get; private set; }


        public SpecificationDetailsViewModel(IUnityContainer container) : base(container) 
		{
			
			if (_getEntitiesForSelectContractCommand == null) _getEntitiesForSelectContractCommand = () => { return UnitOfWork.Repository<Contract>().GetAll(); };
			if (SelectContractCommand == null) SelectContractCommand = new DelegateCommand(SelectContractCommand_Execute_Default);
			if (ClearContractCommand == null) ClearContractCommand = new DelegateCommand(ClearContractCommand_Execute_Default);

		}

		private void SelectContractCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Contract, ContractWrapper>(_getEntitiesForSelectContractCommand(), nameof(Item.Contract), Item.Contract?.Id);
		}

		private void ClearContractCommand_Execute_Default() 
		{
						Item.Contract = null;
		    
		}



    }


    public partial class TenderDetailsViewModel : BaseDetailsViewModel<TenderWrapper, Tender, AfterSaveTenderEvent>
    {
		//private Func<Task<List<Project>>> _getEntitiesForSelectProjectCommand;
		private Func<List<Project>> _getEntitiesForSelectProjectCommand;
		public ICommand SelectProjectCommand { get; private set; }
		public ICommand ClearProjectCommand { get; private set; }

		//private Func<Task<List<Company>>> _getEntitiesForSelectWinnerCommand;
		private Func<List<Company>> _getEntitiesForSelectWinnerCommand;
		public ICommand SelectWinnerCommand { get; private set; }
		public ICommand ClearWinnerCommand { get; private set; }

		private Func<List<TenderType>> _getEntitiesForAddInTypesCommand;
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

		private Func<List<Company>> _getEntitiesForAddInParticipantsCommand;
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
			
			if (_getEntitiesForSelectProjectCommand == null) _getEntitiesForSelectProjectCommand = () => { return UnitOfWork.Repository<Project>().GetAll(); };
			if (SelectProjectCommand == null) SelectProjectCommand = new DelegateCommand(SelectProjectCommand_Execute_Default);
			if (ClearProjectCommand == null) ClearProjectCommand = new DelegateCommand(ClearProjectCommand_Execute_Default);

			
			if (_getEntitiesForSelectWinnerCommand == null) _getEntitiesForSelectWinnerCommand = () => { return UnitOfWork.Repository<Company>().GetAll(); };
			if (SelectWinnerCommand == null) SelectWinnerCommand = new DelegateCommand(SelectWinnerCommand_Execute_Default);
			if (ClearWinnerCommand == null) ClearWinnerCommand = new DelegateCommand(ClearWinnerCommand_Execute_Default);

			
			if (_getEntitiesForAddInTypesCommand == null) _getEntitiesForAddInTypesCommand = () => { return UnitOfWork.Repository<TenderType>().GetAll(); };;
			if (AddInTypesCommand == null) AddInTypesCommand = new DelegateCommand(AddInTypesCommand_Execute_Default);
			if (RemoveFromTypesCommand == null) RemoveFromTypesCommand = new DelegateCommand(RemoveFromTypesCommand_Execute_Default, RemoveFromTypesCommand_CanExecute_Default);

			
			if (_getEntitiesForAddInParticipantsCommand == null) _getEntitiesForAddInParticipantsCommand = () => { return UnitOfWork.Repository<Company>().GetAll(); };;
			if (AddInParticipantsCommand == null) AddInParticipantsCommand = new DelegateCommand(AddInParticipantsCommand_Execute_Default);
			if (RemoveFromParticipantsCommand == null) RemoveFromParticipantsCommand = new DelegateCommand(RemoveFromParticipantsCommand_Execute_Default, RemoveFromParticipantsCommand_CanExecute_Default);

		}

		private void SelectProjectCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Project, ProjectWrapper>(_getEntitiesForSelectProjectCommand(), nameof(Item.Project), Item.Project?.Id);
		}

		private void ClearProjectCommand_Execute_Default() 
		{
						Item.Project = null;
		    
		}

		private void SelectWinnerCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Company, CompanyWrapper>(_getEntitiesForSelectWinnerCommand(), nameof(Item.Winner), Item.Winner?.Id);
		}

		private void ClearWinnerCommand_Execute_Default() 
		{
						Item.Winner = null;
		    
		}

			private void AddInTypesCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<TenderType, TenderTypeWrapper>(_getEntitiesForAddInTypesCommand(), Item.Types);
			}

			private void RemoveFromTypesCommand_Execute_Default()
			{
				Item.Types.Remove(SelectedTypesItem);
			}

			private bool RemoveFromTypesCommand_CanExecute_Default()
			{
				return SelectedTypesItem != null;
			}

			private void AddInParticipantsCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<Company, CompanyWrapper>(_getEntitiesForAddInParticipantsCommand(), Item.Participants);
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
		//private Func<Task<List<Employee>>> _getEntitiesForSelectEmployeeCommand;
		private Func<List<Employee>> _getEntitiesForSelectEmployeeCommand;
		public ICommand SelectEmployeeCommand { get; private set; }
		public ICommand ClearEmployeeCommand { get; private set; }

		private Func<List<UserRole>> _getEntitiesForAddInRolesCommand;
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
			
			if (_getEntitiesForSelectEmployeeCommand == null) _getEntitiesForSelectEmployeeCommand = () => { return UnitOfWork.Repository<Employee>().GetAll(); };
			if (SelectEmployeeCommand == null) SelectEmployeeCommand = new DelegateCommand(SelectEmployeeCommand_Execute_Default);
			if (ClearEmployeeCommand == null) ClearEmployeeCommand = new DelegateCommand(ClearEmployeeCommand_Execute_Default);

			
			if (_getEntitiesForAddInRolesCommand == null) _getEntitiesForAddInRolesCommand = () => { return UnitOfWork.Repository<UserRole>().GetAll(); };;
			if (AddInRolesCommand == null) AddInRolesCommand = new DelegateCommand(AddInRolesCommand_Execute_Default);
			if (RemoveFromRolesCommand == null) RemoveFromRolesCommand = new DelegateCommand(RemoveFromRolesCommand_Execute_Default, RemoveFromRolesCommand_CanExecute_Default);

		}

		private void SelectEmployeeCommand_Execute_Default() 
		{
            SelectAndSetWrapper<Employee, EmployeeWrapper>(_getEntitiesForSelectEmployeeCommand(), nameof(Item.Employee), Item.Employee?.Id);
		}

		private void ClearEmployeeCommand_Execute_Default() 
		{
						Item.Employee = null;
		    
		}

			private void AddInRolesCommand_Execute_Default()
			{
				SelectAndAddInListWrapper<UserRole, UserRoleWrapper>(_getEntitiesForAddInRolesCommand(), Item.Roles);
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
