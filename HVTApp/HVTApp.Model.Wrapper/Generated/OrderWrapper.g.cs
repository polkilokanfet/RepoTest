using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class OrderWrapper : WrapperBase<Order>
  {
    public OrderWrapper(Order model) : base(model) { }
    public OrderWrapper(Order model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.String Number
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
    public bool NumberIsChanged => GetIsChanged(nameof(Number));

    public System.Nullable<System.DateTime> OpenOrderDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> OpenOrderDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(OpenOrderDate));
    public bool OpenOrderDateIsChanged => GetIsChanged(nameof(OpenOrderDate));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<ProductMainWrapper> Products { get; private set; }

    #endregion
  
    protected override void InitializeCollectionComplexProperties(Order model)
    {
      if (model.Products == null) throw new ArgumentException("Products cannot be null");
      Products = new ValidatableChangeTrackingCollection<ProductMainWrapper>(model.Products.Select(e => new ProductMainWrapper(e, ExistsWrappers)));
      RegisterCollection(Products, model.Products);

    }
  }
}
