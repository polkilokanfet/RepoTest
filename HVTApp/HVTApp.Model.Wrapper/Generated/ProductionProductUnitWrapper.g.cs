using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductionProductUnitWrapper : WrapperBase<ProductionProductUnit>
  {
    protected ProductionProductUnitWrapper(ProductionProductUnit model) : base(model) { }

	public static ProductionProductUnitWrapper GetWrapper()
	{
		return GetWrapper(new ProductionProductUnit());
	}

	public static ProductionProductUnitWrapper GetWrapper(ProductionProductUnit model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProductionProductUnitWrapper)Repository.ModelWrapperDictionary[model];

		return new ProductionProductUnitWrapper(model);
	}



    #region SimpleProperties

    public System.Nullable<System.DateTime> StartProductionDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> StartProductionDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(StartProductionDate));
    public bool StartProductionDateIsChanged => GetIsChanged(nameof(StartProductionDate));


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

    protected override void InitializeComplexProperties(ProductionProductUnit model)
    {

        Product = ProductWrapper.GetWrapper(model.Product);

        Order = OrderWrapper.GetWrapper(model.Order);

    }

  }
}
