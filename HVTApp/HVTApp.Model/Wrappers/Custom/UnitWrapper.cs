namespace HVTApp.Model.Wrappers
{
    public partial class UnitWrapper
    {
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