















using HVTApp.Model.POCOs;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Lookup
{

	[AllowEditAttribute(Role.Admin)]

	[Designation("Задание на создание нового продукта")]
	public partial class CreateNewProductTaskLookup : LookupItem<CreateNewProductTask>
	{
		public CreateNewProductTaskLookup(CreateNewProductTask entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(10)]
        public System.String Designation => GetValue<System.String>();


		[OrderStatus(8)]
        public System.String StructureCostNumber => GetValue<System.String>();


		[OrderStatus(6)]
        public System.String Comment => GetValue<System.String>();


        #endregion


        #region ComplexProperties

		[OrderStatus(1)]
	    public ProductLookup Product { get { return GetLookup<ProductLookup>(); } }


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Номер документа")]
	public partial class DocumentNumberLookup : LookupItem<DocumentNumber>
	{
		public DocumentNumberLookup(DocumentNumber entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.Int32 Number => GetValue<System.Int32>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Область рынка")]
	public partial class MarketFieldLookup : LookupItem<MarketField>
	{
		public MarketFieldLookup(MarketField entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(10)]
        public System.String Name => GetValue<System.String>();


        #endregion

		[OrderStatus(9)]
	    public List<ActivityFieldLookup> ActivityFields { get { return GetLookupEnum<ActivityFieldLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Платеж совершенный")]
	public partial class PaymentActualLookup : LookupItem<PaymentActual>
	{
		public PaymentActualLookup(PaymentActual entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.DateTime Date => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.Double Sum => GetValue<System.Double>();


		[OrderStatus(1)]
        public System.String Comment => GetValue<System.String>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Платеж плановый")]
	public partial class PaymentPlannedLookup : LookupItem<PaymentPlanned>
	{
		public PaymentPlannedLookup(PaymentPlanned entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.DateTime Date => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.DateTime DateCalculated => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.Double Part => GetValue<System.Double>();


		[OrderStatus(-10)]
        public System.String Comment => GetValue<System.String>();


        #endregion


        #region ComplexProperties

		[OrderStatus(-5)]
	    public PaymentConditionLookup Condition { get { return GetLookup<PaymentConditionLookup>(); } }


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Признаки услуги")]
	public partial class ProductBlockIsServiceLookup : LookupItem<ProductBlockIsService>
	{
		public ProductBlockIsServiceLookup(ProductBlockIsService entity) : base(entity) 
		{
		}
		
		[OrderStatus(1)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Включенное в стоимость оборудование")]
	public partial class ProductIncludedLookup : LookupItem<ProductIncluded>
	{
		public ProductIncludedLookup(ProductIncluded entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(5)]
        public System.Int32 Amount => GetValue<System.Int32>();


		[OrderStatus(1)]
        public System.Int32 ParentsCount => GetValue<System.Int32>();


        #endregion


        #region ComplexProperties

		[OrderStatus(10)]
	    public ProductLookup Product { get { return GetLookup<ProductLookup>(); } }


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Обозначение продукта")]
	public partial class ProductDesignationLookup : LookupItem<ProductDesignation>
	{
		public ProductDesignationLookup(ProductDesignation entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(10)]
        public System.String Designation => GetValue<System.String>();


        #endregion

		[OrderStatus(1)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Тип продукта")]
	public partial class ProductTypeLookup : LookupItem<ProductType>
	{
		public ProductTypeLookup(ProductType entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.String Name => GetValue<System.String>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

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

	[AllowEditAttribute(Role.Admin)]

	[Designation("Тип проекта")]
	public partial class ProjectTypeLookup : LookupItem<ProjectType>
	{
		public ProjectTypeLookup(ProjectType entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.String Name => GetValue<System.String>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Стандартный маржинальный доход")]
	public partial class StandartMarginalIncomeLookup : LookupItem<StandartMarginalIncome>
	{
		public StandartMarginalIncomeLookup(StandartMarginalIncome entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(10)]
        public System.Double MarginalIncome => GetValue<System.Double>();


        #endregion

		[OrderStatus(9)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Стандартный срок производства")]
	public partial class StandartProductionTermLookup : LookupItem<StandartProductionTerm>
	{
		public StandartProductionTermLookup(StandartProductionTerm entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(10)]
        public System.Int32 ProductionTerm => GetValue<System.Int32>();


        #endregion

		[OrderStatus(9)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Общие настройки")]
	public partial class CommonOptionLookup : LookupItem<CommonOption>
	{
		public CommonOptionLookup(CommonOption entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.DateTime Date => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.Int32 ActualPriceTerm => GetValue<System.Int32>();


		[OrderStatus(1)]
        public System.Int32 StandartTermFromStartToEndProduction => GetValue<System.Int32>();


		[OrderStatus(1)]
        public System.Int32 StandartTermFromPickToEndProduction => GetValue<System.Int32>();


		[OrderStatus(1)]
        public System.Double Vat => GetValue<System.Double>();


        #endregion


        #region ComplexProperties

		[OrderStatus(1)]
	    public CompanyLookup OurCompany { get { return GetLookup<CompanyLookup>(); } }


		[OrderStatus(1)]
	    public PaymentConditionSetLookup StandartPaymentsConditionSet { get { return GetLookup<PaymentConditionSetLookup>(); } }


		[OrderStatus(1)]
	    public ParameterLookup NewProductParameter { get { return GetLookup<ParameterLookup>(); } }


		[OrderStatus(1)]
	    public ParameterGroupLookup NewProductParameterGroup { get { return GetLookup<ParameterGroupLookup>(); } }


        #endregion

	}

	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Адрес")]
	public partial class AddressLookup : LookupItem<Address>
	{
		public AddressLookup(Address entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.String Description => GetValue<System.String>();


        #endregion


        #region ComplexProperties

		[OrderStatus(5)]
	    public LocalityLookup Locality { get { return GetLookup<LocalityLookup>(); } }


        #endregion

	}

	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Страна")]
	public partial class CountryLookup : LookupItem<Country>
	{
		public CountryLookup(Country entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.String Name => GetValue<System.String>();


        #endregion

	}

	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Округ")]
	public partial class DistrictLookup : LookupItem<District>
	{
		public DistrictLookup(District entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(10)]
        public System.String Name => GetValue<System.String>();


        #endregion


        #region ComplexProperties

		[OrderStatus(1)]
	    public CountryLookup Country { get { return GetLookup<CountryLookup>(); } }


        #endregion

	}

	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Населенный пункт")]
	public partial class LocalityLookup : LookupItem<Locality>
	{
		public LocalityLookup(Locality entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(10)]
        public System.String Name => GetValue<System.String>();


		[OrderStatus(1)]
        public System.Boolean IsCountryCapital => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Boolean IsDistrictCapital => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Boolean IsRegionCapital => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Nullable<System.Double> DistanceToEkb => GetValue<System.Nullable<System.Double>>();


        #endregion


        #region ComplexProperties

		[OrderStatus(9)]
	    public LocalityTypeLookup LocalityType { get { return GetLookup<LocalityTypeLookup>(); } }


		[OrderStatus(8)]
	    public RegionLookup Region { get { return GetLookup<RegionLookup>(); } }


        #endregion

	}

	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Тип населенного пункта")]
	public partial class LocalityTypeLookup : LookupItem<LocalityType>
	{
		public LocalityTypeLookup(LocalityType entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(2)]
        public System.String FullName => GetValue<System.String>();


		[OrderStatus(1)]
        public System.String ShortName => GetValue<System.String>();


        #endregion

	}

	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Регион")]
	public partial class RegionLookup : LookupItem<Region>
	{
		public RegionLookup(Region entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(2)]
        public System.String Name => GetValue<System.String>();


        #endregion


        #region ComplexProperties

		[OrderStatus(1)]
	    public DistrictLookup District { get { return GetLookup<DistrictLookup>(); } }


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Сумма (фэйк)")]
	public partial class SumLookup : LookupItem<Sum>
	{
		public SumLookup(Sum entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public HVTApp.Model.POCOs.SumType Type => GetValue<HVTApp.Model.POCOs.SumType>();


		[OrderStatus(1)]
        public HVTApp.Model.POCOs.Currency Currency => GetValue<HVTApp.Model.POCOs.Currency>();


		[OrderStatus(1)]
        public System.Decimal Value => GetValue<System.Decimal>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Курс обмена валют")]
	public partial class CurrencyExchangeRateLookup : LookupItem<CurrencyExchangeRate>
	{
		public CurrencyExchangeRateLookup(CurrencyExchangeRate entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.DateTime Date => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public HVTApp.Model.POCOs.Currency FirstCurrency => GetValue<HVTApp.Model.POCOs.Currency>();


		[OrderStatus(1)]
        public HVTApp.Model.POCOs.Currency SecondCurrency => GetValue<HVTApp.Model.POCOs.Currency>();


		[OrderStatus(1)]
        public System.Double ExchangeRate => GetValue<System.Double>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Заметка")]
	public partial class NoteLookup : LookupItem<Note>
	{
		public NoteLookup(Note entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(4)]
        public System.DateTime Date => GetValue<System.DateTime>();


		[OrderStatus(3)]
        public System.String Text => GetValue<System.String>();


		[OrderStatus(2)]
        public System.Boolean IsImportant => GetValue<System.Boolean>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Единица ТКП")]
	public partial class OfferUnitLookup : LookupItem<OfferUnit>
	{
		public OfferUnitLookup(OfferUnit entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.Double Cost => GetValue<System.Double>();


		[OrderStatus(1)]
        public System.Nullable<System.Double> CostDelivery => GetValue<System.Nullable<System.Double>>();


		[OrderStatus(1)]
        public System.Boolean CostDeliveryIncluded => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Int32 ProductionTerm => GetValue<System.Int32>();


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

	[AllowEditAttribute(Role.Admin)]

	[Designation("Условия оплаты")]
	public partial class PaymentConditionSetLookup : LookupItem<PaymentConditionSet>
	{
		public PaymentConditionSetLookup(PaymentConditionSet entity) : base(entity) 
		{
		}
		
		[OrderStatus(1)]
	    public List<PaymentConditionLookup> PaymentConditions { get { return GetLookupEnum<PaymentConditionLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Блок")]
	public partial class ProductBlockLookup : LookupItem<ProductBlock>
	{
		public ProductBlockLookup(ProductBlock entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.String Designation => GetValue<System.String>();


		[OrderStatus(1)]
        public System.String DesignationSpecial => GetValue<System.String>();


		[OrderStatus(1)]
        public System.String StructureCostNumber => GetValue<System.String>();


		[OrderStatus(1)]
        public System.String Design => GetValue<System.String>();


		[OrderStatus(1)]
        public System.Boolean IsService => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Boolean IsSupervision => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Boolean IsDelivery => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Double Weight => GetValue<System.Double>();


		[OrderStatus(1)]
        public System.Nullable<System.DateTime> LastPriceDate => GetValue<System.Nullable<System.DateTime>>();


        #endregion

		[OrderStatus(1)]
	    public List<ParameterLookup> Parameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }

		[OrderStatus(1)]
	    public List<SumOnDateLookup> Prices { get { return GetLookupEnum<SumOnDateLookup>().ToList(); } }

		[OrderStatus(1)]
	    public List<SumOnDateLookup> FixedCosts { get { return GetLookupEnum<SumOnDateLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Зависимое оборудование")]
	public partial class ProductDependentLookup : LookupItem<ProductDependent>
	{
		public ProductDependentLookup(ProductDependent entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.Guid MainProductId => GetValue<System.Guid>();


		[OrderStatus(5)]
        public System.Int32 Amount => GetValue<System.Int32>();


        #endregion


        #region ComplexProperties

		[OrderStatus(10)]
	    public ProductLookup Product { get { return GetLookup<ProductLookup>(); } }


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Банковские реквизиты")]
	public partial class BankDetailsLookup : LookupItem<BankDetails>
	{
		public BankDetailsLookup(BankDetails entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(5)]
        public System.String BankName => GetValue<System.String>();


		[OrderStatus(4)]
        public System.String BankIdentificationCode => GetValue<System.String>();


		[OrderStatus(3)]
        public System.String CorrespondentAccount => GetValue<System.String>();


		[OrderStatus(2)]
        public System.String CheckingAccount => GetValue<System.String>();


        #endregion

	}

	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.DataBaseFiller)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Компания")]
	public partial class CompanyLookup : LookupItem<Company>
	{
		public CompanyLookup(Company entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(20)]
        public System.String FullName => GetValue<System.String>();


		[OrderStatus(15)]
        public System.String ShortName => GetValue<System.String>();


		[OrderStatus(1)]
        public System.String Inn => GetValue<System.String>();


		[OrderStatus(1)]
        public System.String Kpp => GetValue<System.String>();


        #endregion


        #region ComplexProperties

		[OrderStatus(5)]
	    public CompanyFormLookup Form { get { return GetLookup<CompanyFormLookup>(); } }


		[OrderStatus(1)]
	    public CompanyLookup ParentCompany { get { return GetLookup<CompanyLookup>(); } }


		[OrderStatus(1)]
	    public AddressLookup AddressLegal { get { return GetLookup<AddressLookup>(); } }


		[OrderStatus(1)]
	    public AddressLookup AddressPost { get { return GetLookup<AddressLookup>(); } }


        #endregion

		[OrderStatus(-10)]
	    public List<BankDetailsLookup> BankDetailsList { get { return GetLookupEnum<BankDetailsLookup>().ToList(); } }

		[OrderStatus(1)]
	    public List<ActivityFieldLookup> ActivityFilds { get { return GetLookupEnum<ActivityFieldLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Организационная форма")]
	public partial class CompanyFormLookup : LookupItem<CompanyForm>
	{
		public CompanyFormLookup(CompanyForm entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.String FullName => GetValue<System.String>();


		[OrderStatus(1)]
        public System.String ShortName => GetValue<System.String>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Регистрационные данные")]
	public partial class DocumentsRegistrationDetailsLookup : LookupItem<DocumentsRegistrationDetails>
	{
		public DocumentsRegistrationDetailsLookup(DocumentsRegistrationDetails entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.DateTime Date => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.String Number => GetValue<System.String>();


        #endregion

	}

	[AllowEditAttribute(Role.DataBaseFiller)]
[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Economist)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Должность")]
	public partial class EmployeesPositionLookup : LookupItem<EmployeesPosition>
	{
		public EmployeesPositionLookup(EmployeesPosition entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.String Name => GetValue<System.String>();


        #endregion

	}

	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Тип объекта")]
	public partial class FacilityTypeLookup : LookupItem<FacilityType>
	{
		public FacilityTypeLookup(FacilityType entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.String FullName => GetValue<System.String>();


		[OrderStatus(1)]
        public System.String ShortName => GetValue<System.String>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Сфера деятельности")]
	public partial class ActivityFieldLookup : LookupItem<ActivityField>
	{
		public ActivityFieldLookup(ActivityField entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(10)]
        public System.String Name => GetValue<System.String>();


		[OrderStatus(1)]
        public HVTApp.Model.POCOs.ActivityFieldEnum ActivityFieldEnum => GetValue<HVTApp.Model.POCOs.ActivityFieldEnum>();


        #endregion

	}

	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Контракт")]
	public partial class ContractLookup : LookupItem<Contract>
	{
		public ContractLookup(Contract entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.String Number => GetValue<System.String>();


		[OrderStatus(1)]
        public System.DateTime Date => GetValue<System.DateTime>();


        #endregion


        #region ComplexProperties

		[OrderStatus(1)]
	    public CompanyLookup Contragent { get { return GetLookup<CompanyLookup>(); } }


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Единица измерения")]
	public partial class MeasureLookup : LookupItem<Measure>
	{
		public MeasureLookup(Measure entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.String FullName => GetValue<System.String>();


		[OrderStatus(1)]
        public System.String ShortName => GetValue<System.String>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Параметр")]
	public partial class ParameterLookup : LookupItem<Parameter>
	{
		public ParameterLookup(Parameter entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(4)]
        public System.String Value => GetValue<System.String>();


		[OrderStatus(1)]
        public System.Int32 Rang => GetValue<System.Int32>();


		[OrderStatus(1)]
        public System.String Comment => GetValue<System.String>();


		[OrderStatus(1)]
        public System.Boolean IsOrigin => GetValue<System.Boolean>();


        #endregion


        #region ComplexProperties

		[OrderStatus(5)]
	    public ParameterGroupLookup ParameterGroup { get { return GetLookup<ParameterGroupLookup>(); } }


        #endregion

		[OrderStatus(1)]
	    public List<ParameterRelationLookup> ParameterRelations { get { return GetLookupEnum<ParameterRelationLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Группа параметров")]
	public partial class ParameterGroupLookup : LookupItem<ParameterGroup>
	{
		public ParameterGroupLookup(ParameterGroup entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(10)]
        public System.String Name => GetValue<System.String>();


		[OrderStatus(1)]
        public System.String Comment => GetValue<System.String>();


        #endregion


        #region ComplexProperties

		[OrderStatus(1)]
	    public MeasureLookup Measure { get { return GetLookup<MeasureLookup>(); } }


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Связи продуктов")]
	public partial class ProductRelationLookup : LookupItem<ProductRelation>
	{
		public ProductRelationLookup(ProductRelation entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(10)]
        public System.String Name => GetValue<System.String>();


		[OrderStatus(4)]
        public System.Int32 ChildProductsAmount => GetValue<System.Int32>();


		[OrderStatus(1)]
        public System.Boolean IsUnique => GetValue<System.Boolean>();


        #endregion

		[OrderStatus(8)]
	    public List<ParameterLookup> ParentProductParameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }

		[OrderStatus(6)]
	    public List<ParameterLookup> ChildProductParameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.DataBaseFiller)]
[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Economist)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Персона")]
	public partial class PersonLookup : LookupItem<Person>
	{
		public PersonLookup(Person entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(10)]
        public System.String Surname => GetValue<System.String>();


		[OrderStatus(9)]
        public System.String Name => GetValue<System.String>();


		[OrderStatus(8)]
        public System.String Patronymic => GetValue<System.String>();


		[OrderStatus(1)]
        public System.Boolean IsMan => GetValue<System.Boolean>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Ограничение использования параметра")]
	public partial class ParameterRelationLookup : LookupItem<ParameterRelation>
	{
		public ParameterRelationLookup(ParameterRelation entity) : base(entity) 
		{
		}
		
		[OrderStatus(1)]
	    public List<ParameterLookup> RequiredParameters { get { return GetLookupEnum<ParameterLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Единица продаж")]
	public partial class SalesUnitLookup : LookupItem<SalesUnit>
	{
		public SalesUnitLookup(SalesUnit entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(45)]
        public System.Double Cost => GetValue<System.Double>();


		[OrderStatus(46)]
        public System.Nullable<System.Double> Price => GetValue<System.Nullable<System.Double>>();


		[OrderStatus(1)]
        public System.Int32 ProductionTerm => GetValue<System.Int32>();


		[OrderStatus(1)]
        public System.DateTime DeliveryDateExpected => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.Nullable<System.DateTime> RealizationDate => GetValue<System.Nullable<System.DateTime>>();


		[OrderStatus(1)]
        public System.String TceRequest => GetValue<System.String>();


		[OrderStatus(1)]
        public System.String OrderPosition => GetValue<System.String>();


		[OrderStatus(1)]
        public System.String SerialNumber => GetValue<System.String>();


		[OrderStatus(1)]
        public System.Nullable<System.Int32> AssembleTerm => GetValue<System.Nullable<System.Int32>>();


		[OrderStatus(1)]
        public System.Nullable<System.DateTime> SignalToStartProduction => GetValue<System.Nullable<System.DateTime>>();


		[OrderStatus(1)]
        public System.Nullable<System.DateTime> SignalToStartProductionDone => GetValue<System.Nullable<System.DateTime>>();


		[OrderStatus(1)]
        public System.Nullable<System.DateTime> StartProductionDate => GetValue<System.Nullable<System.DateTime>>();


		[OrderStatus(1)]
        public System.Nullable<System.DateTime> PickingDate => GetValue<System.Nullable<System.DateTime>>();


		[OrderStatus(1)]
        public System.Nullable<System.DateTime> EndProductionPlanDate => GetValue<System.Nullable<System.DateTime>>();


		[OrderStatus(1)]
        public System.Nullable<System.DateTime> EndProductionDate => GetValue<System.Nullable<System.DateTime>>();


		[OrderStatus(1)]
        public System.Nullable<System.Double> CostDelivery => GetValue<System.Nullable<System.Double>>();


		[OrderStatus(1)]
        public System.Boolean CostDeliveryIncluded => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Nullable<System.Int32> ExpectedDeliveryPeriod => GetValue<System.Nullable<System.Int32>>();


		[OrderStatus(1)]
        public System.Nullable<System.Int32> ExpectedDeliveryPeriodCalculated => GetValue<System.Nullable<System.Int32>>();


		[OrderStatus(1)]
        public System.Nullable<System.DateTime> ShipmentDate => GetValue<System.Nullable<System.DateTime>>();


		[OrderStatus(1)]
        public System.Nullable<System.DateTime> ShipmentPlanDate => GetValue<System.Nullable<System.DateTime>>();


		[OrderStatus(1)]
        public System.Nullable<System.DateTime> DeliveryDate => GetValue<System.Nullable<System.DateTime>>();


		[OrderStatus(1)]
        public System.Boolean AllowEditCost => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Boolean AllowEditProduct => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Boolean IsLoosen => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Boolean IsDone => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Boolean IsPaid => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Double SumPaid => GetValue<System.Double>();


		[OrderStatus(1)]
        public System.Double SumNotPaid => GetValue<System.Double>();


		[OrderStatus(1)]
        public System.Double SumToStartProduction => GetValue<System.Double>();


		[OrderStatus(1)]
        public System.Double SumToShipping => GetValue<System.Double>();


		[OrderStatus(1)]
        public System.DateTime OrderInTakeDate => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.Int32 OrderInTakeYear => GetValue<System.Int32>();


		[OrderStatus(1)]
        public System.Int32 OrderInTakeMonth => GetValue<System.Int32>();


		[OrderStatus(1)]
        public System.Nullable<System.DateTime> StartProductionConditionsDoneDate => GetValue<System.Nullable<System.DateTime>>();


		[OrderStatus(1)]
        public System.Nullable<System.DateTime> ShippingConditionsDoneDate => GetValue<System.Nullable<System.DateTime>>();


		[OrderStatus(1)]
        public System.DateTime StartProductionDateCalculated => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.DateTime EndProductionDateCalculated => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.DateTime RealizationDateCalculated => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.DateTime ShipmentDateCalculated => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.DateTime DeliveryDateCalculated => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.Double DeliveryPeriodCalculated => GetValue<System.Double>();


        #endregion


        #region ComplexProperties

		[OrderStatus(50)]
	    public ProductLookup Product { get { return GetLookup<ProductLookup>(); } }


		[OrderStatus(51)]
	    public FacilityLookup Facility { get { return GetLookup<FacilityLookup>(); } }


		[OrderStatus(1)]
	    public PaymentConditionSetLookup PaymentConditionSet { get { return GetLookup<PaymentConditionSetLookup>(); } }


		[OrderStatus(52)]
	    public ProjectLookup Project { get { return GetLookup<ProjectLookup>(); } }


		[OrderStatus(1)]
	    public CompanyLookup Producer { get { return GetLookup<CompanyLookup>(); } }


		[OrderStatus(1)]
	    public OrderLookup Order { get { return GetLookup<OrderLookup>(); } }


		[OrderStatus(1)]
	    public SpecificationLookup Specification { get { return GetLookup<SpecificationLookup>(); } }


		[OrderStatus(1)]
	    public AddressLookup Address { get { return GetLookup<AddressLookup>(); } }


        #endregion

		[OrderStatus(1)]
	    public List<ProductIncludedLookup> ProductsIncluded { get { return GetLookupEnum<ProductIncludedLookup>().ToList(); } }

		[OrderStatus(1)]
	    public List<PaymentActualLookup> PaymentsActual { get { return GetLookupEnum<PaymentActualLookup>().ToList(); } }

		[OrderStatus(1)]
	    public List<PaymentPlannedLookup> PaymentsPlanned { get { return GetLookupEnum<PaymentPlannedLookup>().ToList(); } }

		[OrderStatus(1)]
	    public List<PaymentPlannedLookup> PaymentsPlannedActual { get { return GetLookupEnum<PaymentPlannedLookup>().ToList(); } }

		[OrderStatus(1)]
	    public List<PaymentPlannedLookup> PaymentsPlannedGenerated { get { return GetLookupEnum<PaymentPlannedLookup>().ToList(); } }

		[OrderStatus(1)]
	    public List<PaymentPlannedLookup> PaymentsPlannedCalculated { get { return GetLookupEnum<PaymentPlannedLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Документ")]
	public partial class DocumentLookup : LookupItem<Document>
	{
		public DocumentLookup(Document entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.String Code => GetValue<System.String>();


		[OrderStatus(45)]
        public System.String RegNumber => GetValue<System.String>();


		[OrderStatus(40)]
        public System.DateTime Date => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.Guid SenderId => GetValue<System.Guid>();


		[OrderStatus(1)]
        public System.Guid RecipientId => GetValue<System.Guid>();


		[OrderStatus(1)]
        public System.String Comment => GetValue<System.String>();


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

	[AllowEditAttribute(Role.Admin)]

	[Designation("Сумма на дату")]
	public partial class SumOnDateLookup : LookupItem<SumOnDate>
	{
		public SumOnDateLookup(SumOnDate entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.DateTime Date => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.Double Sum => GetValue<System.Double>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Продукт")]
	public partial class ProductLookup : LookupItem<Product>
	{
		public ProductLookup(Product entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(8)]
        public System.String Designation => GetValue<System.String>();


		[OrderStatus(6)]
        public System.String DesignationSpecial => GetValue<System.String>();


        #endregion


        #region ComplexProperties

		[OrderStatus(10)]
	    public ProductTypeLookup ProductType { get { return GetLookup<ProductTypeLookup>(); } }


		[OrderStatus(5)]
	    public ProductBlockLookup ProductBlock { get { return GetLookup<ProductBlockLookup>(); } }


        #endregion

		[OrderStatus(1)]
	    public List<ProductDependentLookup> DependentProducts { get { return GetLookupEnum<ProductDependentLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Предложение")]
	public partial class OfferLookup : LookupItem<Offer>
	{
		public OfferLookup(Offer entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(4)]
        public System.DateTime ValidityDate => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.Double Vat => GetValue<System.Double>();


		[OrderStatus(1)]
        public System.String Code => GetValue<System.String>();


		[OrderStatus(45)]
        public System.String RegNumber => GetValue<System.String>();


		[OrderStatus(40)]
        public System.DateTime Date => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.Guid SenderId => GetValue<System.Guid>();


		[OrderStatus(1)]
        public System.Guid RecipientId => GetValue<System.Guid>();


		[OrderStatus(1)]
        public System.String Comment => GetValue<System.String>();


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

	[AllowEditAttribute(Role.DataBaseFiller)]
[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Сотрудник")]
	public partial class EmployeeLookup : LookupItem<Employee>
	{
		public EmployeeLookup(Employee entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(20)]
        public System.String PhoneNumber => GetValue<System.String>();


		[OrderStatus(10)]
        public System.String Email => GetValue<System.String>();


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

	[AllowEditAttribute(Role.PlanMaker)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Заводской заказ")]
	public partial class OrderLookup : LookupItem<Order>
	{
		public OrderLookup(Order entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.String Number => GetValue<System.String>();


		[OrderStatus(1)]
        public System.DateTime DateOpen => GetValue<System.DateTime>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Условие платежа")]
	public partial class PaymentConditionLookup : LookupItem<PaymentCondition>
	{
		public PaymentConditionLookup(PaymentCondition entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(6)]
        public System.Double Part => GetValue<System.Double>();


		[OrderStatus(8)]
        public System.Int32 DaysToPoint => GetValue<System.Int32>();


		[OrderStatus(10)]
        public HVTApp.Model.POCOs.PaymentConditionPoint PaymentConditionPoint => GetValue<HVTApp.Model.POCOs.PaymentConditionPoint>();


        #endregion

	}

	[AllowEditAttribute(Role.Economist)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Платежный документ")]
	public partial class PaymentDocumentLookup : LookupItem<PaymentDocument>
	{
		public PaymentDocumentLookup(PaymentDocument entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(10)]
        public System.String Number => GetValue<System.String>();


		[OrderStatus(20)]
        public System.DateTime Date => GetValue<System.DateTime>();


		[OrderStatus(1)]
        public System.Double Vat => GetValue<System.Double>();


        #endregion

		[OrderStatus(1)]
	    public List<PaymentActualLookup> Payments { get { return GetLookupEnum<PaymentActualLookup>().ToList(); } }

	}

	[AllowEditAttribute(Role.SalesManager)]
[AllowEditAttribute(Role.Admin)]

	[Designation("Объект")]
	public partial class FacilityLookup : LookupItem<Facility>
	{
		public FacilityLookup(Facility entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(20)]
        public System.String Name => GetValue<System.String>();


        #endregion


        #region ComplexProperties

		[OrderStatus(18)]
	    public FacilityTypeLookup Type { get { return GetLookup<FacilityTypeLookup>(); } }


		[OrderStatus(16)]
	    public CompanyLookup OwnerCompany { get { return GetLookup<CompanyLookup>(); } }


		[OrderStatus(1)]
	    public AddressLookup Address { get { return GetLookup<AddressLookup>(); } }


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Проект")]
	public partial class ProjectLookup : LookupItem<Project>
	{
		public ProjectLookup(Project entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(9)]
        public System.String Name => GetValue<System.String>();


		[OrderStatus(2)]
        public System.Boolean InWork => GetValue<System.Boolean>();


		[OrderStatus(1)]
        public System.Boolean ForReport => GetValue<System.Boolean>();


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

	[AllowEditAttribute(Role.Admin)]

	[Designation("Роль пользователя")]
	public partial class UserRoleLookup : LookupItem<UserRole>
	{
		public UserRoleLookup(UserRole entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(1)]
        public System.String Name => GetValue<System.String>();


		[OrderStatus(1)]
        public HVTApp.Infrastructure.Role Role => GetValue<HVTApp.Infrastructure.Role>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Спецификация")]
	public partial class SpecificationLookup : LookupItem<Specification>
	{
		public SpecificationLookup(Specification entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(10)]
        public System.String Number => GetValue<System.String>();


		[OrderStatus(9)]
        public System.DateTime Date => GetValue<System.DateTime>();


		[OrderStatus(7)]
        public System.Double Vat => GetValue<System.Double>();


        #endregion


        #region ComplexProperties

		[OrderStatus(8)]
	    public ContractLookup Contract { get { return GetLookup<ContractLookup>(); } }


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Конкурс")]
	public partial class TenderLookup : LookupItem<Tender>
	{
		public TenderLookup(Tender entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(9)]
        public System.DateTime DateOpen => GetValue<System.DateTime>();


		[OrderStatus(8)]
        public System.DateTime DateClose => GetValue<System.DateTime>();


		[OrderStatus(7)]
        public System.Nullable<System.DateTime> DateNotice => GetValue<System.Nullable<System.DateTime>>();


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

	[AllowEditAttribute(Role.Admin)]

	[Designation("Тип тендера")]
	public partial class TenderTypeLookup : LookupItem<TenderType>
	{
		public TenderTypeLookup(TenderType entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(4)]
        public System.String Name => GetValue<System.String>();


		[OrderStatus(1)]
        public HVTApp.Model.POCOs.TenderTypeEnum Type => GetValue<HVTApp.Model.POCOs.TenderTypeEnum>();


        #endregion

	}

	[AllowEditAttribute(Role.Admin)]

	[Designation("Пользователь")]
	public partial class UserLookup : LookupItem<User>
	{
		public UserLookup(User entity) : base(entity) 
		{
		}
		

        #region SimpleProperties

		[OrderStatus(20)]
        public System.String Login => GetValue<System.String>();


		[OrderStatus(2)]
        public System.Guid Password => GetValue<System.Guid>();


		[OrderStatus(15)]
        public System.String PersonalNumber => GetValue<System.String>();


		[OrderStatus(1)]
        public HVTApp.Infrastructure.Role RoleCurrent => GetValue<HVTApp.Infrastructure.Role>();


        #endregion


        #region ComplexProperties

		[OrderStatus(25)]
	    public EmployeeLookup Employee { get { return GetLookup<EmployeeLookup>(); } }


        #endregion

		[OrderStatus(1)]
	    public List<UserRoleLookup> Roles { get { return GetLookupEnum<UserRoleLookup>().ToList(); } }

	}
}
