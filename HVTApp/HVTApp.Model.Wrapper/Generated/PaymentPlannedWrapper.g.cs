using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentPlannedWrapper : WrapperBase<PaymentPlanned>
  {
    public PaymentPlannedWrapper(PaymentPlanned model) : base(model) { }
    public PaymentPlannedWrapper(PaymentPlanned model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
    public PaymentsConditionWrapper PaymentsCondition { get; private set; }

    public PaymentsInfoWrapper PaymentsInfo { get; private set; }

    #endregion

    #region GetProperties
    public System.DateTime Date => GetValue<System.DateTime>(); 

    public System.DateTime CalculatedPaymentDate => GetValue<System.DateTime>(); 

    #endregion
    
    protected override void InitializeComplexProperties(PaymentPlanned model)
    {
      if (model.PaymentsCondition == null) throw new ArgumentException("PaymentsCondition cannot be null");
      if (ExistsWrappers.ContainsKey(model.PaymentsCondition))
      {
          PaymentsCondition = (PaymentsConditionWrapper)ExistsWrappers[model.PaymentsCondition];
      }
      else
      {
          PaymentsCondition = new PaymentsConditionWrapper(model.PaymentsCondition, ExistsWrappers);
          RegisterComplexProperty(PaymentsCondition);
      }

      if (model.PaymentsInfo == null) throw new ArgumentException("PaymentsInfo cannot be null");
      if (ExistsWrappers.ContainsKey(model.PaymentsInfo))
      {
          PaymentsInfo = (PaymentsInfoWrapper)ExistsWrappers[model.PaymentsInfo];
      }
      else
      {
          PaymentsInfo = new PaymentsInfoWrapper(model.PaymentsInfo, ExistsWrappers);
          RegisterComplexProperty(PaymentsInfo);
      }

    }
  }
}
