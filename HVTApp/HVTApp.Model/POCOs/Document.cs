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

        [Designation("�����"), NotMapped, OrderStatus(150)]
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

        [Designation("����"), Required, OrderStatus(140)]
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

        [Designation("�����������"), MaxLength(150), OrderStatus(130)]
        public string Comment { get; set; }

        [Designation("����� � ���"), MaxLength(20), OrderStatus(-10)]
        public string TceNumber { get; set; }


        [Designation("�����������"), NotMapped]
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