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
	public FacilityWrapper Facility 
    {
        get { return GetWrapper<FacilityWrapper>(); }
        set { SetComplexValue<Facility, FacilityWrapper>(Facility, value); }
    }

	public ProductWrapper Product 
    {
        get { return GetWrapper<ProductWrapper>(); }
        set { SetComplexValue<Product, ProductWrapper>(Product, value); }
    }

    #endregion
    public override void InitializeComplexProperties()
    {
        InitializeComplexProperty<FacilityWrapper>(nameof(Facility), Model.Facility == null ? null : new FacilityWrapper(Model.Facility));

        InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));

    }
	}
}
	