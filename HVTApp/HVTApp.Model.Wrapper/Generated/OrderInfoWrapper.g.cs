using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OrderInfoWrapper : WrapperBase<OrderInfo>
  {
    protected OrderInfoWrapper(OrderInfo model) : base(model) { }

	public static OrderInfoWrapper GetWrapper(OrderInfo model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (OrderInfoWrapper)Repository.ModelWrapperDictionary[model];

		return new OrderInfoWrapper(model);
	}



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
        get { return ProductBaseWrapper.GetWrapper(Model.Product); }
        set
        {
            UnRegisterComplexProperty(Product);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public OrderWrapper Order 
    {
        get { return OrderWrapper.GetWrapper(Model.Order); }
        set
        {
            UnRegisterComplexProperty(Order);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion

    protected override void InitializeComplexProperties(OrderInfo model)
    {

        Product = ProductBaseWrapper.GetWrapper(model.Product);

        Order = OrderWrapper.GetWrapper(model.Order);

    }

  }
}
