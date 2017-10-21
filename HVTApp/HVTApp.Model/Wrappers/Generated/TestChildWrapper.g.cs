using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class TestChildWrapper : WrapperBase<TestChild>
  {
    public TestChildWrapper(TestChild model) : base(model) { }



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

	public TestHusbandWrapper Husband { get; set; }

	public TestWifeWrapper Wife { get; set; }

    #endregion

    public override void InitializeComplexProperties()
    {

        Husband = new TestHusbandWrapper(Model.Husband);
		RegisterComplex(Husband);

        Wife = new TestWifeWrapper(Model.Wife);
		RegisterComplex(Wife);

    }

  }
}
