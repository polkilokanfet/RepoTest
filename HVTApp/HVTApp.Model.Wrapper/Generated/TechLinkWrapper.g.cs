using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TechLinkWrapper : WrapperBase<TechLink>
  {
    public TechLinkWrapper(TechLink model) : base(model) { }
    public TechLinkWrapper(TechLink model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static TechLinkWrapper GetWrapper(TechLink model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TechLinkWrapper)Repository.ModelWrapperDictionary[model];

		return new TechLinkWrapper(model);
	}



    #region SimpleProperties

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public TechParameterWrapper Parameter
	{
		get { return GetComplexProperty<TechParameter, TechParameterWrapper>(nameof(Parameter)); }
		set { SetComplexProperty<TechParameter, TechParameterWrapper>(value, nameof(Parameter)); }
	}


	public TechLinkWrapper ParentLink
	{
		get { return GetComplexProperty<TechLink, TechLinkWrapper>(nameof(ParentLink)); }
		set { SetComplexProperty<TechLink, TechLinkWrapper>(value, nameof(ParentLink)); }
	}


    #endregion


    #region CollectionComplexProperties

    public ValidatableChangeTrackingCollection<TechLinkWrapper> ChildLinks { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(TechLink model)
    {

        Parameter = TechParameterWrapper.GetWrapper(model.Parameter);

        ParentLink = TechLinkWrapper.GetWrapper(model.ParentLink);

    }

  
    protected override void InitializeCollectionComplexProperties(TechLink model)
    {

      if (model.ChildLinks == null) throw new ArgumentException("ChildLinks cannot be null");
      ChildLinks = new ValidatableChangeTrackingCollection<TechLinkWrapper>(model.ChildLinks.Select(e => new TechLinkWrapper(e, ExistsWrappers)));
      RegisterCollection(ChildLinks, model.ChildLinks);


    }

  }
}
