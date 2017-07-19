












using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class AddressWrapper : WrapperBase<Address>
  {
    private AddressWrapper(IGetWrapper getWrapper) : base(new Address(), getWrapper) { }
    private AddressWrapper(Address model, IGetWrapper getWrapper) : base(model, getWrapper) { }



    #region SimpleProperties

    public System.String Description
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String DescriptionOriginalValue => GetOriginalValue<System.String>(nameof(Description));
    public bool DescriptionIsChanged => GetIsChanged(nameof(Description));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public LocalityWrapper Locality 
    {
        get { return GetComplexProperty<LocalityWrapper, Locality>(Model.Locality); }
        set { SetComplexProperty<LocalityWrapper, Locality>(Locality, value); }
    }

    public LocalityWrapper LocalityOriginalValue { get; private set; }
    public bool LocalityIsChanged => GetIsChanged(nameof(Locality));


    #endregion

    public override void InitializeComplexProperties()
    {

        Locality = GetWrapper<LocalityWrapper, Locality>(Model.Locality);

    }

  }
}
