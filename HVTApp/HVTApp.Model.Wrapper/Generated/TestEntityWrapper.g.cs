using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TestEntityWrapper : WrapperBase<TestEntity>
  {
    public TestEntityWrapper() : base(new TestEntity()) { }
    public TestEntityWrapper(TestEntity model) : base(model) { }

//	public static TestEntityWrapper GetWrapper()
//	{
//		return GetWrapper(new TestEntity());
//	}
//
//	public static TestEntityWrapper GetWrapper(TestEntity model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (TestEntityWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new TestEntityWrapper(model);
//	}


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
  }
}
