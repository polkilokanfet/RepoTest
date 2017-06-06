using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class LocalityTypeWrapper : WrapperBase<LocalityType>
  {
    public LocalityTypeWrapper() : base(new LocalityType(), new Dictionary<IBaseEntity, object>()) { }
    public LocalityTypeWrapper(LocalityType model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public LocalityTypeWrapper(LocalityType model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }



    #region SimpleProperties

    public System.String FullName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
    public bool FullNameIsChanged => GetIsChanged(nameof(FullName));


    public System.String ShortName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
    public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));


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
