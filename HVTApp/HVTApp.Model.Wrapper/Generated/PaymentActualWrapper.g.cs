using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentActualWrapper : WrapperBase<PaymentActual>
  {
    protected PaymentActualWrapper(PaymentActual model) : base(model) { }
    //public PaymentActualWrapper(PaymentActual model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static PaymentActualWrapper GetWrapper(PaymentActual model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (PaymentActualWrapper)Repository.ModelWrapperDictionary[model];

		return new PaymentActualWrapper(model);
	}



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

	private PaymentDocumentWrapper _fieldPaymentDocument;
	public PaymentDocumentWrapper PaymentDocument 
    {
        get { return _fieldPaymentDocument; }
        set
        {
            if (Equals(_fieldPaymentDocument, value))
                return;

            UnRegisterComplexProperty(_fieldPaymentDocument);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldPaymentDocument = value;
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

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldPaymentsInfo = value;
        }
    }


    #endregion

    protected override void InitializeComplexProperties(PaymentActual model)
    {

        PaymentDocument = PaymentDocumentWrapper.GetWrapper(model.PaymentDocument);

        PaymentsInfo = PaymentsInfoWrapper.GetWrapper(model.PaymentsInfo);

    }

  }
}
