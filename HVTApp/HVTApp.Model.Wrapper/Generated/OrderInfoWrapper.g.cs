using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OrderInfoWrapper : WrapperBase<OrderInfo>
  {
    public OrderInfoWrapper(OrderInfo model) : base(model) { }
    public OrderInfoWrapper(OrderInfo model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


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


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ProductBaseWrapper Product
	{
		get { return GetComplexProperty<ProductBase, ProductBaseWrapper>(nameof(Product)); }
		set { SetComplexProperty<ProductBase, ProductBaseWrapper>(value, this.Product, nameof(Product)); }
	}


	public OrderWrapper Order
	{
		get { return GetComplexProperty<Order, OrderWrapper>(nameof(Order)); }
		set { SetComplexProperty<Order, OrderWrapper>(value, this.Order, nameof(Order)); }
	}


    #endregion

    
    protected override void InitializeComplexProperties(OrderInfo model)
    {

		if (model.Product != null)
		{
			if (ExistsWrappers.ContainsKey(model.Product))
			{
				Product = (ProductBaseWrapper)ExistsWrappers[model.Product];
			}
			else
			{
				Product = new ProductBaseWrapper(model.Product, ExistsWrappers);
				//ExistsWrappers.Add(model.Product, new ProductBaseWrapper(model.Product, ExistsWrappers));
				RegisterComplexProperty(Product);
			}
		}


		if (model.Order != null)
		{
			if (ExistsWrappers.ContainsKey(model.Order))
			{
				Order = (OrderWrapper)ExistsWrappers[model.Order];
			}
			else
			{
				Order = new OrderWrapper(model.Order, ExistsWrappers);
				//ExistsWrappers.Add(model.Order, new OrderWrapper(model.Order, ExistsWrappers));
				RegisterComplexProperty(Order);
			}
		}


    }

  }
}
