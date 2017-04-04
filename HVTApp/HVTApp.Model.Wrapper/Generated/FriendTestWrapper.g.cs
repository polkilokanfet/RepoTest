using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class FriendTestWrapper : WrapperBase<FriendTest>
  {
    protected FriendTestWrapper(FriendTest model) : base(model) { }

	public static FriendTestWrapper GetWrapper(FriendTest model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (FriendTestWrapper)Repository.ModelWrapperDictionary[model];

		return new FriendTestWrapper(model);
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

	public FriendAddressTestWrapper FriendAddressTest 
    {
        get { return FriendAddressTestWrapper.GetWrapper(Model.FriendAddressTest); }
        set
        {
            UnRegisterComplexProperty(FriendAddressTest);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public FriendGroupTestWrapper FriendGroupTest 
    {
        get { return FriendGroupTestWrapper.GetWrapper(Model.FriendGroupTest); }
        set
        {
            UnRegisterComplexProperty(FriendGroupTest);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion


    #region CollectionProperties

    public ValidatableChangeTrackingCollection<FriendEmailTestWrapper> Emails { get; private set; }


    #endregion


    #region GetProperties

    public System.Int32 IdGet => GetValue<System.Int32>(); 


    public HVTApp.Model.FriendEmailTest FriendEmailTestGet => GetValue<HVTApp.Model.FriendEmailTest>(); 


    #endregion

    protected override void InitializeComplexProperties(FriendTest model)
    {

        FriendAddressTest = FriendAddressTestWrapper.GetWrapper(model.FriendAddressTest);

        FriendGroupTest = FriendGroupTestWrapper.GetWrapper(model.FriendGroupTest);

    }

  
    protected override void InitializeCollectionComplexProperties(FriendTest model)
    {

      if (model.Emails == null) throw new ArgumentException("Emails cannot be null");
      Emails = new ValidatableChangeTrackingCollection<FriendEmailTestWrapper>(model.Emails.Select(e => FriendEmailTestWrapper.GetWrapper(e)));
      RegisterCollection(Emails, model.Emails);


    }

  }
}
