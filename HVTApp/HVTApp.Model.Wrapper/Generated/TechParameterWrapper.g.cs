using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TechParameterWrapper : WrapperBase<TechParameter>
  {
    public TechParameterWrapper(TechParameter model) : base(model) { }
    public TechParameterWrapper(TechParameter model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
    public TechParametersGroupWrapper Group { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(TechParameter model)
    {
      if (model.Group == null) throw new ArgumentException("Group cannot be null");
      if (ExistsWrappers.ContainsKey(model.Group))
      {
          Group = (TechParametersGroupWrapper)ExistsWrappers[model.Group];
      }
      else
      {
          Group = new TechParametersGroupWrapper(model.Group, ExistsWrappers);
          RegisterComplexProperty(Group);
      }

    }
  }
}
