using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TestChildWrapper : WrapperBase<TestChild>
  {
    protected TestChildWrapper(TestChild model) : base(model) { }

	public static TestChildWrapper GetWrapper()
	{
		return GetWrapper(new TestChild());
	}

	public static TestChildWrapper GetWrapper(TestChild model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TestChildWrapper)Repository.ModelWrapperDictionary[model];

		return new TestChildWrapper(model);
	}



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
        get { return TestHusbandWrapper.GetWrapper(Model.Husband); }
        set
        {
			var oldPropVal = Husband;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public TestHusbandWrapper HusbandOriginalValue => TestHusbandWrapper.GetWrapper(GetOriginalValue<TestHusband>(nameof(Husband)));
    public bool HusbandIsChanged => GetIsChanged(nameof(Husband));


	public TestWifeWrapper Wife 
    {
        get { return TestWifeWrapper.GetWrapper(Model.Wife); }
        set
        {
			var oldPropVal = Wife;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public TestWifeWrapper WifeOriginalValue => TestWifeWrapper.GetWrapper(GetOriginalValue<TestWife>(nameof(Wife)));
    public bool WifeIsChanged => GetIsChanged(nameof(Wife));


    #endregion

    protected override void InitializeComplexProperties(TestChild model)
    {

        Husband = TestHusbandWrapper.GetWrapper(model.Husband);

        Wife = TestWifeWrapper.GetWrapper(model.Wife);

    }

  }
}
