using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class PaymentPlanWrapper : WrapperBase<PaymentPlan>
  {
    public PaymentPlanWrapper() : base(new PaymentPlan(), new Dictionary<IBaseEntity, object>()) { }
    public PaymentPlanWrapper(PaymentPlan model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public PaymentPlanWrapper(PaymentPlan model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }



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


	public SumAndVatWrapper SumAndVat 
    {
        get { return GetComplexProperty<SumAndVatWrapper, SumAndVat>(Model.SumAndVat); }
        set { SetComplexProperty<SumAndVatWrapper, SumAndVat>(SumAndVat, value); }
    }

    public SumAndVatWrapper SumAndVatOriginalValue { get; private set; }
    public bool SumAndVatIsChanged => GetIsChanged(nameof(SumAndVat));


    #endregion

    protected override void InitializeComplexProperties(PaymentPlan model)
    {

        SalesUnit = GetWrapper<SalesUnitWrapper, SalesUnit>(model.SalesUnit);

        SumAndVat = GetWrapper<SumAndVatWrapper, SumAndVat>(model.SumAndVat);

    }

  }
}
