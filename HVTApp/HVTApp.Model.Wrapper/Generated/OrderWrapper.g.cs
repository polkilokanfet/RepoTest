using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class OrderWrapper : WrapperBase<Order>
  {
    public OrderWrapper() : base(new Order(), new Dictionary<IBaseEntity, object>()) { }
    public OrderWrapper(Order model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public OrderWrapper(Order model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public OrderWrapper(Order model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<ProductionUnitWrapper> ProductionProductUnits { get; private set; }

    #endregion
  
    protected override void InitializeCollectionComplexProperties(Order model)
    {
      if (model.ProductionProductUnits == null) throw new ArgumentException("ProductionProductUnits cannot be null");
      ProductionProductUnits = new ValidatableChangeTrackingCollection<ProductionUnitWrapper>(model.ProductionProductUnits.Select(e => GetWrapper<ProductionUnitWrapper, ProductionUnit>(e)));
      RegisterCollection(ProductionProductUnits, model.ProductionProductUnits);

    }
  }
}
