using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Входящий запрос")]
    public class IncomingRequest : BaseEntity
    {
        [Designation("Запрос"), Required, OrderStatus(50)]
        public virtual Document Document { get; set; }

        [Designation("Исполнители"), Required, OrderStatus(40)]
        public virtual List<Employee> Performers { get; set; } = new List<Employee>();

        [Designation("Исполнен"), OrderStatus(30)]
        public bool IsDone { get; set; } = false;
    }
}