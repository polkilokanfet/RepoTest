using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("��������")]
    public partial class Document : BaseEntity
    {
        [Designation("��"), Required, OrderStatus(50)]
        public virtual DocumentNumber Number { get; set; }

        [Designation("���"), OrderStatus(1), MaxLength(15)]
        public string Code { get; set; }

        [Designation("�����"), NotMapped, OrderStatus(45)]
        public string RegNumber => Number == null ? $"{Code}-{DateTime.Today.Year}-" : $"{Code}-{DateTime.Today.Year}-{Number}";

        [Designation("����"), Required, OrderStatus(40)]
        public DateTime Date { get; set; } = DateTime.Today;

        /// <summary>
        /// ������, ����� �� ������� ��� ��������� ����������.
        /// </summary>
        [Designation("������")]
        public virtual Document RequestDocument { get; set; }

        [Designation("�����"), Required]
        public virtual Employee Author { get; set; }

        [Required]
        public Guid SenderId { get; set; }
        [Designation("�����������"), Required]
        public virtual Employee SenderEmployee { get; set; }

        [Required]
        public Guid RecipientId { get; set; }
        [Designation("����������"), Required]
        public virtual Employee RecipientEmployee { get; set; }

        [Designation("�����")]
        public virtual List<Employee> CopyToRecipients { get; set; } = new List<Employee>();

        [Designation("���.������ ����������")]
        public virtual DocumentsRegistrationDetails RegistrationDetailsOfRecipient { get; set; }

        [Designation("�����������"), MaxLength(100)]
        public string Comment { get; set; }
    }
}