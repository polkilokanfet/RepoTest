using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class SalesUnitWrapper : WrapperBase<SalesUnit>
  {
    protected SalesUnitWrapper(SalesUnit model) : base(model) { }

	public static SalesUnitWrapper GetWrapper()
	{
		return GetWrapper(new SalesUnit());
	}

	public static SalesUnitWrapper GetWrapper(SalesUnit model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (SalesUnitWrapper)Repository.ModelWrapperDictionary[model];

		return new SalesUnitWrapper(model);
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

	public SalesUnitWrapper ParentSalesUnit 
    {
        get { return SalesUnitWrapper.GetWrapper(Model.ParentSalesUnit); }
        set
        {
			var oldPropVal = ParentSalesUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SalesUnitWrapper ParentSalesUnitOriginalValue => SalesUnitWrapper.GetWrapper(GetOriginalValue<SalesUnit>(nameof(ParentSalesUnit)));
    public bool ParentSalesUnitIsChanged => GetIsChanged(nameof(ParentSalesUnit));


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


	public SumAndVatWrapper Cost 
    {
        get { return SumAndVatWrapper.GetWrapper(Model.Cost); }
        set
        {
			var oldPropVal = Cost;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SumAndVatWrapper CostOriginalValue => SumAndVatWrapper.GetWrapper(GetOriginalValue<SumAndVat>(nameof(Cost)));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


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

    public IValidatableChangeTrackingCollection<SalesUnitWrapper> ChildSalesUnits { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentWrapper> PaymentsPlanned { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentWrapper> PaymentsActual { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(SalesUnit model)
    {

        ParentSalesUnit = SalesUnitWrapper.GetWrapper(model.ParentSalesUnit);

        ProductionUnit = ProductionUnitWrapper.GetWrapper(model.ProductionUnit);

        Cost = SumAndVatWrapper.GetWrapper(model.Cost);

        ShipmentUnit = ShipmentUnitWrapper.GetWrapper(model.ShipmentUnit);

    }

  
    protected override void InitializeCollectionComplexProperties(SalesUnit model)
    {

      if (model.ChildSalesUnits == null) throw new ArgumentException("ChildSalesUnits cannot be null");
      ChildSalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(model.ChildSalesUnits.Select(e => SalesUnitWrapper.GetWrapper(e)));
      RegisterCollection(ChildSalesUnits, model.ChildSalesUnits);


      if (model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(model.PaymentsConditions.Select(e => PaymentConditionWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);


      if (model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
      PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentWrapper>(model.PaymentsPlanned.Select(e => PaymentWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsPlanned, model.PaymentsPlanned);


      if (model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
      PaymentsActual = new ValidatableChangeTrackingCollection<PaymentWrapper>(model.PaymentsActual.Select(e => PaymentWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsActual, model.PaymentsActual);


    }

  }
}
