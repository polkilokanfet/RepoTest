using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class OrderWrapper : WrapperBase<Order>
  {
    public OrderWrapper() : base(new Order(), new Dictionary<IBaseEntity, object>()) { }
    public OrderWrapper(Order model) : base(model, new Dictionary<IBaseEntity, object>()) { }
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
    public IValidatableChangeTrackingCollection<ProductionsUnitWrapper> ProductionsUnits { get; private set; }

    #endregion
  
    protected override void InitializeCollectionComplexProperties(Order model)
    {
      if (model.ProductionsUnits == null) throw new ArgumentException("ProductionsUnits cannot be null");
      ProductionsUnits = new ValidatableChangeTrackingCollection<ProductionsUnitWrapper>(model.ProductionsUnits.Select(e => GetWrapper<ProductionsUnitWrapper, ProductionsUnit>(e)));
      RegisterCollection(ProductionsUnits, model.ProductionsUnits);

    }
  }
}
