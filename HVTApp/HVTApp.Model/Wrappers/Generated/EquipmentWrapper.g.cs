using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class EquipmentWrapper : WrapperBase<Equipment>
  {
    private EquipmentWrapper(IGetWrapper getWrapper) : base(new Equipment(), getWrapper) { }
    private EquipmentWrapper(Equipment model, IGetWrapper getWrapper) : base(model, getWrapper) { }



    #region SimpleProperties

    public System.String Designation
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String DesignationOriginalValue => GetOriginalValue<System.String>(nameof(Designation));
    public bool DesignationIsChanged => GetIsChanged(nameof(Designation));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ProductWrapper Product 
    {
        get { return GetComplexProperty<ProductWrapper, Product>(Model.Product); }
        set { SetComplexProperty<ProductWrapper, Product>(Product, value); }
    }

    public ProductWrapper ProductOriginalValue { get; private set; }
    public bool ProductIsChanged => GetIsChanged(nameof(Product));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<EquipmentWrapper> DependentEquipments { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Product = GetWrapper<ProductWrapper, Product>(Model.Product);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.DependentEquipments == null) throw new ArgumentException("DependentEquipments cannot be null");
      DependentEquipments = new ValidatableChangeTrackingCollection<EquipmentWrapper>(Model.DependentEquipments.Select(e => GetWrapper<EquipmentWrapper, Equipment>(e)));
      RegisterCollection(DependentEquipments, Model.DependentEquipments);


    }

  }
}
