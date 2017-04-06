using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ShipmentProductUnitWrapper : WrapperBase<ShipmentProductUnit>
  {
    protected ShipmentProductUnitWrapper(ShipmentProductUnit model) : base(model) { }

	public static ShipmentProductUnitWrapper GetWrapper()
	{
		return GetWrapper(new ShipmentProductUnit());
	}

	public static ShipmentProductUnitWrapper GetWrapper(ShipmentProductUnit model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ShipmentProductUnitWrapper)Repository.ModelWrapperDictionary[model];

		return new ShipmentProductUnitWrapper(model);
	}



    #region SimpleProperties

    public System.Nullable<System.DateTime> DateDesiredDelivery
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> DateDesiredDeliveryOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateDesiredDelivery));
    public bool DateDesiredDeliveryIsChanged => GetIsChanged(nameof(DateDesiredDelivery));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public SalesProductUnitWrapper SalesProductUnit 
    {
        get { return SalesProductUnitWrapper.GetWrapper(Model.SalesProductUnit); }
        set
        {
			var oldPropVal = SalesProductUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SalesProductUnitWrapper SalesProductUnitOriginalValue => SalesProductUnitWrapper.GetWrapper(GetOriginalValue<SalesProductUnit>(nameof(SalesProductUnit)));
    public bool SalesProductUnitIsChanged => GetIsChanged(nameof(SalesProductUnit));


    #endregion

    protected override void InitializeComplexProperties(ShipmentProductUnit model)
    {

        SalesProductUnit = SalesProductUnitWrapper.GetWrapper(model.SalesProductUnit);

    }

  }
}
