using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OfferUnitWrapper : WrapperBase<OfferUnit>
  {
    protected OfferUnitWrapper(OfferUnit model) : base(model) { }

	public static OfferUnitWrapper GetWrapper()
	{
		return GetWrapper(new OfferUnit());
	}

	public static OfferUnitWrapper GetWrapper(OfferUnit model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (OfferUnitWrapper)Repository.ModelWrapperDictionary[model];

		return new OfferUnitWrapper(model);
	}



    #region SimpleProperties

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public OfferWrapper Offer 
    {
        get { return OfferWrapper.GetWrapper(Model.Offer); }
        set
        {
			var oldPropVal = Offer;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public OfferWrapper OfferOriginalValue => OfferWrapper.GetWrapper(GetOriginalValue<Offer>(nameof(Offer)));
    public bool OfferIsChanged => GetIsChanged(nameof(Offer));


	public SalesUnitWrapper SalesUnit 
    {
        get { return SalesUnitWrapper.GetWrapper(Model.SalesUnit); }
        set
        {
			var oldPropVal = SalesUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SalesUnitWrapper SalesUnitOriginalValue => SalesUnitWrapper.GetWrapper(GetOriginalValue<SalesUnit>(nameof(SalesUnit)));
    public bool SalesUnitIsChanged => GetIsChanged(nameof(SalesUnit));


	public OfferUnitWrapper ParentOfferUnit 
    {
        get { return OfferUnitWrapper.GetWrapper(Model.ParentOfferUnit); }
        set
        {
			var oldPropVal = ParentOfferUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public OfferUnitWrapper ParentOfferUnitOriginalValue => OfferUnitWrapper.GetWrapper(GetOriginalValue<OfferUnit>(nameof(ParentOfferUnit)));
    public bool ParentOfferUnitIsChanged => GetIsChanged(nameof(ParentOfferUnit));


	public FacilityWrapper Facility 
    {
        get { return FacilityWrapper.GetWrapper(Model.Facility); }
        set
        {
			var oldPropVal = Facility;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public FacilityWrapper FacilityOriginalValue => FacilityWrapper.GetWrapper(GetOriginalValue<Facility>(nameof(Facility)));
    public bool FacilityIsChanged => GetIsChanged(nameof(Facility));


	public SumAndVatWrapper CostSingle 
    {
        get { return SumAndVatWrapper.GetWrapper(Model.CostSingle); }
        set
        {
			var oldPropVal = CostSingle;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SumAndVatWrapper CostSingleOriginalValue => SumAndVatWrapper.GetWrapper(GetOriginalValue<SumAndVat>(nameof(CostSingle)));
    public bool CostSingleIsChanged => GetIsChanged(nameof(CostSingle));


	public ProductionUnitWrapper ProductionUnit 
    {
        get { return ProductionUnitWrapper.GetWrapper(Model.ProductionUnit); }
        set
        {
			var oldPropVal = ProductionUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProductionUnitWrapper ProductionUnitOriginalValue => ProductionUnitWrapper.GetWrapper(GetOriginalValue<ProductionUnit>(nameof(ProductionUnit)));
    public bool ProductionUnitIsChanged => GetIsChanged(nameof(ProductionUnit));


	public ShipmentUnitWrapper ShipmentUnit 
    {
        get { return ShipmentUnitWrapper.GetWrapper(Model.ShipmentUnit); }
        set
        {
			var oldPropVal = ShipmentUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ShipmentUnitWrapper ShipmentUnitOriginalValue => ShipmentUnitWrapper.GetWrapper(GetOriginalValue<ShipmentUnit>(nameof(ShipmentUnit)));
    public bool ShipmentUnitIsChanged => GetIsChanged(nameof(ShipmentUnit));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<OfferUnitWrapper> ChildOfferUnits { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(OfferUnit model)
    {

        Offer = OfferWrapper.GetWrapper(model.Offer);

        SalesUnit = SalesUnitWrapper.GetWrapper(model.SalesUnit);

        ParentOfferUnit = OfferUnitWrapper.GetWrapper(model.ParentOfferUnit);

        Facility = FacilityWrapper.GetWrapper(model.Facility);

        CostSingle = SumAndVatWrapper.GetWrapper(model.CostSingle);

        ProductionUnit = ProductionUnitWrapper.GetWrapper(model.ProductionUnit);

        ShipmentUnit = ShipmentUnitWrapper.GetWrapper(model.ShipmentUnit);

    }

  
    protected override void InitializeCollectionComplexProperties(OfferUnit model)
    {

      if (model.ChildOfferUnits == null) throw new ArgumentException("ChildOfferUnits cannot be null");
      ChildOfferUnits = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(model.ChildOfferUnits.Select(e => OfferUnitWrapper.GetWrapper(e)));
      RegisterCollection(ChildOfferUnits, model.ChildOfferUnits);


      if (model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(model.PaymentsConditions.Select(e => PaymentConditionWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);


    }

  }
}
