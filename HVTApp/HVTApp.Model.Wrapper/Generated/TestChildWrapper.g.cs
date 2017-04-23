using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TestChildWrapper : WrapperBase<TestChild>
  {
    public TestChildWrapper() : base(new TestChild()) { }
    public TestChildWrapper(TestChild model) : base(model) { }

//	public static TestChildWrapper GetWrapper()
//	{
//		return GetWrapper(new TestChild());
//	}
//
//	public static TestChildWrapper GetWrapper(TestChild model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (TestChildWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new TestChildWrapper(model);
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

	private TestWifeWrapper _fieldWife;
	public TestWifeWrapper Wife 
    {
        get { return _fieldWife; }
        set
        {
			SetComplexProperty<TestWifeWrapper, TestWife>(_fieldWife, value);
			_fieldWife = value;
        }
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
