using System;

namespace HVTApp.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OrderStatusAttribute : Attribute
    {
        public int OrderStatus { get; }

        public OrderStatusAttribute(int orderStatus)
        {
            OrderStatus = orderStatus;
        }
    }

    //public enum OrderStatus
    //{
    //    Highest,
    //    High,
    //    Normal,
    //    Low,
    //    Lowest
    //}
}