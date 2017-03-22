using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TestEntity2Wrapper : WrapperBase<TestEntity2>
  {
    public TestEntity2Wrapper(TestEntity2 model) : base(model) { }
    public TestEntity2Wrapper(TestEntity2 model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


    #region SimpleProperties

    public System.Int32 N
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 NOriginalValue => GetOriginalValue<System.Int32>(nameof(N));
    public bool NIsChanged => GetIsChanged(nameof(N));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public TestEntity1Wrapper TestEntity1
	{
		get { return GetComplexProperty<TestEntity1, TestEntity1Wrapper>(nameof(TestEntity1)); }
		set { SetComplexProperty<TestEntity1, TestEntity1Wrapper>(value, nameof(TestEntity1)); }
	}


    #endregion

    protected override void InitializeComplexProperties(TestEntity2 model)
    {

        TestEntity1 = GetWrapper<TestEntity1, TestEntity1Wrapper>(model.TestEntity1);

    }

  }
}
