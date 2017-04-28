using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class AddressWrapper : WrapperBase<Address>
  {
    public AddressWrapper() : base(new Address(), new Dictionary<IBaseEntity, object>()) { }
    public AddressWrapper(Address model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public AddressWrapper(Address model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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
	public LocalityWrapper Locality 
    {
        get { return GetComplexProperty<LocalityWrapper, Locality>(Model.Locality); }
        set { SetComplexProperty<LocalityWrapper, Locality>(Locality, value); }
    }

    public LocalityWrapper LocalityOriginalValue { get; private set; }
    public bool LocalityIsChanged => GetIsChanged(nameof(Locality));

    #endregion
    protected override void InitializeComplexProperties(Address model)
    {
        Locality = GetWrapper<LocalityWrapper, Locality>(model.Locality);
    }
  }
}
