using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Wrapper
{
	public partial class OrderWrapper : WrapperBase<Order>
	{
	public OrderWrapper(Order model) : base(model) { }

	

    #region SimpleProperties

    public System.String Number
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
    public bool NumberIsChanged => GetIsChanged(nameof(Number));


    public System.DateTime OpenOrderDate
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime OpenOrderDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(OpenOrderDate));
    public bool OpenOrderDateIsChanged => GetIsChanged(nameof(OpenOrderDate));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ProductionUnitWrapper> ProductionUnits { get; private set; }


    #endregion

  
    protected override void InitializeCollectionProperties()
    {

      if (Model.ProductionUnits == null) throw new ArgumentException("ProductionUnits cannot be null");
      ProductionUnits = new ValidatableChangeTrackingCollection<ProductionUnitWrapper>(Model.ProductionUnits.Select(e => new ProductionUnitWrapper(e)));
      RegisterCollection(ProductionUnits, Model.ProductionUnits);


    }

	}
}
	