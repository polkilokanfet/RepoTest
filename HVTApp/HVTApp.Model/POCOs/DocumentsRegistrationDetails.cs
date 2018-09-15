using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class DocumentsRegistrationDetails : BaseEntity
    {
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Today;

        public override string ToString()
        {
            return $"№{RegistrationNumber} от {RegistrationDate.ToShortDateString()}";
        }
    }
}