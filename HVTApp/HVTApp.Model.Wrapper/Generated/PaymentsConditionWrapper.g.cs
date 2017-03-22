using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentsConditionWrapper : WrapperBase<PaymentsCondition>
  {
    public PaymentsConditionWrapper(PaymentsCondition model) : base(model) { }
    public PaymentsConditionWrapper(PaymentsCondition model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.Double PartInPercent
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double PartInPercentOriginalValue => GetOriginalValue<System.Double>(nameof(PartInPercent));
    public bool PartInPercentIsChanged => GetIsChanged(nameof(PartInPercent));

    public System.Int32 DaysToPoint
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 DaysToPointOriginalValue => GetOriginalValue<System.Int32>(nameof(DaysToPoint));
    public bool DaysToPointIsChanged => GetIsChanged(nameof(DaysToPoint));

    public HVTApp.Model.PaymentConditionPoint PaymentConditionPoint
    {
      get { return GetValue<HVTApp.Model.PaymentConditionPoint>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.PaymentConditionPoint PaymentConditionPointOriginalValue => GetOriginalValue<HVTApp.Model.PaymentConditionPoint>(nameof(PaymentConditionPoint));
    public bool PaymentConditionPointIsChanged => GetIsChanged(nameof(PaymentConditionPoint));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion
  }
}
