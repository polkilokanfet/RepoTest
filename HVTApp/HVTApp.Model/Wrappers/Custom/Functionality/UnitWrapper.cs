namespace HVTApp.Model.Wrappers
{
    public partial class UnitWrapper
    {
        public ProductWrapper Product => ProductionsUnit != null ? ProductionsUnit.Product : ProjectsUnit.Product;
        public SumAndVatWrapper Cost => SalesUnit != null ? SalesUnit.Cost : ProjectsUnit.Cost;

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