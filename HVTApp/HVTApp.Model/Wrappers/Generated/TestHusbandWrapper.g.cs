using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class TestHusbandWrapper : WrapperBase<TestHusband>
  {
    private TestHusbandWrapper() : base(new TestHusband()) { }
    private TestHusbandWrapper(TestHusband model) : base(model) { }



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

    public override void InitializeComplexProperties()
    {

        Wife = GetWrapper<TestWifeWrapper, TestWife>(Model.Wife);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.Children == null) throw new ArgumentException("Children cannot be null");
      Children = new ValidatableChangeTrackingCollection<TestChildWrapper>(Model.Children.Select(e => GetWrapper<TestChildWrapper, TestChild>(e)));
      RegisterCollection(Children, Model.Children);


    }

  }
}
