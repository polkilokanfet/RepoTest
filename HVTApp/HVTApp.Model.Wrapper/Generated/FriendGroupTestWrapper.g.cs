using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class FriendGroupTestWrapper : WrapperBase<FriendGroupTest>
  {
    public FriendGroupTestWrapper(FriendGroupTest model) : base(model) { }
    public FriendGroupTestWrapper(FriendGroupTest model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<FriendTestWrapper> FriendTests { get; private set; }

    #endregion
  
    protected override void InitializeCollectionComplexProperties(FriendGroupTest model)
    {
      if (model.FriendTests == null) throw new ArgumentException("FriendTests cannot be null");
      FriendTests = new ValidatableChangeTrackingCollection<FriendTestWrapper>(model.FriendTests.Select(e => new FriendTestWrapper(e, ExistsWrappers)));
      RegisterCollection(FriendTests, model.FriendTests);

    }
  }
}
