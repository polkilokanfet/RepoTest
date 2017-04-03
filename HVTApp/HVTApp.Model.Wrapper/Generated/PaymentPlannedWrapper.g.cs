using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentPlannedWrapper : WrapperBase<PaymentPlanned>
  {
    protected PaymentPlannedWrapper(PaymentPlanned model) : base(model) { }

	public static PaymentPlannedWrapper GetWrapper(PaymentPlanned model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (PaymentPlannedWrapper)Repository.ModelWrapperDictionary[model];

		return new PaymentPlannedWrapper(model);
	}



    #region SimpleProperties

    public System.Nullable<System.DateTime> ExpectedPaymentDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> ExpectedPaymentDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(ExpectedPaymentDate));
    public bool ExpectedPaymentDateIsChanged => GetIsChanged(nameof(ExpectedPaymentDate));


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

	private PaymentsConditionWrapper _fieldPaymentsCondition;
	public PaymentsConditionWrapper PaymentsCondition 
    {
        get { return _fieldPaymentsCondition; }
        set
        {
            if (Equals(_fieldPaymentsCondition, value))
                return;

            UnRegisterComplexProperty(_fieldPaymentsCondition);

            _fieldPaymentsCondition = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


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


    #region GetProperties

    public System.DateTime Date => GetValue<System.DateTime>(); 


    public System.DateTime CalculatedPaymentDate => GetValue<System.DateTime>(); 


    #endregion

    protected override void InitializeComplexProperties(PaymentPlanned model)
    {

        PaymentsCondition = PaymentsConditionWrapper.GetWrapper(model.PaymentsCondition);

        PaymentsInfo = PaymentsInfoWrapper.GetWrapper(model.PaymentsInfo);

    }

  }
}
