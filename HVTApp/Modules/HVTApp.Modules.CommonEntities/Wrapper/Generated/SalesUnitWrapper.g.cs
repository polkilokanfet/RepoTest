using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class SalesUnitWrapper : WrapperBase<SalesUnit>
	{
	public SalesUnitWrapper(SalesUnit model) : base(model) { }

	
    #region SimpleProperties
    public System.Guid OfferUnitId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid OfferUnitIdOriginalValue => GetOriginalValue<System.Guid>(nameof(OfferUnitId));
    public bool OfferUnitIdIsChanged => GetIsChanged(nameof(OfferUnitId));

    public System.Double Cost
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));

    public System.Guid SpecificationId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid SpecificationIdOriginalValue => GetOriginalValue<System.Guid>(nameof(SpecificationId));
    public bool SpecificationIdIsChanged => GetIsChanged(nameof(SpecificationId));

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
	private OfferUnitWrapper _fieldOfferUnit;
	public OfferUnitWrapper OfferUnit 
    {
        get { return _fieldOfferUnit ; }
        set
        {
            SetComplexValue<OfferUnit, OfferUnitWrapper>(_fieldOfferUnit, value);
            _fieldOfferUnit  = value;
        }
    }
	private ProductionUnitWrapper _fieldProductionUnit;
	public ProductionUnitWrapper ProductionUnit 
    {
        get { return _fieldProductionUnit ; }
        set
        {
            SetComplexValue<ProductionUnit, ProductionUnitWrapper>(_fieldProductionUnit, value);
            _fieldProductionUnit  = value;
        }
    }
	private ShipmentUnitWrapper _fieldShipmentUnit;
	public ShipmentUnitWrapper ShipmentUnit 
    {
        get { return _fieldShipmentUnit ; }
        set
        {
            SetComplexValue<ShipmentUnit, ShipmentUnitWrapper>(_fieldShipmentUnit, value);
            _fieldShipmentUnit  = value;
        }
    }
    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }

    public IValidatableChangeTrackingCollection<PaymentActualWrapper> PaymentsActual { get; private set; }

    public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlanned { get; private set; }

    #endregion
    public override void InitializeComplexProperties()
    {
		if (Model.OfferUnit != null)
        {
            _fieldOfferUnit = new OfferUnitWrapper(Model.OfferUnit);
            RegisterComplex(OfferUnit);
        }
		if (Model.ProductionUnit != null)
        {
            _fieldProductionUnit = new ProductionUnitWrapper(Model.ProductionUnit);
            RegisterComplex(ProductionUnit);
        }
		if (Model.ShipmentUnit != null)
        {
            _fieldShipmentUnit = new ShipmentUnitWrapper(Model.ShipmentUnit);
            RegisterComplex(ShipmentUnit);
        }
    }
  
    protected override void InitializeCollectionProperties()
    {
      if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => new PaymentConditionWrapper(e)));
      RegisterCollection(PaymentsConditions, Model.PaymentsConditions);

      if (Model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
      PaymentsActual = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(Model.PaymentsActual.Select(e => new PaymentActualWrapper(e)));
      RegisterCollection(PaymentsActual, Model.PaymentsActual);

      if (Model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
      PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlanned.Select(e => new PaymentPlannedWrapper(e)));
      RegisterCollection(PaymentsPlanned, Model.PaymentsPlanned);

    }
	}
}
	