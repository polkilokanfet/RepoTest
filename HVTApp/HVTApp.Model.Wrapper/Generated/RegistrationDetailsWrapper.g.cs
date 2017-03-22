using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class RegistrationDetailsWrapper : WrapperBase<RegistrationDetails>
  {
    public RegistrationDetailsWrapper(RegistrationDetails model) : base(model) { }
    public RegistrationDetailsWrapper(RegistrationDetails model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.String RegistrationNumber
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String RegistrationNumberOriginalValue => GetOriginalValue<System.String>(nameof(RegistrationNumber));
    public bool RegistrationNumberIsChanged => GetIsChanged(nameof(RegistrationNumber));

    public System.DateTime RegistrationDate
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime RegistrationDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(RegistrationDate));
    public bool RegistrationDateIsChanged => GetIsChanged(nameof(RegistrationDate));

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
