using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class DistrictWrapper : WrapperBase<District>
  {
    private DistrictWrapper() : base(new District()) { }
    private DistrictWrapper(District model) : base(model) { }



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


	public CountryWrapper Country 
    {
        get { return GetComplexProperty<CountryWrapper, Country>(Model.Country); }
        set { SetComplexProperty<CountryWrapper, Country>(Country, value); }
    }

    public CountryWrapper CountryOriginalValue { get; private set; }
    public bool CountryIsChanged => GetIsChanged(nameof(Country));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<RegionWrapper> Regions { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Capital = GetWrapper<LocalityWrapper, Locality>(Model.Capital);

        Country = GetWrapper<CountryWrapper, Country>(Model.Country);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.Regions == null) throw new ArgumentException("Regions cannot be null");
      Regions = new ValidatableChangeTrackingCollection<RegionWrapper>(Model.Regions.Select(e => GetWrapper<RegionWrapper, Region>(e)));
      RegisterCollection(Regions, Model.Regions);


    }

  }
}
