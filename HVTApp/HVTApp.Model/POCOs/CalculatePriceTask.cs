using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Задание на расчет себестоимости")]
    public partial class CalculatePriceTask : BaseEntity
    {
        [Designation("Статус")]
        public CalculatePriceTaskStatus Status { get; set; }

        [Designation("Сумма")]
        public double Sum { get; set; }

        [Designation("Дата")]
        public DateTime Date { get; set; }

        [Designation("Блок")]
        public virtual ProductBlock ProductBlock { get; set; }

        [Designation("Проекты")]
        public List<Project> Projects { get; set; } = new List<Project>();

        [Designation("ТКП")]
        public List<Offer> Offers { get; set; } = new List<Offer>();

        [Designation("Спецификации")]
        public List<Specification> Specifications { get; set; } = new List<Specification>();
    }

    public enum CalculatePriceTaskStatus
    {
        IsEmpty,
        NotActual
    }
}