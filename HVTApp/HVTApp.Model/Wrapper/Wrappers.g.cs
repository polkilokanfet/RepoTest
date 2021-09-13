using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.Model.Wrapper
{
	public partial class CountryUnionWrapper : WrapperBase<CountryUnion>
	{
	    public CountryUnionWrapper(CountryUnion model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<CountryWrapper> Countries { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.Countries == null) throw new ArgumentException("Countries cannot be null");
          Countries = new ValidatableChangeTrackingCollection<CountryWrapper>(Model.Countries.Select(e => new CountryWrapper(e)));
          RegisterCollection(Countries, Model.Countries);
        }
	}

		public partial class BankGuaranteeWrapper : WrapperBase<BankGuarantee>
	{
	    public BankGuaranteeWrapper(BankGuarantee model) : base(model) { }
        #region SimpleProperties
        //Percent
        public System.Double Percent
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double PercentOriginalValue => GetOriginalValue<System.Double>(nameof(Percent));
        public bool PercentIsChanged => GetIsChanged(nameof(Percent));
        //Days
        public System.Int32 Days
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 DaysOriginalValue => GetOriginalValue<System.Int32>(nameof(Days));
        public bool DaysIsChanged => GetIsChanged(nameof(Days));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public BankGuaranteeTypeWrapper BankGuaranteeType 
        {
            get { return GetWrapper<BankGuaranteeTypeWrapper>(); }
            set { SetComplexValue<BankGuaranteeType, BankGuaranteeTypeWrapper>(BankGuaranteeType, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<BankGuaranteeTypeWrapper>(nameof(BankGuaranteeType), Model.BankGuaranteeType == null ? null : new BankGuaranteeTypeWrapper(Model.BankGuaranteeType));
        }
	}

		public partial class BankGuaranteeTypeWrapper : WrapperBase<BankGuaranteeType>
	{
	    public BankGuaranteeTypeWrapper(BankGuaranteeType model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class BudgetWrapper : WrapperBase<Budget>
	{
	    public BudgetWrapper(Budget model) : base(model) { }
        #region SimpleProperties
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //DateStart
        public System.DateTime DateStart
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateStartOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateStart));
        public bool DateStartIsChanged => GetIsChanged(nameof(DateStart));
        //DateFinish
        public System.DateTime DateFinish
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateFinishOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateFinish));
        public bool DateFinishIsChanged => GetIsChanged(nameof(DateFinish));
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<BudgetUnitWrapper> Units { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.Units == null) throw new ArgumentException("Units cannot be null");
          Units = new ValidatableChangeTrackingCollection<BudgetUnitWrapper>(Model.Units.Select(e => new BudgetUnitWrapper(e)));
          RegisterCollection(Units, Model.Units);
        }
	}

		public partial class BudgetUnitWrapper : WrapperBase<BudgetUnit>
	{
	    public BudgetUnitWrapper(BudgetUnit model) : base(model) { }
        #region SimpleProperties
        //OrderInTakeDate
        public System.DateTime OrderInTakeDate
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime OrderInTakeDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(OrderInTakeDate));
        public bool OrderInTakeDateIsChanged => GetIsChanged(nameof(OrderInTakeDate));
        //RealizationDate
        public System.DateTime RealizationDate
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime RealizationDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(RealizationDate));
        public bool RealizationDateIsChanged => GetIsChanged(nameof(RealizationDate));
        //OrderInTakeDateByManager
        public System.DateTime OrderInTakeDateByManager
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime OrderInTakeDateByManagerOriginalValue => GetOriginalValue<System.DateTime>(nameof(OrderInTakeDateByManager));
        public bool OrderInTakeDateByManagerIsChanged => GetIsChanged(nameof(OrderInTakeDateByManager));
        //RealizationDateByManager
        public System.DateTime RealizationDateByManager
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime RealizationDateByManagerOriginalValue => GetOriginalValue<System.DateTime>(nameof(RealizationDateByManager));
        public bool RealizationDateByManagerIsChanged => GetIsChanged(nameof(RealizationDateByManager));
        //Cost
        public System.Double Cost
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));
        //CostByManager
        public System.Double CostByManager
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CostByManagerOriginalValue => GetOriginalValue<System.Double>(nameof(CostByManager));
        public bool CostByManagerIsChanged => GetIsChanged(nameof(CostByManager));
        //IsRemoved
        public System.Boolean IsRemoved
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsRemovedOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsRemoved));
        public bool IsRemovedIsChanged => GetIsChanged(nameof(IsRemoved));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public SalesUnitWrapper SalesUnit 
        {
            get { return GetWrapper<SalesUnitWrapper>(); }
            set { SetComplexValue<SalesUnit, SalesUnitWrapper>(SalesUnit, value); }
        }
	    public PaymentConditionSetWrapper PaymentConditionSet 
        {
            get { return GetWrapper<PaymentConditionSetWrapper>(); }
            set { SetComplexValue<PaymentConditionSet, PaymentConditionSetWrapper>(PaymentConditionSet, value); }
        }
	    public PaymentConditionSetWrapper PaymentConditionSetByManager 
        {
            get { return GetWrapper<PaymentConditionSetWrapper>(); }
            set { SetComplexValue<PaymentConditionSet, PaymentConditionSetWrapper>(PaymentConditionSetByManager, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<SalesUnitWrapper>(nameof(SalesUnit), Model.SalesUnit == null ? null : new SalesUnitWrapper(Model.SalesUnit));
            InitializeComplexProperty<PaymentConditionSetWrapper>(nameof(PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetWrapper(Model.PaymentConditionSet));
            InitializeComplexProperty<PaymentConditionSetWrapper>(nameof(PaymentConditionSetByManager), Model.PaymentConditionSetByManager == null ? null : new PaymentConditionSetWrapper(Model.PaymentConditionSetByManager));
        }
	}

		public partial class ConstructorParametersListWrapper : WrapperBase<ConstructorParametersList>
	{
	    public ConstructorParametersListWrapper(ConstructorParametersList model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);
        }
	}

		public partial class ConstructorsParametersWrapper : WrapperBase<ConstructorsParameters>
	{
	    public ConstructorsParametersWrapper(ConstructorsParameters model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<UserWrapper> Constructors { get; private set; }
        public IValidatableChangeTrackingCollection<ConstructorParametersListWrapper> PatametersLists { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.Constructors == null) throw new ArgumentException("Constructors cannot be null");
          Constructors = new ValidatableChangeTrackingCollection<UserWrapper>(Model.Constructors.Select(e => new UserWrapper(e)));
          RegisterCollection(Constructors, Model.Constructors);
          if (Model.PatametersLists == null) throw new ArgumentException("PatametersLists cannot be null");
          PatametersLists = new ValidatableChangeTrackingCollection<ConstructorParametersListWrapper>(Model.PatametersLists.Select(e => new ConstructorParametersListWrapper(e)));
          RegisterCollection(PatametersLists, Model.PatametersLists);
        }
	}

		public partial class CostsPercentsWrapper : WrapperBase<CostsPercents>
	{
	    public CostsPercentsWrapper(CostsPercents model) : base(model) { }
        #region SimpleProperties
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //ManagmentCosts
        public System.Double ManagmentCosts
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double ManagmentCostsOriginalValue => GetOriginalValue<System.Double>(nameof(ManagmentCosts));
        public bool ManagmentCostsIsChanged => GetIsChanged(nameof(ManagmentCosts));
        //EconomicCosts
        public System.Double EconomicCosts
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double EconomicCostsOriginalValue => GetOriginalValue<System.Double>(nameof(EconomicCosts));
        public bool EconomicCostsIsChanged => GetIsChanged(nameof(EconomicCosts));
        //CommercialCosts
        public System.Double CommercialCosts
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CommercialCostsOriginalValue => GetOriginalValue<System.Double>(nameof(CommercialCosts));
        public bool CommercialCostsIsChanged => GetIsChanged(nameof(CommercialCosts));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class CreateNewProductTaskWrapper : WrapperBase<CreateNewProductTask>
	{
	    public CreateNewProductTaskWrapper(CreateNewProductTask model) : base(model) { }
        #region SimpleProperties
        //Designation
        public System.String Designation
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DesignationOriginalValue => GetOriginalValue<System.String>(nameof(Designation));
        public bool DesignationIsChanged => GetIsChanged(nameof(Designation));
        //StructureCostNumber
        public System.String StructureCostNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String StructureCostNumberOriginalValue => GetOriginalValue<System.String>(nameof(StructureCostNumber));
        public bool StructureCostNumberIsChanged => GetIsChanged(nameof(StructureCostNumber));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public ProductWrapper Product 
        {
            get { return GetWrapper<ProductWrapper>(); }
            set { SetComplexValue<Product, ProductWrapper>(Product, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));
        }
	}

		public partial class DirectumTaskWrapper : WrapperBase<DirectumTask>
	{
	    public DirectumTaskWrapper(DirectumTask model) : base(model) { }
        #region SimpleProperties
        //StartPerformer
        public System.Nullable<System.DateTime> StartPerformer
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> StartPerformerOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(StartPerformer));
        public bool StartPerformerIsChanged => GetIsChanged(nameof(StartPerformer));
        //FinishPlan
        public System.DateTime FinishPlan
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime FinishPlanOriginalValue => GetOriginalValue<System.DateTime>(nameof(FinishPlan));
        public bool FinishPlanIsChanged => GetIsChanged(nameof(FinishPlan));
        //FinishPerformer
        public System.Nullable<System.DateTime> FinishPerformer
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> FinishPerformerOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(FinishPerformer));
        public bool FinishPerformerIsChanged => GetIsChanged(nameof(FinishPerformer));
        //FinishAuthor
        public System.Nullable<System.DateTime> FinishAuthor
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> FinishAuthorOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(FinishAuthor));
        public bool FinishAuthorIsChanged => GetIsChanged(nameof(FinishAuthor));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public DirectumTaskGroupWrapper Group 
        {
            get { return GetWrapper<DirectumTaskGroupWrapper>(); }
            set { SetComplexValue<DirectumTaskGroup, DirectumTaskGroupWrapper>(Group, value); }
        }
	    public UserWrapper Performer 
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(Performer, value); }
        }
	    public DirectumTaskWrapper ParentTask 
        {
            get { return GetWrapper<DirectumTaskWrapper>(); }
            set { SetComplexValue<DirectumTask, DirectumTaskWrapper>(ParentTask, value); }
        }
	    public DirectumTaskWrapper PreviousTask 
        {
            get { return GetWrapper<DirectumTaskWrapper>(); }
            set { SetComplexValue<DirectumTask, DirectumTaskWrapper>(PreviousTask, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<DirectumTaskMessageWrapper> Messages { get; private set; }
        public IValidatableChangeTrackingCollection<DirectumTaskWrapper> Childs { get; private set; }
        public IValidatableChangeTrackingCollection<DirectumTaskWrapper> Parallel { get; private set; }
        public IValidatableChangeTrackingCollection<DirectumTaskWrapper> Next { get; private set; }
        #endregion
        #region GetProperties
        public System.Nullable<System.DateTime> StartResult => GetValue<System.Nullable<System.DateTime>>(); 
        public System.String Status => GetValue<System.String>(); 
        public System.Boolean IsActual => GetValue<System.Boolean>(); 
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<DirectumTaskGroupWrapper>(nameof(Group), Model.Group == null ? null : new DirectumTaskGroupWrapper(Model.Group));
            InitializeComplexProperty<UserWrapper>(nameof(Performer), Model.Performer == null ? null : new UserWrapper(Model.Performer));
            InitializeComplexProperty<DirectumTaskWrapper>(nameof(ParentTask), Model.ParentTask == null ? null : new DirectumTaskWrapper(Model.ParentTask));
            InitializeComplexProperty<DirectumTaskWrapper>(nameof(PreviousTask), Model.PreviousTask == null ? null : new DirectumTaskWrapper(Model.PreviousTask));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.Messages == null) throw new ArgumentException("Messages cannot be null");
          Messages = new ValidatableChangeTrackingCollection<DirectumTaskMessageWrapper>(Model.Messages.Select(e => new DirectumTaskMessageWrapper(e)));
          RegisterCollection(Messages, Model.Messages);
          if (Model.Childs == null) throw new ArgumentException("Childs cannot be null");
          Childs = new ValidatableChangeTrackingCollection<DirectumTaskWrapper>(Model.Childs.Select(e => new DirectumTaskWrapper(e)));
          RegisterCollection(Childs, Model.Childs);
          if (Model.Parallel == null) throw new ArgumentException("Parallel cannot be null");
          Parallel = new ValidatableChangeTrackingCollection<DirectumTaskWrapper>(Model.Parallel.Select(e => new DirectumTaskWrapper(e)));
          RegisterCollection(Parallel, Model.Parallel);
          if (Model.Next == null) throw new ArgumentException("Next cannot be null");
          Next = new ValidatableChangeTrackingCollection<DirectumTaskWrapper>(Model.Next.Select(e => new DirectumTaskWrapper(e)));
          RegisterCollection(Next, Model.Next);
        }
	}

		public partial class DirectumTaskGroupWrapper : WrapperBase<DirectumTaskGroup>
	{
	    public DirectumTaskGroupWrapper(DirectumTaskGroup model) : base(model) { }
        #region SimpleProperties
        //Title
        public System.String Title
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String TitleOriginalValue => GetOriginalValue<System.String>(nameof(Title));
        public bool TitleIsChanged => GetIsChanged(nameof(Title));
        //StartAuthor
        public System.DateTime StartAuthor
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime StartAuthorOriginalValue => GetOriginalValue<System.DateTime>(nameof(StartAuthor));
        public bool StartAuthorIsChanged => GetIsChanged(nameof(StartAuthor));
        //IsStoped
        public System.Boolean IsStoped
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsStopedOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsStoped));
        public bool IsStopedIsChanged => GetIsChanged(nameof(IsStoped));
        //Priority
        public HVTApp.Model.POCOs.DirectumTaskPriority Priority
        {
          get { return GetValue<HVTApp.Model.POCOs.DirectumTaskPriority>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.DirectumTaskPriority PriorityOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.DirectumTaskPriority>(nameof(Priority));
        public bool PriorityIsChanged => GetIsChanged(nameof(Priority));
        //Message
        public System.String Message
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String MessageOriginalValue => GetOriginalValue<System.String>(nameof(Message));
        public bool MessageIsChanged => GetIsChanged(nameof(Message));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public UserWrapper Author 
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(Author, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<UserWrapper> Observers { get; private set; }
        public IValidatableChangeTrackingCollection<DirectumTaskGroupFileWrapper> Files { get; private set; }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<UserWrapper>(nameof(Author), Model.Author == null ? null : new UserWrapper(Model.Author));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.Observers == null) throw new ArgumentException("Observers cannot be null");
          Observers = new ValidatableChangeTrackingCollection<UserWrapper>(Model.Observers.Select(e => new UserWrapper(e)));
          RegisterCollection(Observers, Model.Observers);
          if (Model.Files == null) throw new ArgumentException("Files cannot be null");
          Files = new ValidatableChangeTrackingCollection<DirectumTaskGroupFileWrapper>(Model.Files.Select(e => new DirectumTaskGroupFileWrapper(e)));
          RegisterCollection(Files, Model.Files);
        }
	}

		public partial class DirectumTaskGroupFileWrapper : WrapperBase<DirectumTaskGroupFile>
	{
	    public DirectumTaskGroupFileWrapper(DirectumTaskGroupFile model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //LoadMoment
        public System.DateTime LoadMoment
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime LoadMomentOriginalValue => GetOriginalValue<System.DateTime>(nameof(LoadMoment));
        public bool LoadMomentIsChanged => GetIsChanged(nameof(LoadMoment));
        //DirectumTaskGroupId
        public System.Guid DirectumTaskGroupId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid DirectumTaskGroupIdOriginalValue => GetOriginalValue<System.Guid>(nameof(DirectumTaskGroupId));
        public bool DirectumTaskGroupIdIsChanged => GetIsChanged(nameof(DirectumTaskGroupId));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public UserWrapper Author 
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(Author, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<UserWrapper>(nameof(Author), Model.Author == null ? null : new UserWrapper(Model.Author));
        }
	}

		public partial class DirectumTaskMessageWrapper : WrapperBase<DirectumTaskMessage>
	{
	    public DirectumTaskMessageWrapper(DirectumTaskMessage model) : base(model) { }
        #region SimpleProperties
        //Moment
        public System.DateTime Moment
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime MomentOriginalValue => GetOriginalValue<System.DateTime>(nameof(Moment));
        public bool MomentIsChanged => GetIsChanged(nameof(Moment));
        //Message
        public System.String Message
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String MessageOriginalValue => GetOriginalValue<System.String>(nameof(Message));
        public bool MessageIsChanged => GetIsChanged(nameof(Message));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public UserWrapper Author 
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(Author, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<UserWrapper>(nameof(Author), Model.Author == null ? null : new UserWrapper(Model.Author));
        }
	}

		public partial class DocumentNumberWrapper : WrapperBase<DocumentNumber>
	{
	    public DocumentNumberWrapper(DocumentNumber model) : base(model) { }
        #region SimpleProperties
        //Number
        public System.Int32 Number
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 NumberOriginalValue => GetOriginalValue<System.Int32>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class IncomingRequestWrapper : WrapperBase<IncomingRequest>
	{
	    public IncomingRequestWrapper(IncomingRequest model) : base(model) { }
        #region SimpleProperties
        //IsActual
        public System.Boolean IsActual
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsActualOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsActual));
        public bool IsActualIsChanged => GetIsChanged(nameof(IsActual));
        //InstructionDate
        public System.Nullable<System.DateTime> InstructionDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> InstructionDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(InstructionDate));
        public bool InstructionDateIsChanged => GetIsChanged(nameof(InstructionDate));
        //DoneDate
        public System.Nullable<System.DateTime> DoneDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> DoneDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DoneDate));
        public bool DoneDateIsChanged => GetIsChanged(nameof(DoneDate));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public DocumentWrapper Document 
        {
            get { return GetWrapper<DocumentWrapper>(); }
            set { SetComplexValue<Document, DocumentWrapper>(Document, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<EmployeeWrapper> Performers { get; private set; }
        #endregion
        #region GetProperties
        public System.Boolean IsDone => GetValue<System.Boolean>(); 
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<DocumentWrapper>(nameof(Document), Model.Document == null ? null : new DocumentWrapper(Model.Document));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.Performers == null) throw new ArgumentException("Performers cannot be null");
          Performers = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.Performers.Select(e => new EmployeeWrapper(e)));
          RegisterCollection(Performers, Model.Performers);
        }
	}

		public partial class LaborHourCostWrapper : WrapperBase<LaborHourCost>
	{
	    public LaborHourCostWrapper(LaborHourCost model) : base(model) { }
        #region SimpleProperties
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //Sum
        public System.Double Sum
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
        public bool SumIsChanged => GetIsChanged(nameof(Sum));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class LaborHoursWrapper : WrapperBase<LaborHours>
	{
	    public LaborHoursWrapper(LaborHours model) : base(model) { }
        #region SimpleProperties
        //Amount
        public System.Double Amount
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double AmountOriginalValue => GetOriginalValue<System.Double>(nameof(Amount));
        public bool AmountIsChanged => GetIsChanged(nameof(Amount));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);
        }
	}

		public partial class LogUnitWrapper : WrapperBase<LogUnit>
	{
	    public LogUnitWrapper(LogUnit model) : base(model) { }
        #region SimpleProperties
        //Moment
        public System.DateTime Moment
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime MomentOriginalValue => GetOriginalValue<System.DateTime>(nameof(Moment));
        public bool MomentIsChanged => GetIsChanged(nameof(Moment));
        //Head
        public System.String Head
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String HeadOriginalValue => GetOriginalValue<System.String>(nameof(Head));
        public bool HeadIsChanged => GetIsChanged(nameof(Head));
        //Message
        public System.String Message
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String MessageOriginalValue => GetOriginalValue<System.String>(nameof(Message));
        public bool MessageIsChanged => GetIsChanged(nameof(Message));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public UserWrapper Author 
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(Author, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<UserWrapper>(nameof(Author), Model.Author == null ? null : new UserWrapper(Model.Author));
        }
	}

		public partial class LosingReasonWrapper : WrapperBase<LosingReason>
	{
	    public LosingReasonWrapper(LosingReason model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class MarketFieldWrapper : WrapperBase<MarketField>
	{
	    public MarketFieldWrapper(MarketField model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ActivityFieldWrapper> ActivityFields { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.ActivityFields == null) throw new ArgumentException("ActivityFields cannot be null");
          ActivityFields = new ValidatableChangeTrackingCollection<ActivityFieldWrapper>(Model.ActivityFields.Select(e => new ActivityFieldWrapper(e)));
          RegisterCollection(ActivityFields, Model.ActivityFields);
        }
	}

		public partial class PaymentActualWrapper : WrapperBase<PaymentActual>
	{
	    public PaymentActualWrapper(PaymentActual model) : base(model) { }
        #region SimpleProperties
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //Sum
        public System.Double Sum
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
        public bool SumIsChanged => GetIsChanged(nameof(Sum));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class PaymentConditionPointWrapper : WrapperBase<PaymentConditionPoint>
	{
	    public PaymentConditionPointWrapper(PaymentConditionPoint model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //PaymentConditionPointEnum
        public HVTApp.Model.POCOs.PaymentConditionPointEnum PaymentConditionPointEnum
        {
          get { return GetValue<HVTApp.Model.POCOs.PaymentConditionPointEnum>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.PaymentConditionPointEnum PaymentConditionPointEnumOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.PaymentConditionPointEnum>(nameof(PaymentConditionPointEnum));
        public bool PaymentConditionPointEnumIsChanged => GetIsChanged(nameof(PaymentConditionPointEnum));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class PaymentPlannedWrapper : WrapperBase<PaymentPlanned>
	{
	    public PaymentPlannedWrapper(PaymentPlanned model) : base(model) { }
        #region SimpleProperties
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //DateCalculated
        public System.DateTime DateCalculated
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateCalculatedOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateCalculated));
        public bool DateCalculatedIsChanged => GetIsChanged(nameof(DateCalculated));
        //Part
        public System.Double Part
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double PartOriginalValue => GetOriginalValue<System.Double>(nameof(Part));
        public bool PartIsChanged => GetIsChanged(nameof(Part));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public PaymentConditionWrapper Condition 
        {
            get { return GetWrapper<PaymentConditionWrapper>(); }
            set { SetComplexValue<PaymentCondition, PaymentConditionWrapper>(Condition, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<PaymentConditionWrapper>(nameof(Condition), Model.Condition == null ? null : new PaymentConditionWrapper(Model.Condition));
        }
	}

		public partial class PenaltyWrapper : WrapperBase<Penalty>
	{
	    public PenaltyWrapper(Penalty model) : base(model) { }
        #region SimpleProperties
        //PercentPerDay
        public System.Double PercentPerDay
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double PercentPerDayOriginalValue => GetOriginalValue<System.Double>(nameof(PercentPerDay));
        public bool PercentPerDayIsChanged => GetIsChanged(nameof(PercentPerDay));
        //PercentLimit
        public System.Double PercentLimit
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double PercentLimitOriginalValue => GetOriginalValue<System.Double>(nameof(PercentLimit));
        public bool PercentLimitIsChanged => GetIsChanged(nameof(PercentLimit));
        //PenaltyPaid
        public System.Double PenaltyPaid
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double PenaltyPaidOriginalValue => GetOriginalValue<System.Double>(nameof(PenaltyPaid));
        public bool PenaltyPaidIsChanged => GetIsChanged(nameof(PenaltyPaid));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class PriceCalculationWrapper : WrapperBase<PriceCalculation>
	{
	    public PriceCalculationWrapper(PriceCalculation model) : base(model) { }
        #region SimpleProperties
        //TaskOpenMoment
        public System.Nullable<System.DateTime> TaskOpenMoment
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> TaskOpenMomentOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(TaskOpenMoment));
        public bool TaskOpenMomentIsChanged => GetIsChanged(nameof(TaskOpenMoment));
        //TaskCloseMoment
        public System.Nullable<System.DateTime> TaskCloseMoment
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> TaskCloseMomentOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(TaskCloseMoment));
        public bool TaskCloseMomentIsChanged => GetIsChanged(nameof(TaskCloseMoment));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //IsNeedExcelFile
        public System.Boolean IsNeedExcelFile
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsNeedExcelFileOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsNeedExcelFile));
        public bool IsNeedExcelFileIsChanged => GetIsChanged(nameof(IsNeedExcelFile));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public UserWrapper Initiator 
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(Initiator, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<PriceCalculationItemWrapper> PriceCalculationItems { get; private set; }
        public IValidatableChangeTrackingCollection<PriceCalculationFileWrapper> Files { get; private set; }
        #endregion
        #region GetProperties
        public System.String Name => GetValue<System.String>(); 
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<UserWrapper>(nameof(Initiator), Model.Initiator == null ? null : new UserWrapper(Model.Initiator));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.PriceCalculationItems == null) throw new ArgumentException("PriceCalculationItems cannot be null");
          PriceCalculationItems = new ValidatableChangeTrackingCollection<PriceCalculationItemWrapper>(Model.PriceCalculationItems.Select(e => new PriceCalculationItemWrapper(e)));
          RegisterCollection(PriceCalculationItems, Model.PriceCalculationItems);
          if (Model.Files == null) throw new ArgumentException("Files cannot be null");
          Files = new ValidatableChangeTrackingCollection<PriceCalculationFileWrapper>(Model.Files.Select(e => new PriceCalculationFileWrapper(e)));
          RegisterCollection(Files, Model.Files);
        }
	}

		public partial class PriceCalculationFileWrapper : WrapperBase<PriceCalculationFile>
	{
	    public PriceCalculationFileWrapper(PriceCalculationFile model) : base(model) { }
        #region SimpleProperties
        //CreationMoment
        public System.DateTime CreationMoment
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime CreationMomentOriginalValue => GetOriginalValue<System.DateTime>(nameof(CreationMoment));
        public bool CreationMomentIsChanged => GetIsChanged(nameof(CreationMoment));
        //CalculationId
        public System.Guid CalculationId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid CalculationIdOriginalValue => GetOriginalValue<System.Guid>(nameof(CalculationId));
        public bool CalculationIdIsChanged => GetIsChanged(nameof(CalculationId));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class PriceCalculationItemWrapper : WrapperBase<PriceCalculationItem>
	{
	    public PriceCalculationItemWrapper(PriceCalculationItem model) : base(model) { }
        #region SimpleProperties
        //PriceCalculationId
        public System.Guid PriceCalculationId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid PriceCalculationIdOriginalValue => GetOriginalValue<System.Guid>(nameof(PriceCalculationId));
        public bool PriceCalculationIdIsChanged => GetIsChanged(nameof(PriceCalculationId));
        //OrderInTakeDate
        public System.Nullable<System.DateTime> OrderInTakeDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> OrderInTakeDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(OrderInTakeDate));
        public bool OrderInTakeDateIsChanged => GetIsChanged(nameof(OrderInTakeDate));
        //RealizationDate
        public System.Nullable<System.DateTime> RealizationDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> RealizationDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(RealizationDate));
        public bool RealizationDateIsChanged => GetIsChanged(nameof(RealizationDate));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public PaymentConditionSetWrapper PaymentConditionSet 
        {
            get { return GetWrapper<PaymentConditionSetWrapper>(); }
            set { SetComplexValue<PaymentConditionSet, PaymentConditionSetWrapper>(PaymentConditionSet, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<SalesUnitWrapper> SalesUnits { get; private set; }
        public IValidatableChangeTrackingCollection<StructureCostWrapper> StructureCosts { get; private set; }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<PaymentConditionSetWrapper>(nameof(PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetWrapper(Model.PaymentConditionSet));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
          SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(Model.SalesUnits.Select(e => new SalesUnitWrapper(e)));
          RegisterCollection(SalesUnits, Model.SalesUnits);
          if (Model.StructureCosts == null) throw new ArgumentException("StructureCosts cannot be null");
          StructureCosts = new ValidatableChangeTrackingCollection<StructureCostWrapper>(Model.StructureCosts.Select(e => new StructureCostWrapper(e)));
          RegisterCollection(StructureCosts, Model.StructureCosts);
        }
	}

		public partial class ProductCategoryWrapper : WrapperBase<ProductCategory>
	{
	    public ProductCategoryWrapper(ProductCategory model) : base(model) { }
        #region SimpleProperties
        //NameFull
        public System.String NameFull
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameFullOriginalValue => GetOriginalValue<System.String>(nameof(NameFull));
        public bool NameFullIsChanged => GetIsChanged(nameof(NameFull));
        //NameShort
        public System.String NameShort
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameShortOriginalValue => GetOriginalValue<System.String>(nameof(NameShort));
        public bool NameShortIsChanged => GetIsChanged(nameof(NameShort));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);
        }
	}

		public partial class ProductCategoryPriceAndCostWrapper : WrapperBase<ProductCategoryPriceAndCost>
	{
	    public ProductCategoryPriceAndCostWrapper(ProductCategoryPriceAndCost model) : base(model) { }
        #region SimpleProperties
        //Cost
        public System.Double Cost
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));
        //Price
        public System.Double Price
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double PriceOriginalValue => GetOriginalValue<System.Double>(nameof(Price));
        public bool PriceIsChanged => GetIsChanged(nameof(Price));
        //StructureCost
        public System.String StructureCost
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String StructureCostOriginalValue => GetOriginalValue<System.String>(nameof(StructureCost));
        public bool StructureCostIsChanged => GetIsChanged(nameof(StructureCost));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public ProductCategoryWrapper Category 
        {
            get { return GetWrapper<ProductCategoryWrapper>(); }
            set { SetComplexValue<ProductCategory, ProductCategoryWrapper>(Category, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductCategoryWrapper>(nameof(Category), Model.Category == null ? null : new ProductCategoryWrapper(Model.Category));
        }
	}

		public partial class ProductIncludedWrapper : WrapperBase<ProductIncluded>
	{
	    public ProductIncludedWrapper(ProductIncluded model) : base(model) { }
        #region SimpleProperties
        //Amount
        public System.Int32 Amount
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 AmountOriginalValue => GetOriginalValue<System.Int32>(nameof(Amount));
        public bool AmountIsChanged => GetIsChanged(nameof(Amount));
        //CustomFixedPrice
        public System.Nullable<System.Double> CustomFixedPrice
        {
          get { return GetValue<System.Nullable<System.Double>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Double> CustomFixedPriceOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(CustomFixedPrice));
        public bool CustomFixedPriceIsChanged => GetIsChanged(nameof(CustomFixedPrice));
        //ParentsCount
        public System.Int32 ParentsCount
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 ParentsCountOriginalValue => GetOriginalValue<System.Int32>(nameof(ParentsCount));
        public bool ParentsCountIsChanged => GetIsChanged(nameof(ParentsCount));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public ProductWrapper Product 
        {
            get { return GetWrapper<ProductWrapper>(); }
            set { SetComplexValue<Product, ProductWrapper>(Product, value); }
        }
        #endregion
        #region GetProperties
        public System.Double AmountOnUnit => GetValue<System.Double>(); 
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));
        }
	}

		public partial class ProductDesignationWrapper : WrapperBase<ProductDesignation>
	{
	    public ProductDesignationWrapper(ProductDesignation model) : base(model) { }
        #region SimpleProperties
        //Designation
        public System.String Designation
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DesignationOriginalValue => GetOriginalValue<System.String>(nameof(Designation));
        public bool DesignationIsChanged => GetIsChanged(nameof(Designation));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }
        public IValidatableChangeTrackingCollection<ProductDesignationWrapper> Parents { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);
          if (Model.Parents == null) throw new ArgumentException("Parents cannot be null");
          Parents = new ValidatableChangeTrackingCollection<ProductDesignationWrapper>(Model.Parents.Select(e => new ProductDesignationWrapper(e)));
          RegisterCollection(Parents, Model.Parents);
        }
	}

		public partial class ProductTypeWrapper : WrapperBase<ProductType>
	{
	    public ProductTypeWrapper(ProductType model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class ProductTypeDesignationWrapper : WrapperBase<ProductTypeDesignation>
	{
	    public ProductTypeDesignationWrapper(ProductTypeDesignation model) : base(model) { }
        #region SimpleProperties
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public ProductTypeWrapper ProductType 
        {
            get { return GetWrapper<ProductTypeWrapper>(); }
            set { SetComplexValue<ProductType, ProductTypeWrapper>(ProductType, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductTypeWrapper>(nameof(ProductType), Model.ProductType == null ? null : new ProductTypeWrapper(Model.ProductType));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);
        }
	}

		public partial class ProjectTypeWrapper : WrapperBase<ProjectType>
	{
	    public ProjectTypeWrapper(ProjectType model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class StandartMarginalIncomeWrapper : WrapperBase<StandartMarginalIncome>
	{
	    public StandartMarginalIncomeWrapper(StandartMarginalIncome model) : base(model) { }
        #region SimpleProperties
        //MarginalIncome
        public System.Double MarginalIncome
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double MarginalIncomeOriginalValue => GetOriginalValue<System.Double>(nameof(MarginalIncome));
        public bool MarginalIncomeIsChanged => GetIsChanged(nameof(MarginalIncome));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);
        }
	}

		public partial class StandartProductionTermWrapper : WrapperBase<StandartProductionTerm>
	{
	    public StandartProductionTermWrapper(StandartProductionTerm model) : base(model) { }
        #region SimpleProperties
        //ProductionTerm
        public System.Int32 ProductionTerm
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 ProductionTermOriginalValue => GetOriginalValue<System.Int32>(nameof(ProductionTerm));
        public bool ProductionTermIsChanged => GetIsChanged(nameof(ProductionTerm));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);
        }
	}

		public partial class StructureCostWrapper : WrapperBase<StructureCost>
	{
	    public StructureCostWrapper(StructureCost model) : base(model) { }
        #region SimpleProperties
        //PriceCalculationItemId
        public System.Guid PriceCalculationItemId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid PriceCalculationItemIdOriginalValue => GetOriginalValue<System.Guid>(nameof(PriceCalculationItemId));
        public bool PriceCalculationItemIdIsChanged => GetIsChanged(nameof(PriceCalculationItemId));
        //Number
        public System.String Number
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));
        //Amount
        public System.Double Amount
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double AmountOriginalValue => GetOriginalValue<System.Double>(nameof(Amount));
        public bool AmountIsChanged => GetIsChanged(nameof(Amount));
        //UnitPrice
        public System.Nullable<System.Double> UnitPrice
        {
          get { return GetValue<System.Nullable<System.Double>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Double> UnitPriceOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(UnitPrice));
        public bool UnitPriceIsChanged => GetIsChanged(nameof(UnitPrice));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region GetProperties
        public System.Nullable<System.Double> Total => GetValue<System.Nullable<System.Double>>(); 
        #endregion
	}

		public partial class SupervisionWrapper : WrapperBase<Supervision>
	{
	    public SupervisionWrapper(Supervision model) : base(model) { }
        #region SimpleProperties
        //DateFinish
        public System.Nullable<System.DateTime> DateFinish
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> DateFinishOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateFinish));
        public bool DateFinishIsChanged => GetIsChanged(nameof(DateFinish));
        //DateRequired
        public System.Nullable<System.DateTime> DateRequired
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> DateRequiredOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateRequired));
        public bool DateRequiredIsChanged => GetIsChanged(nameof(DateRequired));
        //ClientOrderNumber
        public System.String ClientOrderNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ClientOrderNumberOriginalValue => GetOriginalValue<System.String>(nameof(ClientOrderNumber));
        public bool ClientOrderNumberIsChanged => GetIsChanged(nameof(ClientOrderNumber));
        //ServiceOrderNumber
        public System.String ServiceOrderNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ServiceOrderNumberOriginalValue => GetOriginalValue<System.String>(nameof(ServiceOrderNumber));
        public bool ServiceOrderNumberIsChanged => GetIsChanged(nameof(ServiceOrderNumber));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public SalesUnitWrapper SalesUnit 
        {
            get { return GetWrapper<SalesUnitWrapper>(); }
            set { SetComplexValue<SalesUnit, SalesUnitWrapper>(SalesUnit, value); }
        }
	    public SalesUnitWrapper SupervisionUnit 
        {
            get { return GetWrapper<SalesUnitWrapper>(); }
            set { SetComplexValue<SalesUnit, SalesUnitWrapper>(SupervisionUnit, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<SalesUnitWrapper>(nameof(SalesUnit), Model.SalesUnit == null ? null : new SalesUnitWrapper(Model.SalesUnit));
            InitializeComplexProperty<SalesUnitWrapper>(nameof(SupervisionUnit), Model.SupervisionUnit == null ? null : new SalesUnitWrapper(Model.SupervisionUnit));
        }
	}

		public partial class AnswerFileTceWrapper : WrapperBase<AnswerFileTce>
	{
	    public AnswerFileTceWrapper(AnswerFileTce model) : base(model) { }
        #region SimpleProperties
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //TechnicalRequrementsTaskId
        public System.Guid TechnicalRequrementsTaskId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid TechnicalRequrementsTaskIdOriginalValue => GetOriginalValue<System.Guid>(nameof(TechnicalRequrementsTaskId));
        public bool TechnicalRequrementsTaskIdIsChanged => GetIsChanged(nameof(TechnicalRequrementsTaskId));
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //IsActual
        public System.Boolean IsActual
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsActualOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsActual));
        public bool IsActualIsChanged => GetIsChanged(nameof(IsActual));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class ShippingCostFileWrapper : WrapperBase<ShippingCostFile>
	{
	    public ShippingCostFileWrapper(ShippingCostFile model) : base(model) { }
        #region SimpleProperties
        //TechnicalRequrementsTaskId
        public System.Guid TechnicalRequrementsTaskId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid TechnicalRequrementsTaskIdOriginalValue => GetOriginalValue<System.Guid>(nameof(TechnicalRequrementsTaskId));
        public bool TechnicalRequrementsTaskIdIsChanged => GetIsChanged(nameof(TechnicalRequrementsTaskId));
        //Moment
        public System.DateTime Moment
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime MomentOriginalValue => GetOriginalValue<System.DateTime>(nameof(Moment));
        public bool MomentIsChanged => GetIsChanged(nameof(Moment));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class TechnicalRequrementsWrapper : WrapperBase<TechnicalRequrements>
	{
	    public TechnicalRequrementsWrapper(TechnicalRequrements model) : base(model) { }
        #region SimpleProperties
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //IsActual
        public System.Boolean IsActual
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsActualOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsActual));
        public bool IsActualIsChanged => GetIsChanged(nameof(IsActual));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<SalesUnitWrapper> SalesUnits { get; private set; }
        public IValidatableChangeTrackingCollection<TechnicalRequrementsFileWrapper> Files { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
          SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(Model.SalesUnits.Select(e => new SalesUnitWrapper(e)));
          RegisterCollection(SalesUnits, Model.SalesUnits);
          if (Model.Files == null) throw new ArgumentException("Files cannot be null");
          Files = new ValidatableChangeTrackingCollection<TechnicalRequrementsFileWrapper>(Model.Files.Select(e => new TechnicalRequrementsFileWrapper(e)));
          RegisterCollection(Files, Model.Files);
        }
	}

		public partial class TechnicalRequrementsFileWrapper : WrapperBase<TechnicalRequrementsFile>
	{
	    public TechnicalRequrementsFileWrapper(TechnicalRequrementsFile model) : base(model) { }
        #region SimpleProperties
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //IsActual
        public System.Boolean IsActual
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsActualOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsActual));
        public bool IsActualIsChanged => GetIsChanged(nameof(IsActual));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class TechnicalRequrementsTaskWrapper : WrapperBase<TechnicalRequrementsTask>
	{
	    public TechnicalRequrementsTaskWrapper(TechnicalRequrementsTask model) : base(model) { }
        #region SimpleProperties
        //TceNumber
        public System.String TceNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String TceNumberOriginalValue => GetOriginalValue<System.String>(nameof(TceNumber));
        public bool TceNumberIsChanged => GetIsChanged(nameof(TceNumber));
        //LastOpenBackManagerMoment
        public System.Nullable<System.DateTime> LastOpenBackManagerMoment
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> LastOpenBackManagerMomentOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(LastOpenBackManagerMoment));
        public bool LastOpenBackManagerMomentIsChanged => GetIsChanged(nameof(LastOpenBackManagerMoment));
        //LastOpenFrontManagerMoment
        public System.Nullable<System.DateTime> LastOpenFrontManagerMoment
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> LastOpenFrontManagerMomentOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(LastOpenFrontManagerMoment));
        public bool LastOpenFrontManagerMomentIsChanged => GetIsChanged(nameof(LastOpenFrontManagerMoment));
        //LogisticsCalculationRequired
        public System.Boolean LogisticsCalculationRequired
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean LogisticsCalculationRequiredOriginalValue => GetOriginalValue<System.Boolean>(nameof(LogisticsCalculationRequired));
        public bool LogisticsCalculationRequiredIsChanged => GetIsChanged(nameof(LogisticsCalculationRequired));
        //ExcelFileIsRequired
        public System.Boolean ExcelFileIsRequired
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean ExcelFileIsRequiredOriginalValue => GetOriginalValue<System.Boolean>(nameof(ExcelFileIsRequired));
        public bool ExcelFileIsRequiredIsChanged => GetIsChanged(nameof(ExcelFileIsRequired));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public UserWrapper BackManager 
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(BackManager, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<TechnicalRequrementsWrapper> Requrements { get; private set; }
        public IValidatableChangeTrackingCollection<PriceCalculationWrapper> PriceCalculations { get; private set; }
        public IValidatableChangeTrackingCollection<AnswerFileTceWrapper> AnswerFiles { get; private set; }
        public IValidatableChangeTrackingCollection<ShippingCostFileWrapper> ShippingCostFiles { get; private set; }
        public IValidatableChangeTrackingCollection<TechnicalRequrementsTaskHistoryElementWrapper> HistoryElements { get; private set; }
        #endregion
        #region GetProperties
        public System.Nullable<System.DateTime> Start => GetValue<System.Nullable<System.DateTime>>(); 
        public System.Nullable<System.DateTime> Finish => GetValue<System.Nullable<System.DateTime>>(); 
        public System.Boolean IsStarted => GetValue<System.Boolean>(); 
        public System.Boolean IsFinished => GetValue<System.Boolean>(); 
        public System.Boolean IsRejected => GetValue<System.Boolean>(); 
        public System.Boolean IsStopped => GetValue<System.Boolean>(); 
        public System.Boolean IsAccepted => GetValue<System.Boolean>(); 
        public HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElement LastHistoryElement => GetValue<HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElement>(); 
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<UserWrapper>(nameof(BackManager), Model.BackManager == null ? null : new UserWrapper(Model.BackManager));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.Requrements == null) throw new ArgumentException("Requrements cannot be null");
          Requrements = new ValidatableChangeTrackingCollection<TechnicalRequrementsWrapper>(Model.Requrements.Select(e => new TechnicalRequrementsWrapper(e)));
          RegisterCollection(Requrements, Model.Requrements);
          if (Model.PriceCalculations == null) throw new ArgumentException("PriceCalculations cannot be null");
          PriceCalculations = new ValidatableChangeTrackingCollection<PriceCalculationWrapper>(Model.PriceCalculations.Select(e => new PriceCalculationWrapper(e)));
          RegisterCollection(PriceCalculations, Model.PriceCalculations);
          if (Model.AnswerFiles == null) throw new ArgumentException("AnswerFiles cannot be null");
          AnswerFiles = new ValidatableChangeTrackingCollection<AnswerFileTceWrapper>(Model.AnswerFiles.Select(e => new AnswerFileTceWrapper(e)));
          RegisterCollection(AnswerFiles, Model.AnswerFiles);
          if (Model.ShippingCostFiles == null) throw new ArgumentException("ShippingCostFiles cannot be null");
          ShippingCostFiles = new ValidatableChangeTrackingCollection<ShippingCostFileWrapper>(Model.ShippingCostFiles.Select(e => new ShippingCostFileWrapper(e)));
          RegisterCollection(ShippingCostFiles, Model.ShippingCostFiles);
          if (Model.HistoryElements == null) throw new ArgumentException("HistoryElements cannot be null");
          HistoryElements = new ValidatableChangeTrackingCollection<TechnicalRequrementsTaskHistoryElementWrapper>(Model.HistoryElements.Select(e => new TechnicalRequrementsTaskHistoryElementWrapper(e)));
          RegisterCollection(HistoryElements, Model.HistoryElements);
        }
	}

		public partial class TechnicalRequrementsTaskHistoryElementWrapper : WrapperBase<TechnicalRequrementsTaskHistoryElement>
	{
	    public TechnicalRequrementsTaskHistoryElementWrapper(TechnicalRequrementsTaskHistoryElement model) : base(model) { }
        #region SimpleProperties
        //TechnicalRequrementsTaskId
        public System.Guid TechnicalRequrementsTaskId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid TechnicalRequrementsTaskIdOriginalValue => GetOriginalValue<System.Guid>(nameof(TechnicalRequrementsTaskId));
        public bool TechnicalRequrementsTaskIdIsChanged => GetIsChanged(nameof(TechnicalRequrementsTaskId));
        //Moment
        public System.DateTime Moment
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime MomentOriginalValue => GetOriginalValue<System.DateTime>(nameof(Moment));
        public bool MomentIsChanged => GetIsChanged(nameof(Moment));
        //Type
        public HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElementType Type
        {
          get { return GetValue<HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElementType>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElementType TypeOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElementType>(nameof(Type));
        public bool TypeIsChanged => GetIsChanged(nameof(Type));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class UserGroupWrapper : WrapperBase<UserGroup>
	{
	    public UserGroupWrapper(UserGroup model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<UserWrapper> Users { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.Users == null) throw new ArgumentException("Users cannot be null");
          Users = new ValidatableChangeTrackingCollection<UserWrapper>(Model.Users.Select(e => new UserWrapper(e)));
          RegisterCollection(Users, Model.Users);
        }
	}

		public partial class GlobalPropertiesWrapper : WrapperBase<GlobalProperties>
	{
	    public GlobalPropertiesWrapper(GlobalProperties model) : base(model) { }
        #region SimpleProperties
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //ActualPriceTerm
        public System.Int32 ActualPriceTerm
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 ActualPriceTermOriginalValue => GetOriginalValue<System.Int32>(nameof(ActualPriceTerm));
        public bool ActualPriceTermIsChanged => GetIsChanged(nameof(ActualPriceTerm));
        //StandartTermFromStartToEndProduction
        public System.Int32 StandartTermFromStartToEndProduction
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 StandartTermFromStartToEndProductionOriginalValue => GetOriginalValue<System.Int32>(nameof(StandartTermFromStartToEndProduction));
        public bool StandartTermFromStartToEndProductionIsChanged => GetIsChanged(nameof(StandartTermFromStartToEndProduction));
        //StandartTermFromPickToEndProduction
        public System.Int32 StandartTermFromPickToEndProduction
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 StandartTermFromPickToEndProductionOriginalValue => GetOriginalValue<System.Int32>(nameof(StandartTermFromPickToEndProduction));
        public bool StandartTermFromPickToEndProductionIsChanged => GetIsChanged(nameof(StandartTermFromPickToEndProduction));
        //Vat
        public System.Double Vat
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double VatOriginalValue => GetOriginalValue<System.Double>(nameof(Vat));
        public bool VatIsChanged => GetIsChanged(nameof(Vat));
        //IncomingRequestsPath
        public System.String IncomingRequestsPath
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String IncomingRequestsPathOriginalValue => GetOriginalValue<System.String>(nameof(IncomingRequestsPath));
        public bool IncomingRequestsPathIsChanged => GetIsChanged(nameof(IncomingRequestsPath));
        //DirectumAttachmentsPath
        public System.String DirectumAttachmentsPath
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DirectumAttachmentsPathOriginalValue => GetOriginalValue<System.String>(nameof(DirectumAttachmentsPath));
        public bool DirectumAttachmentsPathIsChanged => GetIsChanged(nameof(DirectumAttachmentsPath));
        //TechnicalRequrementsFilesPath
        public System.String TechnicalRequrementsFilesPath
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String TechnicalRequrementsFilesPathOriginalValue => GetOriginalValue<System.String>(nameof(TechnicalRequrementsFilesPath));
        public bool TechnicalRequrementsFilesPathIsChanged => GetIsChanged(nameof(TechnicalRequrementsFilesPath));
        //TechnicalRequrementsFilesAnswersPath
        public System.String TechnicalRequrementsFilesAnswersPath
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String TechnicalRequrementsFilesAnswersPathOriginalValue => GetOriginalValue<System.String>(nameof(TechnicalRequrementsFilesAnswersPath));
        public bool TechnicalRequrementsFilesAnswersPathIsChanged => GetIsChanged(nameof(TechnicalRequrementsFilesAnswersPath));
        //ShippingCostFilesPath
        public System.String ShippingCostFilesPath
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ShippingCostFilesPathOriginalValue => GetOriginalValue<System.String>(nameof(ShippingCostFilesPath));
        public bool ShippingCostFilesPathIsChanged => GetIsChanged(nameof(ShippingCostFilesPath));
        //PriceCalculationsFilesPath
        public System.String PriceCalculationsFilesPath
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String PriceCalculationsFilesPathOriginalValue => GetOriginalValue<System.String>(nameof(PriceCalculationsFilesPath));
        public bool PriceCalculationsFilesPathIsChanged => GetIsChanged(nameof(PriceCalculationsFilesPath));
        //LogsPath
        public System.String LogsPath
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String LogsPathOriginalValue => GetOriginalValue<System.String>(nameof(LogsPath));
        public bool LogsPathIsChanged => GetIsChanged(nameof(LogsPath));
        //LastDeveloperVizit
        public System.Nullable<System.DateTime> LastDeveloperVizit
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> LastDeveloperVizitOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(LastDeveloperVizit));
        public bool LastDeveloperVizitIsChanged => GetIsChanged(nameof(LastDeveloperVizit));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public CompanyWrapper OurCompany 
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(OurCompany, value); }
        }
	    public PaymentConditionSetWrapper StandartPaymentsConditionSet 
        {
            get { return GetWrapper<PaymentConditionSetWrapper>(); }
            set { SetComplexValue<PaymentConditionSet, PaymentConditionSetWrapper>(StandartPaymentsConditionSet, value); }
        }
	    public ParameterWrapper NewProductParameter 
        {
            get { return GetWrapper<ParameterWrapper>(); }
            set { SetComplexValue<Parameter, ParameterWrapper>(NewProductParameter, value); }
        }
	    public ParameterGroupWrapper NewProductParameterGroup 
        {
            get { return GetWrapper<ParameterGroupWrapper>(); }
            set { SetComplexValue<ParameterGroup, ParameterGroupWrapper>(NewProductParameterGroup, value); }
        }
	    public ParameterWrapper ServiceParameter 
        {
            get { return GetWrapper<ParameterWrapper>(); }
            set { SetComplexValue<Parameter, ParameterWrapper>(ServiceParameter, value); }
        }
	    public ParameterWrapper SupervisionParameter 
        {
            get { return GetWrapper<ParameterWrapper>(); }
            set { SetComplexValue<Parameter, ParameterWrapper>(SupervisionParameter, value); }
        }
	    public ParameterGroupWrapper VoltageGroup 
        {
            get { return GetWrapper<ParameterGroupWrapper>(); }
            set { SetComplexValue<ParameterGroup, ParameterGroupWrapper>(VoltageGroup, value); }
        }
	    public ParameterGroupWrapper IsolationMaterialGroup 
        {
            get { return GetWrapper<ParameterGroupWrapper>(); }
            set { SetComplexValue<ParameterGroup, ParameterGroupWrapper>(IsolationMaterialGroup, value); }
        }
	    public ParameterGroupWrapper IsolationColorGroup 
        {
            get { return GetWrapper<ParameterGroupWrapper>(); }
            set { SetComplexValue<ParameterGroup, ParameterGroupWrapper>(IsolationColorGroup, value); }
        }
	    public ParameterGroupWrapper IsolationDpuGroup 
        {
            get { return GetWrapper<ParameterGroupWrapper>(); }
            set { SetComplexValue<ParameterGroup, ParameterGroupWrapper>(IsolationDpuGroup, value); }
        }
	    public ParameterGroupWrapper ComplectDesignationGroup 
        {
            get { return GetWrapper<ParameterGroupWrapper>(); }
            set { SetComplexValue<ParameterGroup, ParameterGroupWrapper>(ComplectDesignationGroup, value); }
        }
	    public ParameterWrapper ComplectsParameter 
        {
            get { return GetWrapper<ParameterWrapper>(); }
            set { SetComplexValue<Parameter, ParameterWrapper>(ComplectsParameter, value); }
        }
	    public ParameterGroupWrapper ComplectsGroup 
        {
            get { return GetWrapper<ParameterGroupWrapper>(); }
            set { SetComplexValue<ParameterGroup, ParameterGroupWrapper>(ComplectsGroup, value); }
        }
	    public ProjectTypeWrapper DefaultProjectType 
        {
            get { return GetWrapper<ProjectTypeWrapper>(); }
            set { SetComplexValue<ProjectType, ProjectTypeWrapper>(DefaultProjectType, value); }
        }
	    public EmployeeWrapper RecipientSupervisionLetterEmployee 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(RecipientSupervisionLetterEmployee, value); }
        }
	    public EmployeeWrapper SenderOfferEmployee 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(SenderOfferEmployee, value); }
        }
	    public ActivityFieldWrapper HvtProducersActivityField 
        {
            get { return GetWrapper<ActivityFieldWrapper>(); }
            set { SetComplexValue<ActivityField, ActivityFieldWrapper>(HvtProducersActivityField, value); }
        }
	    public PaymentConditionSetWrapper PaymentConditionSet 
        {
            get { return GetWrapper<PaymentConditionSetWrapper>(); }
            set { SetComplexValue<PaymentConditionSet, PaymentConditionSetWrapper>(PaymentConditionSet, value); }
        }
	    public UserWrapper Developer 
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(Developer, value); }
        }
	    public ProductWrapper ProductIncludedDefault 
        {
            get { return GetWrapper<ProductWrapper>(); }
            set { SetComplexValue<Product, ProductWrapper>(ProductIncludedDefault, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<CompanyWrapper>(nameof(OurCompany), Model.OurCompany == null ? null : new CompanyWrapper(Model.OurCompany));
            InitializeComplexProperty<PaymentConditionSetWrapper>(nameof(StandartPaymentsConditionSet), Model.StandartPaymentsConditionSet == null ? null : new PaymentConditionSetWrapper(Model.StandartPaymentsConditionSet));
            InitializeComplexProperty<ParameterWrapper>(nameof(NewProductParameter), Model.NewProductParameter == null ? null : new ParameterWrapper(Model.NewProductParameter));
            InitializeComplexProperty<ParameterGroupWrapper>(nameof(NewProductParameterGroup), Model.NewProductParameterGroup == null ? null : new ParameterGroupWrapper(Model.NewProductParameterGroup));
            InitializeComplexProperty<ParameterWrapper>(nameof(ServiceParameter), Model.ServiceParameter == null ? null : new ParameterWrapper(Model.ServiceParameter));
            InitializeComplexProperty<ParameterWrapper>(nameof(SupervisionParameter), Model.SupervisionParameter == null ? null : new ParameterWrapper(Model.SupervisionParameter));
            InitializeComplexProperty<ParameterGroupWrapper>(nameof(VoltageGroup), Model.VoltageGroup == null ? null : new ParameterGroupWrapper(Model.VoltageGroup));
            InitializeComplexProperty<ParameterGroupWrapper>(nameof(IsolationMaterialGroup), Model.IsolationMaterialGroup == null ? null : new ParameterGroupWrapper(Model.IsolationMaterialGroup));
            InitializeComplexProperty<ParameterGroupWrapper>(nameof(IsolationColorGroup), Model.IsolationColorGroup == null ? null : new ParameterGroupWrapper(Model.IsolationColorGroup));
            InitializeComplexProperty<ParameterGroupWrapper>(nameof(IsolationDpuGroup), Model.IsolationDpuGroup == null ? null : new ParameterGroupWrapper(Model.IsolationDpuGroup));
            InitializeComplexProperty<ParameterGroupWrapper>(nameof(ComplectDesignationGroup), Model.ComplectDesignationGroup == null ? null : new ParameterGroupWrapper(Model.ComplectDesignationGroup));
            InitializeComplexProperty<ParameterWrapper>(nameof(ComplectsParameter), Model.ComplectsParameter == null ? null : new ParameterWrapper(Model.ComplectsParameter));
            InitializeComplexProperty<ParameterGroupWrapper>(nameof(ComplectsGroup), Model.ComplectsGroup == null ? null : new ParameterGroupWrapper(Model.ComplectsGroup));
            InitializeComplexProperty<ProjectTypeWrapper>(nameof(DefaultProjectType), Model.DefaultProjectType == null ? null : new ProjectTypeWrapper(Model.DefaultProjectType));
            InitializeComplexProperty<EmployeeWrapper>(nameof(RecipientSupervisionLetterEmployee), Model.RecipientSupervisionLetterEmployee == null ? null : new EmployeeWrapper(Model.RecipientSupervisionLetterEmployee));
            InitializeComplexProperty<EmployeeWrapper>(nameof(SenderOfferEmployee), Model.SenderOfferEmployee == null ? null : new EmployeeWrapper(Model.SenderOfferEmployee));
            InitializeComplexProperty<ActivityFieldWrapper>(nameof(HvtProducersActivityField), Model.HvtProducersActivityField == null ? null : new ActivityFieldWrapper(Model.HvtProducersActivityField));
            InitializeComplexProperty<PaymentConditionSetWrapper>(nameof(PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetWrapper(Model.PaymentConditionSet));
            InitializeComplexProperty<UserWrapper>(nameof(Developer), Model.Developer == null ? null : new UserWrapper(Model.Developer));
            InitializeComplexProperty<ProductWrapper>(nameof(ProductIncludedDefault), Model.ProductIncludedDefault == null ? null : new ProductWrapper(Model.ProductIncludedDefault));
        }
	}

		public partial class AddressWrapper : WrapperBase<Address>
	{
	    public AddressWrapper(Address model) : base(model) { }
        #region SimpleProperties
        //Description
        public System.String Description
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DescriptionOriginalValue => GetOriginalValue<System.String>(nameof(Description));
        public bool DescriptionIsChanged => GetIsChanged(nameof(Description));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public LocalityWrapper Locality 
        {
            get { return GetWrapper<LocalityWrapper>(); }
            set { SetComplexValue<Locality, LocalityWrapper>(Locality, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<LocalityWrapper>(nameof(Locality), Model.Locality == null ? null : new LocalityWrapper(Model.Locality));
        }
	}

		public partial class CountryWrapper : WrapperBase<Country>
	{
	    public CountryWrapper(Country model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class DistrictWrapper : WrapperBase<District>
	{
	    public DistrictWrapper(District model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public CountryWrapper Country 
        {
            get { return GetWrapper<CountryWrapper>(); }
            set { SetComplexValue<Country, CountryWrapper>(Country, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<CountryWrapper>(nameof(Country), Model.Country == null ? null : new CountryWrapper(Model.Country));
        }
	}

		public partial class LocalityWrapper : WrapperBase<Locality>
	{
	    public LocalityWrapper(Locality model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //IsCountryCapital
        public System.Boolean IsCountryCapital
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsCountryCapitalOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsCountryCapital));
        public bool IsCountryCapitalIsChanged => GetIsChanged(nameof(IsCountryCapital));
        //IsDistrictCapital
        public System.Boolean IsDistrictCapital
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsDistrictCapitalOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsDistrictCapital));
        public bool IsDistrictCapitalIsChanged => GetIsChanged(nameof(IsDistrictCapital));
        //IsRegionCapital
        public System.Boolean IsRegionCapital
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsRegionCapitalOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsRegionCapital));
        public bool IsRegionCapitalIsChanged => GetIsChanged(nameof(IsRegionCapital));
        //DistanceToEkb
        public System.Nullable<System.Double> DistanceToEkb
        {
          get { return GetValue<System.Nullable<System.Double>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Double> DistanceToEkbOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(DistanceToEkb));
        public bool DistanceToEkbIsChanged => GetIsChanged(nameof(DistanceToEkb));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public LocalityTypeWrapper LocalityType 
        {
            get { return GetWrapper<LocalityTypeWrapper>(); }
            set { SetComplexValue<LocalityType, LocalityTypeWrapper>(LocalityType, value); }
        }
	    public RegionWrapper Region 
        {
            get { return GetWrapper<RegionWrapper>(); }
            set { SetComplexValue<Region, RegionWrapper>(Region, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<LocalityTypeWrapper>(nameof(LocalityType), Model.LocalityType == null ? null : new LocalityTypeWrapper(Model.LocalityType));
            InitializeComplexProperty<RegionWrapper>(nameof(Region), Model.Region == null ? null : new RegionWrapper(Model.Region));
        }
	}

		public partial class LocalityTypeWrapper : WrapperBase<LocalityType>
	{
	    public LocalityTypeWrapper(LocalityType model) : base(model) { }
        #region SimpleProperties
        //FullName
        public System.String FullName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
        public bool FullNameIsChanged => GetIsChanged(nameof(FullName));
        //ShortName
        public System.String ShortName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
        public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class RegionWrapper : WrapperBase<Region>
	{
	    public RegionWrapper(Region model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public DistrictWrapper District 
        {
            get { return GetWrapper<DistrictWrapper>(); }
            set { SetComplexValue<District, DistrictWrapper>(District, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<DistrictWrapper>(nameof(District), Model.District == null ? null : new DistrictWrapper(Model.District));
        }
	}

		public partial class SumWrapper : WrapperBase<Sum>
	{
	    public SumWrapper(Sum model) : base(model) { }
        #region SimpleProperties
        //Type
        public HVTApp.Model.POCOs.SumType Type
        {
          get { return GetValue<HVTApp.Model.POCOs.SumType>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.SumType TypeOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.SumType>(nameof(Type));
        public bool TypeIsChanged => GetIsChanged(nameof(Type));
        //Currency
        public HVTApp.Model.POCOs.Currency Currency
        {
          get { return GetValue<HVTApp.Model.POCOs.Currency>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.Currency CurrencyOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.Currency>(nameof(Currency));
        public bool CurrencyIsChanged => GetIsChanged(nameof(Currency));
        //Value
        public System.Decimal Value
        {
          get { return GetValue<System.Decimal>(); }
          set { SetValue(value); }
        }
        public System.Decimal ValueOriginalValue => GetOriginalValue<System.Decimal>(nameof(Value));
        public bool ValueIsChanged => GetIsChanged(nameof(Value));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class CurrencyExchangeRateWrapper : WrapperBase<CurrencyExchangeRate>
	{
	    public CurrencyExchangeRateWrapper(CurrencyExchangeRate model) : base(model) { }
        #region SimpleProperties
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //FirstCurrency
        public HVTApp.Model.POCOs.Currency FirstCurrency
        {
          get { return GetValue<HVTApp.Model.POCOs.Currency>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.Currency FirstCurrencyOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.Currency>(nameof(FirstCurrency));
        public bool FirstCurrencyIsChanged => GetIsChanged(nameof(FirstCurrency));
        //SecondCurrency
        public HVTApp.Model.POCOs.Currency SecondCurrency
        {
          get { return GetValue<HVTApp.Model.POCOs.Currency>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.Currency SecondCurrencyOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.Currency>(nameof(SecondCurrency));
        public bool SecondCurrencyIsChanged => GetIsChanged(nameof(SecondCurrency));
        //ExchangeRate
        public System.Double ExchangeRate
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double ExchangeRateOriginalValue => GetOriginalValue<System.Double>(nameof(ExchangeRate));
        public bool ExchangeRateIsChanged => GetIsChanged(nameof(ExchangeRate));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class NoteWrapper : WrapperBase<Note>
	{
	    public NoteWrapper(Note model) : base(model) { }
        #region SimpleProperties
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //Text
        public System.String Text
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String TextOriginalValue => GetOriginalValue<System.String>(nameof(Text));
        public bool TextIsChanged => GetIsChanged(nameof(Text));
        //IsImportant
        public System.Boolean IsImportant
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsImportantOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsImportant));
        public bool IsImportantIsChanged => GetIsChanged(nameof(IsImportant));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class OfferUnitWrapper : WrapperBase<OfferUnit>
	{
	    public OfferUnitWrapper(OfferUnit model) : base(model) { }
        #region SimpleProperties
        //Cost
        public System.Double Cost
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));
        //CostDelivery
        public System.Nullable<System.Double> CostDelivery
        {
          get { return GetValue<System.Nullable<System.Double>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Double> CostDeliveryOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(CostDelivery));
        public bool CostDeliveryIsChanged => GetIsChanged(nameof(CostDelivery));
        //CostDeliveryIncluded
        public System.Boolean CostDeliveryIncluded
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean CostDeliveryIncludedOriginalValue => GetOriginalValue<System.Boolean>(nameof(CostDeliveryIncluded));
        public bool CostDeliveryIncludedIsChanged => GetIsChanged(nameof(CostDeliveryIncluded));
        //ProductionTerm
        public System.Int32 ProductionTerm
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 ProductionTermOriginalValue => GetOriginalValue<System.Int32>(nameof(ProductionTerm));
        public bool ProductionTermIsChanged => GetIsChanged(nameof(ProductionTerm));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public OfferWrapper Offer 
        {
            get { return GetWrapper<OfferWrapper>(); }
            set { SetComplexValue<Offer, OfferWrapper>(Offer, value); }
        }
	    public FacilityWrapper Facility 
        {
            get { return GetWrapper<FacilityWrapper>(); }
            set { SetComplexValue<Facility, FacilityWrapper>(Facility, value); }
        }
	    public ProductWrapper Product 
        {
            get { return GetWrapper<ProductWrapper>(); }
            set { SetComplexValue<Product, ProductWrapper>(Product, value); }
        }
	    public PaymentConditionSetWrapper PaymentConditionSet 
        {
            get { return GetWrapper<PaymentConditionSetWrapper>(); }
            set { SetComplexValue<PaymentConditionSet, PaymentConditionSetWrapper>(PaymentConditionSet, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; private set; }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<OfferWrapper>(nameof(Offer), Model.Offer == null ? null : new OfferWrapper(Model.Offer));
            InitializeComplexProperty<FacilityWrapper>(nameof(Facility), Model.Facility == null ? null : new FacilityWrapper(Model.Facility));
            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));
            InitializeComplexProperty<PaymentConditionSetWrapper>(nameof(PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetWrapper(Model.PaymentConditionSet));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.ProductsIncluded == null) throw new ArgumentException("ProductsIncluded cannot be null");
          ProductsIncluded = new ValidatableChangeTrackingCollection<ProductIncludedWrapper>(Model.ProductsIncluded.Select(e => new ProductIncludedWrapper(e)));
          RegisterCollection(ProductsIncluded, Model.ProductsIncluded);
        }
	}

		public partial class PaymentConditionSetWrapper : WrapperBase<PaymentConditionSet>
	{
	    public PaymentConditionSetWrapper(PaymentConditionSet model) : base(model) { }
        #region SimpleProperties
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentConditions { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.PaymentConditions == null) throw new ArgumentException("PaymentConditions cannot be null");
          PaymentConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentConditions.Select(e => new PaymentConditionWrapper(e)));
          RegisterCollection(PaymentConditions, Model.PaymentConditions);
        }
	}

		public partial class ProductBlockWrapper : WrapperBase<ProductBlock>
	{
	    public ProductBlockWrapper(ProductBlock model) : base(model) { }
        #region SimpleProperties
        //DesignationSpecial
        public System.String DesignationSpecial
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DesignationSpecialOriginalValue => GetOriginalValue<System.String>(nameof(DesignationSpecial));
        public bool DesignationSpecialIsChanged => GetIsChanged(nameof(DesignationSpecial));
        //StructureCostNumber
        public System.String StructureCostNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String StructureCostNumberOriginalValue => GetOriginalValue<System.String>(nameof(StructureCostNumber));
        public bool StructureCostNumberIsChanged => GetIsChanged(nameof(StructureCostNumber));
        //Design
        public System.String Design
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DesignOriginalValue => GetOriginalValue<System.String>(nameof(Design));
        public bool DesignIsChanged => GetIsChanged(nameof(Design));
        //IsDelivery
        public System.Boolean IsDelivery
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsDeliveryOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsDelivery));
        public bool IsDeliveryIsChanged => GetIsChanged(nameof(IsDelivery));
        //Weight
        public System.Double Weight
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double WeightOriginalValue => GetOriginalValue<System.Double>(nameof(Weight));
        public bool WeightIsChanged => GetIsChanged(nameof(Weight));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }
        public IValidatableChangeTrackingCollection<SumOnDateWrapper> Prices { get; private set; }
        public IValidatableChangeTrackingCollection<SumOnDateWrapper> FixedCosts { get; private set; }
        #endregion
        #region GetProperties
        public System.String Designation => GetValue<System.String>(); 
        public System.Boolean HasPrice => GetValue<System.Boolean>(); 
        public System.Boolean HasFixedPrice => GetValue<System.Boolean>(); 
        public System.Boolean IsNew => GetValue<System.Boolean>(); 
        public System.Boolean IsService => GetValue<System.Boolean>(); 
        public System.Boolean IsSupervision => GetValue<System.Boolean>(); 
        public System.Nullable<System.DateTime> LastPriceDate => GetValue<System.Nullable<System.DateTime>>(); 
        public HVTApp.Model.POCOs.ProductType ProductType => GetValue<HVTApp.Model.POCOs.ProductType>(); 
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
          Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(Parameters, Model.Parameters);
          if (Model.Prices == null) throw new ArgumentException("Prices cannot be null");
          Prices = new ValidatableChangeTrackingCollection<SumOnDateWrapper>(Model.Prices.Select(e => new SumOnDateWrapper(e)));
          RegisterCollection(Prices, Model.Prices);
          if (Model.FixedCosts == null) throw new ArgumentException("FixedCosts cannot be null");
          FixedCosts = new ValidatableChangeTrackingCollection<SumOnDateWrapper>(Model.FixedCosts.Select(e => new SumOnDateWrapper(e)));
          RegisterCollection(FixedCosts, Model.FixedCosts);
        }
	}

		public partial class ProductDependentWrapper : WrapperBase<ProductDependent>
	{
	    public ProductDependentWrapper(ProductDependent model) : base(model) { }
        #region SimpleProperties
        //MainProductId
        public System.Guid MainProductId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid MainProductIdOriginalValue => GetOriginalValue<System.Guid>(nameof(MainProductId));
        public bool MainProductIdIsChanged => GetIsChanged(nameof(MainProductId));
        //Amount
        public System.Int32 Amount
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 AmountOriginalValue => GetOriginalValue<System.Int32>(nameof(Amount));
        public bool AmountIsChanged => GetIsChanged(nameof(Amount));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public ProductWrapper Product 
        {
            get { return GetWrapper<ProductWrapper>(); }
            set { SetComplexValue<Product, ProductWrapper>(Product, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));
        }
	}

		public partial class BankDetailsWrapper : WrapperBase<BankDetails>
	{
	    public BankDetailsWrapper(BankDetails model) : base(model) { }
        #region SimpleProperties
        //BankName
        public System.String BankName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String BankNameOriginalValue => GetOriginalValue<System.String>(nameof(BankName));
        public bool BankNameIsChanged => GetIsChanged(nameof(BankName));
        //BankIdentificationCode
        public System.String BankIdentificationCode
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String BankIdentificationCodeOriginalValue => GetOriginalValue<System.String>(nameof(BankIdentificationCode));
        public bool BankIdentificationCodeIsChanged => GetIsChanged(nameof(BankIdentificationCode));
        //CorrespondentAccount
        public System.String CorrespondentAccount
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CorrespondentAccountOriginalValue => GetOriginalValue<System.String>(nameof(CorrespondentAccount));
        public bool CorrespondentAccountIsChanged => GetIsChanged(nameof(CorrespondentAccount));
        //CheckingAccount
        public System.String CheckingAccount
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CheckingAccountOriginalValue => GetOriginalValue<System.String>(nameof(CheckingAccount));
        public bool CheckingAccountIsChanged => GetIsChanged(nameof(CheckingAccount));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class CompanyWrapper : WrapperBase<Company>
	{
	    public CompanyWrapper(Company model) : base(model) { }
        #region SimpleProperties
        //FullName
        public System.String FullName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
        public bool FullNameIsChanged => GetIsChanged(nameof(FullName));
        //ShortName
        public System.String ShortName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
        public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));
        //Inn
        public System.String Inn
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String InnOriginalValue => GetOriginalValue<System.String>(nameof(Inn));
        public bool InnIsChanged => GetIsChanged(nameof(Inn));
        //Kpp
        public System.String Kpp
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String KppOriginalValue => GetOriginalValue<System.String>(nameof(Kpp));
        public bool KppIsChanged => GetIsChanged(nameof(Kpp));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public CompanyFormWrapper Form 
        {
            get { return GetWrapper<CompanyFormWrapper>(); }
            set { SetComplexValue<CompanyForm, CompanyFormWrapper>(Form, value); }
        }
	    public CompanyWrapper ParentCompany 
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(ParentCompany, value); }
        }
	    public AddressWrapper AddressLegal 
        {
            get { return GetWrapper<AddressWrapper>(); }
            set { SetComplexValue<Address, AddressWrapper>(AddressLegal, value); }
        }
	    public AddressWrapper AddressPost 
        {
            get { return GetWrapper<AddressWrapper>(); }
            set { SetComplexValue<Address, AddressWrapper>(AddressPost, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<BankDetailsWrapper> BankDetailsList { get; private set; }
        public IValidatableChangeTrackingCollection<ActivityFieldWrapper> ActivityFilds { get; private set; }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<CompanyFormWrapper>(nameof(Form), Model.Form == null ? null : new CompanyFormWrapper(Model.Form));
            InitializeComplexProperty<CompanyWrapper>(nameof(ParentCompany), Model.ParentCompany == null ? null : new CompanyWrapper(Model.ParentCompany));
            InitializeComplexProperty<AddressWrapper>(nameof(AddressLegal), Model.AddressLegal == null ? null : new AddressWrapper(Model.AddressLegal));
            InitializeComplexProperty<AddressWrapper>(nameof(AddressPost), Model.AddressPost == null ? null : new AddressWrapper(Model.AddressPost));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.BankDetailsList == null) throw new ArgumentException("BankDetailsList cannot be null");
          BankDetailsList = new ValidatableChangeTrackingCollection<BankDetailsWrapper>(Model.BankDetailsList.Select(e => new BankDetailsWrapper(e)));
          RegisterCollection(BankDetailsList, Model.BankDetailsList);
          if (Model.ActivityFilds == null) throw new ArgumentException("ActivityFilds cannot be null");
          ActivityFilds = new ValidatableChangeTrackingCollection<ActivityFieldWrapper>(Model.ActivityFilds.Select(e => new ActivityFieldWrapper(e)));
          RegisterCollection(ActivityFilds, Model.ActivityFilds);
        }
	}

		public partial class CompanyFormWrapper : WrapperBase<CompanyForm>
	{
	    public CompanyFormWrapper(CompanyForm model) : base(model) { }
        #region SimpleProperties
        //FullName
        public System.String FullName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
        public bool FullNameIsChanged => GetIsChanged(nameof(FullName));
        //ShortName
        public System.String ShortName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
        public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class DocumentsRegistrationDetailsWrapper : WrapperBase<DocumentsRegistrationDetails>
	{
	    public DocumentsRegistrationDetailsWrapper(DocumentsRegistrationDetails model) : base(model) { }
        #region SimpleProperties
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //Number
        public System.String Number
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class EmployeesPositionWrapper : WrapperBase<EmployeesPosition>
	{
	    public EmployeesPositionWrapper(EmployeesPosition model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class FacilityTypeWrapper : WrapperBase<FacilityType>
	{
	    public FacilityTypeWrapper(FacilityType model) : base(model) { }
        #region SimpleProperties
        //FullName
        public System.String FullName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
        public bool FullNameIsChanged => GetIsChanged(nameof(FullName));
        //ShortName
        public System.String ShortName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
        public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class ActivityFieldWrapper : WrapperBase<ActivityField>
	{
	    public ActivityFieldWrapper(ActivityField model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //ActivityFieldEnum
        public HVTApp.Model.POCOs.ActivityFieldEnum ActivityFieldEnum
        {
          get { return GetValue<HVTApp.Model.POCOs.ActivityFieldEnum>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.ActivityFieldEnum ActivityFieldEnumOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.ActivityFieldEnum>(nameof(ActivityFieldEnum));
        public bool ActivityFieldEnumIsChanged => GetIsChanged(nameof(ActivityFieldEnum));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class ContractWrapper : WrapperBase<Contract>
	{
	    public ContractWrapper(Contract model) : base(model) { }
        #region SimpleProperties
        //Number
        public System.String Number
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public CompanyWrapper Contragent 
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(Contragent, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<CompanyWrapper>(nameof(Contragent), Model.Contragent == null ? null : new CompanyWrapper(Model.Contragent));
        }
	}

		public partial class MeasureWrapper : WrapperBase<Measure>
	{
	    public MeasureWrapper(Measure model) : base(model) { }
        #region SimpleProperties
        //FullName
        public System.String FullName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
        public bool FullNameIsChanged => GetIsChanged(nameof(FullName));
        //ShortName
        public System.String ShortName
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
        public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class ParameterWrapper : WrapperBase<Parameter>
	{
	    public ParameterWrapper(Parameter model) : base(model) { }
        #region SimpleProperties
        //Value
        public System.String Value
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String ValueOriginalValue => GetOriginalValue<System.String>(nameof(Value));
        public bool ValueIsChanged => GetIsChanged(nameof(Value));
        //Rang
        public System.Int32 Rang
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 RangOriginalValue => GetOriginalValue<System.Int32>(nameof(Rang));
        public bool RangIsChanged => GetIsChanged(nameof(Rang));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public ParameterGroupWrapper ParameterGroup 
        {
            get { return GetWrapper<ParameterGroupWrapper>(); }
            set { SetComplexValue<ParameterGroup, ParameterGroupWrapper>(ParameterGroup, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterRelationWrapper> ParameterRelations { get; private set; }
        #endregion
        #region GetProperties
        public System.Boolean IsOrigin => GetValue<System.Boolean>(); 
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ParameterGroupWrapper>(nameof(ParameterGroup), Model.ParameterGroup == null ? null : new ParameterGroupWrapper(Model.ParameterGroup));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.ParameterRelations == null) throw new ArgumentException("ParameterRelations cannot be null");
          ParameterRelations = new ValidatableChangeTrackingCollection<ParameterRelationWrapper>(Model.ParameterRelations.Select(e => new ParameterRelationWrapper(e)));
          RegisterCollection(ParameterRelations, Model.ParameterRelations);
        }
	}

		public partial class ParameterGroupWrapper : WrapperBase<ParameterGroup>
	{
	    public ParameterGroupWrapper(ParameterGroup model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public MeasureWrapper Measure 
        {
            get { return GetWrapper<MeasureWrapper>(); }
            set { SetComplexValue<Measure, MeasureWrapper>(Measure, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<MeasureWrapper>(nameof(Measure), Model.Measure == null ? null : new MeasureWrapper(Model.Measure));
        }
	}

		public partial class ProductRelationWrapper : WrapperBase<ProductRelation>
	{
	    public ProductRelationWrapper(ProductRelation model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //ChildProductsAmount
        public System.Int32 ChildProductsAmount
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 ChildProductsAmountOriginalValue => GetOriginalValue<System.Int32>(nameof(ChildProductsAmount));
        public bool ChildProductsAmountIsChanged => GetIsChanged(nameof(ChildProductsAmount));
        //IsUnique
        public System.Boolean IsUnique
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsUniqueOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsUnique));
        public bool IsUniqueIsChanged => GetIsChanged(nameof(IsUnique));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> ParentProductParameters { get; private set; }
        public IValidatableChangeTrackingCollection<ParameterWrapper> ChildProductParameters { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.ParentProductParameters == null) throw new ArgumentException("ParentProductParameters cannot be null");
          ParentProductParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.ParentProductParameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(ParentProductParameters, Model.ParentProductParameters);
          if (Model.ChildProductParameters == null) throw new ArgumentException("ChildProductParameters cannot be null");
          ChildProductParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.ChildProductParameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(ChildProductParameters, Model.ChildProductParameters);
        }
	}

		public partial class PersonWrapper : WrapperBase<Person>
	{
	    public PersonWrapper(Person model) : base(model) { }
        #region SimpleProperties
        //Surname
        public System.String Surname
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String SurnameOriginalValue => GetOriginalValue<System.String>(nameof(Surname));
        public bool SurnameIsChanged => GetIsChanged(nameof(Surname));
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Patronymic
        public System.String Patronymic
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String PatronymicOriginalValue => GetOriginalValue<System.String>(nameof(Patronymic));
        public bool PatronymicIsChanged => GetIsChanged(nameof(Patronymic));
        //IsMan
        public System.Boolean IsMan
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsManOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsMan));
        public bool IsManIsChanged => GetIsChanged(nameof(IsMan));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class ParameterRelationWrapper : WrapperBase<ParameterRelation>
	{
	    public ParameterRelationWrapper(ParameterRelation model) : base(model) { }
        #region SimpleProperties
        //ParameterId
        public System.Guid ParameterId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid ParameterIdOriginalValue => GetOriginalValue<System.Guid>(nameof(ParameterId));
        public bool ParameterIdIsChanged => GetIsChanged(nameof(ParameterId));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ParameterWrapper> RequiredParameters { get; private set; }
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.RequiredParameters == null) throw new ArgumentException("RequiredParameters cannot be null");
          RequiredParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.RequiredParameters.Select(e => new ParameterWrapper(e)));
          RegisterCollection(RequiredParameters, Model.RequiredParameters);
        }
	}

		public partial class SalesUnitWrapper : WrapperBase<SalesUnit>
	{
	    public SalesUnitWrapper(SalesUnit model) : base(model) { }
        #region SimpleProperties
        //Cost
        public System.Double Cost
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));
        //Price
        public System.Nullable<System.Double> Price
        {
          get { return GetValue<System.Nullable<System.Double>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Double> PriceOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(Price));
        public bool PriceIsChanged => GetIsChanged(nameof(Price));
        //LaborHours
        public System.Nullable<System.Double> LaborHours
        {
          get { return GetValue<System.Nullable<System.Double>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Double> LaborHoursOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(LaborHours));
        public bool LaborHoursIsChanged => GetIsChanged(nameof(LaborHours));
        //ProductionTerm
        public System.Int32 ProductionTerm
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 ProductionTermOriginalValue => GetOriginalValue<System.Int32>(nameof(ProductionTerm));
        public bool ProductionTermIsChanged => GetIsChanged(nameof(ProductionTerm));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //DeliveryDateExpected
        public System.DateTime DeliveryDateExpected
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DeliveryDateExpectedOriginalValue => GetOriginalValue<System.DateTime>(nameof(DeliveryDateExpected));
        public bool DeliveryDateExpectedIsChanged => GetIsChanged(nameof(DeliveryDateExpected));
        //RealizationDate
        public System.Nullable<System.DateTime> RealizationDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> RealizationDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(RealizationDate));
        public bool RealizationDateIsChanged => GetIsChanged(nameof(RealizationDate));
        //OrderPosition
        public System.String OrderPosition
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String OrderPositionOriginalValue => GetOriginalValue<System.String>(nameof(OrderPosition));
        public bool OrderPositionIsChanged => GetIsChanged(nameof(OrderPosition));
        //SerialNumber
        public System.String SerialNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String SerialNumberOriginalValue => GetOriginalValue<System.String>(nameof(SerialNumber));
        public bool SerialNumberIsChanged => GetIsChanged(nameof(SerialNumber));
        //AssembleTerm
        public System.Nullable<System.Int32> AssembleTerm
        {
          get { return GetValue<System.Nullable<System.Int32>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Int32> AssembleTermOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(AssembleTerm));
        public bool AssembleTermIsChanged => GetIsChanged(nameof(AssembleTerm));
        //SignalToStartProduction
        public System.Nullable<System.DateTime> SignalToStartProduction
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> SignalToStartProductionOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(SignalToStartProduction));
        public bool SignalToStartProductionIsChanged => GetIsChanged(nameof(SignalToStartProduction));
        //SignalToStartProductionDone
        public System.Nullable<System.DateTime> SignalToStartProductionDone
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> SignalToStartProductionDoneOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(SignalToStartProductionDone));
        public bool SignalToStartProductionDoneIsChanged => GetIsChanged(nameof(SignalToStartProductionDone));
        //StartProductionDate
        public System.Nullable<System.DateTime> StartProductionDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> StartProductionDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(StartProductionDate));
        public bool StartProductionDateIsChanged => GetIsChanged(nameof(StartProductionDate));
        //PickingDate
        public System.Nullable<System.DateTime> PickingDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> PickingDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(PickingDate));
        public bool PickingDateIsChanged => GetIsChanged(nameof(PickingDate));
        //EndProductionPlanDate
        public System.Nullable<System.DateTime> EndProductionPlanDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> EndProductionPlanDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(EndProductionPlanDate));
        public bool EndProductionPlanDateIsChanged => GetIsChanged(nameof(EndProductionPlanDate));
        //EndProductionDate
        public System.Nullable<System.DateTime> EndProductionDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> EndProductionDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(EndProductionDate));
        public bool EndProductionDateIsChanged => GetIsChanged(nameof(EndProductionDate));
        //CostDelivery
        public System.Nullable<System.Double> CostDelivery
        {
          get { return GetValue<System.Nullable<System.Double>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Double> CostDeliveryOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(CostDelivery));
        public bool CostDeliveryIsChanged => GetIsChanged(nameof(CostDelivery));
        //CostDeliveryIncluded
        public System.Boolean CostDeliveryIncluded
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean CostDeliveryIncludedOriginalValue => GetOriginalValue<System.Boolean>(nameof(CostDeliveryIncluded));
        public bool CostDeliveryIncludedIsChanged => GetIsChanged(nameof(CostDeliveryIncluded));
        //ExpectedDeliveryPeriod
        public System.Nullable<System.Int32> ExpectedDeliveryPeriod
        {
          get { return GetValue<System.Nullable<System.Int32>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.Int32> ExpectedDeliveryPeriodOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(ExpectedDeliveryPeriod));
        public bool ExpectedDeliveryPeriodIsChanged => GetIsChanged(nameof(ExpectedDeliveryPeriod));
        //ShipmentDate
        public System.Nullable<System.DateTime> ShipmentDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> ShipmentDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(ShipmentDate));
        public bool ShipmentDateIsChanged => GetIsChanged(nameof(ShipmentDate));
        //ShipmentPlanDate
        public System.Nullable<System.DateTime> ShipmentPlanDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> ShipmentPlanDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(ShipmentPlanDate));
        public bool ShipmentPlanDateIsChanged => GetIsChanged(nameof(ShipmentPlanDate));
        //DeliveryDate
        public System.Nullable<System.DateTime> DeliveryDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> DeliveryDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DeliveryDate));
        public bool DeliveryDateIsChanged => GetIsChanged(nameof(DeliveryDate));
        //IsRemoved
        public System.Boolean IsRemoved
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean IsRemovedOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsRemoved));
        public bool IsRemovedIsChanged => GetIsChanged(nameof(IsRemoved));
        //OrderInTakeDateInjected
        public System.Nullable<System.DateTime> OrderInTakeDateInjected
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> OrderInTakeDateInjectedOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(OrderInTakeDateInjected));
        public bool OrderInTakeDateInjectedIsChanged => GetIsChanged(nameof(OrderInTakeDateInjected));
        //StartProductionDateInjected
        public System.Nullable<System.DateTime> StartProductionDateInjected
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> StartProductionDateInjectedOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(StartProductionDateInjected));
        public bool StartProductionDateInjectedIsChanged => GetIsChanged(nameof(StartProductionDateInjected));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public FacilityWrapper Facility 
        {
            get { return GetWrapper<FacilityWrapper>(); }
            set { SetComplexValue<Facility, FacilityWrapper>(Facility, value); }
        }
	    public ProductWrapper Product 
        {
            get { return GetWrapper<ProductWrapper>(); }
            set { SetComplexValue<Product, ProductWrapper>(Product, value); }
        }
	    public PaymentConditionSetWrapper PaymentConditionSet 
        {
            get { return GetWrapper<PaymentConditionSetWrapper>(); }
            set { SetComplexValue<PaymentConditionSet, PaymentConditionSetWrapper>(PaymentConditionSet, value); }
        }
	    public ProjectWrapper Project 
        {
            get { return GetWrapper<ProjectWrapper>(); }
            set { SetComplexValue<Project, ProjectWrapper>(Project, value); }
        }
	    public CompanyWrapper Producer 
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(Producer, value); }
        }
	    public OrderWrapper Order 
        {
            get { return GetWrapper<OrderWrapper>(); }
            set { SetComplexValue<Order, OrderWrapper>(Order, value); }
        }
	    public SpecificationWrapper Specification 
        {
            get { return GetWrapper<SpecificationWrapper>(); }
            set { SetComplexValue<Specification, SpecificationWrapper>(Specification, value); }
        }
	    public PenaltyWrapper Penalty 
        {
            get { return GetWrapper<PenaltyWrapper>(); }
            set { SetComplexValue<Penalty, PenaltyWrapper>(Penalty, value); }
        }
	    public AddressWrapper AddressDelivery 
        {
            get { return GetWrapper<AddressWrapper>(); }
            set { SetComplexValue<Address, AddressWrapper>(AddressDelivery, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; private set; }
        public IValidatableChangeTrackingCollection<LosingReasonWrapper> LosingReasons { get; private set; }
        public IValidatableChangeTrackingCollection<PaymentActualWrapper> PaymentsActual { get; private set; }
        public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlanned { get; private set; }
        public IValidatableChangeTrackingCollection<BankGuaranteeWrapper> BankGuarantees { get; private set; }
        public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlannedActual { get; private set; }
        public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlannedGenerated { get; private set; }
        public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlannedCalculated { get; private set; }
        #endregion
        #region GetProperties
        public System.Nullable<System.Int32> ExpectedDeliveryPeriodCalculated => GetValue<System.Nullable<System.Int32>>(); 
        public System.Boolean AllowEditCost => GetValue<System.Boolean>(); 
        public System.Boolean AllowEditProduct => GetValue<System.Boolean>(); 
        public System.Boolean IsLoosen => GetValue<System.Boolean>(); 
        public System.Boolean IsWon => GetValue<System.Boolean>(); 
        public System.Boolean IsDone => GetValue<System.Boolean>(); 
        public System.Guid ActualPriceCalculationItemId => GetValue<System.Guid>(); 
        public System.Boolean OrderIsTaken => GetValue<System.Boolean>(); 
        public System.Boolean OrderIsRealized => GetValue<System.Boolean>(); 
        public System.Boolean AllowTotalRemove => GetValue<System.Boolean>(); 
        public System.Boolean IsPaid => GetValue<System.Boolean>(); 
        public System.Double SumPaid => GetValue<System.Double>(); 
        public System.Double SumNotPaid => GetValue<System.Double>(); 
        public System.Double Vat => GetValue<System.Double>(); 
        public System.Double SumNotPaidWithVat => GetValue<System.Double>(); 
        public System.Double SumToStartProduction => GetValue<System.Double>(); 
        public System.Double SumToShipping => GetValue<System.Double>(); 
        public System.DateTime OrderInTakeDate => GetValue<System.DateTime>(); 
        public System.Int32 OrderInTakeYear => GetValue<System.Int32>(); 
        public System.Int32 OrderInTakeMonth => GetValue<System.Int32>(); 
        public System.Nullable<System.DateTime> StartProductionConditionsDoneDate => GetValue<System.Nullable<System.DateTime>>(); 
        public System.Nullable<System.DateTime> ShippingConditionsDoneDate => GetValue<System.Nullable<System.DateTime>>(); 
        public System.DateTime StartProductionDateCalculated => GetValue<System.DateTime>(); 
        public System.DateTime EndProductionDateCalculated => GetValue<System.DateTime>(); 
        public System.DateTime EndProductionDateByContractCalculated => GetValue<System.DateTime>(); 
        public System.DateTime RealizationDateCalculated => GetValue<System.DateTime>(); 
        public System.DateTime ShipmentDateCalculated => GetValue<System.DateTime>(); 
        public System.DateTime DeliveryDateCalculated => GetValue<System.DateTime>(); 
        public System.Double DeliveryPeriodCalculated => GetValue<System.Double>(); 
        public HVTApp.Model.POCOs.Address AddressDeliveryCalculated => GetValue<HVTApp.Model.POCOs.Address>(); 
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<FacilityWrapper>(nameof(Facility), Model.Facility == null ? null : new FacilityWrapper(Model.Facility));
            InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));
            InitializeComplexProperty<PaymentConditionSetWrapper>(nameof(PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetWrapper(Model.PaymentConditionSet));
            InitializeComplexProperty<ProjectWrapper>(nameof(Project), Model.Project == null ? null : new ProjectWrapper(Model.Project));
            InitializeComplexProperty<CompanyWrapper>(nameof(Producer), Model.Producer == null ? null : new CompanyWrapper(Model.Producer));
            InitializeComplexProperty<OrderWrapper>(nameof(Order), Model.Order == null ? null : new OrderWrapper(Model.Order));
            InitializeComplexProperty<SpecificationWrapper>(nameof(Specification), Model.Specification == null ? null : new SpecificationWrapper(Model.Specification));
            InitializeComplexProperty<PenaltyWrapper>(nameof(Penalty), Model.Penalty == null ? null : new PenaltyWrapper(Model.Penalty));
            InitializeComplexProperty<AddressWrapper>(nameof(AddressDelivery), Model.AddressDelivery == null ? null : new AddressWrapper(Model.AddressDelivery));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.ProductsIncluded == null) throw new ArgumentException("ProductsIncluded cannot be null");
          ProductsIncluded = new ValidatableChangeTrackingCollection<ProductIncludedWrapper>(Model.ProductsIncluded.Select(e => new ProductIncludedWrapper(e)));
          RegisterCollection(ProductsIncluded, Model.ProductsIncluded);
          if (Model.LosingReasons == null) throw new ArgumentException("LosingReasons cannot be null");
          LosingReasons = new ValidatableChangeTrackingCollection<LosingReasonWrapper>(Model.LosingReasons.Select(e => new LosingReasonWrapper(e)));
          RegisterCollection(LosingReasons, Model.LosingReasons);
          if (Model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
          PaymentsActual = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(Model.PaymentsActual.Select(e => new PaymentActualWrapper(e)));
          RegisterCollection(PaymentsActual, Model.PaymentsActual);
          if (Model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
          PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlanned.Select(e => new PaymentPlannedWrapper(e)));
          RegisterCollection(PaymentsPlanned, Model.PaymentsPlanned);
          if (Model.BankGuarantees == null) throw new ArgumentException("BankGuarantees cannot be null");
          BankGuarantees = new ValidatableChangeTrackingCollection<BankGuaranteeWrapper>(Model.BankGuarantees.Select(e => new BankGuaranteeWrapper(e)));
          RegisterCollection(BankGuarantees, Model.BankGuarantees);
          if (Model.PaymentsPlannedActual == null) throw new ArgumentException("PaymentsPlannedActual cannot be null");
          PaymentsPlannedActual = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlannedActual.Select(e => new PaymentPlannedWrapper(e)));
          RegisterCollection(PaymentsPlannedActual, Model.PaymentsPlannedActual);
          if (Model.PaymentsPlannedGenerated == null) throw new ArgumentException("PaymentsPlannedGenerated cannot be null");
          PaymentsPlannedGenerated = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlannedGenerated.Select(e => new PaymentPlannedWrapper(e)));
          RegisterCollection(PaymentsPlannedGenerated, Model.PaymentsPlannedGenerated);
          if (Model.PaymentsPlannedCalculated == null) throw new ArgumentException("PaymentsPlannedCalculated cannot be null");
          PaymentsPlannedCalculated = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlannedCalculated.Select(e => new PaymentPlannedWrapper(e)));
          RegisterCollection(PaymentsPlannedCalculated, Model.PaymentsPlannedCalculated);
        }
	}

		public partial class DocumentWrapper : WrapperBase<Document>
	{
	    public DocumentWrapper(Document model) : base(model) { }
        #region SimpleProperties
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //SenderId
        public System.Guid SenderId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid SenderIdOriginalValue => GetOriginalValue<System.Guid>(nameof(SenderId));
        public bool SenderIdIsChanged => GetIsChanged(nameof(SenderId));
        //RecipientId
        public System.Guid RecipientId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid RecipientIdOriginalValue => GetOriginalValue<System.Guid>(nameof(RecipientId));
        public bool RecipientIdIsChanged => GetIsChanged(nameof(RecipientId));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //TceNumber
        public System.String TceNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String TceNumberOriginalValue => GetOriginalValue<System.String>(nameof(TceNumber));
        public bool TceNumberIsChanged => GetIsChanged(nameof(TceNumber));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public DocumentNumberWrapper Number 
        {
            get { return GetWrapper<DocumentNumberWrapper>(); }
            set { SetComplexValue<DocumentNumber, DocumentNumberWrapper>(Number, value); }
        }
	    public DocumentWrapper RequestDocument 
        {
            get { return GetWrapper<DocumentWrapper>(); }
            set { SetComplexValue<Document, DocumentWrapper>(RequestDocument, value); }
        }
	    public EmployeeWrapper Author 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(Author, value); }
        }
	    public EmployeeWrapper SenderEmployee 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(SenderEmployee, value); }
        }
	    public EmployeeWrapper RecipientEmployee 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(RecipientEmployee, value); }
        }
	    public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfRecipient 
        {
            get { return GetWrapper<DocumentsRegistrationDetailsWrapper>(); }
            set { SetComplexValue<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(RegistrationDetailsOfRecipient, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<EmployeeWrapper> CopyToRecipients { get; private set; }
        #endregion
        #region GetProperties
        public System.String RegNumber => GetValue<System.String>(); 
        public HVTApp.Model.POCOs.DocumentDirection Direction => GetValue<HVTApp.Model.POCOs.DocumentDirection>(); 
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<DocumentNumberWrapper>(nameof(Number), Model.Number == null ? null : new DocumentNumberWrapper(Model.Number));
            InitializeComplexProperty<DocumentWrapper>(nameof(RequestDocument), Model.RequestDocument == null ? null : new DocumentWrapper(Model.RequestDocument));
            InitializeComplexProperty<EmployeeWrapper>(nameof(Author), Model.Author == null ? null : new EmployeeWrapper(Model.Author));
            InitializeComplexProperty<EmployeeWrapper>(nameof(SenderEmployee), Model.SenderEmployee == null ? null : new EmployeeWrapper(Model.SenderEmployee));
            InitializeComplexProperty<EmployeeWrapper>(nameof(RecipientEmployee), Model.RecipientEmployee == null ? null : new EmployeeWrapper(Model.RecipientEmployee));
            InitializeComplexProperty<DocumentsRegistrationDetailsWrapper>(nameof(RegistrationDetailsOfRecipient), Model.RegistrationDetailsOfRecipient == null ? null : new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfRecipient));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
          CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.CopyToRecipients.Select(e => new EmployeeWrapper(e)));
          RegisterCollection(CopyToRecipients, Model.CopyToRecipients);
        }
	}

		public partial class SumOnDateWrapper : WrapperBase<SumOnDate>
	{
	    public SumOnDateWrapper(SumOnDate model) : base(model) { }
        #region SimpleProperties
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //Sum
        public System.Double Sum
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
        public bool SumIsChanged => GetIsChanged(nameof(Sum));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class ProductWrapper : WrapperBase<Product>
	{
	    public ProductWrapper(Product model) : base(model) { }
        #region SimpleProperties
        //DesignationSpecial
        public System.String DesignationSpecial
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String DesignationSpecialOriginalValue => GetOriginalValue<System.String>(nameof(DesignationSpecial));
        public bool DesignationSpecialIsChanged => GetIsChanged(nameof(DesignationSpecial));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public ProductBlockWrapper ProductBlock 
        {
            get { return GetWrapper<ProductBlockWrapper>(); }
            set { SetComplexValue<ProductBlock, ProductBlockWrapper>(ProductBlock, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<ProductDependentWrapper> DependentProducts { get; private set; }
        #endregion
        #region GetProperties
        public System.String Designation => GetValue<System.String>(); 
        public System.Boolean HasBlockWithFixedCost => GetValue<System.Boolean>(); 
        public HVTApp.Model.POCOs.ProductType ProductType => GetValue<HVTApp.Model.POCOs.ProductType>(); 
        public HVTApp.Model.POCOs.ProductCategory Category => GetValue<HVTApp.Model.POCOs.ProductCategory>(); 
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProductBlockWrapper>(nameof(ProductBlock), Model.ProductBlock == null ? null : new ProductBlockWrapper(Model.ProductBlock));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.DependentProducts == null) throw new ArgumentException("DependentProducts cannot be null");
          DependentProducts = new ValidatableChangeTrackingCollection<ProductDependentWrapper>(Model.DependentProducts.Select(e => new ProductDependentWrapper(e)));
          RegisterCollection(DependentProducts, Model.DependentProducts);
        }
	}

		public partial class OfferWrapper : WrapperBase<Offer>
	{
	    public OfferWrapper(Offer model) : base(model) { }
        #region SimpleProperties
        //ValidityDate
        public System.DateTime ValidityDate
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime ValidityDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(ValidityDate));
        public bool ValidityDateIsChanged => GetIsChanged(nameof(ValidityDate));
        //Vat
        public System.Double Vat
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double VatOriginalValue => GetOriginalValue<System.Double>(nameof(Vat));
        public bool VatIsChanged => GetIsChanged(nameof(Vat));
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //SenderId
        public System.Guid SenderId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid SenderIdOriginalValue => GetOriginalValue<System.Guid>(nameof(SenderId));
        public bool SenderIdIsChanged => GetIsChanged(nameof(SenderId));
        //RecipientId
        public System.Guid RecipientId
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid RecipientIdOriginalValue => GetOriginalValue<System.Guid>(nameof(RecipientId));
        public bool RecipientIdIsChanged => GetIsChanged(nameof(RecipientId));
        //Comment
        public System.String Comment
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));
        //TceNumber
        public System.String TceNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String TceNumberOriginalValue => GetOriginalValue<System.String>(nameof(TceNumber));
        public bool TceNumberIsChanged => GetIsChanged(nameof(TceNumber));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public ProjectWrapper Project 
        {
            get { return GetWrapper<ProjectWrapper>(); }
            set { SetComplexValue<Project, ProjectWrapper>(Project, value); }
        }
	    public DocumentNumberWrapper Number 
        {
            get { return GetWrapper<DocumentNumberWrapper>(); }
            set { SetComplexValue<DocumentNumber, DocumentNumberWrapper>(Number, value); }
        }
	    public DocumentWrapper RequestDocument 
        {
            get { return GetWrapper<DocumentWrapper>(); }
            set { SetComplexValue<Document, DocumentWrapper>(RequestDocument, value); }
        }
	    public EmployeeWrapper Author 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(Author, value); }
        }
	    public EmployeeWrapper SenderEmployee 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(SenderEmployee, value); }
        }
	    public EmployeeWrapper RecipientEmployee 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(RecipientEmployee, value); }
        }
	    public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfRecipient 
        {
            get { return GetWrapper<DocumentsRegistrationDetailsWrapper>(); }
            set { SetComplexValue<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(RegistrationDetailsOfRecipient, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<EmployeeWrapper> CopyToRecipients { get; private set; }
        #endregion
        #region GetProperties
        public System.String RegNumber => GetValue<System.String>(); 
        public HVTApp.Model.POCOs.DocumentDirection Direction => GetValue<HVTApp.Model.POCOs.DocumentDirection>(); 
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProjectWrapper>(nameof(Project), Model.Project == null ? null : new ProjectWrapper(Model.Project));
            InitializeComplexProperty<DocumentNumberWrapper>(nameof(Number), Model.Number == null ? null : new DocumentNumberWrapper(Model.Number));
            InitializeComplexProperty<DocumentWrapper>(nameof(RequestDocument), Model.RequestDocument == null ? null : new DocumentWrapper(Model.RequestDocument));
            InitializeComplexProperty<EmployeeWrapper>(nameof(Author), Model.Author == null ? null : new EmployeeWrapper(Model.Author));
            InitializeComplexProperty<EmployeeWrapper>(nameof(SenderEmployee), Model.SenderEmployee == null ? null : new EmployeeWrapper(Model.SenderEmployee));
            InitializeComplexProperty<EmployeeWrapper>(nameof(RecipientEmployee), Model.RecipientEmployee == null ? null : new EmployeeWrapper(Model.RecipientEmployee));
            InitializeComplexProperty<DocumentsRegistrationDetailsWrapper>(nameof(RegistrationDetailsOfRecipient), Model.RegistrationDetailsOfRecipient == null ? null : new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfRecipient));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
          CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.CopyToRecipients.Select(e => new EmployeeWrapper(e)));
          RegisterCollection(CopyToRecipients, Model.CopyToRecipients);
        }
	}

		public partial class EmployeeWrapper : WrapperBase<Employee>
	{
	    public EmployeeWrapper(Employee model) : base(model) { }
        #region SimpleProperties
        //PersonalNumber
        public System.String PersonalNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String PersonalNumberOriginalValue => GetOriginalValue<System.String>(nameof(PersonalNumber));
        public bool PersonalNumberIsChanged => GetIsChanged(nameof(PersonalNumber));
        //PhoneNumber
        public System.String PhoneNumber
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String PhoneNumberOriginalValue => GetOriginalValue<System.String>(nameof(PhoneNumber));
        public bool PhoneNumberIsChanged => GetIsChanged(nameof(PhoneNumber));
        //Email
        public System.String Email
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String EmailOriginalValue => GetOriginalValue<System.String>(nameof(Email));
        public bool EmailIsChanged => GetIsChanged(nameof(Email));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public PersonWrapper Person 
        {
            get { return GetWrapper<PersonWrapper>(); }
            set { SetComplexValue<Person, PersonWrapper>(Person, value); }
        }
	    public CompanyWrapper Company 
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(Company, value); }
        }
	    public EmployeesPositionWrapper Position 
        {
            get { return GetWrapper<EmployeesPositionWrapper>(); }
            set { SetComplexValue<EmployeesPosition, EmployeesPositionWrapper>(Position, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<PersonWrapper>(nameof(Person), Model.Person == null ? null : new PersonWrapper(Model.Person));
            InitializeComplexProperty<CompanyWrapper>(nameof(Company), Model.Company == null ? null : new CompanyWrapper(Model.Company));
            InitializeComplexProperty<EmployeesPositionWrapper>(nameof(Position), Model.Position == null ? null : new EmployeesPositionWrapper(Model.Position));
        }
	}

		public partial class OrderWrapper : WrapperBase<Order>
	{
	    public OrderWrapper(Order model) : base(model) { }
        #region SimpleProperties
        //Number
        public System.String Number
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));
        //DateOpen
        public System.DateTime DateOpen
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOpenOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateOpen));
        public bool DateOpenIsChanged => GetIsChanged(nameof(DateOpen));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class PaymentConditionWrapper : WrapperBase<PaymentCondition>
	{
	    public PaymentConditionWrapper(PaymentCondition model) : base(model) { }
        #region SimpleProperties
        //Part
        public System.Double Part
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double PartOriginalValue => GetOriginalValue<System.Double>(nameof(Part));
        public bool PartIsChanged => GetIsChanged(nameof(Part));
        //DaysToPoint
        public System.Int32 DaysToPoint
        {
          get { return GetValue<System.Int32>(); }
          set { SetValue(value); }
        }
        public System.Int32 DaysToPointOriginalValue => GetOriginalValue<System.Int32>(nameof(DaysToPoint));
        public bool DaysToPointIsChanged => GetIsChanged(nameof(DaysToPoint));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public PaymentConditionPointWrapper PaymentConditionPoint 
        {
            get { return GetWrapper<PaymentConditionPointWrapper>(); }
            set { SetComplexValue<PaymentConditionPoint, PaymentConditionPointWrapper>(PaymentConditionPoint, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<PaymentConditionPointWrapper>(nameof(PaymentConditionPoint), Model.PaymentConditionPoint == null ? null : new PaymentConditionPointWrapper(Model.PaymentConditionPoint));
        }
	}

		public partial class PaymentDocumentWrapper : WrapperBase<PaymentDocument>
	{
	    public PaymentDocumentWrapper(PaymentDocument model) : base(model) { }
        #region SimpleProperties
        //Number
        public System.String Number
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));
        //Vat
        public System.Double Vat
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double VatOriginalValue => GetOriginalValue<System.Double>(nameof(Vat));
        public bool VatIsChanged => GetIsChanged(nameof(Vat));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<PaymentActualWrapper> Payments { get; private set; }
        #endregion
        #region GetProperties
        public System.DateTime Date => GetValue<System.DateTime>(); 
        #endregion
        protected override void InitializeCollectionProperties()
        {
          if (Model.Payments == null) throw new ArgumentException("Payments cannot be null");
          Payments = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(Model.Payments.Select(e => new PaymentActualWrapper(e)));
          RegisterCollection(Payments, Model.Payments);
        }
	}

		public partial class FacilityWrapper : WrapperBase<Facility>
	{
	    public FacilityWrapper(Facility model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public FacilityTypeWrapper Type 
        {
            get { return GetWrapper<FacilityTypeWrapper>(); }
            set { SetComplexValue<FacilityType, FacilityTypeWrapper>(Type, value); }
        }
	    public CompanyWrapper OwnerCompany 
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(OwnerCompany, value); }
        }
	    public AddressWrapper Address 
        {
            get { return GetWrapper<AddressWrapper>(); }
            set { SetComplexValue<Address, AddressWrapper>(Address, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<FacilityTypeWrapper>(nameof(Type), Model.Type == null ? null : new FacilityTypeWrapper(Model.Type));
            InitializeComplexProperty<CompanyWrapper>(nameof(OwnerCompany), Model.OwnerCompany == null ? null : new CompanyWrapper(Model.OwnerCompany));
            InitializeComplexProperty<AddressWrapper>(nameof(Address), Model.Address == null ? null : new AddressWrapper(Model.Address));
        }
	}

		public partial class ProjectWrapper : WrapperBase<Project>
	{
	    public ProjectWrapper(Project model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //InWork
        public System.Boolean InWork
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean InWorkOriginalValue => GetOriginalValue<System.Boolean>(nameof(InWork));
        public bool InWorkIsChanged => GetIsChanged(nameof(InWork));
        //ForReport
        public System.Boolean ForReport
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean ForReportOriginalValue => GetOriginalValue<System.Boolean>(nameof(ForReport));
        public bool ForReportIsChanged => GetIsChanged(nameof(ForReport));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public ProjectTypeWrapper ProjectType 
        {
            get { return GetWrapper<ProjectTypeWrapper>(); }
            set { SetComplexValue<ProjectType, ProjectTypeWrapper>(ProjectType, value); }
        }
	    public UserWrapper Manager 
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(Manager, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<NoteWrapper> Notes { get; private set; }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProjectTypeWrapper>(nameof(ProjectType), Model.ProjectType == null ? null : new ProjectTypeWrapper(Model.ProjectType));
            InitializeComplexProperty<UserWrapper>(nameof(Manager), Model.Manager == null ? null : new UserWrapper(Model.Manager));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.Notes == null) throw new ArgumentException("Notes cannot be null");
          Notes = new ValidatableChangeTrackingCollection<NoteWrapper>(Model.Notes.Select(e => new NoteWrapper(e)));
          RegisterCollection(Notes, Model.Notes);
        }
	}

		public partial class UserRoleWrapper : WrapperBase<UserRole>
	{
	    public UserRoleWrapper(UserRole model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Role
        public HVTApp.Infrastructure.Role Role
        {
          get { return GetValue<HVTApp.Infrastructure.Role>(); }
          set { SetValue(value); }
        }
        public HVTApp.Infrastructure.Role RoleOriginalValue => GetOriginalValue<HVTApp.Infrastructure.Role>(nameof(Role));
        public bool RoleIsChanged => GetIsChanged(nameof(Role));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class SpecificationWrapper : WrapperBase<Specification>
	{
	    public SpecificationWrapper(Specification model) : base(model) { }
        #region SimpleProperties
        //Number
        public System.String Number
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));
        //Date
        public System.DateTime Date
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));
        //SignDate
        public System.Nullable<System.DateTime> SignDate
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> SignDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(SignDate));
        public bool SignDateIsChanged => GetIsChanged(nameof(SignDate));
        //Vat
        public System.Double Vat
        {
          get { return GetValue<System.Double>(); }
          set { SetValue(value); }
        }
        public System.Double VatOriginalValue => GetOriginalValue<System.Double>(nameof(Vat));
        public bool VatIsChanged => GetIsChanged(nameof(Vat));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public ContractWrapper Contract 
        {
            get { return GetWrapper<ContractWrapper>(); }
            set { SetComplexValue<Contract, ContractWrapper>(Contract, value); }
        }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ContractWrapper>(nameof(Contract), Model.Contract == null ? null : new ContractWrapper(Model.Contract));
        }
	}

		public partial class TenderWrapper : WrapperBase<Tender>
	{
	    public TenderWrapper(Tender model) : base(model) { }
        #region SimpleProperties
        //Link
        public System.String Link
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String LinkOriginalValue => GetOriginalValue<System.String>(nameof(Link));
        public bool LinkIsChanged => GetIsChanged(nameof(Link));
        //DateOpen
        public System.DateTime DateOpen
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateOpenOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateOpen));
        public bool DateOpenIsChanged => GetIsChanged(nameof(DateOpen));
        //DateClose
        public System.DateTime DateClose
        {
          get { return GetValue<System.DateTime>(); }
          set { SetValue(value); }
        }
        public System.DateTime DateCloseOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateClose));
        public bool DateCloseIsChanged => GetIsChanged(nameof(DateClose));
        //DateNotice
        public System.Nullable<System.DateTime> DateNotice
        {
          get { return GetValue<System.Nullable<System.DateTime>>(); }
          set { SetValue(value); }
        }
        public System.Nullable<System.DateTime> DateNoticeOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateNotice));
        public bool DateNoticeIsChanged => GetIsChanged(nameof(DateNotice));
        //DidNotTakePlace
        public System.Boolean DidNotTakePlace
        {
          get { return GetValue<System.Boolean>(); }
          set { SetValue(value); }
        }
        public System.Boolean DidNotTakePlaceOriginalValue => GetOriginalValue<System.Boolean>(nameof(DidNotTakePlace));
        public bool DidNotTakePlaceIsChanged => GetIsChanged(nameof(DidNotTakePlace));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public ProjectWrapper Project 
        {
            get { return GetWrapper<ProjectWrapper>(); }
            set { SetComplexValue<Project, ProjectWrapper>(Project, value); }
        }
	    public CompanyWrapper Winner 
        {
            get { return GetWrapper<CompanyWrapper>(); }
            set { SetComplexValue<Company, CompanyWrapper>(Winner, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<TenderTypeWrapper> Types { get; private set; }
        public IValidatableChangeTrackingCollection<CompanyWrapper> Participants { get; private set; }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<ProjectWrapper>(nameof(Project), Model.Project == null ? null : new ProjectWrapper(Model.Project));
            InitializeComplexProperty<CompanyWrapper>(nameof(Winner), Model.Winner == null ? null : new CompanyWrapper(Model.Winner));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.Types == null) throw new ArgumentException("Types cannot be null");
          Types = new ValidatableChangeTrackingCollection<TenderTypeWrapper>(Model.Types.Select(e => new TenderTypeWrapper(e)));
          RegisterCollection(Types, Model.Types);
          if (Model.Participants == null) throw new ArgumentException("Participants cannot be null");
          Participants = new ValidatableChangeTrackingCollection<CompanyWrapper>(Model.Participants.Select(e => new CompanyWrapper(e)));
          RegisterCollection(Participants, Model.Participants);
        }
	}

		public partial class TenderTypeWrapper : WrapperBase<TenderType>
	{
	    public TenderTypeWrapper(TenderType model) : base(model) { }
        #region SimpleProperties
        //Name
        public System.String Name
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
        public bool NameIsChanged => GetIsChanged(nameof(Name));
        //Type
        public HVTApp.Model.POCOs.TenderTypeEnum Type
        {
          get { return GetValue<HVTApp.Model.POCOs.TenderTypeEnum>(); }
          set { SetValue(value); }
        }
        public HVTApp.Model.POCOs.TenderTypeEnum TypeOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.TenderTypeEnum>(nameof(Type));
        public bool TypeIsChanged => GetIsChanged(nameof(Type));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
	}

		public partial class UserWrapper : WrapperBase<User>
	{
	    public UserWrapper(User model) : base(model) { }
        #region SimpleProperties
        //Login
        public System.String Login
        {
          get { return GetValue<System.String>(); }
          set { SetValue(value); }
        }
        public System.String LoginOriginalValue => GetOriginalValue<System.String>(nameof(Login));
        public bool LoginIsChanged => GetIsChanged(nameof(Login));
        //Password
        public System.Guid Password
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid PasswordOriginalValue => GetOriginalValue<System.Guid>(nameof(Password));
        public bool PasswordIsChanged => GetIsChanged(nameof(Password));
        //RoleCurrent
        public HVTApp.Infrastructure.Role RoleCurrent
        {
          get { return GetValue<HVTApp.Infrastructure.Role>(); }
          set { SetValue(value); }
        }
        public HVTApp.Infrastructure.Role RoleCurrentOriginalValue => GetOriginalValue<HVTApp.Infrastructure.Role>(nameof(RoleCurrent));
        public bool RoleCurrentIsChanged => GetIsChanged(nameof(RoleCurrent));
        //Id
        public System.Guid Id
        {
          get { return GetValue<System.Guid>(); }
          set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion
        #region ComplexProperties
	    public EmployeeWrapper Employee 
        {
            get { return GetWrapper<EmployeeWrapper>(); }
            set { SetComplexValue<Employee, EmployeeWrapper>(Employee, value); }
        }
        #endregion
        #region CollectionProperties
        public IValidatableChangeTrackingCollection<UserRoleWrapper> Roles { get; private set; }
        #endregion
        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<EmployeeWrapper>(nameof(Employee), Model.Employee == null ? null : new EmployeeWrapper(Model.Employee));
        }
        protected override void InitializeCollectionProperties()
        {
          if (Model.Roles == null) throw new ArgumentException("Roles cannot be null");
          Roles = new ValidatableChangeTrackingCollection<UserRoleWrapper>(Model.Roles.Select(e => new UserRoleWrapper(e)));
          RegisterCollection(Roles, Model.Roles);
        }
	}

	}
