using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class OfferUnitWrapper : WrapperBase<OfferUnit>
  {
    public OfferUnitWrapper(OfferUnit model) : base(model) { }



    #region SimpleProperties

    public System.Double Cost
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    public System.Int32 ProductionTerm
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 ProductionTermOriginalValue => GetOriginalValue<System.Int32>(nameof(ProductionTerm));
    public bool ProductionTermIsChanged => GetIsChanged(nameof(ProductionTerm));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ProjectUnitWrapper ProjectUnit { get; set; }

	public ProductWrapper Product { get; set; }

	public OfferWrapper Offer { get; set; }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        ProjectUnit = new ProjectUnitWrapper(Model.ProjectUnit);
		RegisterComplex(ProjectUnit);

        Product = new ProductWrapper(Model.Product);
		RegisterComplex(Product);

        Offer = new OfferWrapper(Model.Offer);
		RegisterComplex(Offer);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => new PaymentConditionWrapper(e)));
      RegisterCollection(PaymentsConditions, Model.PaymentsConditions);


    }

  }
}
