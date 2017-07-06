using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProductionUnitWrapper : WrapperBase<ProductionUnit>
  {
    private ProductionUnitWrapper() : base(new ProductionUnit()) { }
    private ProductionUnitWrapper(ProductionUnit model) : base(model) { }



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


    public System.Int32 PlannedTerm_FromStartToEndProduction
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 PlannedTerm_FromStartToEndProductionOriginalValue => GetOriginalValue<System.Int32>(nameof(PlannedTerm_FromStartToEndProduction));
    public bool PlannedTerm_FromStartToEndProductionIsChanged => GetIsChanged(nameof(PlannedTerm_FromStartToEndProduction));


    public System.Int32 PlannedTerm_FromPickToEndProduction
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 PlannedTerm_FromPickToEndProductionOriginalValue => GetOriginalValue<System.Int32>(nameof(PlannedTerm_FromPickToEndProduction));
    public bool PlannedTerm_FromPickToEndProductionIsChanged => GetIsChanged(nameof(PlannedTerm_FromPickToEndProduction));


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

        Product = GetWrapper<ProductWrapper, Product>(Model.Product);

        Order = GetWrapper<OrderWrapper, Order>(Model.Order);

    }

  }
}
