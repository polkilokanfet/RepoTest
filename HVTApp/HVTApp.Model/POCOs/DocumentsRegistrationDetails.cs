using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Регистрационные данные")]
    [AllowEdit(Role.SalesManager, Role.DataBaseFiller, Role.Economist)]
    public partial class DocumentsRegistrationDetails : BaseEntity
    {
        [Designation("Дата"), Required]
        public DateTime Date { get; set; } = DateTime.Today;

        [Designation("Номер"), Required, MaxLength(15)]
        public string Number { get; set; }

        public override string ToString()
        {
            return $"№{Number} от {Date.ToShortDateString()}";
        }
    }
}