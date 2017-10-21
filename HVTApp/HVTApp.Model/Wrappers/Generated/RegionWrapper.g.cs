using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class RegionWrapper : WrapperBase<Region>
  {
    public RegionWrapper(Region model) : base(model) { }



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

	private DistrictWrapper _fieldDistrict;
	public DistrictWrapper District 
    {
        get { return _fieldDistrict ; }
        set
        {
            SetComplexValue<District, DistrictWrapper>(_fieldDistrict, value);
            _fieldDistrict  = value;
        }
    }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<LocalityWrapper> Localities { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.District != null)
        {
            _fieldDistrict = new DistrictWrapper(Model.District);
            RegisterComplex(District);
        }

    }

  
    protected override void InitializeCollectionProperties()
    {

      if (Model.Localities == null) throw new ArgumentException("Localities cannot be null");
      Localities = new ValidatableChangeTrackingCollection<LocalityWrapper>(Model.Localities.Select(e => new LocalityWrapper(e)));
      RegisterCollection(Localities, Model.Localities);


    }

  }
}
