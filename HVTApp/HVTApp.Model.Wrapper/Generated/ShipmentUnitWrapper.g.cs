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

    public System.Nullable<System.Int32> ExpectedDeliveryPeriod
    {
      get { return GetValue<System.Nullable<System.Int32>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.Int32> ExpectedDeliveryPeriodOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(ExpectedDeliveryPeriod));
    public bool ExpectedDeliveryPeriodIsChanged => GetIsChanged(nameof(ExpectedDeliveryPeriod));


    public System.Double ShipmentCost
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double ShipmentCostOriginalValue => GetOriginalValue<System.Double>(nameof(ShipmentCost));
    public bool ShipmentCostIsChanged => GetIsChanged(nameof(ShipmentCost));


    public System.Nullable<System.DateTime> ShipmentDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> ShipmentDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(ShipmentDate));
    public bool ShipmentDateIsChanged => GetIsChanged(nameof(ShipmentDate));


    public System.Nullable<System.DateTime> ShipmentPlanDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> ShipmentPlanDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(ShipmentPlanDate));
    public bool ShipmentPlanDateIsChanged => GetIsChanged(nameof(ShipmentPlanDate));


    public System.Nullable<System.DateTime> RequiredDeliveryDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> RequiredDeliveryDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(RequiredDeliveryDate));
    public bool RequiredDeliveryDateIsChanged => GetIsChanged(nameof(RequiredDeliveryDate));


    public System.Nullable<System.DateTime> DeliveryDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> DeliveryDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DeliveryDate));
    public bool DeliveryDateIsChanged => GetIsChanged(nameof(DeliveryDate));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public AddressWrapper Address 
    {
        get { return AddressWrapper.GetWrapper(Model.Address); }
        set
        {
			var oldPropVal = Address;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public AddressWrapper AddressOriginalValue => AddressWrapper.GetWrapper(GetOriginalValue<Address>(nameof(Address)));
    public bool AddressIsChanged => GetIsChanged(nameof(Address));


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


    #endregion

    protected override void InitializeComplexProperties(ShipmentUnit model)
    {

        Address = AddressWrapper.GetWrapper(model.Address);

        SalesUnit = SalesUnitWrapper.GetWrapper(model.SalesUnit);

    }

  }
}
