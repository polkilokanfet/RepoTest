using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class DistrictWrapper : WrapperBase<District>
  {
    public DistrictWrapper(District model) : base(model) { }
    public DistrictWrapper(District model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
    public CountryWrapper Country { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(District model)
    {
      if (model.Country == null) throw new ArgumentException("Country cannot be null");
      if (ExistsWrappers.ContainsKey(model.Country))
      {
          Country = (CountryWrapper)ExistsWrappers[model.Country];
      }
      else
      {
          Country = new CountryWrapper(model.Country, ExistsWrappers);
          RegisterComplexProperty(Country);
      }

    }
  }
}
