using System;

namespace HVTApp.Model
{
    public class RegistrationDetails : BaseEntity
    {
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}