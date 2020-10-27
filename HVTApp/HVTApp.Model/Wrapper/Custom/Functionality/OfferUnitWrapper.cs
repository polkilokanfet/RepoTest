using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper.Groups;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;

namespace HVTApp.Model.Wrapper
{
    public partial class OfferUnitWrapper : HVTApp.Model.Wrapper.Groups.IWrapperGroup<OfferUnit>
    {
        FacilitySimpleWrapper IWrapperGroup<OfferUnit>.Facility
        {
            get { return new FacilitySimpleWrapper(Model.Facility); }
            set
            {
                if (Equals(Model.Facility?.Id, value?.Model.Id)) return;
                this.Facility = new FacilityWrapper(value.Model);
            }
        }

        PaymentConditionSetSimpleWrapper IWrapperGroup<OfferUnit>.PaymentConditionSet
        {
            get { return new PaymentConditionSetSimpleWrapper(Model.PaymentConditionSet); }
            set
            {
                if (Equals(Model.PaymentConditionSet?.Id, value?.Model.Id)) return;
                this.PaymentConditionSet = new PaymentConditionSetWrapper(value.Model);
            }
        }

        ProductSimpleWrapper IWrapperGroup<OfferUnit>.Product
        {
            get { return new ProductSimpleWrapper(Model.Product); }
            set
            {
                if (Equals(Model.Product?.Id, value?.Model.Id)) return;
                this.Product = new ProductWrapper(value.Model);
            }
        }

        IValidatableChangeTrackingCollection<ProductIncludedSimpleWrapper> IWrapperGroup<OfferUnit>.ProductsIncluded
        {
            get { return new ValidatableChangeTrackingCollection<ProductIncludedSimpleWrapper>(this.ProductsIncluded.Select(x => new ProductIncludedSimpleWrapper(x.Model))); }
        }
    }
}