using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class TestFriendAddressWrapper : WrapperBase<TestFriendAddress>
  {
    private TestFriendAddressWrapper(IGetWrapper getWrapper) : base(new TestFriendAddress(), getWrapper) { }
    private TestFriendAddressWrapper(TestFriendAddress model, IGetWrapper getWrapper) : base(model, getWrapper) { }



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
