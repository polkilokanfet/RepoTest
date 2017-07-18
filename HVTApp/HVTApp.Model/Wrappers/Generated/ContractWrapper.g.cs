using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ContractWrapper : WrapperBase<Contract>
  {
    private ContractWrapper() : base(new Contract()) { }
    private ContractWrapper(Contract model) : base(model) { }



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


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
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

    public override void InitializeComplexProperties()
    {

        Contragent = GetWrapper<CompanyWrapper, Company>(Model.Contragent);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.Specifications == null) throw new ArgumentException("Specifications cannot be null");
      Specifications = new ValidatableChangeTrackingCollection<SpecificationWrapper>(Model.Specifications.Select(e => GetWrapper<SpecificationWrapper, Specification>(e)));
      RegisterCollection(Specifications, Model.Specifications);


    }

  }
}
