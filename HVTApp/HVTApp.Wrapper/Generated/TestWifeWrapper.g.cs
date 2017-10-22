using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Wrapper
{
  public partial class TestWifeWrapper : WrapperBase<TestWife>
  {
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


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private TestHusbandWrapper _fieldHusband;
	public TestHusbandWrapper Husband 
    {
        get { return _fieldHusband ; }
        set
        {
            SetComplexValue<TestHusband, TestHusbandWrapper>(_fieldHusband, value);
            _fieldHusband  = value;
        }
    }

    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.Husband != null)
        {
            _fieldHusband = new TestHusbandWrapper(Model.Husband);
            RegisterComplex(Husband);
        }

    }

  }
}
