using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Входящий запрос")]
    public class IncomingRequest : BaseEntity
    {
        [Designation("Запрос"), Required, OrderStatus(50)]
        public virtual Document Document { get; set; }

        [Designation("Исполнители"), OrderStatus(40)]
        public virtual List<Employee> Performers { get; set; } = new List<Employee>();

        [Designation("Исполнен"), OrderStatus(30), NotMapped]
        public bool IsDone => DoneDate.HasValue && DoneDate.Value <= DateTime.Now;

        [Designation("Актуален"), OrderStatus(20)]
        public bool IsActual { get; set; } = true;

        [Designation("Дата поручения"), OrderStatus(35)]
        public DateTime? InstructionDate { get; set; }

        [Designation("Дата исполнения поручения"), OrderStatus(25)]
        public DateTime? DoneDate { get; set; }
    }
}