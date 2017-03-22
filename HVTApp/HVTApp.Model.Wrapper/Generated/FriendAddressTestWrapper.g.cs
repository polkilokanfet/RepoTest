using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class FriendAddressTestWrapper : WrapperBase<FriendAddressTest>
  {
    public FriendAddressTestWrapper(FriendAddressTest model) : base(model) { }
    public FriendAddressTestWrapper(FriendAddressTest model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.String City
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String CityOriginalValue => GetOriginalValue<System.String>(nameof(City));
    public bool CityIsChanged => GetIsChanged(nameof(City));

    public System.String Street
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String StreetOriginalValue => GetOriginalValue<System.String>(nameof(Street));
    public bool StreetIsChanged => GetIsChanged(nameof(Street));

    public System.String StreetNumber
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String StreetNumberOriginalValue => GetOriginalValue<System.String>(nameof(StreetNumber));
    public bool StreetNumberIsChanged => GetIsChanged(nameof(StreetNumber));

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
