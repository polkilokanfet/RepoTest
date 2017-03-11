using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProjectWrapper : WrapperBase<Project>
  {
    public ProjectWrapper(Project model) : base(model) { }
    public ProjectWrapper(Project model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
    public UserWrapper Manager { get; private set; }

    #endregion

    #region CollectionComplexProperties
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
      if (model.Manager == null) throw new ArgumentException("Manager cannot be null");
      if (ExistsWrappers.ContainsKey(model.Manager))
      {
          Manager = (UserWrapper)ExistsWrappers[model.Manager];
      }
      else
      {
          Manager = new UserWrapper(model.Manager, ExistsWrappers);
          RegisterComplexProperty(Manager);
      }

    }
  
    protected override void InitializeCollectionComplexProperties(Project model)
    {
      if (model.ProductsMainGroups == null) throw new ArgumentException("ProductsMainGroups cannot be null");
      ProductsMainGroups = new ValidatableChangeTrackingCollection<ProductsMainGroupWrapper>(model.ProductsMainGroups.Select(e => new ProductsMainGroupWrapper(e, ExistsWrappers)));
      RegisterCollection(ProductsMainGroups, model.ProductsMainGroups);

      if (model.Tenders == null) throw new ArgumentException("Tenders cannot be null");
      Tenders = new ValidatableChangeTrackingCollection<TenderWrapper>(model.Tenders.Select(e => new TenderWrapper(e, ExistsWrappers)));
      RegisterCollection(Tenders, model.Tenders);

      if (model.Offers == null) throw new ArgumentException("Offers cannot be null");
      Offers = new ValidatableChangeTrackingCollection<OfferWrapper>(model.Offers.Select(e => new OfferWrapper(e, ExistsWrappers)));
      RegisterCollection(Offers, model.Offers);

    }
  }
}
