using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Корректировочные данные для отчетной коррекции
    /// (нужно для подгонки отчетов под нужные показатели)
    /// </summary>
    [Designation("Корректировочные данные")]
    public class FakeData : BaseEntity
    {
        public double? Cost { get; set; }
        public DateTime? RealizationDate { get; set; }
        public DateTime? OrderInTakeDate { get; set; }
        public PaymentConditionSet PaymentConditionSet { get; set; }
    }
}