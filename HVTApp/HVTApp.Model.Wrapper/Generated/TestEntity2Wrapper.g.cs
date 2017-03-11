using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TestEntity2Wrapper : WrapperBase<TestEntity2>
  {
    public TestEntity2Wrapper(TestEntity2 model) : base(model) { }
    public TestEntity2Wrapper(TestEntity2 model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
    public TestEntity1Wrapper TestEntity1 { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(TestEntity2 model)
    {
      if (model.TestEntity1 == null) throw new ArgumentException("TestEntity1 cannot be null");
      if (ExistsWrappers.ContainsKey(model.TestEntity1))
      {
          TestEntity1 = (TestEntity1Wrapper)ExistsWrappers[model.TestEntity1];
      }
      else
      {
          TestEntity1 = new TestEntity1Wrapper(model.TestEntity1, ExistsWrappers);
          RegisterComplexProperty(TestEntity1);
      }

    }
  }
}
