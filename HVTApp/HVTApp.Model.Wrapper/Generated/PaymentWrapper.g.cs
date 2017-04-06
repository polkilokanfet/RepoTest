using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentWrapper : WrapperBase<Payment>
  {
    protected PaymentWrapper(Payment model) : base(model) { }

	public static PaymentWrapper GetWrapper()
	{
		return GetWrapper(new Payment());
	}

	public static PaymentWrapper GetWrapper(Payment model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (PaymentWrapper)Repository.ModelWrapperDictionary[model];

		return new PaymentWrapper(model);
	}



    #region SimpleProperties

    public System.DateTime Date
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
    public bool DateIsChanged => GetIsChanged(nameof(Date));


    public System.Double Summ
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double SummOriginalValue => GetOriginalValue<System.Double>(nameof(Summ));
    public bool SummIsChanged => GetIsChanged(nameof(Summ));


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
