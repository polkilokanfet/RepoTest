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


	public CostInfoWrapper CostInfo 
    {
        get { return CostInfoWrapper.GetWrapper(Model.CostInfo); }
        set
        {
			var oldPropVal = CostInfo;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public CostInfoWrapper CostInfoOriginalValue => CostInfoWrapper.GetWrapper(GetOriginalValue<CostInfo>(nameof(CostInfo)));
    public bool CostInfoIsChanged => GetIsChanged(nameof(CostInfo));


	public PaymentsWrapper Payments 
    {
        get { return PaymentsWrapper.GetWrapper(Model.Payments); }
        set
        {
			var oldPropVal = Payments;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public PaymentsWrapper PaymentsOriginalValue => PaymentsWrapper.GetWrapper(GetOriginalValue<Payments>(nameof(Payments)));
    public bool PaymentsIsChanged => GetIsChanged(nameof(Payments));


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

    protected override void InitializeComplexProperties(SalesProductUnit model)
    {

        ProductionProductUnit = ProductionProductUnitWrapper.GetWrapper(model.ProductionProductUnit);

        CostInfo = CostInfoWrapper.GetWrapper(model.CostInfo);

        Payments = PaymentsWrapper.GetWrapper(model.Payments);

        ShipmentProductUnit = ShipmentProductUnitWrapper.GetWrapper(model.ShipmentProductUnit);

    }

  }
}
