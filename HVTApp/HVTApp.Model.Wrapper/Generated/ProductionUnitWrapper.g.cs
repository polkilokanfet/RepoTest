using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductionUnitWrapper : WrapperBase<ProductionUnit>
  {
    protected ProductionUnitWrapper(ProductionUnit model) : base(model) { }

	public static ProductionUnitWrapper GetWrapper()
	{
		return GetWrapper(new ProductionUnit());
	}

	public static ProductionUnitWrapper GetWrapper(ProductionUnit model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProductionUnitWrapper)Repository.ModelWrapperDictionary[model];

		return new ProductionUnitWrapper(model);
	}



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

	public ProductWrapper Product 
    {
        get { return ProductWrapper.GetWrapper(Model.Product); }
        set
        {
			var oldPropVal = Product;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProductWrapper ProductOriginalValue => ProductWrapper.GetWrapper(GetOriginalValue<Product>(nameof(Product)));
    public bool ProductIsChanged => GetIsChanged(nameof(Product));


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


	public OrderWrapper Order 
    {
        get { return OrderWrapper.GetWrapper(Model.Order); }
        set
        {
			var oldPropVal = Order;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public OrderWrapper OrderOriginalValue => OrderWrapper.GetWrapper(GetOriginalValue<Order>(nameof(Order)));
    public bool OrderIsChanged => GetIsChanged(nameof(Order));


    #endregion

    protected override void InitializeComplexProperties(ProductionUnit model)
    {

        Product = ProductWrapper.GetWrapper(model.Product);

        SalesUnit = SalesUnitWrapper.GetWrapper(model.SalesUnit);

        Order = OrderWrapper.GetWrapper(model.Order);

    }

  }
}
