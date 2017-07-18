using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class TestFriendWrapper : WrapperBase<TestFriend>
  {
    private TestFriendWrapper() : base(new TestFriend()) { }
    private TestFriendWrapper(TestFriend model) : base(model) { }



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


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public TestFriendAddressWrapper TestFriendAddress 
    {
        get { return GetComplexProperty<TestFriendAddressWrapper, TestFriendAddress>(Model.TestFriendAddress); }
        set { SetComplexProperty<TestFriendAddressWrapper, TestFriendAddress>(TestFriendAddress, value); }
    }

    public TestFriendAddressWrapper TestFriendAddressOriginalValue { get; private set; }
    public bool TestFriendAddressIsChanged => GetIsChanged(nameof(TestFriendAddress));


	public TestFriendGroupWrapper TestFriendGroup 
    {
        get { return GetComplexProperty<TestFriendGroupWrapper, TestFriendGroup>(Model.TestFriendGroup); }
        set { SetComplexProperty<TestFriendGroupWrapper, TestFriendGroup>(TestFriendGroup, value); }
    }

    public TestFriendGroupWrapper TestFriendGroupOriginalValue { get; private set; }
    public bool TestFriendGroupIsChanged => GetIsChanged(nameof(TestFriendGroup));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<TestFriendEmailWrapper> Emails { get; private set; }


    #endregion


    #region GetProperties

    public System.Int32 IdGet => GetValue<System.Int32>(); 


    public HVTApp.Model.POCOs.TestFriendEmail TestFriendEmailGet => GetValue<HVTApp.Model.POCOs.TestFriendEmail>(); 


    #endregion

    public override void InitializeComplexProperties()
    {

        TestFriendAddress = GetWrapper<TestFriendAddressWrapper, TestFriendAddress>(Model.TestFriendAddress);

        TestFriendGroup = GetWrapper<TestFriendGroupWrapper, TestFriendGroup>(Model.TestFriendGroup);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.Emails == null) throw new ArgumentException("Emails cannot be null");
      Emails = new ValidatableChangeTrackingCollection<TestFriendEmailWrapper>(Model.Emails.Select(e => GetWrapper<TestFriendEmailWrapper, TestFriendEmail>(e)));
      RegisterCollection(Emails, Model.Emails);


    }

  }
}
