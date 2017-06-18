using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class TestChildWrapper : WrapperBase<TestChild>
  {
    private TestChildWrapper() : base(new TestChild()) { }
    private TestChildWrapper(TestChild model) : base(model) { }



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

    public override void InitializeComplexProperties()
    {

        Husband = GetWrapper<TestHusbandWrapper, TestHusband>(Model.Husband);

        Wife = GetWrapper<TestWifeWrapper, TestWife>(Model.Wife);

    }

  }
}
