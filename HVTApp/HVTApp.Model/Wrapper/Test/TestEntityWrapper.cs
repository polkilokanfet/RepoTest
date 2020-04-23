using HVTApp.Model.POCOs.Test;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.Model.Wrapper.Test
{
    public partial class TestEntityWrapper : WrapperBase<TestEntity>
    {
        public TestEntityWrapper(TestEntity model) : base(model) { }

	

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

    }
}