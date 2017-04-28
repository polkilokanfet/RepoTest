using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class TestFriendEmailWrapper : WrapperBase<TestFriendEmail>
  {
    public TestFriendEmailWrapper() : base(new TestFriendEmail()) { }
    public TestFriendEmailWrapper(TestFriendEmail model) : base(model) { }
    public TestFriendEmailWrapper(TestFriendEmail model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public TestFriendEmailWrapper(TestFriendEmail model, IDictionary<IBaseEntity, object> dictionary) : base(model, new ExistsWrappers(dictionary)) { }



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


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion

  }
}
