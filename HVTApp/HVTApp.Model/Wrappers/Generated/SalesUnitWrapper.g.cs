using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
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

	public OfferUnitWrapper OfferUnit { get; set; }

	public ProductionUnitWrapper ProductionUnit { get; set; }

	public ShipmentUnitWrapper ShipmentUnit { get; set; }

	public SpecificationWrapper Specification { get; set; }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentActualWrapper> PaymentsActual { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlanned { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        OfferUnit = new OfferUnitWrapper(Model.OfferUnit);
		RegisterComplex(OfferUnit);

        ProductionUnit = new ProductionUnitWrapper(Model.ProductionUnit);
		RegisterComplex(ProductionUnit);

        ShipmentUnit = new ShipmentUnitWrapper(Model.ShipmentUnit);
		RegisterComplex(ShipmentUnit);

        Specification = new SpecificationWrapper(Model.Specification);
		RegisterComplex(Specification);

    }

  
    protected override void InitializeCollectionComplexProperties()
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
