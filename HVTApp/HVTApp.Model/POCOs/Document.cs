using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Документ")]
    public partial class Document : BaseEntity
    {
        /// <summary>
        /// Запрос, ответ на который дан настоящим документом.
        /// </summary>
        [Designation("Запрос")]
        public virtual Document RequestDocument { get; set; }

        [Designation("Автор")]
        public virtual Employee Author { get; set; }

        public Guid SenderId { get; set; }
        [Designation("Отправитель")]
        public virtual Employee SenderEmployee { get; set; }

        public Guid RecipientId { get; set; }
        [Designation("Получатель")]
        public virtual Employee RecipientEmployee { get; set; }

        [Designation("Копия")]
        public virtual List<Employee> CopyToRecipients { get; set; } = new List<Employee>();

        [Designation("Исходящий"), Required]
        public virtual DocumentsRegistrationDetails RegistrationDetailsOfSender { get; set; }

        [Designation("Входящий")]
        public virtual DocumentsRegistrationDetails RegistrationDetailsOfRecipient { get; set; }

        [Designation("Комментарий")]
        public string Comment { get; set; }
    }
}