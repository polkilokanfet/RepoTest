using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class DateInfoWrapper : WrapperBase<DateInfo>
  {
    protected DateInfoWrapper(DateInfo model) : base(model) { }

	public static DateInfoWrapper GetWrapper(DateInfo model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (DateInfoWrapper)Repository.ModelWrapperDictionary[model];

		return new DateInfoWrapper(model);
	}



    #region SimpleProperties

    public System.Nullable<System.DateTime> DateDesiredDelivery
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> DateDesiredDeliveryOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateDesiredDelivery));
    public bool DateDesiredDeliveryIsChanged => GetIsChanged(nameof(DateDesiredDelivery));


    public System.Nullable<System.DateTime> DateRealizationPlan
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> DateRealizationPlanOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateRealizationPlan));
    public bool DateRealizationPlanIsChanged => GetIsChanged(nameof(DateRealizationPlan));


    public System.Nullable<System.DateTime> DateShipmentPlan
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> DateShipmentPlanOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateShipmentPlan));
    public bool DateShipmentPlanIsChanged => GetIsChanged(nameof(DateShipmentPlan));


    public System.Nullable<System.DateTime> DateProductionPlacing
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> DateProductionPlacingOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateProductionPlacing));
    public bool DateProductionPlacingIsChanged => GetIsChanged(nameof(DateProductionPlacing));


    public System.Nullable<System.DateTime> DateComplete
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> DateCompleteOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateComplete));
    public bool DateCompleteIsChanged => GetIsChanged(nameof(DateComplete));


    public System.Nullable<System.DateTime> DateEndProduction
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> DateEndProductionOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateEndProduction));
    public bool DateEndProductionIsChanged => GetIsChanged(nameof(DateEndProduction));


    public System.Nullable<System.DateTime> DateRealization
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> DateRealizationOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateRealization));
    public bool DateRealizationIsChanged => GetIsChanged(nameof(DateRealization));


    public System.Nullable<System.DateTime> DateShipment
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> DateShipmentOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateShipment));
    public bool DateShipmentIsChanged => GetIsChanged(nameof(DateShipment));


    public System.Nullable<System.DateTime> DateDelivery
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> DateDeliveryOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(DateDelivery));
    public bool DateDeliveryIsChanged => GetIsChanged(nameof(DateDelivery));


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
			var oldPropVal = Product;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProductBaseWrapper ProductOriginalValue => ProductBaseWrapper.GetWrapper(GetOriginalValue<ProductBase>(nameof(Product)));
    public bool ProductIsChanged => GetIsChanged(nameof(Product));


    #endregion


    #region GetProperties

    public System.DateTime DateOrderInTakeCalculated => GetValue<System.DateTime>(); 


    public System.Nullable<System.DateTime> DateExecutionConditionsToStartProductionCalculatedByActual => GetValue<System.Nullable<System.DateTime>>(); 


    public System.Nullable<System.DateTime> DateExecutionConditionsToStartProductionCalculatedByPlan => GetValue<System.Nullable<System.DateTime>>(); 


    public System.Nullable<System.DateTime> DateExecutionConditionsToShipmentCalculatedByActual => GetValue<System.Nullable<System.DateTime>>(); 


    public System.Nullable<System.DateTime> DateExecutionConditionsToShipmentCalculatedByPlan => GetValue<System.Nullable<System.DateTime>>(); 


    public System.DateTime DateEndProductionCalculated => GetValue<System.DateTime>(); 


    public System.DateTime DateRealizationCalculated => GetValue<System.DateTime>(); 


    public System.DateTime DateShipmentCalculated => GetValue<System.DateTime>(); 


    public System.DateTime DateDeliveryCalculated => GetValue<System.DateTime>(); 


    #endregion

    protected override void InitializeComplexProperties(DateInfo model)
    {

        Product = ProductBaseWrapper.GetWrapper(model.Product);

    }

  }
}
