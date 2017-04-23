using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class SalesUnitWrapper : WrapperBase<SalesUnit>
  {
    public SalesUnitWrapper() : base(new SalesUnit()) { }
    public SalesUnitWrapper(SalesUnit model) : base(model) { }

//	public static SalesUnitWrapper GetWrapper()
//	{
//		return GetWrapper(new SalesUnit());
//	}
//
//	public static SalesUnitWrapper GetWrapper(SalesUnit model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (SalesUnitWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new SalesUnitWrapper(model);
//	}


    #region SimpleProperties
    public System.Nullable<System.DateTime> RealizationDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> RealizationDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(RealizationDate));
    public bool RealizationDateIsChanged => GetIsChanged(nameof(RealizationDate));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	private SalesUnitWrapper _fieldParentSalesUnit;
	public SalesUnitWrapper ParentSalesUnit 
    {
        get { return _fieldParentSalesUnit; }
        set
        {
			SetComplexProperty<SalesUnitWrapper, SalesUnit>(_fieldParentSalesUnit, value);
			_fieldParentSalesUnit = value;
        }
    }
    public SalesUnitWrapper ParentSalesUnitOriginalValue { get; private set; }
    public bool ParentSalesUnitIsChanged => GetIsChanged(nameof(ParentSalesUnit));

	private ProjectWrapper _fieldProject;
	public ProjectWrapper Project 
    {
        get { return _fieldProject; }
        set
        {
			SetComplexProperty<ProjectWrapper, Project>(_fieldProject, value);
			_fieldProject = value;
        }
    }
    public ProjectWrapper ProjectOriginalValue { get; private set; }
    public bool ProjectIsChanged => GetIsChanged(nameof(Project));

	private FacilityWrapper _fieldFacility;
	public FacilityWrapper Facility 
    {
        get { return _fieldFacility; }
        set
        {
			SetComplexProperty<FacilityWrapper, Facility>(_fieldFacility, value);
			_fieldFacility = value;
        }
    }
    public FacilityWrapper FacilityOriginalValue { get; private set; }
    public bool FacilityIsChanged => GetIsChanged(nameof(Facility));

	private SpecificationWrapper _fieldSpecification;
	public SpecificationWrapper Specification 
    {
        get { return _fieldSpecification; }
        set
        {
			SetComplexProperty<SpecificationWrapper, Specification>(_fieldSpecification, value);
			_fieldSpecification = value;
        }
    }
    public SpecificationWrapper SpecificationOriginalValue { get; private set; }
    public bool SpecificationIsChanged => GetIsChanged(nameof(Specification));

	private SumAndVatWrapper _fieldCostSingle;
	public SumAndVatWrapper CostSingle 
    {
        get { return _fieldCostSingle; }
        set
        {
			SetComplexProperty<SumAndVatWrapper, SumAndVat>(_fieldCostSingle, value);
			_fieldCostSingle = value;
        }
    }
    public SumAndVatWrapper CostSingleOriginalValue { get; private set; }
    public bool CostSingleIsChanged => GetIsChanged(nameof(CostSingle));

	private ProductionUnitWrapper _fieldProductionUnit;
	public ProductionUnitWrapper ProductionUnit 
    {
        get { return _fieldProductionUnit; }
        set
        {
			SetComplexProperty<ProductionUnitWrapper, ProductionUnit>(_fieldProductionUnit, value);
			_fieldProductionUnit = value;
        }
    }
    public ProductionUnitWrapper ProductionUnitOriginalValue { get; private set; }
    public bool ProductionUnitIsChanged => GetIsChanged(nameof(ProductionUnit));

	private ShipmentUnitWrapper _fieldShipmentUnit;
	public ShipmentUnitWrapper ShipmentUnit 
    {
        get { return _fieldShipmentUnit; }
        set
        {
			SetComplexProperty<ShipmentUnitWrapper, ShipmentUnit>(_fieldShipmentUnit, value);
			_fieldShipmentUnit = value;
        }
    }
    public ShipmentUnitWrapper ShipmentUnitOriginalValue { get; private set; }
    public bool ShipmentUnitIsChanged => GetIsChanged(nameof(ShipmentUnit));

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<SalesUnitWrapper> ChildSalesUnits { get; private set; }

    public IValidatableChangeTrackingCollection<TenderUnitWrapper> TenderUnits { get; private set; }

    public IValidatableChangeTrackingCollection<OfferUnitWrapper> OfferUnits { get; private set; }

    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }

    public IValidatableChangeTrackingCollection<PaymentPlanWrapper> PaymentsPlanned { get; private set; }

    public IValidatableChangeTrackingCollection<PaymentActualWrapper> PaymentsActual { get; private set; }

    #endregion
    protected override void InitializeComplexProperties(SalesUnit model)
    {
        ParentSalesUnit = GetWrapper<SalesUnitWrapper, SalesUnit>(model.ParentSalesUnit);
        Project = GetWrapper<ProjectWrapper, Project>(model.Project);
        Facility = GetWrapper<FacilityWrapper, Facility>(model.Facility);
        Specification = GetWrapper<SpecificationWrapper, Specification>(model.Specification);
        CostSingle = GetWrapper<SumAndVatWrapper, SumAndVat>(model.CostSingle);
        ProductionUnit = GetWrapper<ProductionUnitWrapper, ProductionUnit>(model.ProductionUnit);
        ShipmentUnit = GetWrapper<ShipmentUnitWrapper, ShipmentUnit>(model.ShipmentUnit);
    }
  
    protected override void InitializeCollectionComplexProperties(SalesUnit model)
    {
      if (model.ChildSalesUnits == null) throw new ArgumentException("ChildSalesUnits cannot be null");
      ChildSalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(model.ChildSalesUnits.Select(e => GetWrapper<SalesUnitWrapper, SalesUnit>(e)));
      RegisterCollection(ChildSalesUnits, model.ChildSalesUnits);

      if (model.TenderUnits == null) throw new ArgumentException("TenderUnits cannot be null");
      TenderUnits = new ValidatableChangeTrackingCollection<TenderUnitWrapper>(model.TenderUnits.Select(e => GetWrapper<TenderUnitWrapper, TenderUnit>(e)));
      RegisterCollection(TenderUnits, model.TenderUnits);

      if (model.OfferUnits == null) throw new ArgumentException("OfferUnits cannot be null");
      OfferUnits = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(model.OfferUnits.Select(e => GetWrapper<OfferUnitWrapper, OfferUnit>(e)));
      RegisterCollection(OfferUnits, model.OfferUnits);

      if (model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(model.PaymentsConditions.Select(e => GetWrapper<PaymentConditionWrapper, PaymentCondition>(e)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);

      if (model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
      PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentPlanWrapper>(model.PaymentsPlanned.Select(e => GetWrapper<PaymentPlanWrapper, PaymentPlan>(e)));
      RegisterCollection(PaymentsPlanned, model.PaymentsPlanned);

      if (model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
      PaymentsActual = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(model.PaymentsActual.Select(e => GetWrapper<PaymentActualWrapper, PaymentActual>(e)));
      RegisterCollection(PaymentsActual, model.PaymentsActual);

    }
  }
}
