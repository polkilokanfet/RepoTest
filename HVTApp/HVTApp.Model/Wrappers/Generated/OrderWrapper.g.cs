using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class OrderWrapper : WrapperBase<Order>
  {
    private OrderWrapper() : base(new Order()) { }
    private OrderWrapper(Order model) : base(model) { }



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

    public IValidatableChangeTrackingCollection<ProductComplexUnitWrapper> ProductComplexUnits { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.ProductionUnits == null) throw new ArgumentException("SalesUnits cannot be null");
      ProductComplexUnits = new ValidatableChangeTrackingCollection<ProductComplexUnitWrapper>(Model.ProductionUnits.Select(e => GetWrapper<ProductComplexUnitWrapper, ProductComplexUnit>(e)));
      RegisterCollection(ProductComplexUnits, Model.ProductionUnits);


    }

  }
}
