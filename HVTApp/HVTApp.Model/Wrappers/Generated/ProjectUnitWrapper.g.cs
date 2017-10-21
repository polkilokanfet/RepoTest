using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProjectUnitWrapper : WrapperBase<ProjectUnit>
  {
    public ProjectUnitWrapper(ProjectUnit model) : base(model) { }



    #region SimpleProperties

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

	public ProjectWrapper Project { get; set; }

	public FacilityWrapper Facility { get; set; }

	public ProductWrapper Product { get; set; }

    #endregion

    public override void InitializeComplexProperties()
    {

        Project = new ProjectWrapper(Model.Project);
		RegisterComplex(Project);

        Facility = new FacilityWrapper(Model.Facility);
		RegisterComplex(Facility);

        Product = new ProductWrapper(Model.Product);
		RegisterComplex(Product);

    }

  }
}
