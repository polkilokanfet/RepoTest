using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class TestChildWrapper : WrapperBase<TestChild>
	{
	public TestChildWrapper(TestChild model) : base(model) { }

	
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

    #region ComplexProperties
	public TestHusbandWrapper Husband 
    {
        get { return GetWrapper<TestHusbandWrapper>(); }
        set { SetComplexValue<TestHusband, TestHusbandWrapper>(Husband, value); }
    }

	public TestWifeWrapper Wife 
    {
        get { return GetWrapper<TestWifeWrapper>(); }
        set { SetComplexValue<TestWife, TestWifeWrapper>(Wife, value); }
    }

    #endregion
    public override void InitializeComplexProperties()
    {
        InitializeComplexProperty<TestHusbandWrapper>(nameof(Husband), Model.Husband == null ? null : new TestHusbandWrapper(Model.Husband));

        InitializeComplexProperty<TestWifeWrapper>(nameof(Wife), Model.Wife == null ? null : new TestWifeWrapper(Model.Wife));

    }
	}
}
	