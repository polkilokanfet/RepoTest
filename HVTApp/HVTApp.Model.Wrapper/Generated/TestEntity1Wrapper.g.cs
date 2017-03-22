using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TestEntity1Wrapper : WrapperBase<TestEntity1>
  {
    public TestEntity1Wrapper(TestEntity1 model) : base(model) { }
    public TestEntity1Wrapper(TestEntity1 model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


    #region SimpleProperties

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public TestEntity2Wrapper TestEntity2
	{
		get { return GetComplexProperty<TestEntity2, TestEntity2Wrapper>(nameof(TestEntity2)); }
		set { SetComplexProperty<TestEntity2, TestEntity2Wrapper>(value, nameof(TestEntity2)); }
	}


    #endregion

    
    protected override void InitializeComplexProperties(TestEntity1 model)
    {

		if (model.TestEntity2 != null)
		{
            TestEntity2 = GetWrapper<TestEntity2, TestEntity2Wrapper>(model.TestEntity2);
			//if (ExistsWrappers.ContainsKey(model.TestEntity2))
			//{
			//	TestEntity2 = (TestEntity2Wrapper)ExistsWrappers[model.TestEntity2];
			//}
			//else
			//{
			//	TestEntity2 = new TestEntity2Wrapper(model.TestEntity2, ExistsWrappers);
			//	RegisterComplexProperty(TestEntity2);
			//}
		}


    }

  }
}
