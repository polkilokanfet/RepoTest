using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class PaymentConditionWrapper : WrapperBase<PaymentCondition>
  {
    public PaymentConditionWrapper() : base(new PaymentCondition()) { }
    public PaymentConditionWrapper(PaymentCondition model) : base(model) { }
    public PaymentConditionWrapper(PaymentCondition model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public PaymentConditionWrapper(PaymentCondition model, IDictionary<IBaseEntity, object> dictionary) : base(model, new ExistsWrappers(dictionary)) { }



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
