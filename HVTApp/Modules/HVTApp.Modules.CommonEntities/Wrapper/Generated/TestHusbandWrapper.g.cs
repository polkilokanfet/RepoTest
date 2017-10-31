using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class TestHusbandWrapper : WrapperBase<TestHusband>
	{
	public TestHusbandWrapper(TestHusband model) : base(model) { }

	
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
	private TestWifeWrapper _fieldWife;
	public TestWifeWrapper Wife 
    {
        get { return _fieldWife ; }
        set
        {
            SetComplexValue<TestWife, TestWifeWrapper>(_fieldWife, value);
            _fieldWife  = value;
        }
    }
    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<TestChildWrapper> Children { get; private set; }

    #endregion
    public override void InitializeComplexProperties()
    {
		if (Model.Wife != null)
        {
            _fieldWife = new TestWifeWrapper(Model.Wife);
            RegisterComplex(Wife);
        }
    }
  
    protected override void InitializeCollectionProperties()
    {
      if (Model.Children == null) throw new ArgumentException("Children cannot be null");
      Children = new ValidatableChangeTrackingCollection<TestChildWrapper>(Model.Children.Select(e => new TestChildWrapper(e)));
      RegisterCollection(Children, Model.Children);

    }
	}
}
	