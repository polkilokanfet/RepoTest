using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TestWifeWrapper : WrapperBase<TestWife>
  {
    public TestWifeWrapper() : base(new TestWife()) { }
    public TestWifeWrapper(TestWife model) : base(model) { }

//	public static TestWifeWrapper GetWrapper()
//	{
//		return GetWrapper(new TestWife());
//	}
//
//	public static TestWifeWrapper GetWrapper(TestWife model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (TestWifeWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new TestWifeWrapper(model);
//	}


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
	private TestHusbandWrapper _fieldHusband;
	public TestHusbandWrapper Husband 
    {
        get { return _fieldHusband; }
        set
        {
			SetComplexProperty<TestHusbandWrapper, TestHusband>(_fieldHusband, value);
			_fieldHusband = value;
        }
    }
    public TestHusbandWrapper HusbandOriginalValue { get; private set; }
    public bool HusbandIsChanged => GetIsChanged(nameof(Husband));

    #endregion
    protected override void InitializeComplexProperties(TestWife model)
    {
        Husband = GetWrapper<TestHusbandWrapper, TestHusband>(model.Husband);
    }
  }
}
