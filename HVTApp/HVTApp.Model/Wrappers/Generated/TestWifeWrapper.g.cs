using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class TestWifeWrapper : WrapperBase<TestWife>
  {
    public TestWifeWrapper() : base(new TestWife()) { }
    public TestWifeWrapper(TestWife model) : base(model) { }



    #region SimpleProperties

    public System.Int32 N
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 NOriginalValue => GetOriginalValue<System.Int32>(nameof(N));
    public bool NIsChanged => GetIsChanged(nameof(N));


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


    #endregion

    public override void InitializeComplexProperties()
    {

        Husband = GetWrapper<TestHusbandWrapper, TestHusband>(Model.Husband);

    }

  }
}
