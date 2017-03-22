using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentActualWrapper : WrapperBase<PaymentActual>
  {
    public PaymentActualWrapper(PaymentActual model) : base(model) { }
    public PaymentActualWrapper(PaymentActual model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


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
		get { return GetComplexProperty<PaymentDocument, PaymentDocumentWrapper>(nameof(PaymentDocument)); }
		set { SetComplexProperty<PaymentDocument, PaymentDocumentWrapper>(value, nameof(PaymentDocument)); }
	}


	public PaymentsInfoWrapper PaymentsInfo
	{
		get { return GetComplexProperty<PaymentsInfo, PaymentsInfoWrapper>(nameof(PaymentsInfo)); }
		set { SetComplexProperty<PaymentsInfo, PaymentsInfoWrapper>(value, nameof(PaymentsInfo)); }
	}


    #endregion

    protected override void InitializeComplexProperties(PaymentActual model)
    {

        PaymentDocument = GetWrapper<PaymentDocument, PaymentDocumentWrapper>(model.PaymentDocument);

        PaymentsInfo = GetWrapper<PaymentsInfo, PaymentsInfoWrapper>(model.PaymentsInfo);

    }

  }
}
