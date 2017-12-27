using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class TestHusbandWrapper : WrapperBase<TestHusband>
  {
    public TestHusbandWrapper() : base(new TestHusband(), new Dictionary<IBaseEntity, object>()) { }
    public TestHusbandWrapper(TestHusband model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public TestHusbandWrapper(TestHusband model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public TestHusbandWrapper(TestHusband model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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
        get { return GetComplexProperty<TestWifeWrapper, TestWife>(Model.Wife); }
        set { SetComplexProperty<TestWifeWrapper, TestWife>(Wife, value); }
    }

    public TestWifeWrapper WifeOriginalValue { get; private set; }
    public bool WifeIsChanged => GetIsChanged(nameof(Wife));

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<TestChildWrapper> Children { get; private set; }

    #endregion
    protected override void InitializeComplexProperties(TestHusband model)
    {
        Wife = GetWrapper<TestWifeWrapper, TestWife>(model.Wife);
    }
  
    protected override void InitializeCollectionComplexProperties(TestHusband model)
    {
      if (model.Children == null) throw new ArgumentException("Children cannot be null");
      Children = new ValidatableChangeTrackingCollection<TestChildWrapper>(model.Children.Select(e => GetWrapper<TestChildWrapper, TestChild>(e)));
      RegisterCollection(Children, model.Children);

    }
  }
}
