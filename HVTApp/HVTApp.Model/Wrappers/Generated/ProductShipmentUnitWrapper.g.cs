using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProductShipmentUnitWrapper : WrapperBase<ShipmentUnit>
  {
    private ProductShipmentUnitWrapper() : base(new ShipmentUnit()) { }
    private ProductShipmentUnitWrapper(ShipmentUnit model) : base(model) { }



    #region SimpleProperties

    public System.Nullable<System.Int32> ExpectedDeliveryPeriod
    {
      get { return GetValue<System.Nullable<System.Int32>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.Int32> ExpectedDeliveryPeriodOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(ExpectedDeliveryPeriod));
    public bool ExpectedDeliveryPeriodIsChanged => GetIsChanged(nameof(ExpectedDeliveryPeriod));


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

	public ProductComplexUnitWrapper ProductComplexUnit 
    {
        get { return GetComplexProperty<ProductComplexUnitWrapper, ProductComplexUnit>(Model.ProductComplexUnit); }
        set { SetComplexProperty<ProductComplexUnitWrapper, ProductComplexUnit>(ProductComplexUnit, value); }
    }

    public ProductComplexUnitWrapper ProductComplexUnitOriginalValue { get; private set; }
    public bool ProductComplexUnitIsChanged => GetIsChanged(nameof(ProductComplexUnit));


	public AddressWrapper Address 
    {
        get { return GetComplexProperty<AddressWrapper, Address>(Model.Address); }
        set { SetComplexProperty<AddressWrapper, Address>(Address, value); }
    }

    public AddressWrapper AddressOriginalValue { get; private set; }
    public bool AddressIsChanged => GetIsChanged(nameof(Address));


	public CostWrapper Cost 
    {
        get { return GetComplexProperty<CostWrapper, Cost>(Model.Cost); }
        set { SetComplexProperty<CostWrapper, Cost>(Cost, value); }
    }

    public CostWrapper CostOriginalValue { get; private set; }
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    #endregion

    public override void InitializeComplexProperties()
    {

        ProductComplexUnit = GetWrapper<ProductComplexUnitWrapper, ProductComplexUnit>(Model.ProductComplexUnit);

        Address = GetWrapper<AddressWrapper, Address>(Model.Address);

        Cost = GetWrapper<CostWrapper, Cost>(Model.Cost);

    }

  }
}
