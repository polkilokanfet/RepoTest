using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ActivityFieldWrapper : WrapperBase<ActivityField>
  {
    public ActivityFieldWrapper() : base(new ActivityField(), new Dictionary<IBaseEntity, object>()) { }
    public ActivityFieldWrapper(ActivityField model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public ActivityFieldWrapper(ActivityField model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


    #region SimpleProperties
    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public HVTApp.Model.POCOs.FieldOfActivity FieldOfActivity
    {
      get { return GetValue<HVTApp.Model.POCOs.FieldOfActivity>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.POCOs.FieldOfActivity FieldOfActivityOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.FieldOfActivity>(nameof(FieldOfActivity));
    public bool FieldOfActivityIsChanged => GetIsChanged(nameof(FieldOfActivity));

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
