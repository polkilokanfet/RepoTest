using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Заводской заказ")]
    public partial class Order : BaseEntity, IComparable
    {
        [Designation("Номер"), Required, MaxLength(10)]
        public string Number { get; set; }

        [Designation("Дата открытия"), Required]
        public DateTime DateOpen { get; set; } = DateTime.Today;

        public override string ToString()
        {
            return $"{Number}";
        }

        public int CompareTo(object obj)
        {
            var other = (Order) obj;
            return this.DateOpen.CompareTo(other.DateOpen);
        }
    }
}