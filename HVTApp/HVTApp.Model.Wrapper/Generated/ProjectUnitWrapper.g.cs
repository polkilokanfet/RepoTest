using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProjectUnitWrapper : WrapperBase<ProjectUnit>
  {
    protected ProjectUnitWrapper(ProjectUnit model) : base(model) { }

	public static ProjectUnitWrapper GetWrapper()
	{
		return GetWrapper(new ProjectUnit());
	}

	public static ProjectUnitWrapper GetWrapper(ProjectUnit model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProjectUnitWrapper)Repository.ModelWrapperDictionary[model];

		return new ProjectUnitWrapper(model);
	}



    #region SimpleProperties

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ProjectWrapper Project 
    {
        get { return ProjectWrapper.GetWrapper(Model.Project); }
        set
        {
			var oldPropVal = Project;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProjectWrapper ProjectOriginalValue => ProjectWrapper.GetWrapper(GetOriginalValue<Project>(nameof(Project)));
    public bool ProjectIsChanged => GetIsChanged(nameof(Project));


	public FacilityWrapper Facility 
    {
        get { return FacilityWrapper.GetWrapper(Model.Facility); }
        set
        {
			var oldPropVal = Facility;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public FacilityWrapper FacilityOriginalValue => FacilityWrapper.GetWrapper(GetOriginalValue<Facility>(nameof(Facility)));
    public bool FacilityIsChanged => GetIsChanged(nameof(Facility));


	public SalesUnitWrapper SalesUnit 
    {
        get { return SalesUnitWrapper.GetWrapper(Model.SalesUnit); }
        set
        {
			var oldPropVal = SalesUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SalesUnitWrapper SalesUnitOriginalValue => SalesUnitWrapper.GetWrapper(GetOriginalValue<SalesUnit>(nameof(SalesUnit)));
    public bool SalesUnitIsChanged => GetIsChanged(nameof(SalesUnit));


    #endregion

    protected override void InitializeComplexProperties(ProjectUnit model)
    {

        Project = ProjectWrapper.GetWrapper(model.Project);

        Facility = FacilityWrapper.GetWrapper(model.Facility);

        SalesUnit = SalesUnitWrapper.GetWrapper(model.SalesUnit);

    }

  }
}
