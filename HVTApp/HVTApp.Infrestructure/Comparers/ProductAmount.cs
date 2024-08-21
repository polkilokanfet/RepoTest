using System;

namespace HVTApp.Infrastructure.Comparers
{
    public class ProductAmount
    {
        public Guid ProductId { get; }
        public int Amount { get; }
        public double? Price { get; }

        public ProductAmount(Guid productId, int amount, double? price)
        {
            ProductId = productId;
            Amount = amount;
            Price = price;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ProductAmount;
            return other != null && 
                   Equals(this.ProductId, other.ProductId) && this.Amount == other.Amount && 
                   Equals(this.Price, other.Price);
        }

        protected bool Equals(ProductAmount other)
        {
            return ProductId.Equals(other.ProductId) && Amount == other.Amount && Nullable.Equals(Price, other.Price);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ProductId.GetHashCode();
                hashCode = (hashCode * 397) ^ Amount;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }
    }
}