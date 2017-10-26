using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class ProjectUnitWrapper : WrapperBase<ProjectUnit>
	{
	public ProjectUnitWrapper(ProjectUnit model) : base(model) { }

	

    #region SimpleProperties

    public System.Guid ProjectId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid ProjectIdOriginalValue => GetOriginalValue<System.Guid>(nameof(ProjectId));
    public bool ProjectIdIsChanged => GetIsChanged(nameof(ProjectId));


    public System.Double Cost
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    public System.DateTime RequiredDeliveryDate
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime RequiredDeliveryDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(RequiredDeliveryDate));
    public bool RequiredDeliveryDateIsChanged => GetIsChanged(nameof(RequiredDeliveryDate));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private FacilityWrapper _fieldFacility;
	public FacilityWrapper Facility 
    {
        get { return _fieldFacility ; }
        set
        {
            SetComplexValue<Facility, FacilityWrapper>(_fieldFacility, value);
            _fieldFacility  = value;
        }
    }

	private ProductWrapper _fieldProduct;
	public ProductWrapper Product 
    {
        get { return _fieldProduct ; }
        set
        {
            SetComplexValue<Product, ProductWrapper>(_fieldProduct, value);
            _fieldProduct  = value;
        }
    }

    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.Facility != null)
        {
            _fieldFacility = new FacilityWrapper(Model.Facility);
            RegisterComplex(Facility);
        }

		if (Model.Product != null)
        {
            _fieldProduct = new ProductWrapper(Model.Product);
            RegisterComplex(Product);
        }

    }

	}
}
	