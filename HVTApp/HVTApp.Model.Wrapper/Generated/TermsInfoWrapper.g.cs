using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TermsInfoWrapper : WrapperBase<TermsInfo>
  {
    protected TermsInfoWrapper(TermsInfo model) : base(model) { }
    //public TermsInfoWrapper(TermsInfo model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static TermsInfoWrapper GetWrapper(TermsInfo model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TermsInfoWrapper)Repository.ModelWrapperDictionary[model];

		return new TermsInfoWrapper(model);
	}



    #region SimpleProperties

    public System.Int32 TermProductionPlan
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 TermProductionPlanOriginalValue => GetOriginalValue<System.Int32>(nameof(TermProductionPlan));
    public bool TermProductionPlanIsChanged => GetIsChanged(nameof(TermProductionPlan));


    public System.Int32 TermFromCompleteToProductionPlan
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 TermFromCompleteToProductionPlanOriginalValue => GetOriginalValue<System.Int32>(nameof(TermFromCompleteToProductionPlan));
    public bool TermFromCompleteToProductionPlanIsChanged => GetIsChanged(nameof(TermFromCompleteToProductionPlan));


    public System.Int32 TermFromShipmentToDeliveryPlan
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 TermFromShipmentToDeliveryPlanOriginalValue => GetOriginalValue<System.Int32>(nameof(TermFromShipmentToDeliveryPlan));
    public bool TermFromShipmentToDeliveryPlanIsChanged => GetIsChanged(nameof(TermFromShipmentToDeliveryPlan));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion

  }
}
