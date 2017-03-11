using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class FriendTestWrapper : WrapperBase<FriendTest>
  {
    public FriendTestWrapper(FriendTest model) : base(model) { }
    public FriendTestWrapper(FriendTest model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.Int32 FriendGroupId
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 FriendGroupIdOriginalValue => GetOriginalValue<System.Int32>(nameof(FriendGroupId));
    public bool FriendGroupIdIsChanged => GetIsChanged(nameof(FriendGroupId));

    public System.String FirstName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String FirstNameOriginalValue => GetOriginalValue<System.String>(nameof(FirstName));
    public bool FirstNameIsChanged => GetIsChanged(nameof(FirstName));

    public System.String LastName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String LastNameOriginalValue => GetOriginalValue<System.String>(nameof(LastName));
    public bool LastNameIsChanged => GetIsChanged(nameof(LastName));

    public System.Nullable<System.DateTime> Birthday
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> BirthdayOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(Birthday));
    public bool BirthdayIsChanged => GetIsChanged(nameof(Birthday));

    public System.Boolean IsDeveloper
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }
    public System.Boolean IsDeveloperOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsDeveloper));
    public bool IsDeveloperIsChanged => GetIsChanged(nameof(IsDeveloper));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
    public FriendAddressTestWrapper FriendAddressTest { get; private set; }

    public FriendGroupTestWrapper FriendGroupTest { get; private set; }

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<FriendEmailTestWrapper> Emails { get; private set; }

    #endregion

    #region CollectionSimpleProperties
    public TrackingCollection<Int32> IntList { get; private set; }

    public TrackingCollection<Int32> IntListGet { get; private set; }

    #endregion

    #region GetProperties
    public System.Int32 IdGet => GetValue<System.Int32>(); 

    public HVTApp.Model.FriendEmailTest FriendEmailTestGet => GetValue<HVTApp.Model.FriendEmailTest>(); 

    #endregion
    
    protected override void InitializeComplexProperties(FriendTest model)
    {
      if (model.FriendAddressTest == null) throw new ArgumentException("FriendAddressTest cannot be null");
      if (ExistsWrappers.ContainsKey(model.FriendAddressTest))
      {
          FriendAddressTest = (FriendAddressTestWrapper)ExistsWrappers[model.FriendAddressTest];
      }
      else
      {
          FriendAddressTest = new FriendAddressTestWrapper(model.FriendAddressTest, ExistsWrappers);
          RegisterComplexProperty(FriendAddressTest);
      }

      if (model.FriendGroupTest == null) throw new ArgumentException("FriendGroupTest cannot be null");
      if (ExistsWrappers.ContainsKey(model.FriendGroupTest))
      {
          FriendGroupTest = (FriendGroupTestWrapper)ExistsWrappers[model.FriendGroupTest];
      }
      else
      {
          FriendGroupTest = new FriendGroupTestWrapper(model.FriendGroupTest, ExistsWrappers);
          RegisterComplexProperty(FriendGroupTest);
      }

    }
  
    protected override void InitializeCollectionComplexProperties(FriendTest model)
    {
      if (model.Emails == null) throw new ArgumentException("Emails cannot be null");
      Emails = new ValidatableChangeTrackingCollection<FriendEmailTestWrapper>(model.Emails.Select(e => new FriendEmailTestWrapper(e, ExistsWrappers)));
      RegisterCollection(Emails, model.Emails);

    }
  
    protected override void InitializeCollectionSimpleProperties(FriendTest model)
    {
      if (model.IntList == null) throw new ArgumentException("IntList cannot be null");
      IntList = new TrackingCollection<Int32>(model.IntList);
      RegisterCollection(IntList, model.IntList);

      if (model.IntListGet == null) throw new ArgumentException("IntListGet cannot be null");
      IntListGet = new TrackingCollection<Int32>(model.IntListGet);
      RegisterCollection(IntListGet, model.IntListGet);

    }
  }
}
