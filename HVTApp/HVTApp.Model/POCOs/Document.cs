using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Документ")]
    public partial class Document : BaseEntity
    {
        [Designation("ИД"), Required, OrderStatus(50)]
        public virtual DocumentNumber Number { get; set; }

        [Designation("Код"), OrderStatus(1), MaxLength(15)]
        public string Code { get; set; }

        [Designation("Номер"), NotMapped, OrderStatus(45)]
        public string RegNumber => Number == null ? $"{Code}-{DateTime.Today.Year}-" : $"{Code}-{DateTime.Today.Year}-{Number}";

        [Designation("Дата"), Required, OrderStatus(40)]
        public DateTime Date { get; set; } = DateTime.Today;

        /// <summary>
        /// Запрос, ответ на который дан настоящим документом.
        /// </summary>
        [Designation("Запрос")]
        public virtual Document RequestDocument { get; set; }

        [Designation("Автор"), Required]
        public virtual Employee Author { get; set; }

        [Required]
        public Guid SenderId { get; set; }
        [Designation("Отправитель"), Required]
        public virtual Employee SenderEmployee { get; set; }

        [Required]
        public Guid RecipientId { get; set; }
        [Designation("Получатель"), Required]
        public virtual Employee RecipientEmployee { get; set; }

        [Designation("Копия")]
        public virtual List<Employee> CopyToRecipients { get; set; } = new List<Employee>();

        [Designation("Рег.данные получателя")]
        public virtual DocumentsRegistrationDetails RegistrationDetailsOfRecipient { get; set; }

        [Designation("Комментарий"), MaxLength(100)]
        public string Comment { get; set; }
    }
}