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

        [Designation("Номер"), NotMapped, OrderStatus(150)]
        public string RegNumber
        {
            get
            {
                if (Direction == DocumentDirection.Outgoing &&
                    !string.IsNullOrEmpty(Author?.PersonalNumber))
                {
                    return Number == null
                        ? $"{Author.PersonalNumber}-{Date.Year.ToString().Remove(0, 2)}-"
                        : $"{Author.PersonalNumber}-{Date.Year.ToString().Remove(0, 2)}-{Number.Number:D4}";
                }

                return Number != null ? $"{Number.Number:D5}" : "n/n";
            }
        }

        [Designation("Дата"), Required, OrderStatus(140)]
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

        [Designation("Комментарий"), MaxLength(150), OrderStatus(130)]
        public string Comment { get; set; }

        [Designation("Номер в ТСЕ"), MaxLength(20), OrderStatus(-10)]
        public string TceNumber { get; set; }


        [Designation("Направление"), NotMapped]
        public DocumentDirection Direction => GlobalAppProperties.Actual.OurCompany.Id == SenderEmployee?.Company.Id 
            ? DocumentDirection.Outgoing 
            : DocumentDirection.Incoming;
    }

    public enum DocumentDirection
    {
        Incoming,
        Outgoing
    }
}