using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Стракчакост")]
    public class StructureCost : BaseEntity
    {
        public Guid PriceCalculationItemId { get; set; }

        [Designation("Номер"), Required, MaxLength(50)]
        public string Number { get; set; }

        [Designation("Количество"), Required]
        public double Amount { get; set; } = 1;

        [Designation("Себестоимость единицы")]
        public double? UnitPrice { get; set; }

        public double? Total => UnitPrice * Amount;

        [Designation("Комментарий"), MaxLength(200)]
        public string Comment { get; set; }

        public override string ToString()
        {
            return $"{Comment} = {Amount:N2} шт. = {Number}";
        }
    }
}