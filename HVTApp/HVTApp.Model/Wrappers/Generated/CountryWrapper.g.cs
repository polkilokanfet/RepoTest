using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class CountryWrapper : WrapperBase<Country>
  {
    private CountryWrapper(IGetWrapper getWrapper) : base(new Country(), getWrapper) { }
    private CountryWrapper(Country model, IGetWrapper getWrapper) : base(model, getWrapper) { }



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

	public LocalityWrapper Capital 
    {
        get { return GetComplexProperty<LocalityWrapper, Locality>(Model.Capital); }
        set { SetComplexProperty<LocalityWrapper, Locality>(Capital, value); }
    }

    public LocalityWrapper CapitalOriginalValue { get; private set; }
    public bool CapitalIsChanged => GetIsChanged(nameof(Capital));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<DistrictWrapper> Districts { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Capital = GetWrapper<LocalityWrapper, Locality>(Model.Capital);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.Districts == null) throw new ArgumentException("Districts cannot be null");
      Districts = new ValidatableChangeTrackingCollection<DistrictWrapper>(Model.Districts.Select(e => GetWrapper<DistrictWrapper, District>(e)));
      RegisterCollection(Districts, Model.Districts);


    }

  }
}
