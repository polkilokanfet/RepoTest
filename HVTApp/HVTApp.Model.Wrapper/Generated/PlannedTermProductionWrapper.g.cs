using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PlannedTermProductionWrapper : WrapperBase<PlannedTermProduction>
  {
    public PlannedTermProductionWrapper(PlannedTermProduction model) : base(model) { }
    public PlannedTermProductionWrapper(PlannedTermProduction model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.Int32 TermFrom
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 TermFromOriginalValue => GetOriginalValue<System.Int32>(nameof(TermFrom));
    public bool TermFromIsChanged => GetIsChanged(nameof(TermFrom));

    public System.Int32 TermTo
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 TermToOriginalValue => GetOriginalValue<System.Int32>(nameof(TermTo));
    public bool TermToIsChanged => GetIsChanged(nameof(TermTo));

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
