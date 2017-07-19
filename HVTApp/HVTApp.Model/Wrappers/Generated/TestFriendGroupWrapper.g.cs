using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class TestFriendGroupWrapper : WrapperBase<TestFriendGroup>
  {
    private TestFriendGroupWrapper(IGetWrapper getWrapper) : base(new TestFriendGroup(), getWrapper) { }
    private TestFriendGroupWrapper(TestFriendGroup model, IGetWrapper getWrapper) : base(model, getWrapper) { }



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<TestFriendWrapper> FriendTests { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.FriendTests == null) throw new ArgumentException("FriendTests cannot be null");
      FriendTests = new ValidatableChangeTrackingCollection<TestFriendWrapper>(Model.FriendTests.Select(e => GetWrapper<TestFriendWrapper, TestFriend>(e)));
      RegisterCollection(FriendTests, Model.FriendTests);


    }

  }
}
