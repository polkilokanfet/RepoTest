using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ShipmentUnitWrapper : WrapperBase<ShipmentUnit>
  {
    public ShipmentUnitWrapper() : base(new ShipmentUnit()) { }
    public ShipmentUnitWrapper(ShipmentUnit model) : base(model) { }

//	public static ShipmentUnitWrapper GetWrapper()
//	{
//		return GetWrapper(new ShipmentUnit());
//	}
//
//	public static ShipmentUnitWrapper GetWrapper(ShipmentUnit model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ExistsWrappers.ContainsKey(model))
//			return (ShipmentUnitWrapper)Repository.ExistsWrappers[model];
//
//		return new ShipmentUnitWrapper(model);
//	}


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
	private AddressWrapper _fieldAddress;
	public AddressWrapper Address 
    {
        get { return _fieldAddress; }
        set
        {
			SetComplexProperty<AddressWrapper, Address>(_fieldAddress, value);
			_fieldAddress = value;
        }
    }
    public AddressWrapper AddressOriginalValue { get; private set; }
    public bool AddressIsChanged => GetIsChanged(nameof(Address));

	private SalesUnitWrapper _fieldSalesUnit;
	public SalesUnitWrapper SalesUnit 
    {
        get { return _fieldSalesUnit; }
        set
        {
			SetComplexProperty<SalesUnitWrapper, SalesUnit>(_fieldSalesUnit, value);
			_fieldSalesUnit = value;
        }
    }
    public SalesUnitWrapper SalesUnitOriginalValue { get; private set; }
    public bool SalesUnitIsChanged => GetIsChanged(nameof(SalesUnit));

    #endregion
    protected override void InitializeComplexProperties(ShipmentUnit model)
    {
        Address = GetWrapper<AddressWrapper, Address>(model.Address);
        SalesUnit = GetWrapper<SalesUnitWrapper, SalesUnit>(model.SalesUnit);
    }
  }
}
