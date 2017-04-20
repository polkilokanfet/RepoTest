using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentActualWrapper : WrapperBase<PaymentActual>
  {
    protected PaymentActualWrapper(PaymentActual model) : base(model) { }

	public static PaymentActualWrapper GetWrapper()
	{
		return GetWrapper(new PaymentActual());
	}

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

	public SalesUnitWrapper SalesUnit 
    {
        get { return SalesUnitWrapper.GetWrapper(Model.SalesUnit); }
        set
        {
			var oldPropVal = SalesUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SalesUnitWrapper SalesUnitOriginalValue => SalesUnitWrapper.GetWrapper(GetOriginalValue<SalesUnit>(nameof(SalesUnit)));
    public bool SalesUnitIsChanged => GetIsChanged(nameof(SalesUnit));


	public PaymentDocumentWrapper Document 
    {
        get { return PaymentDocumentWrapper.GetWrapper(Model.Document); }
        set
        {
			var oldPropVal = Document;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public PaymentDocumentWrapper DocumentOriginalValue => PaymentDocumentWrapper.GetWrapper(GetOriginalValue<PaymentDocument>(nameof(Document)));
    public bool DocumentIsChanged => GetIsChanged(nameof(Document));


	public SumAndVatWrapper SumAndVat 
    {
        get { return SumAndVatWrapper.GetWrapper(Model.SumAndVat); }
        set
        {
			var oldPropVal = SumAndVat;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SumAndVatWrapper SumAndVatOriginalValue => SumAndVatWrapper.GetWrapper(GetOriginalValue<SumAndVat>(nameof(SumAndVat)));
    public bool SumAndVatIsChanged => GetIsChanged(nameof(SumAndVat));


    #endregion

    protected override void InitializeComplexProperties(PaymentActual model)
    {

        SalesUnit = SalesUnitWrapper.GetWrapper(model.SalesUnit);

        Document = PaymentDocumentWrapper.GetWrapper(model.Document);

        SumAndVat = SumAndVatWrapper.GetWrapper(model.SumAndVat);

    }

  }
}
