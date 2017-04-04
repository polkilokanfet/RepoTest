using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentActualWrapper : WrapperBase<PaymentActual>
  {
    protected PaymentActualWrapper(PaymentActual model) : base(model) { }

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

	public PaymentDocumentWrapper PaymentDocument 
    {
        get { return PaymentDocumentWrapper.GetWrapper(Model.PaymentDocument); }
        set
        {
            UnRegisterComplexProperty(PaymentDocument);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public PaymentsInfoWrapper PaymentsInfo 
    {
        get { return PaymentsInfoWrapper.GetWrapper(Model.PaymentsInfo); }
        set
        {
            UnRegisterComplexProperty(PaymentsInfo);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
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
