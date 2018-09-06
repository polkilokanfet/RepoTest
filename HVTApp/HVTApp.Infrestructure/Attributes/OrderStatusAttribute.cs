using System;

namespace HVTApp.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OrderStatusAttribute : Attribute
    {
        public OrderStatus OrderStatus { get; }

        public OrderStatusAttribute(OrderStatus orderStatus)
        {
            OrderStatus = orderStatus;
        }
    }

    public enum OrderStatus
    {
        Highest,
        High,
        Normal,
        Low,
        Lowest
    }
}