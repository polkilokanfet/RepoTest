using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class PaymentActualWrapper : WrapperBase<PaymentActual>
  {
    public PaymentActualWrapper(PaymentActual model) : base(model) { }



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


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private SalesUnitWrapper _fieldSalesUnit;
	public SalesUnitWrapper SalesUnit 
    {
        get { return _fieldSalesUnit ; }
        set
        {
            SetComplexValue<SalesUnit, SalesUnitWrapper>(_fieldSalesUnit, value);
            _fieldSalesUnit  = value;
        }
    }

	private PaymentDocumentWrapper _fieldDocument;
	public PaymentDocumentWrapper Document 
    {
        get { return _fieldDocument ; }
        set
        {
            SetComplexValue<PaymentDocument, PaymentDocumentWrapper>(_fieldDocument, value);
            _fieldDocument  = value;
        }
    }

    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.SalesUnit != null)
        {
            _fieldSalesUnit = new SalesUnitWrapper(Model.SalesUnit);
            RegisterComplex(SalesUnit);
        }

		if (Model.Document != null)
        {
            _fieldDocument = new PaymentDocumentWrapper(Model.Document);
            RegisterComplex(Document);
        }

    }

  }
}
