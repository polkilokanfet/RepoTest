using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentBaseWrapper : WrapperBase<PaymentBase>
  {
    protected PaymentBaseWrapper(PaymentBase model) : base(model) { }

	public static PaymentBaseWrapper GetWrapper(PaymentBase model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (PaymentBaseWrapper)Repository.ModelWrapperDictionary[model];

		return new PaymentBaseWrapper(model);
	}



    #region SimpleProperties

    public System.Double Sum
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
    public bool SumIsChanged => GetIsChanged(nameof(Sum));


    public System.String Comment
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
    public bool CommentIsChanged => GetIsChanged(nameof(Comment));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private PaymentsInfoWrapper _fieldPaymentsInfo;
	public PaymentsInfoWrapper PaymentsInfo 
    {
        get { return _fieldPaymentsInfo; }
        set
        {
            if (Equals(_fieldPaymentsInfo, value))
                return;

            UnRegisterComplexProperty(_fieldPaymentsInfo);

            _fieldPaymentsInfo = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion

    protected override void InitializeComplexProperties(PaymentBase model)
    {

        PaymentsInfo = PaymentsInfoWrapper.GetWrapper(model.PaymentsInfo);

    }

  }
}
