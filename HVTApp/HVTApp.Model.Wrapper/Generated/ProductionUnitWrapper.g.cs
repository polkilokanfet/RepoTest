using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductionUnitWrapper : WrapperBase<ProductionUnit>
  {
    public ProductionUnitWrapper() : base(new ProductionUnit()) { }
    public ProductionUnitWrapper(ProductionUnit model) : base(model) { }

//	public static ProductionUnitWrapper GetWrapper()
//	{
//		return GetWrapper(new ProductionUnit());
//	}
//
//	public static ProductionUnitWrapper GetWrapper(ProductionUnit model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ExistsWrappers.ContainsKey(model))
//			return (ProductionUnitWrapper)Repository.ExistsWrappers[model];
//
//		return new ProductionUnitWrapper(model);
//	}


    #region SimpleProperties
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

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	private ProductWrapper _fieldProduct;
	public ProductWrapper Product 
    {
        get { return _fieldProduct; }
        set
        {
			SetComplexProperty<ProductWrapper, Product>(_fieldProduct, value);
			_fieldProduct = value;
        }
    }
    public ProductWrapper ProductOriginalValue { get; private set; }
    public bool ProductIsChanged => GetIsChanged(nameof(Product));

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

	private OrderWrapper _fieldOrder;
	public OrderWrapper Order 
    {
        get { return _fieldOrder; }
        set
        {
			SetComplexProperty<OrderWrapper, Order>(_fieldOrder, value);
			_fieldOrder = value;
        }
    }
    public OrderWrapper OrderOriginalValue { get; private set; }
    public bool OrderIsChanged => GetIsChanged(nameof(Order));

    #endregion
    protected override void InitializeComplexProperties(ProductionUnit model)
    {
        Product = GetWrapper<ProductWrapper, Product>(model.Product);
        SalesUnit = GetWrapper<SalesUnitWrapper, SalesUnit>(model.SalesUnit);
        Order = GetWrapper<OrderWrapper, Order>(model.Order);
    }
  }
}
