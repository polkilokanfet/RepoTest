using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TestChildWrapper : WrapperBase<TestChild>
  {
    protected TestChildWrapper(TestChild model) : base(model) { }

	public static TestChildWrapper GetWrapper(TestChild model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TestChildWrapper)Repository.ModelWrapperDictionary[model];

		return new TestChildWrapper(model);
	}



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

	private TestHusbandWrapper _fieldHusband;
	public TestHusbandWrapper Husband 
    {
        get { return _fieldHusband; }
        set
        {
            if (Equals(_fieldHusband, value))
                return;

            UnRegisterComplexProperty(_fieldHusband);

            _fieldHusband = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private TestWifeWrapper _fieldWife;
	public TestWifeWrapper Wife 
    {
        get { return _fieldWife; }
        set
        {
            if (Equals(_fieldWife, value))
                return;

            UnRegisterComplexProperty(_fieldWife);

            _fieldWife = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion

    protected override void InitializeComplexProperties(TestChild model)
    {

        Husband = TestHusbandWrapper.GetWrapper(model.Husband);

        Wife = TestWifeWrapper.GetWrapper(model.Wife);

    }

  }
}
