using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("��������������� ������")]
    public partial class DocumentsRegistrationDetails : BaseEntity
    {
        [Designation("����"), Required]
        public DateTime Date { get; set; } = DateTime.Today;

        [Designation("�����"), Required, MaxLength(15)]
        public string Number { get; set; }

        public override string ToString()
        {
            return $"�{Number} �� {Date.ToShortDateString()}";
        }
    }
}