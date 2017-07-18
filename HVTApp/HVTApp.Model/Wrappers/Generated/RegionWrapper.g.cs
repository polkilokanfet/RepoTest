using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class RegionWrapper : WrapperBase<Region>
  {
    private RegionWrapper() : base(new Region()) { }
    private RegionWrapper(Region model) : base(model) { }



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

    public override void InitializeComplexProperties()
    {

        District = GetWrapper<DistrictWrapper, District>(Model.District);

        Capital = GetWrapper<LocalityWrapper, Locality>(Model.Capital);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.Localities == null) throw new ArgumentException("Localities cannot be null");
      Localities = new ValidatableChangeTrackingCollection<LocalityWrapper>(Model.Localities.Select(e => GetWrapper<LocalityWrapper, Locality>(e)));
      RegisterCollection(Localities, Model.Localities);


    }

  }
}
