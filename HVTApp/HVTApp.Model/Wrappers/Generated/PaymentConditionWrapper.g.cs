using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class PaymentConditionWrapper : WrapperBase<PaymentCondition>
  {
    public PaymentConditionWrapper(PaymentCondition model) : base(model) { }



    #region SimpleProperties

    public System.Double Part
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double PartOriginalValue => GetOriginalValue<System.Double>(nameof(Part));
    public bool PartIsChanged => GetIsChanged(nameof(Part));


    public System.Int32 DaysToPoint
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 DaysToPointOriginalValue => GetOriginalValue<System.Int32>(nameof(DaysToPoint));
    public bool DaysToPointIsChanged => GetIsChanged(nameof(DaysToPoint));


    public HVTApp.Model.POCOs.PaymentConditionPoint PaymentConditionPoint
    {
      get { return GetValue<HVTApp.Model.POCOs.PaymentConditionPoint>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.POCOs.PaymentConditionPoint PaymentConditionPointOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.PaymentConditionPoint>(nameof(PaymentConditionPoint));
    public bool PaymentConditionPointIsChanged => GetIsChanged(nameof(PaymentConditionPoint));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion

  }
}
