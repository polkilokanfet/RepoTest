using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TechLinksCollectionWrapper : WrapperBase<TechLinksCollection>
  {
    public TechLinksCollectionWrapper(TechLinksCollection model) : base(model) { }
    public TechLinksCollectionWrapper(TechLinksCollection model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region GetProperties
    public System.Int32 Count => GetValue<System.Int32>(); 

    public System.Boolean IsReadOnly => GetValue<System.Boolean>(); 

    #endregion
  }
}
