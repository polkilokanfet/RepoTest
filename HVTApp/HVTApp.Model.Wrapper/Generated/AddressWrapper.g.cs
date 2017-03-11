using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class AddressWrapper : WrapperBase<Address>
  {
    public AddressWrapper(Address model) : base(model) { }
    public AddressWrapper(Address model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.String Description
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String DescriptionOriginalValue => GetOriginalValue<System.String>(nameof(Description));
    public bool DescriptionIsChanged => GetIsChanged(nameof(Description));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
    public LocalityWrapper Locality { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(Address model)
    {
      if (model.Locality == null) throw new ArgumentException("Locality cannot be null");
      if (ExistsWrappers.ContainsKey(model.Locality))
      {
          Locality = (LocalityWrapper)ExistsWrappers[model.Locality];
      }
      else
      {
          Locality = new LocalityWrapper(model.Locality, ExistsWrappers);
          RegisterComplexProperty(Locality);
      }

    }
  }
}
