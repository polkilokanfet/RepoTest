using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class PartWrapper : WrapperBase<Part>
  {
    private PartWrapper(IGetWrapper getWrapper) : base(new Part(), getWrapper) { }
    private PartWrapper(Part model, IGetWrapper getWrapper) : base(model, getWrapper) { }



    #region SimpleProperties

    public System.String Designation
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String DesignationOriginalValue => GetOriginalValue<System.String>(nameof(Designation));
    public bool DesignationIsChanged => GetIsChanged(nameof(Designation));


    public System.String StructureCostNumber
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String StructureCostNumberOriginalValue => GetOriginalValue<System.String>(nameof(StructureCostNumber));
    public bool StructureCostNumberIsChanged => GetIsChanged(nameof(StructureCostNumber));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }


    public IValidatableChangeTrackingCollection<CostOnDateWrapper> Prices { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
      Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.Parameters.Select(e => GetWrapper<ParameterWrapper, Parameter>(e)));
      RegisterCollection(Parameters, Model.Parameters);


      if (Model.Prices == null) throw new ArgumentException("Prices cannot be null");
      Prices = new ValidatableChangeTrackingCollection<CostOnDateWrapper>(Model.Prices.Select(e => GetWrapper<CostOnDateWrapper, CostOnDate>(e)));
      RegisterCollection(Prices, Model.Prices);


    }

  }
}