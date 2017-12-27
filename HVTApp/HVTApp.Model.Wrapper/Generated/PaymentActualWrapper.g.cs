using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentActualWrapper : WrapperBase<PaymentActual>
  {
    public PaymentActualWrapper() : base(new PaymentActual(), new Dictionary<IBaseEntity, object>()) { }
    public PaymentActualWrapper(PaymentActual model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public PaymentActualWrapper(PaymentActual model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public PaymentActualWrapper(PaymentActual model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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
        get { return GetComplexProperty<SalesUnitWrapper, SalesUnit>(Model.SalesUnit); }
        set { SetComplexProperty<SalesUnitWrapper, SalesUnit>(SalesUnit, value); }
    }

    public SalesUnitWrapper SalesUnitOriginalValue { get; private set; }
    public bool SalesUnitIsChanged => GetIsChanged(nameof(SalesUnit));

	public PaymentDocumentWrapper Document 
    {
        get { return GetComplexProperty<PaymentDocumentWrapper, PaymentDocument>(Model.Document); }
        set { SetComplexProperty<PaymentDocumentWrapper, PaymentDocument>(Document, value); }
    }

    public PaymentDocumentWrapper DocumentOriginalValue { get; private set; }
    public bool DocumentIsChanged => GetIsChanged(nameof(Document));

	public SumAndVatWrapper SumAndVat 
    {
        get { return GetComplexProperty<SumAndVatWrapper, SumAndVat>(Model.SumAndVat); }
        set { SetComplexProperty<SumAndVatWrapper, SumAndVat>(SumAndVat, value); }
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
