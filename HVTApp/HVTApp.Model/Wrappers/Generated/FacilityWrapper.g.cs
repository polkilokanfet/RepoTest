using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class FacilityWrapper : WrapperBase<Facility>
  {
    public FacilityWrapper(Facility model) : base(model) { }



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

	public FacilityTypeWrapper Type { get; set; }

	public CompanyWrapper OwnerCompany { get; set; }

	public AddressWrapper Address { get; set; }

    #endregion

    public override void InitializeComplexProperties()
    {

        Type = new FacilityTypeWrapper(Model.Type);
		RegisterComplex(Type);

        OwnerCompany = new CompanyWrapper(Model.OwnerCompany);
		RegisterComplex(OwnerCompany);

        Address = new AddressWrapper(Model.Address);
		RegisterComplex(Address);

    }

  }
}
