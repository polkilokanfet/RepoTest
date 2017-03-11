using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class SpecificationWrapper : WrapperBase<Specification>
  {
    public SpecificationWrapper(Specification model) : base(model) { }
    public SpecificationWrapper(Specification model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
    public ContractWrapper Contract { get; private set; }

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<ProductsMainGroupWrapper> SalesGroups { get; private set; }

    public ValidatableChangeTrackingCollection<PaymentsConditionWrapper> PaymentsConditions { get; private set; }

    #endregion

    #region GetProperties
    public System.Double Sum => GetValue<System.Double>(); 

    public System.Double SumWithVat => GetValue<System.Double>(); 

    #endregion
    
    protected override void InitializeComplexProperties(Specification model)
    {
      if (model.Contract == null) throw new ArgumentException("Contract cannot be null");
      if (ExistsWrappers.ContainsKey(model.Contract))
      {
          Contract = (ContractWrapper)ExistsWrappers[model.Contract];
      }
      else
      {
          Contract = new ContractWrapper(model.Contract, ExistsWrappers);
          RegisterComplexProperty(Contract);
      }

    }
  
    protected override void InitializeCollectionComplexProperties(Specification model)
    {
      if (model.SalesGroups == null) throw new ArgumentException("SalesGroups cannot be null");
      SalesGroups = new ValidatableChangeTrackingCollection<ProductsMainGroupWrapper>(model.SalesGroups.Select(e => new ProductsMainGroupWrapper(e, ExistsWrappers)));
      RegisterCollection(SalesGroups, model.SalesGroups);

      if (model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentsConditionWrapper>(model.PaymentsConditions.Select(e => new PaymentsConditionWrapper(e, ExistsWrappers)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);

    }
  }
}
