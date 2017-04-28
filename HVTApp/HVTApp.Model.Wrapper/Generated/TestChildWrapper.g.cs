using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class TestChildWrapper : WrapperBase<TestChild>
  {
    public TestChildWrapper() : base(new TestChild(), new Dictionary<IBaseEntity, object>()) { }
    public TestChildWrapper(TestChild model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public TestChildWrapper(TestChild model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public TestChildWrapper(TestChild model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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

    #region ComplexProperties
	public TestHusbandWrapper Husband 
    {
        get { return GetComplexProperty<TestHusbandWrapper, TestHusband>(Model.Husband); }
        set { SetComplexProperty<TestHusbandWrapper, TestHusband>(Husband, value); }
    }

    public TestHusbandWrapper HusbandOriginalValue { get; private set; }
    public bool HusbandIsChanged => GetIsChanged(nameof(Husband));

	public TestWifeWrapper Wife 
    {
        get { return GetComplexProperty<TestWifeWrapper, TestWife>(Model.Wife); }
        set { SetComplexProperty<TestWifeWrapper, TestWife>(Wife, value); }
    }

    public TestWifeWrapper WifeOriginalValue { get; private set; }
    public bool WifeIsChanged => GetIsChanged(nameof(Wife));

    #endregion
    protected override void InitializeComplexProperties(TestChild model)
    {
        Husband = GetWrapper<TestHusbandWrapper, TestHusband>(model.Husband);
        Wife = GetWrapper<TestWifeWrapper, TestWife>(model.Wife);
    }
  }
}
