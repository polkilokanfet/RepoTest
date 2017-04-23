using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class SumOnDateWrapper : WrapperBase<SumOnDate>
  {
    public SumOnDateWrapper() : base(new SumOnDate()) { }
    public SumOnDateWrapper(SumOnDate model) : base(model) { }

//	public static SumOnDateWrapper GetWrapper()
//	{
//		return GetWrapper(new SumOnDate());
//	}
//
//	public static SumOnDateWrapper GetWrapper(SumOnDate model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ExistsWrappers.ContainsKey(model))
//			return (SumOnDateWrapper)Repository.ExistsWrappers[model];
//
//		return new SumOnDateWrapper(model);
//	}


    #region SimpleProperties
    public System.DateTime Date
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
    public bool DateIsChanged => GetIsChanged(nameof(Date));

    public System.Double Sum
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
    public bool SumIsChanged => GetIsChanged(nameof(Sum));

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
