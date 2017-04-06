using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PlannedTermProductionWrapper : WrapperBase<PlannedTermProduction>
  {
    protected PlannedTermProductionWrapper(PlannedTermProduction model) : base(model) { }

	public static PlannedTermProductionWrapper GetWrapper()
	{
		return GetWrapper(new PlannedTermProduction());
	}

	public static PlannedTermProductionWrapper GetWrapper(PlannedTermProduction model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (PlannedTermProductionWrapper)Repository.ModelWrapperDictionary[model];

		return new PlannedTermProductionWrapper(model);
	}



    #region SimpleProperties

    public System.Int32 TermFrom
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 TermFromOriginalValue => GetOriginalValue<System.Int32>(nameof(TermFrom));
    public bool TermFromIsChanged => GetIsChanged(nameof(TermFrom));


    public System.Int32 TermTo
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 TermToOriginalValue => GetOriginalValue<System.Int32>(nameof(TermTo));
    public bool TermToIsChanged => GetIsChanged(nameof(TermTo));


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
