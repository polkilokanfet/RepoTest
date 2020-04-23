using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.POCOs.Test;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.Model.Wrapper.Test
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

        public TestFriendAddressWrapper TestFriendAddress 
        {
            get { return GetWrapper<TestFriendAddressWrapper>(); }
            set { SetComplexValue<TestFriendAddress, TestFriendAddressWrapper>(TestFriendAddress, value); }
        }


        public TestFriendGroupWrapper TestFriendGroup 
        {
            get { return GetWrapper<TestFriendGroupWrapper>(); }
            set { SetComplexValue<TestFriendGroup, TestFriendGroupWrapper>(TestFriendGroup, value); }
        }


        #endregion


        #region CollectionProperties

        public IValidatableChangeTrackingCollection<TestFriendEmailWrapper> Emails { get; private set; }


        #endregion


        #region GetProperties

        public System.Int32 IdGet => GetValue<System.Int32>(); 


        public TestFriendEmail TestFriendEmailGet => GetValue<TestFriendEmail>(); 


        #endregion

        public override void InitializeComplexProperties()
        {

            InitializeComplexProperty<TestFriendAddressWrapper>(nameof(TestFriendAddress), Model.TestFriendAddress == null ? null : new TestFriendAddressWrapper(Model.TestFriendAddress));


            InitializeComplexProperty<TestFriendGroupWrapper>(nameof(TestFriendGroup), Model.TestFriendGroup == null ? null : new TestFriendGroupWrapper(Model.TestFriendGroup));


        }

  
        protected override void InitializeCollectionProperties()
        {

            if (Model.Emails == null) throw new ArgumentException("Emails cannot be null");
            Emails = new ValidatableChangeTrackingCollection<TestFriendEmailWrapper>(Model.Emails.Select(e => new TestFriendEmailWrapper(e)));
            RegisterCollection(Emails, Model.Emails);


        }

    }
}