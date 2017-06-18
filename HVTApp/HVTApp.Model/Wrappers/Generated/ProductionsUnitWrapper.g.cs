using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProductionsUnitWrapper : WrapperBase<ProductionsUnit>
  {
    private ProductionsUnitWrapper() : base(new ProductionsUnit()) { }
    private ProductionsUnitWrapper(ProductionsUnit model) : base(model) { }



    #region SimpleProperties

    public System.Int32 OrderPosition
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 OrderPositionOriginalValue => GetOriginalValue<System.Int32>(nameof(OrderPosition));
    public bool OrderPositionIsChanged => GetIsChanged(nameof(OrderPosition));


    public System.String SerialNumber
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String SerialNumberOriginalValue => GetOriginalValue<System.String>(nameof(SerialNumber));
    public bool SerialNumberIsChanged => GetIsChanged(nameof(SerialNumber));


    public System.Int32 PlannedProductionTerm
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 PlannedProductionTermOriginalValue => GetOriginalValue<System.Int32>(nameof(PlannedProductionTerm));
    public bool PlannedProductionTermIsChanged => GetIsChanged(nameof(PlannedProductionTerm));


    public System.Int32 PlanedTermFromPickToEndProductionEnd
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 PlanedTermFromPickToEndProductionEndOriginalValue => GetOriginalValue<System.Int32>(nameof(PlanedTermFromPickToEndProductionEnd));
    public bool PlanedTermFromPickToEndProductionEndIsChanged => GetIsChanged(nameof(PlanedTermFromPickToEndProductionEnd));


    public System.Nullable<System.DateTime> StartProductionDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> StartProductionDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(StartProductionDate));
    public bool StartProductionDateIsChanged => GetIsChanged(nameof(StartProductionDate));


    public System.Nullable<System.DateTime> PickingDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> PickingDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(PickingDate));
    public bool PickingDateIsChanged => GetIsChanged(nameof(PickingDate));


    public System.Nullable<System.DateTime> EndProductionDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> EndProductionDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(EndProductionDate));
    public bool EndProductionDateIsChanged => GetIsChanged(nameof(EndProductionDate));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public UnitWrapper Unit 
    {
        get { return GetComplexProperty<UnitWrapper, Unit>(Model.Unit); }
        set { SetComplexProperty<UnitWrapper, Unit>(Unit, value); }
    }

    public UnitWrapper UnitOriginalValue { get; private set; }
    public bool UnitIsChanged => GetIsChanged(nameof(Unit));


	public ProductWrapper Product 
    {
        get { return GetComplexProperty<ProductWrapper, Product>(Model.Product); }
        set { SetComplexProperty<ProductWrapper, Product>(Product, value); }
    }

    public ProductWrapper ProductOriginalValue { get; private set; }
    public bool ProductIsChanged => GetIsChanged(nameof(Product));


	public OrderWrapper Order 
    {
        get { return GetComplexProperty<OrderWrapper, Order>(Model.Order); }
        set { SetComplexProperty<OrderWrapper, Order>(Order, value); }
    }

    public OrderWrapper OrderOriginalValue { get; private set; }
    public bool OrderIsChanged => GetIsChanged(nameof(Order));


    #endregion

    public override void InitializeComplexProperties()
    {

        Unit = GetWrapper<UnitWrapper, Unit>(Model.Unit);

        Product = GetWrapper<ProductWrapper, Product>(Model.Product);

        Order = GetWrapper<OrderWrapper, Order>(Model.Order);

    }

  }
}
