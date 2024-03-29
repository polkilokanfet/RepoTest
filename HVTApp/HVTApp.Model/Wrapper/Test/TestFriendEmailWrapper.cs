using HVTApp.Model.POCOs;
using HVTApp.Model.POCOs.Test;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.Model.Wrapper.Test
{
    public partial class TestFriendEmailWrapper : WrapperBase<TestFriendEmail>
    {
        public TestFriendEmailWrapper(TestFriendEmail model) : base(model) { }

        #region SimpleProperties

        public System.String Email
        {
            get { return GetValue<System.String>(); }
            set { SetValue(value); }
        }
        public System.String EmailOriginalValue => GetOriginalValue<System.String>(nameof(Email));
        public bool EmailIsChanged => GetIsChanged(nameof(Email));


        public System.String Comment
        {
            get { return GetValue<System.String>(); }
            set { SetValue(value); }
        }
        public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));


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