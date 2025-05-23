using HVTApp.Model.POCOs;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Lookup
{
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("NotificationUnit")]
	public partial class NotificationUnitLookup : LookupItem<NotificationUnit>
	{
		public NotificationUnitLookup(NotificationUnit entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.DateTime Moment => Entity.Moment;

		[OrderStatus(1)]
        public HVTApp.Infrastructure.Enums.NotificationActionType ActionType => Entity.ActionType;

		[OrderStatus(1)]
        public System.Guid TargetEntityId => Entity.TargetEntityId;

		[OrderStatus(1)]
        public System.Guid SenderUserId => Entity.SenderUserId;

		[OrderStatus(1)]
        public HVTApp.Infrastructure.Role SenderRole => Entity.SenderRole;

		[OrderStatus(1)]
        public System.Guid RecipientUserId => Entity.RecipientUserId;

		[OrderStatus(1)]
        public HVTApp.Infrastructure.Role RecipientRole => Entity.RecipientRole;

		[OrderStatus(1)]
        public System.Boolean IsSentByEmail => Entity.IsSentByEmail;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public UserLookup SenderUser { get { return GetLookup<UserLookup>(); } }

		[OrderStatus(1)]
	    public UserLookup RecipientUser { get { return GetLookup<UserLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Объединение стран")]
	public partial class CountryUnionLookup : LookupItem<CountryUnion>
	{
		public CountryUnionLookup(CountryUnion entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(2)]
        public System.String Name => Entity.Name;

        #endregion
		[OrderStatus(1)]
	    public List<CountryLookup> Countries { get { return GetLookupEnum<CountryLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Бюджет")]
	public partial class BudgetLookup : LookupItem<Budget>
	{
		public BudgetLookup(Budget entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(110)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(80)]
        public System.DateTime DateStart => Entity.DateStart;

		[OrderStatus(70)]
        public System.DateTime DateFinish => Entity.DateFinish;

		[OrderStatus(100)]
        public System.String Name => Entity.Name;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Единица бюджета")]
	public partial class BudgetUnitLookup : LookupItem<BudgetUnit>
	{
		public BudgetUnitLookup(BudgetUnit entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(90)]
        public System.DateTime OrderInTakeDate => Entity.OrderInTakeDate;

		[OrderStatus(80)]
        public System.DateTime RealizationDate => Entity.RealizationDate;

		[OrderStatus(85)]
        public System.DateTime OrderInTakeDateByManager => Entity.OrderInTakeDateByManager;

		[OrderStatus(75)]
        public System.DateTime RealizationDateByManager => Entity.RealizationDateByManager;

		[OrderStatus(60)]
        public System.Double Cost => Entity.Cost;

		[OrderStatus(55)]
        public System.Double CostByManager => Entity.CostByManager;

		[OrderStatus(40)]
        public System.Boolean IsRemoved => Entity.IsRemoved;

        #endregion

        #region ComplexProperties
		[OrderStatus(100)]
	    public SalesUnitLookup SalesUnit { get { return GetLookup<SalesUnitLookup>(); } }

		[OrderStatus(50)]
	    public PaymentConditionSetLookup PaymentConditionSet { get { return GetLookup<PaymentConditionSetLookup>(); } }

		[OrderStatus(45)]
	    public PaymentConditionSetLookup PaymentConditionSetByManager { get { return GetLookup<PaymentConditionSetLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Конструктора - параметры (список)")]
	public partial class ConstructorParametersListLookup : LookupItem<ConstructorParametersList>
	{
		public ConstructorParametersListLookup(ConstructorParametersList entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String Name => Entity.Name;

        #endregion
		[OrderStatus(1)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Конструктора - параметры")]
	public partial class ConstructorsParametersLookup : LookupItem<ConstructorsParameters>
	{
		public ConstructorsParametersLookup(ConstructorsParameters entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String Name => Entity.Name;

        #endregion
		[OrderStatus(1)]
	    public List<UserLookup> Constructors { get { return GetLookupEnum<UserLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<ConstructorParametersListLookup> PatametersLists { get { return GetLookupEnum<ConstructorParametersListLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Расходы в процентах")]
	public partial class CostsPercentsLookup : LookupItem<CostsPercents>
	{
		public CostsPercentsLookup(CostsPercents entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(1)]
        public System.Double ManagmentCosts => Entity.ManagmentCosts;

		[OrderStatus(1)]
        public System.Double EconomicCosts => Entity.EconomicCosts;

		[OrderStatus(1)]
        public System.Double CommercialCosts => Entity.CommercialCosts;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Задание на создание нового продукта")]
	public partial class CreateNewProductTaskLookup : LookupItem<CreateNewProductTask>
	{
		public CreateNewProductTaskLookup(CreateNewProductTask entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.String Designation => Entity.Designation;

		[OrderStatus(8)]
        public System.String StructureCostNumber => Entity.StructureCostNumber;

		[OrderStatus(6)]
        public System.String Comment => Entity.Comment;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public ProductLookup Product { get { return GetLookup<ProductLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Департамент ОГК")]
	public partial class DesignDepartmentLookup : LookupItem<DesignDepartment>
	{
		public DesignDepartmentLookup(DesignDepartment entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(110)]
        public System.String Name => Entity.Name;

        #endregion

        #region ComplexProperties
		[OrderStatus(100)]
	    public UserLookup Head { get { return GetLookup<UserLookup>(); } }

        #endregion
		[OrderStatus(90)]
	    public List<UserLookup> Staff { get { return GetLookupEnum<UserLookup>().ToList(); } }
		[OrderStatus(50)]
	    public List<DesignDepartmentParametersLookup> ParameterSets { get { return GetLookupEnum<DesignDepartmentParametersLookup>().ToList(); } }
		[OrderStatus(40)]
	    public List<DesignDepartmentParametersAddedBlocksLookup> ParameterSetsAddedBlocks { get { return GetLookupEnum<DesignDepartmentParametersAddedBlocksLookup>().ToList(); } }
		[OrderStatus(30)]
	    public List<DesignDepartmentParametersSubTaskLookup> ParameterSetsSubTask { get { return GetLookupEnum<DesignDepartmentParametersSubTaskLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Задача")]
	public partial class DirectumTaskLookup : LookupItem<DirectumTask>
	{
		public DirectumTaskLookup(DirectumTask entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(80)]
        public System.Nullable<System.DateTime> StartPerformer => Entity.StartPerformer;

		[OrderStatus(75)]
        public System.DateTime FinishPlan => Entity.FinishPlan;

		[OrderStatus(70)]
        public System.Nullable<System.DateTime> FinishPerformer => Entity.FinishPerformer;

		[OrderStatus(65)]
        public System.Nullable<System.DateTime> FinishAuthor => Entity.FinishAuthor;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> StartResult => Entity.StartResult;

		[OrderStatus(1)]
        public System.String Status => Entity.Status;

		[OrderStatus(1)]
        public System.Boolean IsActual => Entity.IsActual;

        #endregion

        #region ComplexProperties
		[OrderStatus(100)]
	    public DirectumTaskGroupLookup Group { get { return GetLookup<DirectumTaskGroupLookup>(); } }

		[OrderStatus(90)]
	    public UserLookup Performer { get { return GetLookup<UserLookup>(); } }

		[OrderStatus(45)]
	    public DirectumTaskLookup ParentTask { get { return GetLookup<DirectumTaskLookup>(); } }

		[OrderStatus(40)]
	    public DirectumTaskLookup PreviousTask { get { return GetLookup<DirectumTaskLookup>(); } }

        #endregion
		[OrderStatus(55)]
	    public List<DirectumTaskMessageLookup> Messages { get { return GetLookupEnum<DirectumTaskMessageLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Группа задач")]
	public partial class DirectumTaskGroupLookup : LookupItem<DirectumTaskGroup>
	{
		public DirectumTaskGroupLookup(DirectumTaskGroup entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(100)]
        public System.String Title => Entity.Title;

		[OrderStatus(85)]
        public System.DateTime StartAuthor => Entity.StartAuthor;

		[OrderStatus(60)]
        public System.Boolean IsStoped => Entity.IsStoped;

		[OrderStatus(35)]
        public HVTApp.Model.POCOs.DirectumTaskPriority Priority => Entity.Priority;

		[OrderStatus(30)]
        public System.String Message => Entity.Message;

        #endregion

        #region ComplexProperties
		[OrderStatus(95)]
	    public UserLookup Author { get { return GetLookup<UserLookup>(); } }

        #endregion
		[OrderStatus(50)]
	    public List<UserLookup> Observers { get { return GetLookupEnum<UserLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<DirectumTaskGroupFileLookup> Files { get { return GetLookupEnum<DirectumTaskGroupFileLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Файл (DirectumLite)")]
	public partial class DirectumTaskGroupFileLookup : LookupItem<DirectumTaskGroupFile>
	{
		public DirectumTaskGroupFileLookup(DirectumTaskGroupFile entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.String Name => Entity.Name;

		[OrderStatus(5)]
        public System.DateTime LoadMoment => Entity.LoadMoment;

		[OrderStatus(1)]
        public System.Guid DirectumTaskGroupId => Entity.DirectumTaskGroupId;

        #endregion

        #region ComplexProperties
		[OrderStatus(2)]
	    public UserLookup Author { get { return GetLookup<UserLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Сообщение в задаче")]
	public partial class DirectumTaskMessageLookup : LookupItem<DirectumTaskMessage>
	{
		public DirectumTaskMessageLookup(DirectumTaskMessage entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.DateTime Moment => Entity.Moment;

		[OrderStatus(1)]
        public System.String Message => Entity.Message;

        #endregion

        #region ComplexProperties
		[OrderStatus(5)]
	    public UserLookup Author { get { return GetLookup<UserLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Номер документа")]
	public partial class DocumentNumberLookup : LookupItem<DocumentNumber>
	{
		public DocumentNumberLookup(DocumentNumber entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Int32 Number => Entity.Number;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Входящий запрос")]
	public partial class IncomingRequestLookup : LookupItem<IncomingRequest>
	{
		public IncomingRequestLookup(IncomingRequest entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(30)]
        public System.Boolean IsDone => Entity.IsDone;

		[OrderStatus(20)]
        public System.Boolean IsActual => Entity.IsActual;

		[OrderStatus(35)]
        public System.Nullable<System.DateTime> InstructionDate => Entity.InstructionDate;

		[OrderStatus(25)]
        public System.Nullable<System.DateTime> DoneDate => Entity.DoneDate;

        #endregion

        #region ComplexProperties
		[OrderStatus(50)]
	    public DocumentLookup Document { get { return GetLookup<DocumentLookup>(); } }

        #endregion
		[OrderStatus(40)]
	    public List<EmployeeLookup> Performers { get { return GetLookupEnum<EmployeeLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Нормо-час стоимость")]
	public partial class LaborHourCostLookup : LookupItem<LaborHourCost>
	{
		public LaborHourCostLookup(LaborHourCost entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(1)]
        public System.Double Sum => Entity.Sum;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Нормо-часы")]
	public partial class LaborHoursLookup : LookupItem<LaborHours>
	{
		public LaborHoursLookup(LaborHours entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(9)]
        public System.Double Amount => Entity.Amount;

		[OrderStatus(20)]
        public System.String Comment => Entity.Comment;

        #endregion
		[OrderStatus(8)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Запись лога")]
	public partial class LogUnitLookup : LookupItem<LogUnit>
	{
		public LogUnitLookup(LogUnit entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(50)]
        public System.DateTime Moment => Entity.Moment;

		[OrderStatus(30)]
        public System.String Head => Entity.Head;

		[OrderStatus(20)]
        public System.String Message => Entity.Message;

        #endregion

        #region ComplexProperties
		[OrderStatus(40)]
	    public UserLookup Author { get { return GetLookup<UserLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Причина проигрыша")]
	public partial class LosingReasonLookup : LookupItem<LosingReason>
	{
		public LosingReasonLookup(LosingReason entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String Name => Entity.Name;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Область рынка")]
	public partial class MarketFieldLookup : LookupItem<MarketField>
	{
		public MarketFieldLookup(MarketField entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.String Name => Entity.Name;

        #endregion
		[OrderStatus(9)]
	    public List<ActivityFieldLookup> ActivityFields { get { return GetLookupEnum<ActivityFieldLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Платеж совершённый")]
	public partial class PaymentActualLookup : LookupItem<PaymentActual>
	{
		public PaymentActualLookup(PaymentActual entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Guid SalesUnitId => Entity.SalesUnitId;

		[OrderStatus(1)]
        public System.Guid PaymentDocumentId => Entity.PaymentDocumentId;

		[OrderStatus(1)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(1)]
        public System.Double Sum => Entity.Sum;

		[OrderStatus(1)]
        public System.String Comment => Entity.Comment;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Условие платежа (точка отсчета)")]
	public partial class PaymentConditionPointLookup : LookupItem<PaymentConditionPoint>
	{
		public PaymentConditionPointLookup(PaymentConditionPoint entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(6)]
        public System.String Name => Entity.Name;

		[OrderStatus(1)]
        public HVTApp.Model.POCOs.PaymentConditionPointEnum PaymentConditionPointEnum => Entity.PaymentConditionPointEnum;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Платеж плановый")]
	public partial class PaymentPlannedLookup : LookupItem<PaymentPlanned>
	{
		public PaymentPlannedLookup(PaymentPlanned entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(1)]
        public System.DateTime DateCalculated => Entity.DateCalculated;

		[OrderStatus(1)]
        public System.Double Part => Entity.Part;

		[OrderStatus(-10)]
        public System.String Comment => Entity.Comment;

        #endregion

        #region ComplexProperties
		[OrderStatus(-5)]
	    public PaymentConditionLookup Condition { get { return GetLookup<PaymentConditionLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Штрафные санкции")]
	public partial class PenaltyLookup : LookupItem<Penalty>
	{
		public PenaltyLookup(Penalty entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Double PercentPerDay => Entity.PercentPerDay;

		[OrderStatus(1)]
        public System.Double PercentLimit => Entity.PercentLimit;

		[OrderStatus(1)]
        public System.Double PenaltyPaid => Entity.PenaltyPaid;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Расчет себестоимости оборудования")]
	public partial class PriceCalculationLookup : LookupItem<PriceCalculation>
	{
		public PriceCalculationLookup(PriceCalculation entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Boolean IsStarted => Entity.IsStarted;

		[OrderStatus(1)]
        public System.Boolean IsFinished => Entity.IsFinished;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> TaskOpenMoment => Entity.TaskOpenMoment;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> TaskCloseMoment => Entity.TaskCloseMoment;

		[OrderStatus(1)]
        public System.Boolean IsNeedExcelFile => Entity.IsNeedExcelFile;

		[OrderStatus(1)]
        public System.String Name => Entity.Name;

		[OrderStatus(1)]
        public System.Nullable<System.Guid> PriceEngineeringTasksId => Entity.PriceEngineeringTasksId;

		[OrderStatus(1)]
        public System.Boolean IsTceConnected => Entity.IsTceConnected;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public PriceCalculationHistoryItemLookup LastHistoryItem { get { return GetLookup<PriceCalculationHistoryItemLookup>(); } }

		[OrderStatus(1)]
	    public UserLookup Initiator { get { return GetLookup<UserLookup>(); } }

		[OrderStatus(1)]
	    public UserLookup FrontManager { get { return GetLookup<UserLookup>(); } }

        #endregion
		[OrderStatus(1)]
	    public List<PriceCalculationItemLookup> PriceCalculationItems { get { return GetLookupEnum<PriceCalculationItemLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<PriceCalculationHistoryItemLookup> History { get { return GetLookupEnum<PriceCalculationHistoryItemLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<PriceCalculationFileLookup> Files { get { return GetLookupEnum<PriceCalculationFileLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Расчет себестоимости оборудования (файл)")]
	public partial class PriceCalculationFileLookup : LookupItem<PriceCalculationFile>
	{
		public PriceCalculationFileLookup(PriceCalculationFile entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(20)]
        public System.DateTime CreationMoment => Entity.CreationMoment;

		[OrderStatus(1)]
        public System.Guid CalculationId => Entity.CalculationId;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Элемент истории расчета ПЗ")]
	public partial class PriceCalculationHistoryItemLookup : LookupItem<PriceCalculationHistoryItem>
	{
		public PriceCalculationHistoryItemLookup(PriceCalculationHistoryItem entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Guid PriceCalculationId => Entity.PriceCalculationId;

		[OrderStatus(50)]
        public System.DateTime Moment => Entity.Moment;

		[OrderStatus(10)]
        public HVTApp.Model.POCOs.PriceCalculationHistoryItemType Type => Entity.Type;

		[OrderStatus(-10)]
        public System.String Comment => Entity.Comment;

        #endregion

        #region ComplexProperties
		[OrderStatus(-15)]
	    public UserLookup User { get { return GetLookup<UserLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Единица расчета себестоимости оборудования")]
	public partial class PriceCalculationItemLookup : LookupItem<PriceCalculationItem>
	{
		public PriceCalculationItemLookup(PriceCalculationItem entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Guid PriceCalculationId => Entity.PriceCalculationId;

		[OrderStatus(1)]
        public System.Nullable<System.Guid> PriceEngineeringTaskId => Entity.PriceEngineeringTaskId;

		[OrderStatus(1)]
        public System.DateTime OrderInTakeDate => Entity.OrderInTakeDate;

		[OrderStatus(1)]
        public System.DateTime RealizationDate => Entity.RealizationDate;

		[OrderStatus(1)]
        public System.Nullable<System.Int32> PositionInTeamCenter => Entity.PositionInTeamCenter;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> FinishDate => Entity.FinishDate;

		[OrderStatus(1)]
        public System.Boolean HasPrice => Entity.HasPrice;

		[OrderStatus(1)]
        public System.Nullable<System.Double> Price => Entity.Price;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public PriceCalculationLookup PriceCalculation { get { return GetLookup<PriceCalculationLookup>(); } }

		[OrderStatus(1)]
	    public PaymentConditionSetLookup PaymentConditionSet { get { return GetLookup<PaymentConditionSetLookup>(); } }

        #endregion
		[OrderStatus(1)]
	    public List<SalesUnitLookup> SalesUnits { get { return GetLookupEnum<SalesUnitLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<StructureCostLookup> StructureCosts { get { return GetLookupEnum<StructureCostLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Параметры департамента ОГК")]
	public partial class DesignDepartmentParametersLookup : LookupItem<DesignDepartmentParameters>
	{
		public DesignDepartmentParametersLookup(DesignDepartmentParameters entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(-10)]
        public System.Guid DesignDepartmentId => Entity.DesignDepartmentId;

		[OrderStatus(10)]
        public System.String Name => Entity.Name;

        #endregion
		[OrderStatus(1)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Параметры департамента ОГК (добавленное оборудование)")]
	public partial class DesignDepartmentParametersAddedBlocksLookup : LookupItem<DesignDepartmentParametersAddedBlocks>
	{
		public DesignDepartmentParametersAddedBlocksLookup(DesignDepartmentParametersAddedBlocks entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(-10)]
        public System.Guid DesignDepartmentId => Entity.DesignDepartmentId;

		[OrderStatus(10)]
        public System.String Name => Entity.Name;

        #endregion
		[OrderStatus(1)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Параметры департамента ОГК (для подзадач)")]
	public partial class DesignDepartmentParametersSubTaskLookup : LookupItem<DesignDepartmentParametersSubTask>
	{
		public DesignDepartmentParametersSubTaskLookup(DesignDepartmentParametersSubTask entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(-10)]
        public System.Guid DesignDepartmentId => Entity.DesignDepartmentId;

		[OrderStatus(10)]
        public System.String Name => Entity.Name;

        #endregion
		[OrderStatus(1)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Настройки рассылки отчётов")]
	public partial class NotificationsReportsSettingsLookup : LookupItem<NotificationsReportsSettings>
	{
		public NotificationsReportsSettingsLookup(NotificationsReportsSettings entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.DateTime ChiefEngineerReportMoment => Entity.ChiefEngineerReportMoment;

        #endregion
		[OrderStatus(8)]
	    public List<UserLookup> ChiefEngineerReportDistributionList { get { return GetLookupEnum<UserLookup>().ToList(); } }
		[OrderStatus(7)]
	    public List<UserLookup> SavePaymentDocumentDistributionList { get { return GetLookupEnum<UserLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Технико-стоимостная проработка")]
	public partial class PriceEngineeringTaskLookup : LookupItem<PriceEngineeringTask>
	{
		public PriceEngineeringTaskLookup(PriceEngineeringTask entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(3000)]
        public System.Int32 Number => Entity.Number;

		[OrderStatus(2000)]
        public System.Nullable<System.Guid> ParentPriceEngineeringTasksId => Entity.ParentPriceEngineeringTasksId;

		[OrderStatus(950)]
        public System.Int32 Amount => Entity.Amount;

		[OrderStatus(100)]
        public System.Nullable<System.Guid> ParentPriceEngineeringTaskId => Entity.ParentPriceEngineeringTaskId;

		[OrderStatus(81)]
        public System.Nullable<System.DateTime> TermPriority => Entity.TermPriority;

		[OrderStatus(40)]
        public System.Boolean RequestForVerificationFromHead => Entity.RequestForVerificationFromHead;

		[OrderStatus(35)]
        public System.Boolean RequestForVerificationFromConstructor => Entity.RequestForVerificationFromConstructor;

		[OrderStatus(1)]
        public System.Boolean VerificationIsRequested => Entity.VerificationIsRequested;

		[OrderStatus(36)]
        public System.Boolean IsValidForProduction => Entity.IsValidForProduction;

		[OrderStatus(1)]
        public System.String TcePosition => Entity.TcePosition;

		[OrderStatus(1)]
        public System.Boolean HasAnyUpdateStructureCostNumberTaskNotFinished => Entity.HasAnyUpdateStructureCostNumberTaskNotFinished;

		[OrderStatus(1)]
        public System.Boolean NeedDesignDocumentationDevelopment => Entity.NeedDesignDocumentationDevelopment;

		[OrderStatus(1)]
        public System.Nullable<System.Int16> DaysToDesignDocumentationDevelopment => Entity.DaysToDesignDocumentationDevelopment;

		[OrderStatus(1)]
        public System.String DesignDocumentationAvailabilityComment => Entity.DesignDocumentationAvailabilityComment;

		[OrderStatus(1)]
        public System.Boolean NeedEquipment => Entity.NeedEquipment;

		[OrderStatus(1)]
        public System.Boolean IsInProcessByConstructor => Entity.IsInProcessByConstructor;

		[OrderStatus(1)]
        public System.Boolean IsFinishedByConstructor => Entity.IsFinishedByConstructor;

		[OrderStatus(1)]
        public System.Boolean IsAccepted => Entity.IsAccepted;

		[OrderStatus(1)]
        public System.Boolean IsAcceptedTotal => Entity.IsAcceptedTotal;

		[OrderStatus(1)]
        public System.Boolean IsStoppedTotal => Entity.IsStoppedTotal;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> StartMoment => Entity.StartMoment;

		[OrderStatus(1)]
        public System.Boolean IsStarted => Entity.IsStarted;

		[OrderStatus(1)]
        public System.Boolean AllProductBlocksHasSccNumbersInTce => Entity.AllProductBlocksHasSccNumbersInTce;

		[OrderStatus(1)]
        public System.Boolean HasDesignDocumentationInfo => Entity.HasDesignDocumentationInfo;

		[OrderStatus(1)]
        public System.Boolean IsFinishedByDesignDepartment => Entity.IsFinishedByDesignDepartment;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> MomentFinishByDesignDepartment => Entity.MomentFinishByDesignDepartment;

		[OrderStatus(1)]
        public System.Boolean IsTop => Entity.IsTop;

        #endregion

        #region ComplexProperties
		[OrderStatus(1900)]
	    public DesignDepartmentLookup DesignDepartment { get { return GetLookup<DesignDepartmentLookup>(); } }

		[OrderStatus(1800)]
	    public UserLookup UserConstructor { get { return GetLookup<UserLookup>(); } }

		[OrderStatus(1700)]
	    public UserLookup UserPlanMaker { get { return GetLookup<UserLookup>(); } }

		[OrderStatus(1750)]
	    public UserLookup UserConstructorInspector { get { return GetLookup<UserLookup>(); } }

		[OrderStatus(1)]
	    public UserLookup UserConstructorInitiator { get { return GetLookup<UserLookup>(); } }

		[OrderStatus(900)]
	    public ProductBlockLookup ProductBlockManager { get { return GetLookup<ProductBlockLookup>(); } }

		[OrderStatus(850)]
	    public ProductBlockLookup ProductBlockEngineer { get { return GetLookup<ProductBlockLookup>(); } }

		[OrderStatus(1)]
	    public SpecificationLookup Specification { get { return GetLookup<SpecificationLookup>(); } }

        #endregion
		[OrderStatus(800)]
	    public List<PriceEngineeringTaskProductBlockAddedLookup> ProductBlocksAdded { get { return GetLookupEnum<PriceEngineeringTaskProductBlockAddedLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<PriceEngineeringTaskProductBlockAddedLookup> ProductBlocksAddedActual { get { return GetLookupEnum<PriceEngineeringTaskProductBlockAddedLookup>().ToList(); } }
		[OrderStatus(610)]
	    public List<PriceEngineeringTaskFileTechnicalRequirementsLookup> FilesTechnicalRequirements { get { return GetLookupEnum<PriceEngineeringTaskFileTechnicalRequirementsLookup>().ToList(); } }
		[OrderStatus(600)]
	    public List<PriceEngineeringTaskFileAnswerLookup> FilesAnswers { get { return GetLookupEnum<PriceEngineeringTaskFileAnswerLookup>().ToList(); } }
		[OrderStatus(500)]
	    public List<PriceEngineeringTaskMessageLookup> Messages { get { return GetLookupEnum<PriceEngineeringTaskMessageLookup>().ToList(); } }
		[OrderStatus(90)]
	    public List<PriceEngineeringTaskLookup> ChildPriceEngineeringTasks { get { return GetLookupEnum<PriceEngineeringTaskLookup>().ToList(); } }
		[OrderStatus(80)]
	    public List<StructureCostVersionLookup> StructureCostVersions { get { return GetLookupEnum<StructureCostVersionLookup>().ToList(); } }
		[OrderStatus(80)]
	    public List<PriceCalculationItemLookup> PriceCalculationItems { get { return GetLookupEnum<PriceCalculationItemLookup>().ToList(); } }
		[OrderStatus(50)]
	    public List<PriceEngineeringTaskStatusLookup> Statuses { get { return GetLookupEnum<PriceEngineeringTaskStatusLookup>().ToList(); } }
		[OrderStatus(10)]
	    public List<SalesUnitLookup> SalesUnits { get { return GetLookupEnum<SalesUnitLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<UpdateStructureCostNumberTaskLookup> UpdateStructureCostNumberTasks { get { return GetLookupEnum<UpdateStructureCostNumberTaskLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Технико-стоимостная проработка (файл ответа ОГК)")]
	public partial class PriceEngineeringTaskFileAnswerLookup : LookupItem<PriceEngineeringTaskFileAnswer>
	{
		public PriceEngineeringTaskFileAnswerLookup(PriceEngineeringTaskFileAnswer entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(900)]
        public System.Guid PriceEngineeringTaskId => Entity.PriceEngineeringTaskId;

		[OrderStatus(850)]
        public System.Boolean IsActual => Entity.IsActual;

		[OrderStatus(800)]
        public System.DateTime CreationMoment => Entity.CreationMoment;

		[OrderStatus(700)]
        public System.String Name => Entity.Name;

		[OrderStatus(600)]
        public System.String Comment => Entity.Comment;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Технико-стоимостная проработка (файл технического задания)")]
	public partial class PriceEngineeringTaskFileTechnicalRequirementsLookup : LookupItem<PriceEngineeringTaskFileTechnicalRequirements>
	{
		public PriceEngineeringTaskFileTechnicalRequirementsLookup(PriceEngineeringTaskFileTechnicalRequirements entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(850)]
        public System.Boolean IsActual => Entity.IsActual;

		[OrderStatus(800)]
        public System.DateTime CreationMoment => Entity.CreationMoment;

		[OrderStatus(700)]
        public System.String Name => Entity.Name;

		[OrderStatus(600)]
        public System.String Comment => Entity.Comment;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Технико-стоимостная проработка (сообщение)")]
	public partial class PriceEngineeringTaskMessageLookup : LookupItem<PriceEngineeringTaskMessage>
	{
		public PriceEngineeringTaskMessageLookup(PriceEngineeringTaskMessage entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(900)]
        public System.Guid PriceEngineeringTaskId => Entity.PriceEngineeringTaskId;

		[OrderStatus(700)]
        public System.DateTime Moment => Entity.Moment;

		[OrderStatus(100)]
        public System.String Message => Entity.Message;

        #endregion

        #region ComplexProperties
		[OrderStatus(800)]
	    public UserLookup Author { get { return GetLookup<UserLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Технико-стоимостная проработка (добавленный блок)")]
	public partial class PriceEngineeringTaskProductBlockAddedLookup : LookupItem<PriceEngineeringTaskProductBlockAdded>
	{
		public PriceEngineeringTaskProductBlockAddedLookup(PriceEngineeringTaskProductBlockAdded entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(500)]
        public System.Guid PriceEngineeringTaskId => Entity.PriceEngineeringTaskId;

		[OrderStatus(950)]
        public System.Int32 Amount => Entity.Amount;

		[OrderStatus(800)]
        public System.Boolean IsOnBlock => Entity.IsOnBlock;

		[OrderStatus(950)]
        public System.Boolean IsRemoved => Entity.IsRemoved;

		[OrderStatus(1)]
        public System.Boolean HasSccNumberInTce => Entity.HasSccNumberInTce;

        #endregion

        #region ComplexProperties
		[OrderStatus(900)]
	    public ProductBlockLookup ProductBlock { get { return GetLookup<ProductBlockLookup>(); } }

        #endregion
		[OrderStatus(80)]
	    public List<StructureCostVersionLookup> StructureCostVersions { get { return GetLookupEnum<StructureCostVersionLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Технико-стоимостная проработка (группа)")]
	public partial class PriceEngineeringTasksLookup : LookupItem<PriceEngineeringTasks>
	{
		public PriceEngineeringTasksLookup(PriceEngineeringTasks entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(3000)]
        public System.Int32 Number => Entity.Number;

		[OrderStatus(3000)]
        public System.String NumberFull => Entity.NumberFull;

		[OrderStatus(2000)]
        public System.String TceNumber => Entity.TceNumber;

		[OrderStatus(1500)]
        public System.DateTime WorkUpTo => Entity.WorkUpTo;

		[OrderStatus(1400)]
        public System.String Comment => Entity.Comment;

		[OrderStatus(1350)]
        public System.String CommentBackOfficeBoss => Entity.CommentBackOfficeBoss;

		[OrderStatus(1)]
        public System.Boolean IsAccepted => Entity.IsAccepted;

		[OrderStatus(2000)]
        public System.Nullable<System.DateTime> StartMoment => Entity.StartMoment;

        #endregion

        #region ComplexProperties
		[OrderStatus(1900)]
	    public UserLookup UserManager { get { return GetLookup<UserLookup>(); } }

		[OrderStatus(1800)]
	    public UserLookup BackManager { get { return GetLookup<UserLookup>(); } }

        #endregion
		[OrderStatus(610)]
	    public List<PriceEngineeringTasksFileTechnicalRequirementsLookup> FilesTechnicalRequirements { get { return GetLookupEnum<PriceEngineeringTasksFileTechnicalRequirementsLookup>().ToList(); } }
		[OrderStatus(90)]
	    public List<PriceEngineeringTaskLookup> ChildPriceEngineeringTasks { get { return GetLookupEnum<PriceEngineeringTaskLookup>().ToList(); } }
		[OrderStatus(50)]
	    public List<PriceCalculationLookup> PriceCalculations { get { return GetLookupEnum<PriceCalculationLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Технико-стоимостная проработка (файл группы технических заданий)")]
	public partial class PriceEngineeringTasksFileTechnicalRequirementsLookup : LookupItem<PriceEngineeringTasksFileTechnicalRequirements>
	{
		public PriceEngineeringTasksFileTechnicalRequirementsLookup(PriceEngineeringTasksFileTechnicalRequirements entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(900)]
        public System.Guid PrEngTasksId => Entity.PrEngTasksId;

		[OrderStatus(850)]
        public System.Boolean IsActual => Entity.IsActual;

		[OrderStatus(800)]
        public System.DateTime CreationMoment => Entity.CreationMoment;

		[OrderStatus(700)]
        public System.String Name => Entity.Name;

		[OrderStatus(600)]
        public System.String Comment => Entity.Comment;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("PriceEngineeringTaskStatus")]
	public partial class PriceEngineeringTaskStatusLookup : LookupItem<PriceEngineeringTaskStatus>
	{
		public PriceEngineeringTaskStatusLookup(PriceEngineeringTaskStatus entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(900)]
        public System.Guid PriceEngineeringTaskId => Entity.PriceEngineeringTaskId;

		[OrderStatus(1)]
        public System.DateTime Moment => Entity.Moment;

		[OrderStatus(1)]
        public System.String Comment => Entity.Comment;

		[OrderStatus(1)]
        public System.Int32 StatusEnum => Entity.StatusEnum;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Технико-стоимостная проработка - версия стракчакоста")]
	public partial class StructureCostVersionLookup : LookupItem<StructureCostVersion>
	{
		public StructureCostVersionLookup(StructureCostVersion entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Nullable<System.Guid> PriceEngineeringTaskId => Entity.PriceEngineeringTaskId;

		[OrderStatus(1)]
        public System.Nullable<System.Guid> PriceEngineeringTaskProductBlockAddedId => Entity.PriceEngineeringTaskProductBlockAddedId;

		[OrderStatus(1)]
        public System.String OriginalStructureCostNumber => Entity.OriginalStructureCostNumber;

		[OrderStatus(1)]
        public System.Nullable<System.Int32> Version => Entity.Version;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("UpdateStructureCostNumberTask")]
	public partial class UpdateStructureCostNumberTaskLookup : LookupItem<UpdateStructureCostNumberTask>
	{
		public UpdateStructureCostNumberTaskLookup(UpdateStructureCostNumberTask entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.DateTime MomentStart => Entity.MomentStart;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> MomentFinish => Entity.MomentFinish;

		[OrderStatus(1)]
        public System.String StructureCostNumberOriginal => Entity.StructureCostNumberOriginal;

		[OrderStatus(1)]
        public System.String StructureCostNumber => Entity.StructureCostNumber;

		[OrderStatus(1)]
        public System.Nullable<System.Boolean> IsAccepted => Entity.IsAccepted;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public ProductBlockLookup ProductBlock { get { return GetLookup<ProductBlockLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Категория продукта")]
	public partial class ProductCategoryLookup : LookupItem<ProductCategory>
	{
		public ProductCategoryLookup(ProductCategory entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(90)]
        public System.String NameFull => Entity.NameFull;

		[OrderStatus(80)]
        public System.String NameShort => Entity.NameShort;

        #endregion
		[OrderStatus(50)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Director)] [AllowEditAttribute(Infrastructure.Role.ReportMaker)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Стоимость и ПЗ категории продукта")]
	public partial class ProductCategoryPriceAndCostLookup : LookupItem<ProductCategoryPriceAndCost>
	{
		public ProductCategoryPriceAndCostLookup(ProductCategoryPriceAndCost entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(80)]
        public System.Double Cost => Entity.Cost;

		[OrderStatus(70)]
        public System.Double Price => Entity.Price;

		[OrderStatus(70)]
        public System.String StructureCost => Entity.StructureCost;

        #endregion

        #region ComplexProperties
		[OrderStatus(90)]
	    public ProductCategoryLookup Category { get { return GetLookup<ProductCategoryLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Включенное в стоимость оборудование")]
	public partial class ProductIncludedLookup : LookupItem<ProductIncluded>
	{
		public ProductIncludedLookup(ProductIncluded entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(5)]
        public System.Int32 Amount => Entity.Amount;

		[OrderStatus(3)]
        public System.Nullable<System.Double> CustomFixedPrice => Entity.CustomFixedPrice;

		[OrderStatus(1)]
        public System.Int32 ParentsCount => Entity.ParentsCount;

		[OrderStatus(1)]
        public System.Double AmountOnUnit => Entity.AmountOnUnit;

        #endregion

        #region ComplexProperties
		[OrderStatus(10)]
	    public ProductLookup Product { get { return GetLookup<ProductLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Обозначение продукта")]
	public partial class ProductDesignationLookup : LookupItem<ProductDesignation>
	{
		public ProductDesignationLookup(ProductDesignation entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.String Designation => Entity.Designation;

        #endregion
		[OrderStatus(1)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<ProductDesignationLookup> Parents { get { return GetLookupEnum<ProductDesignationLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Тип продукта")]
	public partial class ProductTypeLookup : LookupItem<ProductType>
	{
		public ProductTypeLookup(ProductType entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String Name => Entity.Name;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Обозначение типа продукта")]
	public partial class ProductTypeDesignationLookup : LookupItem<ProductTypeDesignation>
	{
		public ProductTypeDesignationLookup(ProductTypeDesignation entity) : base(entity) 
		{
		}
		
        #region ComplexProperties
		[OrderStatus(10)]
	    public ProductTypeLookup ProductType { get { return GetLookup<ProductTypeLookup>(); } }

        #endregion
		[OrderStatus(1)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Тип проекта")]
	public partial class ProjectTypeLookup : LookupItem<ProjectType>
	{
		public ProjectTypeLookup(ProjectType entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String Name => Entity.Name;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Стандартный маржинальный доход")]
	public partial class StandartMarginalIncomeLookup : LookupItem<StandartMarginalIncome>
	{
		public StandartMarginalIncomeLookup(StandartMarginalIncome entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.Double MarginalIncome => Entity.MarginalIncome;

        #endregion
		[OrderStatus(9)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Стандартный срок производства")]
	public partial class StandartProductionTermLookup : LookupItem<StandartProductionTerm>
	{
		public StandartProductionTermLookup(StandartProductionTerm entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.Int32 ProductionTerm => Entity.ProductionTerm;

        #endregion
		[OrderStatus(9)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Стракчакост")]
	public partial class StructureCostLookup : LookupItem<StructureCost>
	{
		public StructureCostLookup(StructureCost entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Guid PriceCalculationItemId => Entity.PriceCalculationItemId;

		[OrderStatus(1)]
        public System.String Number => Entity.Number;

		[OrderStatus(1)]
        public System.String OriginalStructureCostNumber => Entity.OriginalStructureCostNumber;

		[OrderStatus(1)]
        public System.Double AmountNumerator => Entity.AmountNumerator;

		[OrderStatus(1)]
        public System.Double AmountDenomerator => Entity.AmountDenomerator;

		[OrderStatus(1)]
        public System.Double Amount => Entity.Amount;

		[OrderStatus(1)]
        public System.Nullable<System.Double> UnitPrice => Entity.UnitPrice;

		[OrderStatus(1)]
        public System.Nullable<System.Double> Total => Entity.Total;

		[OrderStatus(1)]
        public System.String Comment => Entity.Comment;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public ProductBlockLookup OriginalStructureCostProductBlock { get { return GetLookup<ProductBlockLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Шеф-монтаж")]
	public partial class SupervisionLookup : LookupItem<Supervision>
	{
		public SupervisionLookup(Supervision entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(40)]
        public System.Nullable<System.DateTime> DateFinish => Entity.DateFinish;

		[OrderStatus(35)]
        public System.Nullable<System.DateTime> DateRequired => Entity.DateRequired;

		[OrderStatus(30)]
        public System.String ClientOrderNumber => Entity.ClientOrderNumber;

		[OrderStatus(20)]
        public System.String ServiceOrderNumber => Entity.ServiceOrderNumber;

        #endregion

        #region ComplexProperties
		[OrderStatus(50)]
	    public SalesUnitLookup SalesUnit { get { return GetLookup<SalesUnitLookup>(); } }

		[OrderStatus(45)]
	    public SalesUnitLookup SupervisionUnit { get { return GetLookup<SalesUnitLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Счёт на оплату (задание)")]
	public partial class TaskInvoiceForPaymentLookup : LookupItem<TaskInvoiceForPayment>
	{
		public TaskInvoiceForPaymentLookup(TaskInvoiceForPayment entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(200)]
        public System.Nullable<System.DateTime> MomentStart => Entity.MomentStart;

		[OrderStatus(190)]
        public System.Nullable<System.DateTime> MomentFinish => Entity.MomentFinish;

		[OrderStatus(190)]
        public System.Nullable<System.DateTime> MomentFinishByPlanMaker => Entity.MomentFinishByPlanMaker;

		[OrderStatus(1)]
        public System.Boolean OriginalIsRequired => Entity.OriginalIsRequired;

		[OrderStatus(1)]
        public System.String Comment => Entity.Comment;

		[OrderStatus(1)]
        public System.Boolean PlanMakerIsRequired => Entity.PlanMakerIsRequired;

        #endregion

        #region ComplexProperties
		[OrderStatus(50)]
	    public UserLookup BackManager { get { return GetLookup<UserLookup>(); } }

		[OrderStatus(51)]
	    public UserLookup PlanMaker { get { return GetLookup<UserLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Счёт на оплату (строка задания)")]
	public partial class TaskInvoiceForPaymentItemLookup : LookupItem<TaskInvoiceForPaymentItem>
	{
		public TaskInvoiceForPaymentItemLookup(TaskInvoiceForPaymentItem entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Guid TaskId => Entity.TaskId;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public PriceEngineeringTaskLookup PriceEngineeringTask { get { return GetLookup<PriceEngineeringTaskLookup>(); } }

		[OrderStatus(1)]
	    public TechnicalRequrementsLookup TechnicalRequrements { get { return GetLookup<TechnicalRequrementsLookup>(); } }

		[OrderStatus(1)]
	    public PaymentConditionLookup PaymentCondition { get { return GetLookup<PaymentConditionLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Файл-ответ ОГК")]
	public partial class AnswerFileTceLookup : LookupItem<AnswerFileTce>
	{
		public AnswerFileTceLookup(AnswerFileTce entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(300)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(1)]
        public System.Guid TechnicalRequrementsTaskId => Entity.TechnicalRequrementsTaskId;

		[OrderStatus(20)]
        public System.String Name => Entity.Name;

		[OrderStatus(10)]
        public System.String Comment => Entity.Comment;

		[OrderStatus(2)]
        public System.Boolean IsActual => Entity.IsActual;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Файл расчета транспортных затрат")]
	public partial class ShippingCostFileLookup : LookupItem<ShippingCostFile>
	{
		public ShippingCostFileLookup(ShippingCostFile entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Guid TechnicalRequrementsTaskId => Entity.TechnicalRequrementsTaskId;

		[OrderStatus(90)]
        public System.DateTime Moment => Entity.Moment;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Тех.задание")]
	public partial class TechnicalRequrementsLookup : LookupItem<TechnicalRequrements>
	{
		public TechnicalRequrementsLookup(TechnicalRequrements entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Guid TaskId => Entity.TaskId;

		[OrderStatus(16)]
        public System.Nullable<System.DateTime> OrderInTakeDate => Entity.OrderInTakeDate;

		[OrderStatus(14)]
        public System.Nullable<System.DateTime> RealizationDate => Entity.RealizationDate;

		[OrderStatus(5)]
        public System.String Comment => Entity.Comment;

		[OrderStatus(2)]
        public System.Boolean IsActual => Entity.IsActual;

		[OrderStatus(3)]
        public System.Nullable<System.Int32> PositionInTeamCenter => Entity.PositionInTeamCenter;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public SpecificationLookup Specification { get { return GetLookup<SpecificationLookup>(); } }

        #endregion
		[OrderStatus(20)]
	    public List<SalesUnitLookup> SalesUnits { get { return GetLookupEnum<SalesUnitLookup>().ToList(); } }
		[OrderStatus(10)]
	    public List<TechnicalRequrementsFileLookup> Files { get { return GetLookupEnum<TechnicalRequrementsFileLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Тех.задание (файл)")]
	public partial class TechnicalRequrementsFileLookup : LookupItem<TechnicalRequrementsFile>
	{
		public TechnicalRequrementsFileLookup(TechnicalRequrementsFile entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(300)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(20)]
        public System.String Name => Entity.Name;

		[OrderStatus(10)]
        public System.String Comment => Entity.Comment;

		[OrderStatus(2)]
        public System.Boolean IsActual => Entity.IsActual;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Тех.задание (задача)")]
	public partial class TechnicalRequrementsTaskLookup : LookupItem<TechnicalRequrementsTask>
	{
		public TechnicalRequrementsTaskLookup(TechnicalRequrementsTask entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(4)]
        public System.String TceNumber => Entity.TceNumber;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> LastOpenBackManagerMoment => Entity.LastOpenBackManagerMoment;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> LastOpenFrontManagerMoment => Entity.LastOpenFrontManagerMoment;

		[OrderStatus(-4)]
        public System.Boolean LogisticsCalculationRequired => Entity.LogisticsCalculationRequired;

		[OrderStatus(-5)]
        public System.Boolean ExcelFileIsRequired => Entity.ExcelFileIsRequired;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> DesiredFinishDate => Entity.DesiredFinishDate;

		[OrderStatus(3)]
        public System.Nullable<System.DateTime> Start => Entity.Start;

		[OrderStatus(2)]
        public System.Nullable<System.DateTime> Finish => Entity.Finish;

		[OrderStatus(1)]
        public System.Boolean IsStarted => Entity.IsStarted;

		[OrderStatus(1)]
        public System.Boolean IsFinished => Entity.IsFinished;

		[OrderStatus(1)]
        public System.Boolean IsRejected => Entity.IsRejected;

		[OrderStatus(1)]
        public System.Boolean IsStopped => Entity.IsStopped;

		[OrderStatus(1)]
        public System.Boolean IsAccepted => Entity.IsAccepted;

		[OrderStatus(1)]
        public System.String Products => Entity.Products;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public UserLookup BackManager { get { return GetLookup<UserLookup>(); } }

		[OrderStatus(-10)]
	    public UserLookup FrontManager { get { return GetLookup<UserLookup>(); } }

		[OrderStatus(1)]
	    public TechnicalRequrementsTaskHistoryElementLookup LastHistoryElement { get { return GetLookup<TechnicalRequrementsTaskHistoryElementLookup>(); } }

        #endregion
		[OrderStatus(20)]
	    public List<TechnicalRequrementsLookup> Requrements { get { return GetLookupEnum<TechnicalRequrementsLookup>().ToList(); } }
		[OrderStatus(-10)]
	    public List<PriceCalculationLookup> PriceCalculations { get { return GetLookupEnum<PriceCalculationLookup>().ToList(); } }
		[OrderStatus(-6)]
	    public List<AnswerFileTceLookup> AnswerFiles { get { return GetLookupEnum<AnswerFileTceLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<ShippingCostFileLookup> ShippingCostFiles { get { return GetLookupEnum<ShippingCostFileLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<TechnicalRequrementsTaskHistoryElementLookup> HistoryElements { get { return GetLookupEnum<TechnicalRequrementsTaskHistoryElementLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Статус тех.задания (задача)")]
	public partial class TechnicalRequrementsTaskHistoryElementLookup : LookupItem<TechnicalRequrementsTaskHistoryElement>
	{
		public TechnicalRequrementsTaskHistoryElementLookup(TechnicalRequrementsTaskHistoryElement entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Guid TechnicalRequrementsTaskId => Entity.TechnicalRequrementsTaskId;

		[OrderStatus(90)]
        public System.DateTime Moment => Entity.Moment;

		[OrderStatus(80)]
        public HVTApp.Model.POCOs.TechnicalRequrementsTaskHistoryElementType Type => Entity.Type;

		[OrderStatus(5)]
        public System.String Comment => Entity.Comment;

        #endregion

        #region ComplexProperties
		[OrderStatus(70)]
	    public UserLookup User { get { return GetLookup<UserLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Группа пользователей")]
	public partial class UserGroupLookup : LookupItem<UserGroup>
	{
		public UserGroupLookup(UserGroup entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String Name => Entity.Name;

        #endregion
		[OrderStatus(1)]
	    public List<UserLookup> Users { get { return GetLookupEnum<UserLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Общие настройки")]
	public partial class GlobalPropertiesLookup : LookupItem<GlobalProperties>
	{
		public GlobalPropertiesLookup(GlobalProperties entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(20)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(1)]
        public System.Int32 ActualPriceTerm => Entity.ActualPriceTerm;

		[OrderStatus(1)]
        public System.Int32 StandartTermFromStartToEndProduction => Entity.StandartTermFromStartToEndProduction;

		[OrderStatus(1)]
        public System.Int32 StandartTermFromPickToEndProduction => Entity.StandartTermFromPickToEndProduction;

		[OrderStatus(1)]
        public System.Double Vat => Entity.Vat;

		[OrderStatus(500)]
        public System.String IncomingRequestsPath => Entity.IncomingRequestsPath;

		[OrderStatus(501)]
        public System.String DirectumAttachmentsPath => Entity.DirectumAttachmentsPath;

		[OrderStatus(502)]
        public System.String TechnicalRequrementsFilesPath => Entity.TechnicalRequrementsFilesPath;

		[OrderStatus(503)]
        public System.String TechnicalRequrementsFilesAnswersPath => Entity.TechnicalRequrementsFilesAnswersPath;

		[OrderStatus(503)]
        public System.String ShippingCostFilesPath => Entity.ShippingCostFilesPath;

		[OrderStatus(504)]
        public System.String PriceCalculationsFilesPath => Entity.PriceCalculationsFilesPath;

		[OrderStatus(505)]
        public System.String LogsPath => Entity.LogsPath;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> LastDeveloperVizit => Entity.LastDeveloperVizit;

        #endregion

        #region ComplexProperties
		[OrderStatus(19)]
	    public CompanyLookup OurCompany { get { return GetLookup<CompanyLookup>(); } }

		[OrderStatus(1)]
	    public PaymentConditionSetLookup StandartPaymentsConditionSet { get { return GetLookup<PaymentConditionSetLookup>(); } }

		[OrderStatus(1)]
	    public ParameterLookup NewProductParameter { get { return GetLookup<ParameterLookup>(); } }

		[OrderStatus(1)]
	    public ParameterGroupLookup NewProductParameterGroup { get { return GetLookup<ParameterGroupLookup>(); } }

		[OrderStatus(301)]
	    public ParameterLookup ServiceParameter { get { return GetLookup<ParameterLookup>(); } }

		[OrderStatus(302)]
	    public ParameterLookup SupervisionParameter { get { return GetLookup<ParameterLookup>(); } }

		[OrderStatus(303)]
	    public ParameterGroupLookup VoltageGroup { get { return GetLookup<ParameterGroupLookup>(); } }

		[OrderStatus(304)]
	    public ParameterGroupLookup IsolationMaterialGroup { get { return GetLookup<ParameterGroupLookup>(); } }

		[OrderStatus(305)]
	    public ParameterGroupLookup IsolationColorGroup { get { return GetLookup<ParameterGroupLookup>(); } }

		[OrderStatus(306)]
	    public ParameterGroupLookup IsolationDpuGroup { get { return GetLookup<ParameterGroupLookup>(); } }

		[OrderStatus(-50)]
	    public ParameterGroupLookup ComplectDesignationGroup { get { return GetLookup<ParameterGroupLookup>(); } }

		[OrderStatus(-50)]
	    public ParameterLookup ComplectsParameter { get { return GetLookup<ParameterLookup>(); } }

		[OrderStatus(-50)]
	    public ParameterGroupLookup ComplectsGroup { get { return GetLookup<ParameterGroupLookup>(); } }

		[OrderStatus(1)]
	    public ProjectTypeLookup DefaultProjectType { get { return GetLookup<ProjectTypeLookup>(); } }

		[OrderStatus(1)]
	    public EmployeeLookup RecipientSupervisionLetterEmployee { get { return GetLookup<EmployeeLookup>(); } }

		[OrderStatus(1)]
	    public EmployeeLookup SenderOfferEmployee { get { return GetLookup<EmployeeLookup>(); } }

		[OrderStatus(1)]
	    public ActivityFieldLookup HvtProducersActivityField { get { return GetLookup<ActivityFieldLookup>(); } }

		[OrderStatus(1)]
	    public PaymentConditionSetLookup PaymentConditionSet { get { return GetLookup<PaymentConditionSetLookup>(); } }

		[OrderStatus(1)]
	    public UserLookup Developer { get { return GetLookup<UserLookup>(); } }

		[OrderStatus(1)]
	    public ProductLookup ProductIncludedDefault { get { return GetLookup<ProductLookup>(); } }

		[OrderStatus(-100)]
	    public ParameterLookup EmptyParameterCurrentTransformersSet { get { return GetLookup<ParameterLookup>(); } }

		[OrderStatus(-200)]
	    public ParameterLookup ParameterCurrentTransformersSetCustom { get { return GetLookup<ParameterLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Economist)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Адрес")]
	public partial class AddressLookup : LookupItem<Address>
	{
		public AddressLookup(Address entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String Description => Entity.Description;

        #endregion

        #region ComplexProperties
		[OrderStatus(5)]
	    public LocalityLookup Locality { get { return GetLookup<LocalityLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Страна")]
	public partial class CountryLookup : LookupItem<Country>
	{
		public CountryLookup(Country entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String Name => Entity.Name;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Округ")]
	public partial class DistrictLookup : LookupItem<District>
	{
		public DistrictLookup(District entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.String Name => Entity.Name;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public CountryLookup Country { get { return GetLookup<CountryLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Населенный пункт")]
	public partial class LocalityLookup : LookupItem<Locality>
	{
		public LocalityLookup(Locality entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.String Name => Entity.Name;

		[OrderStatus(1)]
        public System.Boolean IsCountryCapital => Entity.IsCountryCapital;

		[OrderStatus(1)]
        public System.Boolean IsDistrictCapital => Entity.IsDistrictCapital;

		[OrderStatus(1)]
        public System.Boolean IsRegionCapital => Entity.IsRegionCapital;

		[OrderStatus(1)]
        public System.Nullable<System.Double> DistanceToEkb => Entity.DistanceToEkb;

        #endregion

        #region ComplexProperties
		[OrderStatus(9)]
	    public LocalityTypeLookup LocalityType { get { return GetLookup<LocalityTypeLookup>(); } }

		[OrderStatus(8)]
	    public RegionLookup Region { get { return GetLookup<RegionLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Тип населенного пункта")]
	public partial class LocalityTypeLookup : LookupItem<LocalityType>
	{
		public LocalityTypeLookup(LocalityType entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(2)]
        public System.String FullName => Entity.FullName;

		[OrderStatus(1)]
        public System.String ShortName => Entity.ShortName;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Регион")]
	public partial class RegionLookup : LookupItem<Region>
	{
		public RegionLookup(Region entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(2)]
        public System.String Name => Entity.Name;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public DistrictLookup District { get { return GetLookup<DistrictLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Сумма (фэйк)")]
	public partial class SumLookup : LookupItem<Sum>
	{
		public SumLookup(Sum entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public HVTApp.Model.POCOs.SumType Type => Entity.Type;

		[OrderStatus(1)]
        public HVTApp.Model.POCOs.Currency Currency => Entity.Currency;

		[OrderStatus(1)]
        public System.Decimal Value => Entity.Value;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Курс обмена валют")]
	public partial class CurrencyExchangeRateLookup : LookupItem<CurrencyExchangeRate>
	{
		public CurrencyExchangeRateLookup(CurrencyExchangeRate entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(1)]
        public HVTApp.Model.POCOs.Currency FirstCurrency => Entity.FirstCurrency;

		[OrderStatus(1)]
        public HVTApp.Model.POCOs.Currency SecondCurrency => Entity.SecondCurrency;

		[OrderStatus(1)]
        public System.Double ExchangeRate => Entity.ExchangeRate;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Заметка")]
	public partial class NoteLookup : LookupItem<Note>
	{
		public NoteLookup(Note entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(4)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(3)]
        public System.String Text => Entity.Text;

		[OrderStatus(2)]
        public System.Boolean IsImportant => Entity.IsImportant;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Единица ТКП")]
	public partial class OfferUnitLookup : LookupItem<OfferUnit>
	{
		public OfferUnitLookup(OfferUnit entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Double Cost => Entity.Cost;

		[OrderStatus(1)]
        public System.Nullable<System.Double> CostDelivery => Entity.CostDelivery;

		[OrderStatus(1)]
        public System.Boolean CostDeliveryIncluded => Entity.CostDeliveryIncluded;

		[OrderStatus(1)]
        public System.Int32 ProductionTerm => Entity.ProductionTerm;

		[OrderStatus(1)]
        public System.String Comment => Entity.Comment;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public OfferLookup Offer { get { return GetLookup<OfferLookup>(); } }

		[OrderStatus(1)]
	    public FacilityLookup Facility { get { return GetLookup<FacilityLookup>(); } }

		[OrderStatus(1)]
	    public ProductLookup Product { get { return GetLookup<ProductLookup>(); } }

		[OrderStatus(1)]
	    public PaymentConditionSetLookup PaymentConditionSet { get { return GetLookup<PaymentConditionSetLookup>(); } }

        #endregion
		[OrderStatus(1)]
	    public List<ProductIncludedLookup> ProductsIncluded { get { return GetLookupEnum<ProductIncludedLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Условия оплаты")]
	public partial class PaymentConditionSetLookup : LookupItem<PaymentConditionSet>
	{
		public PaymentConditionSetLookup(PaymentConditionSet entity) : base(entity) 
		{
		}
			}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Блок")]
	public partial class ProductBlockLookup : LookupItem<ProductBlock>
	{
		public ProductBlockLookup(ProductBlock entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(8)]
        public System.String DesignationSpecial => Entity.DesignationSpecial;

		[OrderStatus(7)]
        public System.String StructureCostNumber => Entity.StructureCostNumber;

		[OrderStatus(1)]
        public System.String Design => Entity.Design;

		[OrderStatus(1)]
        public System.Double Weight => Entity.Weight;

		[OrderStatus(1)]
        public System.Nullable<System.Double> LaborCosts => Entity.LaborCosts;

		[OrderStatus(9)]
        public System.String Designation => Entity.Designation;

		[OrderStatus(1)]
        public System.Boolean HasPrice => Entity.HasPrice;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> LastPriceDate => Entity.LastPriceDate;

		[OrderStatus(1)]
        public System.Boolean HasFixedPrice => Entity.HasFixedPrice;

		[OrderStatus(1)]
        public System.Boolean IsNew => Entity.IsNew;

		[OrderStatus(1)]
        public System.Boolean IsService => Entity.IsService;

		[OrderStatus(1)]
        public System.Boolean IsSupervision => Entity.IsSupervision;

		[OrderStatus(1)]
        public System.Boolean IsDelivery => Entity.IsDelivery;

        #endregion

        #region ComplexProperties
		[OrderStatus(10)]
	    public ProductTypeLookup ProductType { get { return GetLookup<ProductTypeLookup>(); } }

        #endregion
		[OrderStatus(-10)]
	    public List<ParameterLookup> ParametersOrdered { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Зависимое оборудование")]
	public partial class ProductDependentLookup : LookupItem<ProductDependent>
	{
		public ProductDependentLookup(ProductDependent entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Guid MainProductId => Entity.MainProductId;

		[OrderStatus(5)]
        public System.Int32 Amount => Entity.Amount;

        #endregion

        #region ComplexProperties
		[OrderStatus(10)]
	    public ProductLookup Product { get { return GetLookup<ProductLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Банковские реквизиты")]
	public partial class BankDetailsLookup : LookupItem<BankDetails>
	{
		public BankDetailsLookup(BankDetails entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(5)]
        public System.String BankName => Entity.BankName;

		[OrderStatus(4)]
        public System.String BankIdentificationCode => Entity.BankIdentificationCode;

		[OrderStatus(3)]
        public System.String CorrespondentAccount => Entity.CorrespondentAccount;

		[OrderStatus(2)]
        public System.String CheckingAccount => Entity.CheckingAccount;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Economist)] [AllowEditAttribute(Infrastructure.Role.DataBaseFiller)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Компания")]
	public partial class CompanyLookup : LookupItem<Company>
	{
		public CompanyLookup(Company entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(20)]
        public System.String FullName => Entity.FullName;

		[OrderStatus(15)]
        public System.String ShortName => Entity.ShortName;

		[OrderStatus(1)]
        public System.String Inn => Entity.Inn;

		[OrderStatus(1)]
        public System.String Kpp => Entity.Kpp;

        #endregion

        #region ComplexProperties
		[OrderStatus(5)]
	    public CompanyFormLookup Form { get { return GetLookup<CompanyFormLookup>(); } }

		[OrderStatus(1)]
	    public CompanyLookup ParentCompany { get { return GetLookup<CompanyLookup>(); } }

		[OrderStatus(1)]
	    public AddressLookup AddressLegal { get { return GetLookup<AddressLookup>(); } }

        #endregion
		[OrderStatus(-10)]
	    public List<BankDetailsLookup> BankDetailsList { get { return GetLookupEnum<BankDetailsLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<ActivityFieldLookup> ActivityFilds { get { return GetLookupEnum<ActivityFieldLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Организационная форма")]
	public partial class CompanyFormLookup : LookupItem<CompanyForm>
	{
		public CompanyFormLookup(CompanyForm entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String FullName => Entity.FullName;

		[OrderStatus(1)]
        public System.String ShortName => Entity.ShortName;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Economist)] [AllowEditAttribute(Infrastructure.Role.DataBaseFiller)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Регистрационные данные")]
	public partial class DocumentsRegistrationDetailsLookup : LookupItem<DocumentsRegistrationDetails>
	{
		public DocumentsRegistrationDetailsLookup(DocumentsRegistrationDetails entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(1)]
        public System.String Number => Entity.Number;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Economist)] [AllowEditAttribute(Infrastructure.Role.DataBaseFiller)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Должность")]
	public partial class EmployeesPositionLookup : LookupItem<EmployeesPosition>
	{
		public EmployeesPositionLookup(EmployeesPosition entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String Name => Entity.Name;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Тип объекта")]
	public partial class FacilityTypeLookup : LookupItem<FacilityType>
	{
		public FacilityTypeLookup(FacilityType entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String FullName => Entity.FullName;

		[OrderStatus(1)]
        public System.String ShortName => Entity.ShortName;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Сфера деятельности")]
	public partial class ActivityFieldLookup : LookupItem<ActivityField>
	{
		public ActivityFieldLookup(ActivityField entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.String Name => Entity.Name;

		[OrderStatus(1)]
        public HVTApp.Model.POCOs.ActivityFieldEnum ActivityFieldEnum => Entity.ActivityFieldEnum;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Контракт")]
	public partial class ContractLookup : LookupItem<Contract>
	{
		public ContractLookup(Contract entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String Number => Entity.Number;

		[OrderStatus(1)]
        public System.DateTime Date => Entity.Date;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public CompanyLookup Contragent { get { return GetLookup<CompanyLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Единица измерения")]
	public partial class MeasureLookup : LookupItem<Measure>
	{
		public MeasureLookup(Measure entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String FullName => Entity.FullName;

		[OrderStatus(1)]
        public System.String ShortName => Entity.ShortName;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Параметр")]
	public partial class ParameterLookup : LookupItem<Parameter>
	{
		public ParameterLookup(Parameter entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(4)]
        public System.String Value => Entity.Value;

		[OrderStatus(1)]
        public System.Int32 Rang => Entity.Rang;

		[OrderStatus(1)]
        public System.String Comment => Entity.Comment;

		[OrderStatus(1)]
        public System.Boolean IsOrigin => Entity.IsOrigin;

        #endregion

        #region ComplexProperties
		[OrderStatus(5)]
	    public ParameterGroupLookup ParameterGroup { get { return GetLookup<ParameterGroupLookup>(); } }

        #endregion
		[OrderStatus(1)]
	    public List<ParameterRelationLookup> ParameterRelations { get { return GetLookupEnum<ParameterRelationLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Группа параметров")]
	public partial class ParameterGroupLookup : LookupItem<ParameterGroup>
	{
		public ParameterGroupLookup(ParameterGroup entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.String Name => Entity.Name;

		[OrderStatus(1)]
        public System.String Comment => Entity.Comment;

		[OrderStatus(1)]
        public System.Int32 Powerful => Entity.Powerful;

        #endregion

        #region ComplexProperties
		[OrderStatus(1)]
	    public MeasureLookup Measure { get { return GetLookup<MeasureLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Связи продуктов")]
	public partial class ProductRelationLookup : LookupItem<ProductRelation>
	{
		public ProductRelationLookup(ProductRelation entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.String Name => Entity.Name;

		[OrderStatus(4)]
        public System.Int32 ChildProductsAmount => Entity.ChildProductsAmount;

		[OrderStatus(1)]
        public System.Boolean IsUnique => Entity.IsUnique;

        #endregion
		[OrderStatus(8)]
	    public List<ParameterLookup> ParentProductParameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
		[OrderStatus(6)]
	    public List<ParameterLookup> ChildProductParameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Economist)] [AllowEditAttribute(Infrastructure.Role.DataBaseFiller)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Персона")]
	public partial class PersonLookup : LookupItem<Person>
	{
		public PersonLookup(Person entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.String Surname => Entity.Surname;

		[OrderStatus(9)]
        public System.String Name => Entity.Name;

		[OrderStatus(8)]
        public System.String Patronymic => Entity.Patronymic;

		[OrderStatus(1)]
        public System.Boolean IsMan => Entity.IsMan;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Ограничение использования параметра")]
	public partial class ParameterRelationLookup : LookupItem<ParameterRelation>
	{
		public ParameterRelationLookup(ParameterRelation entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.Guid ParameterId => Entity.ParameterId;

        #endregion
		[OrderStatus(1)]
	    public List<ParameterLookup> RequiredParameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Единица продаж")]
	public partial class SalesUnitLookup : LookupItem<SalesUnit>
	{
		public SalesUnitLookup(SalesUnit entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(990)]
        public System.Double Cost => Entity.Cost;

		[OrderStatus(985)]
        public System.Nullable<System.Double> Price => Entity.Price;

		[OrderStatus(980)]
        public System.Nullable<System.Double> LaborHours => Entity.LaborHours;

		[OrderStatus(1)]
        public System.Int32 ProductionTerm => Entity.ProductionTerm;

		[OrderStatus(1)]
        public System.String Comment => Entity.Comment;

		[OrderStatus(1)]
        public System.DateTime DeliveryDateExpected => Entity.DeliveryDateExpected;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> RealizationDate => Entity.RealizationDate;

		[OrderStatus(1)]
        public System.String OrderPosition => Entity.OrderPosition;

		[OrderStatus(1)]
        public System.String SerialNumber => Entity.SerialNumber;

		[OrderStatus(1)]
        public System.Nullable<System.Int32> AssembleTerm => Entity.AssembleTerm;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> SignalToStartProduction => Entity.SignalToStartProduction;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> SignalToStartProductionDone => Entity.SignalToStartProductionDone;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> StartProductionDate => Entity.StartProductionDate;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> PickingDate => Entity.PickingDate;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> EndProductionPlanDate => Entity.EndProductionPlanDate;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> EndProductionDate => Entity.EndProductionDate;

		[OrderStatus(980)]
        public System.Nullable<System.Double> CostDelivery => Entity.CostDelivery;

		[OrderStatus(1)]
        public System.Boolean CostDeliveryIncluded => Entity.CostDeliveryIncluded;

		[OrderStatus(1)]
        public System.Nullable<System.Int32> ExpectedDeliveryPeriod => Entity.ExpectedDeliveryPeriod;

		[OrderStatus(1)]
        public System.Nullable<System.Int32> ExpectedDeliveryPeriodCalculated => Entity.ExpectedDeliveryPeriodCalculated;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> ShipmentDate => Entity.ShipmentDate;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> ShipmentPlanDate => Entity.ShipmentPlanDate;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> DeliveryDate => Entity.DeliveryDate;

		[OrderStatus(1)]
        public System.Boolean IsRemoved => Entity.IsRemoved;

		[OrderStatus(1)]
        public System.Boolean AllowEditCost => Entity.AllowEditCost;

		[OrderStatus(1)]
        public System.Boolean AllowEditProduct => Entity.AllowEditProduct;

		[OrderStatus(1)]
        public System.Boolean IsLoosen => Entity.IsLoosen;

		[OrderStatus(1)]
        public System.Boolean IsWon => Entity.IsWon;

		[OrderStatus(1)]
        public System.Boolean IsDone => Entity.IsDone;

		[OrderStatus(1)]
        public System.Guid ActualPriceCalculationItemId => Entity.ActualPriceCalculationItemId;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> FirstPaymentDate => Entity.FirstPaymentDate;

		[OrderStatus(1)]
        public System.Double PaidSum => Entity.PaidSum;

		[OrderStatus(1)]
        public System.Boolean OrderIsTaken => Entity.OrderIsTaken;

		[OrderStatus(1)]
        public System.Boolean OrderIsRealized => Entity.OrderIsRealized;

		[OrderStatus(1)]
        public System.Boolean AllowTotalRemove => Entity.AllowTotalRemove;

		[OrderStatus(1)]
        public System.Boolean IsPaid => Entity.IsPaid;

		[OrderStatus(1)]
        public System.Double SumNotPaid => Entity.SumNotPaid;

		[OrderStatus(1)]
        public System.Double Vat => Entity.Vat;

		[OrderStatus(1)]
        public System.Double SumNotPaidWithVat => Entity.SumNotPaidWithVat;

		[OrderStatus(1)]
        public System.Double SumToStartProduction => Entity.SumToStartProduction;

		[OrderStatus(1)]
        public System.Double SumToShipping => Entity.SumToShipping;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> OrderInTakeDateInjected => Entity.OrderInTakeDateInjected;

		[OrderStatus(990)]
        public System.DateTime OrderInTakeDate => Entity.OrderInTakeDate;

		[OrderStatus(985)]
        public System.Int32 OrderInTakeYear => Entity.OrderInTakeYear;

		[OrderStatus(980)]
        public System.Int32 OrderInTakeMonth => Entity.OrderInTakeMonth;

		[OrderStatus(870)]
        public System.Nullable<System.DateTime> StartProductionConditionsDoneDate => Entity.StartProductionConditionsDoneDate;

		[OrderStatus(865)]
        public System.Nullable<System.DateTime> ShippingConditionsDoneDate => Entity.ShippingConditionsDoneDate;

		[OrderStatus(1)]
        public System.Nullable<System.DateTime> StartProductionDateInjected => Entity.StartProductionDateInjected;

		[OrderStatus(860)]
        public System.DateTime StartProductionDateCalculated => Entity.StartProductionDateCalculated;

		[OrderStatus(855)]
        public System.DateTime EndProductionDateCalculated => Entity.EndProductionDateCalculated;

		[OrderStatus(854)]
        public System.DateTime EndProductionDateByContractCalculated => Entity.EndProductionDateByContractCalculated;

		[OrderStatus(850)]
        public System.DateTime RealizationDateCalculated => Entity.RealizationDateCalculated;

		[OrderStatus(845)]
        public System.DateTime ShipmentDateCalculated => Entity.ShipmentDateCalculated;

		[OrderStatus(840)]
        public System.DateTime DeliveryDateCalculated => Entity.DeliveryDateCalculated;

		[OrderStatus(1)]
        public System.Double DeliveryPeriodCalculated => Entity.DeliveryPeriodCalculated;

        #endregion

        #region ComplexProperties
		[OrderStatus(1000)]
	    public FacilityLookup Facility { get { return GetLookup<FacilityLookup>(); } }

		[OrderStatus(995)]
	    public ProductLookup Product { get { return GetLookup<ProductLookup>(); } }

		[OrderStatus(1)]
	    public PaymentConditionSetLookup PaymentConditionSet { get { return GetLookup<PaymentConditionSetLookup>(); } }

		[OrderStatus(1005)]
	    public ProjectLookup Project { get { return GetLookup<ProjectLookup>(); } }

		[OrderStatus(1)]
	    public CompanyLookup Producer { get { return GetLookup<CompanyLookup>(); } }

		[OrderStatus(1)]
	    public OrderLookup Order { get { return GetLookup<OrderLookup>(); } }

		[OrderStatus(1)]
	    public SpecificationLookup Specification { get { return GetLookup<SpecificationLookup>(); } }

		[OrderStatus(1)]
	    public PenaltyLookup Penalty { get { return GetLookup<PenaltyLookup>(); } }

		[OrderStatus(1)]
	    public AddressLookup AddressDelivery { get { return GetLookup<AddressLookup>(); } }

		[OrderStatus(1)]
	    public AddressLookup AddressDeliveryCalculated { get { return GetLookup<AddressLookup>(); } }

        #endregion
		[OrderStatus(1)]
	    public List<ProductIncludedLookup> ProductsIncluded { get { return GetLookupEnum<ProductIncludedLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<LosingReasonLookup> LosingReasons { get { return GetLookupEnum<LosingReasonLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<PaymentActualLookup> PaymentsActual { get { return GetLookupEnum<PaymentActualLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<PaymentPlannedLookup> PaymentsPlanned { get { return GetLookupEnum<PaymentPlannedLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<PriceCalculationItemLookup> PriceCalculationItems { get { return GetLookupEnum<PriceCalculationItemLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<PaymentPlannedLookup> PaymentsPlannedActual { get { return GetLookupEnum<PaymentPlannedLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<PaymentPlannedLookup> PaymentsPlannedGenerated { get { return GetLookupEnum<PaymentPlannedLookup>().ToList(); } }
		[OrderStatus(1)]
	    public List<PaymentPlannedLookup> PaymentsPlannedCalculated { get { return GetLookupEnum<PaymentPlannedLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Документ")]
	public partial class DocumentLookup : LookupItem<Document>
	{
		public DocumentLookup(Document entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(150)]
        public System.String RegNumber => Entity.RegNumber;

		[OrderStatus(140)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(1)]
        public System.Guid SenderId => Entity.SenderId;

		[OrderStatus(1)]
        public System.Guid RecipientId => Entity.RecipientId;

		[OrderStatus(130)]
        public System.String Comment => Entity.Comment;

		[OrderStatus(-10)]
        public System.String TceNumber => Entity.TceNumber;

		[OrderStatus(1)]
        public HVTApp.Model.POCOs.DocumentDirection Direction => Entity.Direction;

        #endregion

        #region ComplexProperties
		[OrderStatus(50)]
	    public DocumentNumberLookup Number { get { return GetLookup<DocumentNumberLookup>(); } }

		[OrderStatus(1)]
	    public DocumentLookup RequestDocument { get { return GetLookup<DocumentLookup>(); } }

		[OrderStatus(1)]
	    public EmployeeLookup Author { get { return GetLookup<EmployeeLookup>(); } }

		[OrderStatus(1)]
	    public EmployeeLookup SenderEmployee { get { return GetLookup<EmployeeLookup>(); } }

		[OrderStatus(1)]
	    public EmployeeLookup RecipientEmployee { get { return GetLookup<EmployeeLookup>(); } }

		[OrderStatus(1)]
	    public DocumentsRegistrationDetailsLookup RegistrationDetailsOfRecipient { get { return GetLookup<DocumentsRegistrationDetailsLookup>(); } }

        #endregion
		[OrderStatus(1)]
	    public List<EmployeeLookup> CopyToRecipients { get { return GetLookupEnum<EmployeeLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Сумма на дату")]
	public partial class SumOnDateLookup : LookupItem<SumOnDate>
	{
		public SumOnDateLookup(SumOnDate entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(1)]
        public System.Double Sum => Entity.Sum;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Продукт")]
	public partial class ProductLookup : LookupItem<Product>
	{
		public ProductLookup(Product entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(8)]
        public System.String Designation => Entity.Designation;

		[OrderStatus(1)]
        public System.Boolean HasBlockWithFixedCost => Entity.HasBlockWithFixedCost;

		[OrderStatus(6)]
        public System.String DesignationSpecial => Entity.DesignationSpecial;

		[OrderStatus(1)]
        public System.String Comment => Entity.Comment;

        #endregion

        #region ComplexProperties
		[OrderStatus(10)]
	    public ProductTypeLookup ProductType { get { return GetLookup<ProductTypeLookup>(); } }

		[OrderStatus(9)]
	    public ProductCategoryLookup Category { get { return GetLookup<ProductCategoryLookup>(); } }

		[OrderStatus(5)]
	    public ProductBlockLookup ProductBlock { get { return GetLookup<ProductBlockLookup>(); } }

        #endregion
		[OrderStatus(1)]
	    public List<ProductDependentLookup> DependentProducts { get { return GetLookupEnum<ProductDependentLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Предложение")]
	public partial class OfferLookup : LookupItem<Offer>
	{
		public OfferLookup(Offer entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(4)]
        public System.DateTime ValidityDate => Entity.ValidityDate;

		[OrderStatus(1)]
        public System.Double Vat => Entity.Vat;

		[OrderStatus(150)]
        public System.String RegNumber => Entity.RegNumber;

		[OrderStatus(140)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(1)]
        public System.Guid SenderId => Entity.SenderId;

		[OrderStatus(1)]
        public System.Guid RecipientId => Entity.RecipientId;

		[OrderStatus(130)]
        public System.String Comment => Entity.Comment;

		[OrderStatus(-10)]
        public System.String TceNumber => Entity.TceNumber;

		[OrderStatus(1)]
        public HVTApp.Model.POCOs.DocumentDirection Direction => Entity.Direction;

        #endregion

        #region ComplexProperties
		[OrderStatus(5)]
	    public ProjectLookup Project { get { return GetLookup<ProjectLookup>(); } }

		[OrderStatus(50)]
	    public DocumentNumberLookup Number { get { return GetLookup<DocumentNumberLookup>(); } }

		[OrderStatus(1)]
	    public DocumentLookup RequestDocument { get { return GetLookup<DocumentLookup>(); } }

		[OrderStatus(1)]
	    public EmployeeLookup Author { get { return GetLookup<EmployeeLookup>(); } }

		[OrderStatus(1)]
	    public EmployeeLookup SenderEmployee { get { return GetLookup<EmployeeLookup>(); } }

		[OrderStatus(1)]
	    public EmployeeLookup RecipientEmployee { get { return GetLookup<EmployeeLookup>(); } }

		[OrderStatus(1)]
	    public DocumentsRegistrationDetailsLookup RegistrationDetailsOfRecipient { get { return GetLookup<DocumentsRegistrationDetailsLookup>(); } }

        #endregion
		[OrderStatus(1)]
	    public List<EmployeeLookup> CopyToRecipients { get { return GetLookupEnum<EmployeeLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Economist)] [AllowEditAttribute(Infrastructure.Role.DataBaseFiller)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Сотрудник")]
	public partial class EmployeeLookup : LookupItem<Employee>
	{
		public EmployeeLookup(Employee entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(15)]
        public System.String PersonalNumber => Entity.PersonalNumber;

		[OrderStatus(20)]
        public System.String PhoneNumber => Entity.PhoneNumber;

		[OrderStatus(10)]
        public System.String Email => Entity.Email;

        #endregion

        #region ComplexProperties
		[OrderStatus(30)]
	    public PersonLookup Person { get { return GetLookup<PersonLookup>(); } }

		[OrderStatus(50)]
	    public CompanyLookup Company { get { return GetLookup<CompanyLookup>(); } }

		[OrderStatus(40)]
	    public EmployeesPositionLookup Position { get { return GetLookup<EmployeesPositionLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.PlanMaker)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Заводской заказ")]
	public partial class OrderLookup : LookupItem<Order>
	{
		public OrderLookup(Order entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String Number => Entity.Number;

		[OrderStatus(1)]
        public System.DateTime DateOpen => Entity.DateOpen;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Условие платежа")]
	public partial class PaymentConditionLookup : LookupItem<PaymentCondition>
	{
		public PaymentConditionLookup(PaymentCondition entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(6)]
        public System.Double Part => Entity.Part;

		[OrderStatus(8)]
        public System.Int32 DaysToPoint => Entity.DaysToPoint;

        #endregion

        #region ComplexProperties
		[OrderStatus(10)]
	    public PaymentConditionPointLookup PaymentConditionPoint { get { return GetLookup<PaymentConditionPointLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Economist)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Платежный документ")]
	public partial class PaymentDocumentLookup : LookupItem<PaymentDocument>
	{
		public PaymentDocumentLookup(PaymentDocument entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.String Number => Entity.Number;

		[OrderStatus(20)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(1)]
        public System.Double Vat => Entity.Vat;

		[OrderStatus(1)]
        public System.Double SumWithVat => Entity.SumWithVat;

        #endregion
		[OrderStatus(1)]
	    public List<PaymentActualLookup> Payments { get { return GetLookupEnum<PaymentActualLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Объект")]
	public partial class FacilityLookup : LookupItem<Facility>
	{
		public FacilityLookup(Facility entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(20)]
        public System.String Name => Entity.Name;

        #endregion

        #region ComplexProperties
		[OrderStatus(18)]
	    public FacilityTypeLookup Type { get { return GetLookup<FacilityTypeLookup>(); } }

		[OrderStatus(16)]
	    public CompanyLookup OwnerCompany { get { return GetLookup<CompanyLookup>(); } }

		[OrderStatus(10)]
	    public AddressLookup Address { get { return GetLookup<AddressLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Проект")]
	public partial class ProjectLookup : LookupItem<Project>
	{
		public ProjectLookup(Project entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(9)]
        public System.String Name => Entity.Name;

		[OrderStatus(2)]
        public System.Boolean InWork => Entity.InWork;

		[OrderStatus(1)]
        public System.Boolean ForReport => Entity.ForReport;

        #endregion

        #region ComplexProperties
		[OrderStatus(5)]
	    public ProjectTypeLookup ProjectType { get { return GetLookup<ProjectTypeLookup>(); } }

		[OrderStatus(4)]
	    public UserLookup Manager { get { return GetLookup<UserLookup>(); } }

        #endregion
		[OrderStatus(-10)]
	    public List<NoteLookup> Notes { get { return GetLookupEnum<NoteLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Роль пользователя")]
	public partial class UserRoleLookup : LookupItem<UserRole>
	{
		public UserRoleLookup(UserRole entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String Name => Entity.Name;

		[OrderStatus(1)]
        public HVTApp.Infrastructure.Role Role => Entity.Role;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.SalesManager)] [AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Спецификация")]
	public partial class SpecificationLookup : LookupItem<Specification>
	{
		public SpecificationLookup(Specification entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(10)]
        public System.String Number => Entity.Number;

		[OrderStatus(9)]
        public System.DateTime Date => Entity.Date;

		[OrderStatus(5)]
        public System.Nullable<System.DateTime> SignDate => Entity.SignDate;

		[OrderStatus(7)]
        public System.Double Vat => Entity.Vat;

        #endregion

        #region ComplexProperties
		[OrderStatus(8)]
	    public ContractLookup Contract { get { return GetLookup<ContractLookup>(); } }

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Конкурс")]
	public partial class TenderLookup : LookupItem<Tender>
	{
		public TenderLookup(Tender entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(1)]
        public System.String Link => Entity.Link;

		[OrderStatus(9)]
        public System.DateTime DateOpen => Entity.DateOpen;

		[OrderStatus(8)]
        public System.DateTime DateClose => Entity.DateClose;

		[OrderStatus(7)]
        public System.Nullable<System.DateTime> DateNotice => Entity.DateNotice;

		[OrderStatus(2)]
        public System.Boolean DidNotTakePlace => Entity.DidNotTakePlace;

        #endregion

        #region ComplexProperties
		[OrderStatus(4)]
	    public ProjectLookup Project { get { return GetLookup<ProjectLookup>(); } }

		[OrderStatus(5)]
	    public CompanyLookup Winner { get { return GetLookup<CompanyLookup>(); } }

        #endregion
		[OrderStatus(11)]
	    public List<TenderTypeLookup> Types { get { return GetLookupEnum<TenderTypeLookup>().ToList(); } }
		[OrderStatus(6)]
	    public List<CompanyLookup> Participants { get { return GetLookupEnum<CompanyLookup>().ToList(); } }
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Тип тендера")]
	public partial class TenderTypeLookup : LookupItem<TenderType>
	{
		public TenderTypeLookup(TenderType entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(4)]
        public System.String Name => Entity.Name;

		[OrderStatus(1)]
        public HVTApp.Model.POCOs.TenderTypeEnum Type => Entity.Type;

        #endregion
	}
	[AllowEditAttribute(Infrastructure.Role.Admin)]
	[Designation("Пользователь")]
	public partial class UserLookup : LookupItem<User>
	{
		public UserLookup(User entity) : base(entity) 
		{
		}
		
        #region SimpleProperties
		[OrderStatus(20)]
        public System.String Login => Entity.Login;

		[OrderStatus(2)]
        public System.Guid Password => Entity.Password;

		[OrderStatus(1)]
        public HVTApp.Infrastructure.Role RoleCurrent => Entity.RoleCurrent;

		[OrderStatus(5)]
        public System.Boolean IsActual => Entity.IsActual;

        #endregion

        #region ComplexProperties
		[OrderStatus(25)]
	    public EmployeeLookup Employee { get { return GetLookup<EmployeeLookup>(); } }

        #endregion
		[OrderStatus(1)]
	    public List<UserRoleLookup> Roles { get { return GetLookupEnum<UserRoleLookup>().ToList(); } }
	}
}
