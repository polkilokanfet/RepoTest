












using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
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

	public SalesUnitWrapper AdditionalSalesUnit { get; set; }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<SalesUnitWrapper> ParentSalesUnits { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        AdditionalSalesUnit = new SalesUnitWrapper(Model.AdditionalSalesUnit);
		RegisterComplex(AdditionalSalesUnit);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.ParentSalesUnits == null) throw new ArgumentException("ParentSalesUnits cannot be null");
      ParentSalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(Model.ParentSalesUnits.Select(e => new SalesUnitWrapper(e)));
      RegisterCollection(ParentSalesUnits, Model.ParentSalesUnits);


    }

  }
}
