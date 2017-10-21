using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
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

	public TestWifeWrapper Wife { get; set; }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<TestChildWrapper> Children { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Wife = new TestWifeWrapper(Model.Wife);
		RegisterComplex(Wife);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.Children == null) throw new ArgumentException("Children cannot be null");
      Children = new ValidatableChangeTrackingCollection<TestChildWrapper>(Model.Children.Select(e => new TestChildWrapper(e)));
      RegisterCollection(Children, Model.Children);


    }

  }
}
