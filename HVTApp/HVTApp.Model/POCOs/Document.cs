using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("��������")]
    public partial class Document : BaseEntity
    {
        /// <summary>
        /// ������, ����� �� ������� ��� ��������� ����������.
        /// </summary>
        [Designation("������")]
        public virtual Document RequestDocument { get; set; }

        [Designation("�����")]
        public virtual Employee Author { get; set; }

        public Guid SenderId { get; set; }
        [Designation("�����������")]
        public virtual Employee SenderEmployee { get; set; }

        public Guid RecipientId { get; set; }
        [Designation("����������")]
        public virtual Employee RecipientEmployee { get; set; }

        [Designation("�����")]
        public virtual List<Employee> CopyToRecipients { get; set; } = new List<Employee>();

        [Designation("���������"), Required]
        public virtual DocumentsRegistrationDetails RegistrationDetailsOfSender { get; set; }

        [Designation("��������")]
        public virtual DocumentsRegistrationDetails RegistrationDetailsOfRecipient { get; set; }

        [Designation("�����������")]
        public string Comment { get; set; }
    }
}