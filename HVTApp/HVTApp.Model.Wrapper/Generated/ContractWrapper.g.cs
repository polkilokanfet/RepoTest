using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ContractWrapper : WrapperBase<Contract>
  {
    public ContractWrapper(Contract model) : base(model) { }
    public ContractWrapper(Contract model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.String Number
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
    public bool NumberIsChanged => GetIsChanged(nameof(Number));

    public System.DateTime Date
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
    public bool DateIsChanged => GetIsChanged(nameof(Date));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
    public CompanyWrapper Contragent { get; private set; }

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<SpecificationWrapper> Specifications { get; private set; }

    #endregion

    #region GetProperties
    public System.Double Sum => GetValue<System.Double>(); 

    public System.Double SumWithVat => GetValue<System.Double>(); 

    #endregion
    
    protected override void InitializeComplexProperties(Contract model)
    {
      if (model.Contragent == null) throw new ArgumentException("Contragent cannot be null");
      if (ExistsWrappers.ContainsKey(model.Contragent))
      {
          Contragent = (CompanyWrapper)ExistsWrappers[model.Contragent];
      }
      else
      {
          Contragent = new CompanyWrapper(model.Contragent, ExistsWrappers);
          RegisterComplexProperty(Contragent);
      }

    }
  
    protected override void InitializeCollectionComplexProperties(Contract model)
    {
      if (model.Specifications == null) throw new ArgumentException("Specifications cannot be null");
      Specifications = new ValidatableChangeTrackingCollection<SpecificationWrapper>(model.Specifications.Select(e => new SpecificationWrapper(e, ExistsWrappers)));
      RegisterCollection(Specifications, model.Specifications);

    }
  }
}
