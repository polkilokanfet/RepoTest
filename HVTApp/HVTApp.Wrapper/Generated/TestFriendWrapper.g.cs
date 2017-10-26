using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Wrapper
{
	public partial class TestFriendWrapper : WrapperBase<TestFriend>
	{
	public TestFriendWrapper(TestFriend model) : base(model) { }

	

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

	private TestFriendAddressWrapper _fieldTestFriendAddress;
	public TestFriendAddressWrapper TestFriendAddress 
    {
        get { return _fieldTestFriendAddress ; }
        set
        {
            SetComplexValue<TestFriendAddress, TestFriendAddressWrapper>(_fieldTestFriendAddress, value);
            _fieldTestFriendAddress  = value;
        }
    }

	private TestFriendGroupWrapper _fieldTestFriendGroup;
	public TestFriendGroupWrapper TestFriendGroup 
    {
        get { return _fieldTestFriendGroup ; }
        set
        {
            SetComplexValue<TestFriendGroup, TestFriendGroupWrapper>(_fieldTestFriendGroup, value);
            _fieldTestFriendGroup  = value;
        }
    }

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

		if (Model.TestFriendAddress != null)
        {
            _fieldTestFriendAddress = new TestFriendAddressWrapper(Model.TestFriendAddress);
            RegisterComplex(TestFriendAddress);
        }

		if (Model.TestFriendGroup != null)
        {
            _fieldTestFriendGroup = new TestFriendGroupWrapper(Model.TestFriendGroup);
            RegisterComplex(TestFriendGroup);
        }

    }

  
    protected override void InitializeCollectionProperties()
    {

      if (Model.Emails == null) throw new ArgumentException("Emails cannot be null");
      Emails = new ValidatableChangeTrackingCollection<TestFriendEmailWrapper>(Model.Emails.Select(e => new TestFriendEmailWrapper(e)));
      RegisterCollection(Emails, Model.Emails);


    }

	}
}
	