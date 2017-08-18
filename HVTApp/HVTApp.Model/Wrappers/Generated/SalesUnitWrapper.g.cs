using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class SalesUnitWrapper : WrapperBase<SalesUnit>
  {
    private SalesUnitWrapper(IGetWrapper getWrapper) : base(new SalesUnit(), getWrapper) { }
    private SalesUnitWrapper(SalesUnit model, IGetWrapper getWrapper) : base(model, getWrapper) { }



    #region SimpleProperties

    public System.Double Cost
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    public System.Nullable<System.DateTime> RealizationDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> RealizationDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(RealizationDate));
    public bool RealizationDateIsChanged => GetIsChanged(nameof(RealizationDate));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public OfferUnitWrapper OfferUnit 
    {
        get { return GetComplexProperty<OfferUnitWrapper, OfferUnit>(Model.OfferUnit); }
        set { SetComplexProperty<OfferUnitWrapper, OfferUnit>(OfferUnit, value); }
    }

    public OfferUnitWrapper OfferUnitOriginalValue { get; private set; }
    public bool OfferUnitIsChanged => GetIsChanged(nameof(OfferUnit));


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


	public SpecificationWrapper Specification 
    {
        get { return GetComplexProperty<SpecificationWrapper, Specification>(Model.Specification); }
        set { SetComplexProperty<SpecificationWrapper, Specification>(Specification, value); }
    }

    public SpecificationWrapper SpecificationOriginalValue { get; private set; }
    public bool SpecificationIsChanged => GetIsChanged(nameof(Specification));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentActualWrapper> PaymentsActual { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlanned { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        OfferUnit = GetWrapper<OfferUnitWrapper, OfferUnit>(Model.OfferUnit);

        ProductionUnit = GetWrapper<ProductionUnitWrapper, ProductionUnit>(Model.ProductionUnit);

        ShipmentUnit = GetWrapper<ShipmentUnitWrapper, ShipmentUnit>(Model.ShipmentUnit);

        Specification = GetWrapper<SpecificationWrapper, Specification>(Model.Specification);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => GetWrapper<PaymentConditionWrapper, PaymentCondition>(e)));
      RegisterCollection(PaymentsConditions, Model.PaymentsConditions);


      if (Model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
      PaymentsActual = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(Model.PaymentsActual.Select(e => GetWrapper<PaymentActualWrapper, PaymentActual>(e)));
      RegisterCollection(PaymentsActual, Model.PaymentsActual);


      if (Model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
      PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlanned.Select(e => GetWrapper<PaymentPlannedWrapper, PaymentPlanned>(e)));
      RegisterCollection(PaymentsPlanned, Model.PaymentsPlanned);


    }

  }
}
