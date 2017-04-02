using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProjectWrapper : WrapperBase<Project>
  {
    protected ProjectWrapper(Project model) : base(model) { }
    //public ProjectWrapper(Project model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static ProjectWrapper GetWrapper(Project model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProjectWrapper)Repository.ModelWrapperDictionary[model];

		return new ProjectWrapper(model);
	}



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public System.DateTime EstimatedDate
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime EstimatedDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(EstimatedDate));
    public bool EstimatedDateIsChanged => GetIsChanged(nameof(EstimatedDate));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private UserWrapper _fieldManager;
	public UserWrapper Manager 
    {
        get { return _fieldManager; }
        set
        {
            if (Equals(_fieldManager, value))
                return;

            UnRegisterComplexProperty(_fieldManager);

            _fieldManager = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion


    #region CollectionProperties

    public ValidatableChangeTrackingCollection<ProductsMainGroupWrapper> ProductsMainGroups { get; private set; }


    public ValidatableChangeTrackingCollection<TenderWrapper> Tenders { get; private set; }


    public ValidatableChangeTrackingCollection<OfferWrapper> Offers { get; private set; }


    #endregion


    #region GetProperties

    public System.Double Sum => GetValue<System.Double>(); 


    public HVTApp.Model.Company ProjectMaker => GetValue<HVTApp.Model.Company>(); 


    public HVTApp.Model.Company Worker => GetValue<HVTApp.Model.Company>(); 


    public HVTApp.Model.Company Supplier => GetValue<HVTApp.Model.Company>(); 


    #endregion

    protected override void InitializeComplexProperties(Project model)
    {

        Manager = UserWrapper.GetWrapper(model.Manager);

    }

  
    protected override void InitializeCollectionComplexProperties(Project model)
    {

      if (model.ProductsMainGroups == null) throw new ArgumentException("ProductsMainGroups cannot be null");
      ProductsMainGroups = new ValidatableChangeTrackingCollection<ProductsMainGroupWrapper>(model.ProductsMainGroups.Select(e => ProductsMainGroupWrapper.GetWrapper(e)));
      RegisterCollection(ProductsMainGroups, model.ProductsMainGroups);


      if (model.Tenders == null) throw new ArgumentException("Tenders cannot be null");
      Tenders = new ValidatableChangeTrackingCollection<TenderWrapper>(model.Tenders.Select(e => TenderWrapper.GetWrapper(e)));
      RegisterCollection(Tenders, model.Tenders);


      if (model.Offers == null) throw new ArgumentException("Offers cannot be null");
      Offers = new ValidatableChangeTrackingCollection<OfferWrapper>(model.Offers.Select(e => OfferWrapper.GetWrapper(e)));
      RegisterCollection(Offers, model.Offers);


    }

  }
}
