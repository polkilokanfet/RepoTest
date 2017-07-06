using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class PaymentActualWrapper : WrapperBase<PaymentActual>
  {
    private PaymentActualWrapper() : base(new PaymentActual()) { }
    private PaymentActualWrapper(PaymentActual model) : base(model) { }



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

	public PaymentDocumentWrapper Document 
    {
        get { return GetComplexProperty<PaymentDocumentWrapper, PaymentDocument>(Model.Document); }
        set { SetComplexProperty<PaymentDocumentWrapper, PaymentDocument>(Document, value); }
    }

    public PaymentDocumentWrapper DocumentOriginalValue { get; private set; }
    public bool DocumentIsChanged => GetIsChanged(nameof(Document));


	public ProductComplexUnitWrapper ProductComplexUnit 
    {
        get { return GetComplexProperty<ProductComplexUnitWrapper, ProductComplexUnit>(Model.SalesUnit); }
        set { SetComplexProperty<ProductComplexUnitWrapper, ProductComplexUnit>(ProductComplexUnit, value); }
    }

    public ProductComplexUnitWrapper ProductComplexUnitOriginalValue { get; private set; }
    public bool ProductComplexUnitIsChanged => GetIsChanged(nameof(ProductComplexUnit));


	public CostWrapper Cost 
    {
        get { return GetComplexProperty<CostWrapper, Cost>(Model.Cost); }
        set { SetComplexProperty<CostWrapper, Cost>(Cost, value); }
    }

    public CostWrapper CostOriginalValue { get; private set; }
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    #endregion

    public override void InitializeComplexProperties()
    {

        Document = GetWrapper<PaymentDocumentWrapper, PaymentDocument>(Model.Document);

        ProductComplexUnit = GetWrapper<ProductComplexUnitWrapper, ProductComplexUnit>(Model.SalesUnit);

        Cost = GetWrapper<CostWrapper, Cost>(Model.Cost);

    }

  }
}
