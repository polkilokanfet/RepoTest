using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class CostInfoWrapper : WrapperBase<CostInfo>
  {
    public CostInfoWrapper(CostInfo model) : base(model) { }
    public CostInfoWrapper(CostInfo model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.Double Cost
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));

    public System.Double CostPrice
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double CostPriceOriginalValue => GetOriginalValue<System.Double>(nameof(CostPrice));
    public bool CostPriceIsChanged => GetIsChanged(nameof(CostPrice));

    public System.Double Vat
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double VatOriginalValue => GetOriginalValue<System.Double>(nameof(Vat));
    public bool VatIsChanged => GetIsChanged(nameof(Vat));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region GetProperties
    public System.Double CostWithVat => GetValue<System.Double>(); 

    public System.Double MarginalIncome => GetValue<System.Double>(); 

    public System.Double MarginalIncomePercent => GetValue<System.Double>(); 

    #endregion
  }
}
