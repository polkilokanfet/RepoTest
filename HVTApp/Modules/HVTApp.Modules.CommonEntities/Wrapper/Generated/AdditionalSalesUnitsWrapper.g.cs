using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class AdditionalSalesUnitsWrapper : WrapperBase<AdditionalSalesUnits>
	{
	public AdditionalSalesUnitsWrapper(AdditionalSalesUnits model) : base(model) { }

	
    #region SimpleProperties
    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	public SalesUnitWrapper AdditionalSalesUnit 
    {
        get { return GetWrapper<SalesUnitWrapper>(); }
        set { SetComplexValue<SalesUnit, SalesUnitWrapper>(AdditionalSalesUnit, value); }
    }

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<SalesUnitWrapper> ParentSalesUnits { get; private set; }

    #endregion
    public override void InitializeComplexProperties()
    {
        InitializeComplexProperty<SalesUnitWrapper>(nameof(AdditionalSalesUnit), Model.AdditionalSalesUnit == null ? null : new SalesUnitWrapper(Model.AdditionalSalesUnit));

    }
  
    protected override void InitializeCollectionProperties()
    {
      if (Model.ParentSalesUnits == null) throw new ArgumentException("ParentSalesUnits cannot be null");
      ParentSalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(Model.ParentSalesUnits.Select(e => new SalesUnitWrapper(e)));
      RegisterCollection(ParentSalesUnits, Model.ParentSalesUnits);

    }
	}
}
	