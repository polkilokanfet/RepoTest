using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Заметка")]
    public partial class Note : BaseEntity
    {
        [Designation("Дата"), Required, OrderStatus(4)]
        public DateTime Date { get; set; }

        [Designation("Текст"), Required, OrderStatus(3), MaxLength(150)]
        public string Text { get; set; }

        [Designation("Важно?"), OrderStatus(2)]
        public bool IsImportant { get; set; } = false;
    }
}
