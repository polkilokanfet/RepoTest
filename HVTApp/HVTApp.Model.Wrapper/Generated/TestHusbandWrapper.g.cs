using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TestHusbandWrapper : WrapperBase<TestHusband>
  {
    protected TestHusbandWrapper(TestHusband model) : base(model) { }

	public static TestHusbandWrapper GetWrapper(TestHusband model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TestHusbandWrapper)Repository.ModelWrapperDictionary[model];

		return new TestHusbandWrapper(model);
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


    #endregion


    #region CollectionProperties

    public ValidatableChangeTrackingCollection<TestChildWrapper> Children { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(TestHusband model)
    {

        Wife = TestWifeWrapper.GetWrapper(model.Wife);

    }

  
    protected override void InitializeCollectionComplexProperties(TestHusband model)
    {

      if (model.Children == null) throw new ArgumentException("Children cannot be null");
      Children = new ValidatableChangeTrackingCollection<TestChildWrapper>(model.Children.Select(e => TestChildWrapper.GetWrapper(e)));
      RegisterCollection(Children, model.Children);


    }

  }
}
