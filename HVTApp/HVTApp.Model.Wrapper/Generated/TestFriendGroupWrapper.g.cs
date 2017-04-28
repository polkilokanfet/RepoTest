using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class TestFriendGroupWrapper : WrapperBase<TestFriendGroup>
  {
    public TestFriendGroupWrapper() : base(new TestFriendGroup()) { }
    public TestFriendGroupWrapper(TestFriendGroup model) : base(model) { }
    public TestFriendGroupWrapper(TestFriendGroup model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public TestFriendGroupWrapper(TestFriendGroup model, IDictionary<IBaseEntity, object> dictionary) : base(model, new ExistsWrappers(dictionary)) { }



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


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<TestFriendWrapper> FriendTests { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties(TestFriendGroup model)
    {

      if (model.FriendTests == null) throw new ArgumentException("FriendTests cannot be null");
      FriendTests = new ValidatableChangeTrackingCollection<TestFriendWrapper>(model.FriendTests.Select(e => GetWrapper<TestFriendWrapper, TestFriend>(e)));
      RegisterCollection(FriendTests, model.FriendTests);


    }

  }
}
