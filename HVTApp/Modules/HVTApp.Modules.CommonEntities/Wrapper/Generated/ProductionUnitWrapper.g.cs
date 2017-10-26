using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class ProductionUnitWrapper : WrapperBase<ProductionUnit>
	{
	public ProductionUnitWrapper(ProductionUnit model) : base(model) { }

	

    #region SimpleProperties

    public System.Guid OrderId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid OrderIdOriginalValue => GetOriginalValue<System.Guid>(nameof(OrderId));
    public bool OrderIdIsChanged => GetIsChanged(nameof(OrderId));


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


    public System.Int32 PlannedTermFromStartToEndProduction
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 PlannedTermFromStartToEndProductionOriginalValue => GetOriginalValue<System.Int32>(nameof(PlannedTermFromStartToEndProduction));
    public bool PlannedTermFromStartToEndProductionIsChanged => GetIsChanged(nameof(PlannedTermFromStartToEndProduction));


    public System.Int32 PlannedTermFromPickToEndProduction
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 PlannedTermFromPickToEndProductionOriginalValue => GetOriginalValue<System.Int32>(nameof(PlannedTermFromPickToEndProduction));
    public bool PlannedTermFromPickToEndProductionIsChanged => GetIsChanged(nameof(PlannedTermFromPickToEndProduction));


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


    public System.Nullable<System.DateTime> EndProductionDateByPlan
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> EndProductionDateByPlanOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(EndProductionDateByPlan));
    public bool EndProductionDateByPlanIsChanged => GetIsChanged(nameof(EndProductionDateByPlan));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private ProductWrapper _fieldProduct;
	public ProductWrapper Product 
    {
        get { return _fieldProduct ; }
        set
        {
            SetComplexValue<Product, ProductWrapper>(_fieldProduct, value);
            _fieldProduct  = value;
        }
    }

    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.Product != null)
        {
            _fieldProduct = new ProductWrapper(Model.Product);
            RegisterComplex(Product);
        }

    }

	}
}
	