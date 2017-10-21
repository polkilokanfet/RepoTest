using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ShipmentUnitWrapper : WrapperBase<ShipmentUnit>
  {
    public ShipmentUnitWrapper(ShipmentUnit model) : base(model) { }



    #region SimpleProperties

    public System.Nullable<System.Int32> ExpectedDeliveryPeriod
    {
      get { return GetValue<System.Nullable<System.Int32>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.Int32> ExpectedDeliveryPeriodOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(ExpectedDeliveryPeriod));
    public bool ExpectedDeliveryPeriodIsChanged => GetIsChanged(nameof(ExpectedDeliveryPeriod));


    public System.Double Cost
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


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


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private SalesUnitWrapper _fieldSalesUnit;
	public SalesUnitWrapper SalesUnit 
    {
        get { return _fieldSalesUnit ; }
        set
        {
            SetComplexValue<SalesUnit, SalesUnitWrapper>(_fieldSalesUnit, value);
            _fieldSalesUnit  = value;
        }
    }

	private AddressWrapper _fieldAddress;
	public AddressWrapper Address 
    {
        get { return _fieldAddress ; }
        set
        {
            SetComplexValue<Address, AddressWrapper>(_fieldAddress, value);
            _fieldAddress  = value;
        }
    }

    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.SalesUnit != null)
        {
            _fieldSalesUnit = new SalesUnitWrapper(Model.SalesUnit);
            RegisterComplex(SalesUnit);
        }

		if (Model.Address != null)
        {
            _fieldAddress = new AddressWrapper(Model.Address);
            RegisterComplex(Address);
        }

    }

  }
}
