namespace HVTApp.Model.Wrappers
{
    public partial class UnitWrapper
    {
        private ProductWrapper _product;

        public ProductWrapper Product
        {
            get
            {
                if (ProductionsUnit != null) return ProductionsUnit.Product;
                return _product;
            }
            set
            {
                if (ProductionsUnit != null) ProductionsUnit.Product = value;
                _product = value;
            }
        }

        public SumAndVatWrapper Cost => SalesUnit?.Cost;

        public bool IsTheSame(UnitWrapper unit)
        {
            if (Equals(this.Facility, unit.Facility) &&
                Equals(this.ProductionsUnit.Product, unit.ProductionsUnit.Product) &&
                Equals(this.SalesUnit.Cost.Sum, unit.SalesUnit.Cost.Sum))
                return true;
            return false;
        }
    }
}