using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class SumAndVatWrapper : WrapperBase<SumAndVat>
  {
    protected SumAndVatWrapper(SumAndVat model) : base(model) { }

	public static SumAndVatWrapper GetWrapper()
	{
		return GetWrapper(new SumAndVat());
	}

	public static SumAndVatWrapper GetWrapper(SumAndVat model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (SumAndVatWrapper)Repository.ModelWrapperDictionary[model];

		return new SumAndVatWrapper(model);
	}



    #region SimpleProperties

    public System.Double Sum
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
    public bool SumIsChanged => GetIsChanged(nameof(Sum));


    public System.Double Vat
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double VatOriginalValue => GetOriginalValue<System.Double>(nameof(Vat));
    public bool VatIsChanged => GetIsChanged(nameof(Vat));


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
