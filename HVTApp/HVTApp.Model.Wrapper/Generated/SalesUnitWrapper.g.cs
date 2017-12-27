using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class SalesUnitWrapper : WrapperBase<SalesUnit>
  {
    public SalesUnitWrapper() : base(new SalesUnit(), new Dictionary<IBaseEntity, object>()) { }
    public SalesUnitWrapper(SalesUnit model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public SalesUnitWrapper(SalesUnit model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public SalesUnitWrapper(SalesUnit model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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
	public SalesUnitWrapper ParentSalesUnit 
    {
        get { return GetComplexProperty<SalesUnitWrapper, SalesUnit>(Model.ParentSalesUnit); }
        set { SetComplexProperty<SalesUnitWrapper, SalesUnit>(ParentSalesUnit, value); }
    }

    public SalesUnitWrapper ParentSalesUnitOriginalValue { get; private set; }
    public bool ParentSalesUnitIsChanged => GetIsChanged(nameof(ParentSalesUnit));

	public ProjectWrapper Project 
    {
        get { return GetComplexProperty<ProjectWrapper, Project>(Model.Project); }
        set { SetComplexProperty<ProjectWrapper, Project>(Project, value); }
    }

    public ProjectWrapper ProjectOriginalValue { get; private set; }
    public bool ProjectIsChanged => GetIsChanged(nameof(Project));

	public FacilityWrapper Facility 
    {
        get { return GetComplexProperty<FacilityWrapper, Facility>(Model.Facility); }
        set { SetComplexProperty<FacilityWrapper, Facility>(Facility, value); }
    }

    public FacilityWrapper FacilityOriginalValue { get; private set; }
    public bool FacilityIsChanged => GetIsChanged(nameof(Facility));

	public SpecificationWrapper Specification 
    {
        get { return GetComplexProperty<SpecificationWrapper, Specification>(Model.Specification); }
        set { SetComplexProperty<SpecificationWrapper, Specification>(Specification, value); }
    }

    public SpecificationWrapper SpecificationOriginalValue { get; private set; }
    public bool SpecificationIsChanged => GetIsChanged(nameof(Specification));

	public SumAndVatWrapper CostSingle 
    {
        get { return GetComplexProperty<SumAndVatWrapper, SumAndVat>(Model.CostSingle); }
        set { SetComplexProperty<SumAndVatWrapper, SumAndVat>(CostSingle, value); }
    }

    public SumAndVatWrapper CostSingleOriginalValue { get; private set; }
    public bool CostSingleIsChanged => GetIsChanged(nameof(CostSingle));

	public ProductionUnitWrapper ProductionUnit 
    {
        get { return GetComplexProperty<ProductionUnitWrapper, ProductionUnit>(Model.ProductionUnit); }
        set { SetComplexProperty<ProductionUnitWrapper, ProductionUnit>(ProductionUnit, value); }
    }

    public ProductionUnitWrapper ProductionUnitOriginalValue { get; private set; }
    public bool ProductionUnitIsChanged => GetIsChanged(nameof(ProductionUnit));

	public ShipmentUnitWrapper ShipmentUnit 
    {
        get { return GetComplexProperty<ShipmentUnitWrapper, ShipmentUnit>(Model.ShipmentUnit); }
        set { SetComplexProperty<ShipmentUnitWrapper, ShipmentUnit>(ShipmentUnit, value); }
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
