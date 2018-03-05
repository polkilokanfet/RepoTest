using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class ProductDependent : BaseEntity, IProductCost
    {
        public virtual Product Product { get; set; }
        public double Cost { get; set; }
        public int Amount { get; set; }
        public bool IsIndependent { get; set; } = false;

        public override int GetHashCode()
        {
            var result = Product.Id.GetHashCode() + Cost.GetHashCode() + Amount.GetHashCode() + IsIndependent.GetHashCode();
            return Product.Id.GetHashCode() + Cost.GetHashCode() + Amount.GetHashCode() + IsIndependent.GetHashCode();
        }
    }
}