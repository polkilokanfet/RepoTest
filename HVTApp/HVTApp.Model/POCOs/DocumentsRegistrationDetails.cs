using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class DocumentsRegistrationDetails : BaseEntity
    {
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}