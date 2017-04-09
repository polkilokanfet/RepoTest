using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ShipmentUnitWrapper : WrapperBase<ShipmentUnit>
  {
    protected ShipmentUnitWrapper(ShipmentUnit model) : base(model) { }

	public static ShipmentUnitWrapper GetWrapper()
	{
		return GetWrapper(new ShipmentUnit());
	}

	public static ShipmentUnitWrapper GetWrapper(ShipmentUnit model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ShipmentUnitWrapper)Repository.ModelWrapperDictionary[model];

		return new ShipmentUnitWrapper(model);
	}



    #region SimpleProperties

    public System.Nullable<System.DateTime> RequiredDeliveryDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> RequiredDeliveryDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(RequiredDeliveryDate));
    public bool RequiredDeliveryDateIsChanged => GetIsChanged(nameof(RequiredDeliveryDate));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

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


    #endregion

    protected override void InitializeComplexProperties(ShipmentUnit model)
    {

        SalesUnit = SalesUnitWrapper.GetWrapper(model.SalesUnit);

        Cost = SumAndVatWrapper.GetWrapper(model.Cost);

    }

  }
}
