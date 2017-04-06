using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProjectProductUnitWrapper : WrapperBase<ProjectProductUnit>
  {
    protected ProjectProductUnitWrapper(ProjectProductUnit model) : base(model) { }

	public static ProjectProductUnitWrapper GetWrapper()
	{
		return GetWrapper(new ProjectProductUnit());
	}

	public static ProjectProductUnitWrapper GetWrapper(ProjectProductUnit model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProjectProductUnitWrapper)Repository.ModelWrapperDictionary[model];

		return new ProjectProductUnitWrapper(model);
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


	public ProductWrapper Product 
    {
        get { return ProductWrapper.GetWrapper(Model.Product); }
        set
        {
			var oldPropVal = Product;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProductWrapper ProductOriginalValue => ProductWrapper.GetWrapper(GetOriginalValue<Product>(nameof(Product)));
    public bool ProductIsChanged => GetIsChanged(nameof(Product));


	public CostInfoWrapper CostInfo 
    {
        get { return CostInfoWrapper.GetWrapper(Model.CostInfo); }
        set
        {
			var oldPropVal = CostInfo;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public CostInfoWrapper CostInfoOriginalValue => CostInfoWrapper.GetWrapper(GetOriginalValue<CostInfo>(nameof(CostInfo)));
    public bool CostInfoIsChanged => GetIsChanged(nameof(CostInfo));


    #endregion

    protected override void InitializeComplexProperties(ProjectProductUnit model)
    {

        Facility = FacilityWrapper.GetWrapper(model.Facility);

        Product = ProductWrapper.GetWrapper(model.Product);

        CostInfo = CostInfoWrapper.GetWrapper(model.CostInfo);

    }

  }
}
