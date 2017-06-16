using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProjectsUnitWrapper : WrapperBase<ProjectsUnit>
  {
    public ProjectsUnitWrapper() : base(new ProjectsUnit()) { }
    public ProjectsUnitWrapper(ProjectsUnit model) : base(model) { }



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

	public UnitWrapper Unit 
    {
        get { return GetComplexProperty<UnitWrapper, Unit>(Model.Unit); }
        set { SetComplexProperty<UnitWrapper, Unit>(Unit, value); }
    }

    public UnitWrapper UnitOriginalValue { get; private set; }
    public bool UnitIsChanged => GetIsChanged(nameof(Unit));


	public ProductWrapper Product 
    {
        get { return GetComplexProperty<ProductWrapper, Product>(Model.Product); }
        set { SetComplexProperty<ProductWrapper, Product>(Product, value); }
    }

    public ProductWrapper ProductOriginalValue { get; private set; }
    public bool ProductIsChanged => GetIsChanged(nameof(Product));


	public SumAndVatWrapper Cost 
    {
        get { return GetComplexProperty<SumAndVatWrapper, SumAndVat>(Model.Cost); }
        set { SetComplexProperty<SumAndVatWrapper, SumAndVat>(Cost, value); }
    }

    public SumAndVatWrapper CostOriginalValue { get; private set; }
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    #endregion

    public override void InitializeComplexProperties()
    {

        Unit = GetWrapper<UnitWrapper, Unit>(Model.Unit);

        Product = GetWrapper<ProductWrapper, Product>(Model.Product);

        Cost = GetWrapper<SumAndVatWrapper, SumAndVat>(Model.Cost);

    }

  }
}
