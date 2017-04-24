using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ContractWrapper : WrapperBase<Contract>
  {
    public ContractWrapper() : base(new Contract()) { }
    public ContractWrapper(Contract model) : base(model) { }
    public ContractWrapper(Contract model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }



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

	public CompanyWrapper Contragent 
    {
        get { return GetComplexProperty<CompanyWrapper, Company>(Model.Contragent); }
        set { SetComplexProperty<CompanyWrapper, Company>(Contragent, value); }
    }

    public CompanyWrapper ContragentOriginalValue { get; private set; }
    public bool ContragentIsChanged => GetIsChanged(nameof(Contragent));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<SpecificationWrapper> Specifications { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Contract model)
    {

        Contragent = GetWrapper<CompanyWrapper, Company>(model.Contragent);

    }

  
    protected override void InitializeCollectionComplexProperties(Contract model)
    {

      if (model.Specifications == null) throw new ArgumentException("Specifications cannot be null");
      Specifications = new ValidatableChangeTrackingCollection<SpecificationWrapper>(model.Specifications.Select(e => GetWrapper<SpecificationWrapper, Specification>(e)));
      RegisterCollection(Specifications, model.Specifications);


    }

  }
}
