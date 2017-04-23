using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentActualWrapper : WrapperBase<PaymentActual>
  {
    public PaymentActualWrapper() : base(new PaymentActual()) { }
    public PaymentActualWrapper(PaymentActual model) : base(model) { }

//	public static PaymentActualWrapper GetWrapper()
//	{
//		return GetWrapper(new PaymentActual());
//	}
//
//	public static PaymentActualWrapper GetWrapper(PaymentActual model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (PaymentActualWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new PaymentActualWrapper(model);
//	}


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
	private SalesUnitWrapper _fieldSalesUnit;
	public SalesUnitWrapper SalesUnit 
    {
        get { return _fieldSalesUnit; }
        set
        {
			SetComplexProperty<SalesUnitWrapper, SalesUnit>(_fieldSalesUnit, value);
			_fieldSalesUnit = value;
        }
    }
    public SalesUnitWrapper SalesUnitOriginalValue { get; private set; }
    public bool SalesUnitIsChanged => GetIsChanged(nameof(SalesUnit));

	private PaymentDocumentWrapper _fieldDocument;
	public PaymentDocumentWrapper Document 
    {
        get { return _fieldDocument; }
        set
        {
			SetComplexProperty<PaymentDocumentWrapper, PaymentDocument>(_fieldDocument, value);
			_fieldDocument = value;
        }
    }
    public PaymentDocumentWrapper DocumentOriginalValue { get; private set; }
    public bool DocumentIsChanged => GetIsChanged(nameof(Document));

	private SumAndVatWrapper _fieldSumAndVat;
	public SumAndVatWrapper SumAndVat 
    {
        get { return _fieldSumAndVat; }
        set
        {
			SetComplexProperty<SumAndVatWrapper, SumAndVat>(_fieldSumAndVat, value);
			_fieldSumAndVat = value;
        }
    }
    public SumAndVatWrapper SumAndVatOriginalValue { get; private set; }
    public bool SumAndVatIsChanged => GetIsChanged(nameof(SumAndVat));

    #endregion
    protected override void InitializeComplexProperties(PaymentActual model)
    {
        SalesUnit = GetWrapper<SalesUnitWrapper, SalesUnit>(model.SalesUnit);
        Document = GetWrapper<PaymentDocumentWrapper, PaymentDocument>(model.Document);
        SumAndVat = GetWrapper<SumAndVatWrapper, SumAndVat>(model.SumAndVat);
    }
  }
}
