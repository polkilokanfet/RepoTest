using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ActivityFieldWrapper : WrapperBase<ActivityField>
  {
    private ActivityFieldWrapper() : base(new ActivityField()) { }
    private ActivityFieldWrapper(ActivityField model) : base(model) { }



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public HVTApp.Model.POCOs.ActivityFieldEnum ActivityFieldEnum
    {
      get { return GetValue<HVTApp.Model.POCOs.ActivityFieldEnum>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.POCOs.ActivityFieldEnum ActivityFieldEnumOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.ActivityFieldEnum>(nameof(ActivityFieldEnum));
    public bool ActivityFieldEnumIsChanged => GetIsChanged(nameof(ActivityFieldEnum));


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
