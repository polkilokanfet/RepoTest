using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class RegionWrapper : WrapperBase<Region>
  {
    public RegionWrapper() : base(new Region()) { }
    public RegionWrapper(Region model) : base(model) { }



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

	public DistrictWrapper District 
    {
        get { return GetComplexProperty<DistrictWrapper, District>(Model.District); }
        set { SetComplexProperty<DistrictWrapper, District>(District, value); }
    }

    public DistrictWrapper DistrictOriginalValue { get; private set; }
    public bool DistrictIsChanged => GetIsChanged(nameof(District));


	public LocalityWrapper Capital 
    {
        get { return GetComplexProperty<LocalityWrapper, Locality>(Model.Capital); }
        set { SetComplexProperty<LocalityWrapper, Locality>(Capital, value); }
    }

    public LocalityWrapper CapitalOriginalValue { get; private set; }
    public bool CapitalIsChanged => GetIsChanged(nameof(Capital));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<LocalityWrapper> Localities { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Region model)
    {

        District = GetWrapper<DistrictWrapper, District>(model.District);

        Capital = GetWrapper<LocalityWrapper, Locality>(model.Capital);

    }

  
    protected override void InitializeCollectionComplexProperties(Region model)
    {

      if (model.Localities == null) throw new ArgumentException("Localities cannot be null");
      Localities = new ValidatableChangeTrackingCollection<LocalityWrapper>(model.Localities.Select(e => GetWrapper<LocalityWrapper, Locality>(e)));
      RegisterCollection(Localities, model.Localities);


    }

  }
}
