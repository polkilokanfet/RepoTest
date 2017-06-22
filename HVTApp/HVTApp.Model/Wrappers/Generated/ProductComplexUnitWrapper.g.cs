using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProductComplexUnitWrapper : WrapperBase<ProductComplexUnit>
  {
    private ProductComplexUnitWrapper() : base(new ProductComplexUnit()) { }
    private ProductComplexUnitWrapper(ProductComplexUnit model) : base(model) { }



    #region SimpleProperties

    public System.Int32 OrderPosition
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 OrderPositionOriginalValue => GetOriginalValue<System.Int32>(nameof(OrderPosition));
    public bool OrderPositionIsChanged => GetIsChanged(nameof(OrderPosition));


    public System.String SerialNumber
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String SerialNumberOriginalValue => GetOriginalValue<System.String>(nameof(SerialNumber));
    public bool SerialNumberIsChanged => GetIsChanged(nameof(SerialNumber));


    public System.Int32 PlannedProductionTerm
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 PlannedProductionTermOriginalValue => GetOriginalValue<System.Int32>(nameof(PlannedProductionTerm));
    public bool PlannedProductionTermIsChanged => GetIsChanged(nameof(PlannedProductionTerm));


    public System.Int32 PlanedTermFromPickToEndProductionEnd
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 PlanedTermFromPickToEndProductionEndOriginalValue => GetOriginalValue<System.Int32>(nameof(PlanedTermFromPickToEndProductionEnd));
    public bool PlanedTermFromPickToEndProductionEndIsChanged => GetIsChanged(nameof(PlanedTermFromPickToEndProductionEnd));


    public System.Nullable<System.DateTime> StartProductionDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> StartProductionDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(StartProductionDate));
    public bool StartProductionDateIsChanged => GetIsChanged(nameof(StartProductionDate));


    public System.Nullable<System.DateTime> PickingDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> PickingDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(PickingDate));
    public bool PickingDateIsChanged => GetIsChanged(nameof(PickingDate));


    public System.Nullable<System.DateTime> EndProductionDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> EndProductionDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(EndProductionDate));
    public bool EndProductionDateIsChanged => GetIsChanged(nameof(EndProductionDate));


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


	public ProductWrapper Product 
    {
        get { return GetComplexProperty<ProductWrapper, Product>(Model.Product); }
        set { SetComplexProperty<ProductWrapper, Product>(Product, value); }
    }

    public ProductWrapper ProductOriginalValue { get; private set; }
    public bool ProductIsChanged => GetIsChanged(nameof(Product));


	public OrderWrapper Order 
    {
        get { return GetComplexProperty<OrderWrapper, Order>(Model.Order); }
        set { SetComplexProperty<OrderWrapper, Order>(Order, value); }
    }

    public OrderWrapper OrderOriginalValue { get; private set; }
    public bool OrderIsChanged => GetIsChanged(nameof(Order));


	public SumAndVatWrapper Cost 
    {
        get { return GetComplexProperty<SumAndVatWrapper, SumAndVat>(Model.Cost); }
        set { SetComplexProperty<SumAndVatWrapper, SumAndVat>(Cost, value); }
    }

    public SumAndVatWrapper CostOriginalValue { get; private set; }
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


	public SpecificationWrapper Specification 
    {
        get { return GetComplexProperty<SpecificationWrapper, Specification>(Model.Specification); }
        set { SetComplexProperty<SpecificationWrapper, Specification>(Specification, value); }
    }

    public SpecificationWrapper SpecificationOriginalValue { get; private set; }
    public bool SpecificationIsChanged => GetIsChanged(nameof(Specification));


	public ProductShipmentUnitWrapper ProductShipmentUnit 
    {
        get { return GetComplexProperty<ProductShipmentUnitWrapper, ProductShipmentUnit>(Model.ProductShipmentUnit); }
        set { SetComplexProperty<ProductShipmentUnitWrapper, ProductShipmentUnit>(ProductShipmentUnit, value); }
    }

    public ProductShipmentUnitWrapper ProductShipmentUnitOriginalValue { get; private set; }
    public bool ProductShipmentUnitIsChanged => GetIsChanged(nameof(ProductShipmentUnit));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentPlanWrapper> PaymentsPlanned { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentActualWrapper> PaymentsActual { get; private set; }


    public IValidatableChangeTrackingCollection<ProductTenderUnitWrapper> TendersUnits { get; private set; }


    public IValidatableChangeTrackingCollection<ProductOfferUnitWrapper> OffersUnits { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Project = GetWrapper<ProjectWrapper, Project>(Model.Project);

        Facility = GetWrapper<FacilityWrapper, Facility>(Model.Facility);

        Product = GetWrapper<ProductWrapper, Product>(Model.Product);

        Order = GetWrapper<OrderWrapper, Order>(Model.Order);

        Cost = GetWrapper<SumAndVatWrapper, SumAndVat>(Model.Cost);

        Specification = GetWrapper<SpecificationWrapper, Specification>(Model.Specification);

        ProductShipmentUnit = GetWrapper<ProductShipmentUnitWrapper, ProductShipmentUnit>(Model.ProductShipmentUnit);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => GetWrapper<PaymentConditionWrapper, PaymentCondition>(e)));
      RegisterCollection(PaymentsConditions, Model.PaymentsConditions);


      if (Model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
      PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentPlanWrapper>(Model.PaymentsPlanned.Select(e => GetWrapper<PaymentPlanWrapper, PaymentPlan>(e)));
      RegisterCollection(PaymentsPlanned, Model.PaymentsPlanned);


      if (Model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
      PaymentsActual = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(Model.PaymentsActual.Select(e => GetWrapper<PaymentActualWrapper, PaymentActual>(e)));
      RegisterCollection(PaymentsActual, Model.PaymentsActual);


      if (Model.TendersUnits == null) throw new ArgumentException("TendersUnits cannot be null");
      TendersUnits = new ValidatableChangeTrackingCollection<ProductTenderUnitWrapper>(Model.TendersUnits.Select(e => GetWrapper<ProductTenderUnitWrapper, ProductTenderUnit>(e)));
      RegisterCollection(TendersUnits, Model.TendersUnits);


      if (Model.OffersUnits == null) throw new ArgumentException("OffersUnits cannot be null");
      OffersUnits = new ValidatableChangeTrackingCollection<ProductOfferUnitWrapper>(Model.OffersUnits.Select(e => GetWrapper<ProductOfferUnitWrapper, ProductOfferUnit>(e)));
      RegisterCollection(OffersUnits, Model.OffersUnits);


    }

  }
}
