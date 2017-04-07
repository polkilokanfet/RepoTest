using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class SalesProductUnitWrapper : WrapperBase<SalesProductUnit>
  {
    protected SalesProductUnitWrapper(SalesProductUnit model) : base(model) { }

	public static SalesProductUnitWrapper GetWrapper()
	{
		return GetWrapper(new SalesProductUnit());
	}

	public static SalesProductUnitWrapper GetWrapper(SalesProductUnit model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (SalesProductUnitWrapper)Repository.ModelWrapperDictionary[model];

		return new SalesProductUnitWrapper(model);
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

	public ProductionProductUnitWrapper ProductionProductUnit 
    {
        get { return ProductionProductUnitWrapper.GetWrapper(Model.ProductionProductUnit); }
        set
        {
			var oldPropVal = ProductionProductUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProductionProductUnitWrapper ProductionProductUnitOriginalValue => ProductionProductUnitWrapper.GetWrapper(GetOriginalValue<ProductionProductUnit>(nameof(ProductionProductUnit)));
    public bool ProductionProductUnitIsChanged => GetIsChanged(nameof(ProductionProductUnit));


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


	public ShipmentProductUnitWrapper ShipmentProductUnit 
    {
        get { return ShipmentProductUnitWrapper.GetWrapper(Model.ShipmentProductUnit); }
        set
        {
			var oldPropVal = ShipmentProductUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ShipmentProductUnitWrapper ShipmentProductUnitOriginalValue => ShipmentProductUnitWrapper.GetWrapper(GetOriginalValue<ShipmentProductUnit>(nameof(ShipmentProductUnit)));
    public bool ShipmentProductUnitIsChanged => GetIsChanged(nameof(ShipmentProductUnit));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentWrapper> PaymentsPlanned { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentWrapper> PaymentsActual { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(SalesProductUnit model)
    {

        ProductionProductUnit = ProductionProductUnitWrapper.GetWrapper(model.ProductionProductUnit);

        Cost = SumAndVatWrapper.GetWrapper(model.Cost);

        ShipmentProductUnit = ShipmentProductUnitWrapper.GetWrapper(model.ShipmentProductUnit);

    }

  
    protected override void InitializeCollectionComplexProperties(SalesProductUnit model)
    {

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
