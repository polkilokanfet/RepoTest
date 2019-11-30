using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Стракчакост")]
    public class StructureCost : BaseEntity
    {
        [Designation("Номер"), Required, MaxLength(50)]
        public string Number { get; set; }

        [Designation("Количество"), Required]
        public double Amount { get; set; } = 1;

        [Designation("Себестоимость единицы")]
        public double? UnitPrice { get; set; }

        [Designation("Дата себестоимости")]
        public DateTime? UnitPriceDate { get; set; }

        [Designation("Комментарий"), MaxLength(200)]
        public string Comment { get; set; }

        public override string ToString()
        {
            return Number;
        }
    }
}