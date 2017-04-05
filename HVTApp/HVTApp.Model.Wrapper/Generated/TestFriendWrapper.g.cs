using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TestFriendWrapper : WrapperBase<TestFriend>
  {
    protected TestFriendWrapper(TestFriend model) : base(model) { }

	public static TestFriendWrapper GetWrapper(TestFriend model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TestFriendWrapper)Repository.ModelWrapperDictionary[model];

		return new TestFriendWrapper(model);
	}



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

	public TestFriendAddressWrapper TestFriendAddress 
    {
        get { return TestFriendAddressWrapper.GetWrapper(Model.TestFriendAddress); }
        set
        {
			var oldPropVal = TestFriendAddress;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public TestFriendAddressWrapper TestFriendAddressOriginalValue => TestFriendAddressWrapper.GetWrapper(GetOriginalValue<TestFriendAddress>(nameof(TestFriendAddress)));
    public bool TestFriendAddressIsChanged => GetIsChanged(nameof(TestFriendAddress));


	public TestFriendGroupWrapper TestFriendGroup 
    {
        get { return TestFriendGroupWrapper.GetWrapper(Model.TestFriendGroup); }
        set
        {
			var oldPropVal = TestFriendGroup;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public TestFriendGroupWrapper TestFriendGroupOriginalValue => TestFriendGroupWrapper.GetWrapper(GetOriginalValue<TestFriendGroup>(nameof(TestFriendGroup)));
    public bool TestFriendGroupIsChanged => GetIsChanged(nameof(TestFriendGroup));


    #endregion


    #region CollectionProperties

    public ValidatableChangeTrackingCollection<TestFriendEmailWrapper> Emails { get; private set; }


    #endregion


    #region GetProperties

    public System.Int32 IdGet => GetValue<System.Int32>(); 


    public HVTApp.Model.TestFriendEmail TestFriendEmailGet => GetValue<HVTApp.Model.TestFriendEmail>(); 


    #endregion

    protected override void InitializeComplexProperties(TestFriend model)
    {

        TestFriendAddress = TestFriendAddressWrapper.GetWrapper(model.TestFriendAddress);

        TestFriendGroup = TestFriendGroupWrapper.GetWrapper(model.TestFriendGroup);

    }

  
    protected override void InitializeCollectionComplexProperties(TestFriend model)
    {

      if (model.Emails == null) throw new ArgumentException("Emails cannot be null");
      Emails = new ValidatableChangeTrackingCollection<TestFriendEmailWrapper>(model.Emails.Select(e => TestFriendEmailWrapper.GetWrapper(e)));
      RegisterCollection(Emails, model.Emails);


    }

  }
}
