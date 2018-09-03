using System;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferUnitWrapper : IProductUnit //: ProductCostDependentProductsWrapper<OfferUnit>, IProductUnit
    {
        private double _price;

        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                PriceChanged?.Invoke();
            }
        }

        public event Action PriceChanged;
    }
}