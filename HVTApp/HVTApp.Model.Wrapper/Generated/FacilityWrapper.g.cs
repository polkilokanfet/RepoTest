using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class FacilityWrapper : WrapperBase<Facility>
  {
    public FacilityWrapper(Facility model) : base(model) { }
    public FacilityWrapper(Facility model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
    public FacilityTypeWrapper Type { get; private set; }

    public CompanyWrapper OwnerCompany { get; private set; }

    public AddressWrapper Address { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(Facility model)
    {
      if (model.Type == null) throw new ArgumentException("Type cannot be null");
      if (ExistsWrappers.ContainsKey(model.Type))
      {
          Type = (FacilityTypeWrapper)ExistsWrappers[model.Type];
      }
      else
      {
          Type = new FacilityTypeWrapper(model.Type, ExistsWrappers);
          RegisterComplexProperty(Type);
      }

      if (model.OwnerCompany == null) throw new ArgumentException("OwnerCompany cannot be null");
      if (ExistsWrappers.ContainsKey(model.OwnerCompany))
      {
          OwnerCompany = (CompanyWrapper)ExistsWrappers[model.OwnerCompany];
      }
      else
      {
          OwnerCompany = new CompanyWrapper(model.OwnerCompany, ExistsWrappers);
          RegisterComplexProperty(OwnerCompany);
      }

      if (model.Address == null) throw new ArgumentException("Address cannot be null");
      if (ExistsWrappers.ContainsKey(model.Address))
      {
          Address = (AddressWrapper)ExistsWrappers[model.Address];
      }
      else
      {
          Address = new AddressWrapper(model.Address, ExistsWrappers);
          RegisterComplexProperty(Address);
      }

    }
  }
}
