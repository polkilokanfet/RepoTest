using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ContractWrapper : WrapperBase<Contract>
  {
    public ContractWrapper(Contract model) : base(model) { }



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

	private CompanyWrapper _fieldContragent;
	public CompanyWrapper Contragent 
    {
        get { return _fieldContragent ; }
        set
        {
            SetComplexValue<Company, CompanyWrapper>(_fieldContragent, value);
            _fieldContragent  = value;
        }
    }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<SpecificationWrapper> Specifications { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.Contragent != null)
        {
            _fieldContragent = new CompanyWrapper(Model.Contragent);
            RegisterComplex(Contragent);
        }

    }

  
    protected override void InitializeCollectionProperties()
    {

      if (Model.Specifications == null) throw new ArgumentException("Specifications cannot be null");
      Specifications = new ValidatableChangeTrackingCollection<SpecificationWrapper>(Model.Specifications.Select(e => new SpecificationWrapper(e)));
      RegisterCollection(Specifications, Model.Specifications);


    }

  }
}
