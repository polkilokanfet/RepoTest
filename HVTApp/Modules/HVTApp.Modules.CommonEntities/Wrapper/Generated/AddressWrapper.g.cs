using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class AddressWrapper : WrapperBase<Address>
	{
	public AddressWrapper(Address model) : base(model) { }

	
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
        get { return GetWrapper<LocalityWrapper>(); }
        set { SetComplexValue<Locality, LocalityWrapper>(Locality, value); }
    }

    #endregion
    public override void InitializeComplexProperties()
    {
        InitializeComplexProperty<LocalityWrapper>(nameof(Locality), Model.Locality == null ? null : new LocalityWrapper(Model.Locality));

    }
	}
}
	