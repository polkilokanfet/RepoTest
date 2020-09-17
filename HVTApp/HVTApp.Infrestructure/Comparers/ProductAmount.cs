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
            return other != null && Equals(this.ProductId, other.ProductId) && this.Amount == other.Amount && Equals(this.Price, other.Price);
        }
    }
}